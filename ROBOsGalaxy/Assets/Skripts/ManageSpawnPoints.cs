using Unity.VisualScripting;
using UnityEngine;

public class ManageSpawnPoints : MonoBehaviour
{
    // spawn
    [SerializeField] private Vector3 standardSpawn;
    private Vector3 activeSpawn;
    private static bool created = false;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            activeSpawn = standardSpawn;
        }
    }

    public void setSpawnTo(GameObject spawn)
    {
        activeSpawn = spawn.transform.position;
    }

    public Vector3 GetSpawnCoordinates()
    {
        return activeSpawn;
    }

    public void ResetSpawn()
    {
        activeSpawn = standardSpawn;
    }

}
