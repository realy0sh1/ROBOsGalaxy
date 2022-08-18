using UnityEngine;

public class EnemyGravityController : AbstractPhysicsObject
{
    private Rigidbody rb;
    private float rotationSpeed = 2f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        rb.freezeRotation = true;
    }
    
    private void FixedUpdate()
    {   
        // funkton of abstractPhysicsObjects
        // writes current gravity direction to vector3: gravityDirection
        // writes current total mass of all active gravity fields to float: totalMassOfGravityAreas
        // I chose this way, because it is not possible to return 2 values and its the same class anyway
        UpdateGravityDirectionAndTotalMass();
        
        // smoothly rotate from current position into gravity position (Slerp)
        rb.MoveRotation(Quaternion.Slerp(rb.rotation,  Quaternion.FromToRotation(transform.up, gravityDirection*-1)*rb.rotation, rotationSpeed*Time.fixedDeltaTime));
        // add force 
        rb.AddForce(gravityDirection * totalMassOfGravityAreas * rb.mass);
    }
}
