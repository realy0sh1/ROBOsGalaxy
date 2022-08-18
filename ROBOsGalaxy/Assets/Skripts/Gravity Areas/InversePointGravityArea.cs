using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InversePointGravityArea : AbstractGravityArea
{
    // use this instead InverseMiddleOfTriggerGravityArea if you need a center of mass in a place that is not center
    [SerializeField] private Vector3 gravityCenter;
    public override Vector3 getGravity(AbstractPhysicsObject pObject)
    {
        // simple vector math: position of object (e.g player) - Center of gravity = vector from gravity center to player
        Vector3 direction = (pObject.transform.position - (this.transform.position + gravityCenter));
        direction.Normalize();
        return direction;
    }
}