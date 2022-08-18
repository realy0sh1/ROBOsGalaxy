using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class IntroHandler : MonoBehaviour
{
    private PlayableDirector pd;
    private Data data;
    private ManageSpawnPoints spawn;
    [SerializeField] private PlayableAsset intro;
    [SerializeField] private PlayableAsset moon;
    [SerializeField] private PlayableAsset outro;

    private bool shownMoonRotatoin = false;
    private bool shownRiddleSolved = false;
        private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
        data = GameObject.Find("Data").GetComponent<Data>();
        spawn = GameObject.Find("SpawnPoints").GetComponent<ManageSpawnPoints>();
    }


    void Start()
    {
        if (!data.introAlreadyPlayed)
        {
            data.introAlreadyPlayed = true;
            pd.playableAsset = intro;
            pd.Play();
        }
    }

    private void Update()
    {
        if (data.moonRotates && !shownMoonRotatoin)
        {
            shownMoonRotatoin = true;
            pd.playableAsset = moon;
            pd.Play();
        }

        if (data.riddleSolved && !shownRiddleSolved)
        {
            shownRiddleSolved = true;
            pd.playableAsset = outro;
            pd.Play();
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(9f);
        // reset all data
        data.moonRotates = false;
        data.riddleSolved = false;
        data.introAlreadyPlayed = false;
        spawn.ResetSpawn();
        SceneManager.LoadScene("StartScreen");
    }
}
