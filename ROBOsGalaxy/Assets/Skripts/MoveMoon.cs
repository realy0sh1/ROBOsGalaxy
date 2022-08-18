using UnityEngine;

public class MoveMoon : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private float moveSpeed;

    private Rigidbody rb;
    private Data data;

    private void Awake()
    {
        data = GameObject.Find("Data").GetComponent<Data>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (data.moonRotates)
        {
            rb.rotation = rb.rotation * Quaternion.AngleAxis(moveSpeed * Time.deltaTime, rotationAxis);
        }
    }
}
