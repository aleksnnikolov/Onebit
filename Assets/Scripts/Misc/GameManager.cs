using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static int extraCrawlersRetrieved = 0;

    public bool hasActivatedCheckpoint = false;

    public static GameObject crossfadeImage;
    public GameObject bit;
    public GameObject cam;
    public GameObject[] crawlers;
    public GameObject[] boxes;

    BitBehaviour bitScript;

    float rTimer;
    public bool canReload = true;

    public static bool inputBlocked;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    //Gets called when new scene is loaded
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Code gets executed at start of scene
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
    
        crossfadeImage = GameObject.Find("CrossfadeImage");
        bit = GameObject.FindGameObjectWithTag("Bit");
        bitScript = bit.GetComponent<BitBehaviour>();
        cam = Camera.main.gameObject;
        crawlers = GameObject.FindGameObjectsWithTag("Player");
        boxes = GameObject.FindGameObjectsWithTag("Box");
        GeneralManager.SetVolumes();

        rTimer = 3.0f;
              
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update() {
        if (rTimer > 0)
            rTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && rTimer <= 0 && hasActivatedCheckpoint && canReload) {
            StartCoroutine(restartFromCheckPoint());
            rTimer = 3.0f;
        }

    }

    public void InitiateGameSave() {
        SaveSystem.SaveData(bit.transform.position, cam.transform.position, crawlers, boxes);
    }

    public void InitiateGameLoad() {
        SaveData data = SaveSystem.LoadData();

        Vector3 bitPos = new Vector3();
        bitPos.x = data.bitPosition[0];
        bitPos.y = data.bitPosition[1];
        bitPos.z = data.bitPosition[2];
        bit.transform.position = bitPos + new Vector3(1f, 0f, 0f);  //Offsets bit from player slightly

        SetDefaultOptions();

        Vector3 camPos = new Vector3();
        camPos.x = data.camPosition[0];
        camPos.y = data.camPosition[1];
        camPos.z = data.camPosition[2];

        Vector3 crawlerPos;
        for (int i = 0; i < crawlers.Length; i++) {
            crawlerPos = Vector3.zero;
            crawlerPos.x = data.crawlersPosition[i, 0];
            crawlerPos.y = data.crawlersPosition[i, 1];
            crawlerPos.z = data.crawlersPosition[i, 2];
            crawlers[i].transform.position = crawlerPos;
        }

        Vector3 boxPos;
        for (int i = 0; i < boxes.Length; i++) {
            boxPos = Vector3.zero;
            boxPos.x = data.boxesPosition[i, 0];
            boxPos.y = data.boxesPosition[i, 1];
            boxPos.z = data.boxesPosition[i, 2];
            boxes[i].transform.position = boxPos;
        }
    }

    public void LoadNextScene() {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
 
    //Some rules for after loading
    private void SetDefaultOptions() {
        bitScript.ForceDrop();
        bitScript.NoControllable();
        bit.GetComponent<Rigidbody2D>().simulated = true;
    }

    public static int GetCurrentSceneIndex() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public IEnumerator restartFromCheckPoint() {
        Pause.canPause = false;
        canReload = false;
        crossfadeImage.GetComponent<Animator>().SetTrigger("Start");

        yield return new WaitForSeconds(0.5f);
        InitiateGameLoad();
        yield return new WaitForSeconds(0.25f);

        bit.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        foreach (GameObject crawler in crawlers) {
            crawler.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        crossfadeImage.GetComponent<Animator>().SetTrigger("End");
        bitScript.SelectControllableUnit();
        canReload = true;
        Pause.canPause = true;
    }

    IEnumerator LoadScene(int index) {
        crossfadeImage.GetComponent<Animator>().SetTrigger("Start");

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(index);
    }

}
