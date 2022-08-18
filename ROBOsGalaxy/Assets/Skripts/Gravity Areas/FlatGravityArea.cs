using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatGravityArea : AbstractGravityArea
{
    public override Vector3 getGravity(AbstractPhysicsObject pObject)
    {
        // always downwards force
        return -1*transform.up;
    }
}
