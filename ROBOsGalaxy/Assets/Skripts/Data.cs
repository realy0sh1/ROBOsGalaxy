using UnityEngine;

public class Data : MonoBehaviour
{
    [SerializeField] public bool moonRotates = false;
    [SerializeField] public bool riddleSolved = false;
    [SerializeField] public bool introAlreadyPlayed = false;
    private static bool created = false;
    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }
    
}
