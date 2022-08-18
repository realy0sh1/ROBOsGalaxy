using System.Collections;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveRadius = 0;
    private GameObject player;

    private Vector3 rootPosition;
    private Rigidbody rb;
    private float localYRotation;
    private bool playerDetected = false;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float lookUpDownAngle = 30f;
    [SerializeField] private float seeDistance = 8f;
    [SerializeField] private int area = 0;

    private Animator anim;
    private bool isWalking;

    private EnemyKillTracker killTracker;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rootPosition = transform.position;
        player = GameObject.Find("ROBOPrefab");
        anim = GetComponent<Animator>();
        killTracker = GameObject.Find("EnemyKillCounter").GetComponent<EnemyKillTracker>();

    }


    private void FixedUpdate()
    {
        // check if player near and rotate towards player
        Vector3 enemyToPlayer = player.transform.position - transform.position;
        float theoreticalLookAngle = Vector3.Angle(transform.up, enemyToPlayer);
        if (theoreticalLookAngle > (90 - lookUpDownAngle) && theoreticalLookAngle < (90 + lookUpDownAngle) &&
            Vector3.Magnitude(enemyToPlayer) <= seeDistance)
        {
            // enemy detects player
            playerDetected = true;
            // look at player (local y only, becasue of the gravty system)
            Vector3 axis = Vector3.Cross(transform.position, enemyToPlayer);
            Vector3 a = Vector3.Cross(axis, transform.forward);
            Vector3 b = Vector3.Cross(axis, enemyToPlayer);
            localYRotation = Vector3.SignedAngle(a, b, transform.up);
            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    Quaternion.AngleAxis(localYRotation, transform.up) * rb.rotation,
                    turnSpeed * Time.fixedDeltaTime));
        }
        else
        {
            playerDetected = false;
        }


        // move toward player
        if (playerDetected)
        {
            Vector3 localMoveDirection = new Vector3(0, 0, 1);
            // check if movement is possible
            Vector3 plannedMoveDirection = rb.transform.TransformDirection(localMoveDirection);
            Vector3 center = transform.position;
            float radius = 0.1f;
            float distance = Time.fixedDeltaTime * moveSpeed;
            RaycastHit hit;
            if (!Physics.SphereCast(center, radius, plannedMoveDirection, out hit, distance, 0,
                    QueryTriggerInteraction.Ignore))
            {
                // only move if possible
                // only move if in radius, or can move freely
                if ((moveRadius == 0) ||
                    (Vector3.Magnitude(((transform.position + plannedMoveDirection * distance) - rootPosition)) <
                     moveRadius) || Vector3.Magnitude(transform.position - rootPosition) > moveRadius)
                {
                    rb.MovePosition(rb.position + plannedMoveDirection * distance);
                    isWalking = true;
                }
                else
                {
                    // cannot follow player anymore
                    isWalking = false;
                }
            }
        }

        anim.SetBool("isWalking", isWalking);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == player)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("ForceAttack"))
        {
            StartCoroutine(GetHit());
            //rb.AddExplosionForce(20000f, collider.gameObject.GetComponent<Rigidbody>().velocity.normalized, 100f);
            float strength = 2000f;
            rb.AddForce(collider.gameObject.GetComponent<Rigidbody>().velocity.normalized * strength);
        }
        else if (collider.CompareTag("Lava")||collider.CompareTag("DeepLava"))
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("performAttack", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("performAttack", false);
        yield return null;
    }
    
    IEnumerator GetHit()
    {
        anim.SetBool("getHit", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("getHit", false);
        yield return null;
    }
    
    IEnumerator Die()
    {
        anim.SetBool("die", true);
        yield return new WaitForSeconds(2f);
        killTracker.killed(area);
        Destroy(gameObject);
        yield return null;
    }

}
