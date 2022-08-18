using UnityEngine;

public class RiddleController : MonoBehaviour
{
    private Data data;

    private ParticleSystem[] ps = {null, null, null, null};

    private bool[] riddleStatus = {false, false, false, false};

    private void Awake()
    {
        
        data = GameObject.Find("Data").GetComponent<Data>();
        ps[0] = GameObject.Find("v1").GetComponent<ParticleSystem>();
        ps[1] = GameObject.Find("v3").GetComponent<ParticleSystem>();
        ps[2] = GameObject.Find("v2").GetComponent<ParticleSystem>();
        ps[3] = GameObject.Find("v4").GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        ResetRiddle();
    }
    
    private void ResetRiddle()
    {
        foreach (ParticleSystem p in ps)
        {
            p.Play();
        }
        riddleStatus[0] = false;
        riddleStatus[1] = false;
        riddleStatus[2] = false;
        riddleStatus[3] = false;
    }

    public void TriggerSwitch(int number)
    {
        switch (number)
        {
            case 0:
                riddleStatus[0] = true;
                ps[0].Stop();
                break;
            case 1:
                if (riddleStatus[0])
                {
                    riddleStatus[1] = true;
                    
                    ps[1].Stop();
                }
                else
                    ResetRiddle();
                break;
           case 2:
                if (riddleStatus[0] && riddleStatus[1])
                {
                    riddleStatus[2] = true;
                    ps[2].Stop();
                }
                else
                    ResetRiddle();
                break;
           case 3:
               if (riddleStatus[0] && riddleStatus[1] && riddleStatus[2])
               {
                   riddleStatus[3] = true;
                   ps[3].Stop();
                   data.riddleSolved = true;
               }
               else
                   ResetRiddle();
               break;
           default:
               break;
        }
    }
}
