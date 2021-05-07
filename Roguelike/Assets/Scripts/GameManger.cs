using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManger : MonoBehaviour
{

    static int number;

    public static int Number { get => number; set => number = value; }

    static GameManger instance;
    public Text orbText;
    public Text timeText;
    public GameObject load;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    public static void UpdateOrbUI(int ordNum)
    {
        instance.orbText.text = "剩余金币：" + ordNum.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public static void SetText(int numbe)
    {
        Debug.Log(numbe);

    }
    float time;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int min = (int)(time / 60);
        float seconds = time % 60;
        instance.timeText.text = min.ToString("00") + ";" + seconds.ToString("00");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            load.SetActive(true);
        }
    }
    public void OnLoadSece()
    {
        load.SetActive(false);
        number = 0;
        time = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
