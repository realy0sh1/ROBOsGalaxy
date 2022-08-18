using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleOfTriggerGravityArea : AbstractGravityArea
{
    public override Vector3 getGravity(AbstractPhysicsObject pObject)
    {
        // simple vector math: Center of planet - position of object (e.g. player) = vector from player to planet
        Vector3 direction = (this.transform.position - pObject.transform.position);
        direction.Normalize();
        return direction;
    }
}
