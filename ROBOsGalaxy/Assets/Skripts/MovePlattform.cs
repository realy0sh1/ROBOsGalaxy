using System.Collections;
using UnityEngine;

public class MovePlattform : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float time;
    
    private Rigidbody rb;

        // Start is called before the first frame update
    void Start()
    {
        moveDirection = moveDirection.normalized;
        rb = GetComponent<Rigidbody>();
        if(moveSpeed > 0)
            StartCoroutine(SwitchDirection());
    }
    
    IEnumerator SwitchDirection()
    {
        while (true)
        {
            rb.velocity = moveDirection * moveSpeed;
            yield return new WaitForSeconds(time);
            rb.velocity = moveDirection * moveSpeed*-1;
            yield return new WaitForSeconds(time);
        }
    }
}
