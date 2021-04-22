using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ord : MonoBehaviour
{
    public GameObject deathVFXPrefab;
    int player;
  
    // Start is called before the first frame update
    void Start()
    {
        player = LayerMask.NameToLayer("Player");
        GameManager.RestarOrb(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == player)
        {
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
            AudioManager.PlayOrdAudio();
            GameManager.PlayerGrabbedOrd(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
