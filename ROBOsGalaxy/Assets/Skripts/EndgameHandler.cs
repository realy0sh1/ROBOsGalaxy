using UnityEngine;

public class EndgameHandler : MonoBehaviour
{
    private GameObject water;
    private GameObject lava;
    private GameObject bigLavaJet;
    private Data data;

    private void Awake()
    {
        data = GameObject.Find("Data").GetComponent<Data>();
        water = GameObject.Find("BigWaterGround");
        lava = GameObject.Find("BigLavaGround");
        bigLavaJet = GameObject.Find("BigLavaJet");
    }

    private void Update()
    {
        if (data.riddleSolved)
        {
            water.SetActive(true);
            lava.SetActive(false);
            bigLavaJet.SetActive(false);
        }
        else
        {
            water.SetActive(false);
            lava.SetActive(true);
            bigLavaJet.SetActive(true);
        }
    }
}
