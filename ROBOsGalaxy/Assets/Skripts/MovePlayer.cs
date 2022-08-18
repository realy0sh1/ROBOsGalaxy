using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    //---adjust values---
    private float jumpPower = 1500f;
    private float moveSpeed = 7f;
    private float turnSpeed = 150f;
    //------------------

    private PlayerMovement playerMovement;

    private Rigidbody rb;
    private bool touchesGroud = false;

    private Animator anim;
    private bool walking;
    private bool dancing = false;

    private GameObject cameraTarget;

    private bool performingAttack = false;

    private float health = 100f;
    private float hitDamage = 30f;
    private float hitBackwardsForce = 1300f;
    private float lavaDamagePerSec = 10f;

    // attack
    [SerializeField] private GameObject forceAttack;
    private float attackSpeed = 10f;
    
    // spawns
    private ManageSpawnPoints spawns;
    
    // UI health
    [SerializeField] private GameObject healthUI;
    private Image healthStatus;

//---camera movement---
    // sothat there a no magic numbers
    private readonly float maxLeftRight = 120f;
    private readonly float maxUp = 30f;
    private readonly float maxDown = 10f;
    private float rotationSpeed = 180f;

    private bool fixWordUp = false;
    //--------------------

    public void Awake()
    {
        playerMovement = new PlayerMovement();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        spawns = GameObject.Find("SpawnPoints").GetComponent<ManageSpawnPoints>();
        healthStatus = healthUI.GetComponent<Image>();
    }

    private void Start()
    {
        cameraTarget = GameObject.Find("Target");
        rb.position = spawns.GetSpawnCoordinates();
        healthStatus.fillAmount = 1f;
    }

    private void OnEnable()
    {
        playerMovement.Player.Walk.Enable();
        playerMovement.Player.Jump.Enable();
        playerMovement.Player.MoveCamera.Enable();
        playerMovement.Player.Respown.Enable();
        playerMovement.Player.Attack.Enable();
        playerMovement.Player.ToggleDance.Enable();

        playerMovement.Player.Jump.performed += Jump;
        playerMovement.Player.Respown.performed += SelfRespown;
        playerMovement.Player.Attack.performed += Attack;
        playerMovement.Player.ToggleDance.performed += ToggleDance;
    }

    private void OnDestroy()
    {
        playerMovement.Player.Walk.Disable();
        playerMovement.Player.Jump.Disable();
        playerMovement.Player.MoveCamera.Disable();
        playerMovement.Player.Respown.Disable();
        playerMovement.Player.Attack.Disable();
        playerMovement.Player.ToggleDance.Disable();

        playerMovement.Player.Jump.performed -= Jump;
        playerMovement.Player.Respown.performed -= SelfRespown;
        playerMovement.Player.Attack.performed -= Attack;
        playerMovement.Player.ToggleDance.performed -= ToggleDance;
    }


    private void FixedUpdate()
    {
        // move player
        Vector2 rawVectorLeftStick = playerMovement.Player.Walk.ReadValue<Vector2>();
        if (rawVectorLeftStick != Vector2.zero)
        {
            // rotate accordign to A and D (left,right movement of left stick)
            rb.MoveRotation(rb.rotation *
                            Quaternion.Euler(0f, rawVectorLeftStick.x * turnSpeed * Time.fixedDeltaTime, 0f));

            // move according to WASD (2d stick vector)
            Vector3 localMoveDirection = new Vector3(0, 0, rawVectorLeftStick.y);



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
                rb.MovePosition(rb.position + plannedMoveDirection * distance);

            }

            walking = true;

        }
        else
        {
            walking = false;
        }

        anim.SetBool("isWalking", walking);
    }
    

    private void Update()
    {
        // turn target sothat cinemaschine moves camera
        Vector2 rawVectorRightStick = playerMovement.Player.MoveCamera.ReadValue<Vector2>();
        if (rawVectorRightStick != Vector2.zero)
        {
            float targetX = 0;
            float targetY = 0;
            if (cameraTarget.transform.localRotation.eulerAngles.x < maxUp ||
                cameraTarget.transform.localRotation.eulerAngles.x >
                (360 - maxDown))
                targetY = rawVectorRightStick.y * rotationSpeed * Time.deltaTime;

            if (cameraTarget.transform.localRotation.eulerAngles.y < maxLeftRight ||
                cameraTarget.transform.localRotation.eulerAngles.y >
                (360 - maxLeftRight))
                targetX = -1 * rawVectorRightStick.x * rotationSpeed * Time.deltaTime;
            cameraTarget.transform.Rotate(new Vector3(targetY, targetX, 0), Space.Self);
        }
        else
        {
            cameraTarget.transform.localRotation = Quaternion.identity;
        }
    }


    private void Jump(InputAction.CallbackContext obj)
    {
        if (touchesGroud)
        {
            rb.AddForce(transform.up * rb.mass * jumpPower);
        }
    }

    private void FixWordUp(InputAction.CallbackContext obj)
    {
        fixWordUp = !fixWordUp;
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        if (!performingAttack)
            StartCoroutine(PerformAttack());
    }

    private void ToggleDance(InputAction.CallbackContext obj)
    {
        dancing = !dancing;
        Dance(dancing);
    }

    private void Dance(bool start)
    {
        anim.SetBool("dance", start);
    }
    
    private void SelfRespown(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("LavaLevel");
    }

    private void OnTriggerEnter(Collider collider)
    {
        // if colling with moon, move with moon
        if (collider.CompareTag("Platform"))
        {
            this.transform.parent = collider.transform;
        }
        else if (collider.CompareTag("DeepLava"))
        {
            ReduceHealth(100);
        } 
        else if (collider.CompareTag("Spawn"))
        {
            spawns.setSpawnTo(collider.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            // take damage from lava
            ReduceHealth(Time.deltaTime*lavaDamagePerSec);
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Platform"))
        {
            if (this.transform.parent == collider.transform)
            {
                this.transform.parent = null;
            }
        }
        else if (collider.CompareTag("OutOfLevel"))
        {
            StartCoroutine(Respawn());
        }
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Platform"))
        {
            this.transform.parent = other.transform;
        }
        else if (other.collider.CompareTag("PhysicsObject"))
        {
            StartCoroutine(GetHit());
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if ((other.collider.CompareTag("Ground") || other.collider.CompareTag("Platform")) && !touchesGroud)
        {
            touchesGroud = true;
            anim.SetBool("inAir", false);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Ground") && touchesGroud)
        {
            touchesGroud = false;
            anim.SetBool("inAir", true);
        }
        else if (other.collider.CompareTag("Platform"))
        {
            if (this.transform.parent == other.transform)
                this.transform.parent = null;
            touchesGroud = false;
            anim.SetBool("inAir", true);
        }
    }

    private void ReduceHealth(float damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
        healthStatus.fillAmount = health/100;
        if (health == 0)
            StartCoroutine(Respawn());

    }
    

    IEnumerator PerformAttack()
    {
        performingAttack = true;
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(0.7f);
        //Instantiate gravity ball
        GameObject attack = Instantiate(forceAttack);
        attack.transform.position = transform.position + (0.4f*transform.forward) + (0.8f*transform.up);
        attack.GetComponent<Rigidbody>().velocity = rb.transform.forward*attackSpeed;
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("attack", false);
        performingAttack = false;
        yield return null;
    }
    
    IEnumerator GetHit()
    {
        anim.SetBool("getHit", true);
        ReduceHealth(hitDamage);
        rb.AddForce(rb.transform.forward*-1*hitBackwardsForce);
        yield return new WaitForSeconds(1f);
        anim.SetBool("getHit", false);
        yield return null;
    }
    
    IEnumerator Respawn()
    {
        //respawn
        OnDestroy();
        anim.SetBool("getHit", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("LavaLevel");
        yield return null;
    }

}