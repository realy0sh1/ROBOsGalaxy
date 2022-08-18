using UnityEngine;

public class EnemyKillTracker : MonoBehaviour
{
    private int killCounArea1ScinceRespawn;
    [SerializeField] int neededKillsToTriggerMoonRotation = 3;
    private Data data;
    
    private void Awake()
    {
        killCounArea1ScinceRespawn = 0;
        data = GameObject.Find("Data").GetComponent<Data>();
    }

    public void killed(int area)
    {
        if (area == 1)
        {
            killCounArea1ScinceRespawn += 1;
            if (killCounArea1ScinceRespawn == neededKillsToTriggerMoonRotation)
            {
                data.moonRotates = true;
            }
        }
    }
}
