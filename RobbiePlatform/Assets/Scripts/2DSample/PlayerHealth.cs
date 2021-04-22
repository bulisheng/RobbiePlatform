using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject deathVFXPrefab;
    public GameObject deathRobbieGhost;
    int trapsLayer;
    // Start is called before the first frame update
    void Start()
    {
        trapsLayer = LayerMask.NameToLayer("Traps");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer== trapsLayer)
        {
            AudioManager.PlayDeathAudio();
            Instantiate(deathVFXPrefab,transform.position,transform.rotation);
            Instantiate(deathRobbieGhost, transform.position, Quaternion.Euler(0,0,Random.Range(-45,90)));
            gameObject.SetActive(false);
            GameManager.PlayerDied();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
