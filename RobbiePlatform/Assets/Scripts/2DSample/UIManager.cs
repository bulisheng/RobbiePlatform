using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public TextMeshProUGUI orbText, deathsText, timeText, gameOverText;
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
        instance.orbText.text = ordNum.ToString();
    }
    public static void UpdateDeathsUI(int deaths)
    {
        instance.deathsText.text = deaths.ToString();
    }
    public static void UpdateTimeUI(float time)
    {
        int min = (int)(time / 60);
        float seconds = time % 60;
        instance.timeText.text = min.ToString("00")+";"+ seconds.ToString("00");
    }
    public static void UpdateOverUI()
    {
        instance.gameOverText.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
