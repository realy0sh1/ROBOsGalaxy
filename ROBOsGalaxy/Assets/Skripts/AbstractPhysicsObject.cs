using System.Collections.Generic;
using UnityEngine;

public enum Priority
{
    prio0, prio1, prio2, prio3
}
public abstract class AbstractPhysicsObject : MonoBehaviour
{
    protected Vector3 gravityDirection;
    protected float totalMassOfGravityAreas;
    // simple bucket sort, K=4: sortieren in O(1) 
    
    //lowest priority
    private List<AbstractGravityArea> gravityWithPrio0 = new List<AbstractGravityArea>();
    private List<AbstractGravityArea> gravityWithPrio1 = new List<AbstractGravityArea>();
    private List<AbstractGravityArea> gravityWithPrio2 = new List<AbstractGravityArea>();
    //hightest priority
    private List<AbstractGravityArea> gravityWithPrio3 = new List<AbstractGravityArea>();
    

    // writes to protected vars gravityDirection and totalMassOfGravity Areas
    protected void UpdateGravityDirectionAndTotalMass()
    {
        gravityDirection = Vector3.zero;
        totalMassOfGravityAreas = 0;
        if (gravityWithPrio3.Count != 0)
        {
            foreach(AbstractGravityArea gArea in gravityWithPrio3)
            {
                gravityDirection += gArea.getGravity(this);
                // take average mass of all active points as total mass
                totalMassOfGravityAreas += (gArea.getMass()/gravityWithPrio3.Count);
            }
        }else if (gravityWithPrio2.Count != 0)
        {
            foreach(AbstractGravityArea gArea in gravityWithPrio2)
            {
                gravityDirection += gArea.getGravity(this);
                // take average mass of all active points as total mass
                totalMassOfGravityAreas += (gArea.getMass()/gravityWithPrio2.Count);
            }
        }else if (gravityWithPrio1.Count != 0)
        {
            foreach(AbstractGravityArea gArea in gravityWithPrio1)
            {
                gravityDirection += gArea.getGravity(this);
                // take average mass of all active points as total mass
                totalMassOfGravityAreas += (gArea.getMass()/gravityWithPrio1.Count);
            }
        }else if (gravityWithPrio0.Count != 0)
        {
            foreach(AbstractGravityArea gArea in gravityWithPrio0)
            {
                gravityDirection += gArea.getGravity(this);
                // take average mass of all active points as total mass
                totalMassOfGravityAreas += (gArea.getMass()/gravityWithPrio0.Count);
            }
        }
        gravityDirection.Normalize();
    }

    public void AddGravity(Priority prio, AbstractGravityArea gArea)
    {
        switch (prio)
        {
            case Priority.prio0:
                gravityWithPrio0.Add(gArea);
                break;
            case Priority.prio1:
                gravityWithPrio1.Add(gArea);
                break;
            case Priority.prio2:
                gravityWithPrio2.Add(gArea);
                break;
            case Priority.prio3:
                gravityWithPrio3.Add(gArea);
                break;
        }
    }
    
    public void RemoveGravity(Priority prio, AbstractGravityArea gArea)
    {
        switch (prio)
        {
            case Priority.prio0:
                gravityWithPrio0.Remove(gArea);
                break;
            case Priority.prio1:
                gravityWithPrio1.Remove(gArea);
                break;
            case Priority.prio2:
                gravityWithPrio2.Remove(gArea);
                break;
            case Priority.prio3:
                gravityWithPrio3.Remove(gArea);
                break;
        }
    }
}