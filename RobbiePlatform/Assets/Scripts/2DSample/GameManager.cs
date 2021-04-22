using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    SenceFoner senceFoner;
    DoorManager doorn;
    List<Ord> ords;
    public int orbnum;
    public int deathnum;
    float gameTime;
    private bool isOver = false;



    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        ords = new List<Ord>();
        DontDestroyOnLoad(this);
  
    }
    public static bool IsOver() { return instance.isOver; }
    public static void PlayerDied()
    {
        instance.senceFoner.FadeOut();
        instance.ords = new List<Ord>();
        instance.deathnum++;
        UIManager.UpdateDeathsUI(instance.deathnum);
        instance.Invoke("RestarScene",1.5f);
    }

    public static void GameOver()
    {
        instance.isOver = true;
        UIManager.UpdateOverUI();
        AudioManager.PlayWinAudio();
    }
    public static  void RestarOrb(Ord orb)
    {
        if (instance==null)
        {
            return;
        }
        if (!instance.ords.Contains(orb))
        {
            instance.ords.Add(orb);
            UIManager.UpdateOrbUI(instance.ords.Count);
        }
    }
    void RestarScene()
    {
        instance.orbnum = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void RegisterSceneFader(SenceFoner sence)
    {
        instance.senceFoner = sence;
    }
    public static void RegisterDoor(DoorManager door)
    {
        instance.doorn = door;
    }
    public static void PlayerGrabbedOrd(Ord ord)
    {
        if (instance.ords.Contains(ord))
        {
            instance.ords.Remove(ord);
            if (instance.ords.Count  < 5)
            {
                instance.doorn.PlayOpen();
            }
            UIManager.UpdateOrbUI(instance.ords.Count);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (instance.isOver)
        {
            return;
        }
        gameTime += Time.deltaTime;
        UIManager.UpdateTimeUI(gameTime);
    }
}
