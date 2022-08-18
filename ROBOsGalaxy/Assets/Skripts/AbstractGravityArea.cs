using UnityEngine;

public abstract class AbstractGravityArea : MonoBehaviour
{
    [SerializeField] Priority priority;
    [SerializeField] protected float mass;
    public abstract Vector3 getGravity(AbstractPhysicsObject pObject);

    public float getMass()
    {
        return mass;
    }
    private void OnTriggerEnter(Collider other)
    {
        // check if objects wants gravity
        if (other.CompareTag("PhysicsObject"))
        {
            AbstractPhysicsObject pObject = other.GetComponent<AbstractPhysicsObject>();
            if(pObject != null)
                pObject.AddGravity(priority, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PhysicsObject"))
        {
            AbstractPhysicsObject pObject = other.GetComponent<AbstractPhysicsObject>();
            if(pObject != null)
                pObject.RemoveGravity(priority, this);
        }
    }
}
