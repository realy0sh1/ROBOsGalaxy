using System.Collections;
using UnityEngine;

public class ForceAttack : MonoBehaviour
{
    private void Start()
    {
        //start Corotine
        StartCoroutine(Destroy());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
        yield return null;
    }
}
