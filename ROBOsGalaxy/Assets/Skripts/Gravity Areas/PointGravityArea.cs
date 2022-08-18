using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGravityArea : AbstractGravityArea
{
    // use this instead MiddleOfTriggerGravityArea if you need a center of mass in a place that is not center
    [SerializeField] private Vector3 gravityCenter;
    public override Vector3 getGravity(AbstractPhysicsObject pObject)
    {
        // simple vector math: point - position of object (e.g. player) = vector from player to point
        Vector3 direction = ((this.transform.position + gravityCenter) - pObject.transform.position);
        direction.Normalize();
        return direction;
    }
}
