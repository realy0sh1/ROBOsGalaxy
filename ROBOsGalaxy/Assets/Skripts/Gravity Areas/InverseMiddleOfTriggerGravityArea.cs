using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseMiddleOfTriggerGravityArea : AbstractGravityArea
{
    public override Vector3 getGravity(AbstractPhysicsObject pObject)
    {
        // simple vector math: position of object (e.g player) - Center of planet = vector from planet to player
        Vector3 direction = (pObject.transform.position - this.transform.position);
        direction.Normalize();
        return direction;
    }
}
