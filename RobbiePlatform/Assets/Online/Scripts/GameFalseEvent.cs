using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFalseEvent : MonoBehaviour
{
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void GameFalse()
    {
        game.GetComponent<Player>().GameFalse();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
