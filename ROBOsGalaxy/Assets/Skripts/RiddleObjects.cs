using UnityEngine;

public class RiddleObjects : MonoBehaviour
{
    [SerializeField] private int numberOfObject;
    private RiddleController rc;

    private void Awake()
    {
        rc = GameObject.Find("Riddle").GetComponent<RiddleController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("works");
        if(other.CompareTag("ForceAttack"))
            rc.TriggerSwitch(numberOfObject);
    }
}
