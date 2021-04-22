using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    int player;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = LayerMask.NameToLayer("Player");
        animator = GetComponentInParent<Animator>();
        GameManager.RegisterDoor(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == player)
        {
            GameManager.GameOver();
        }
    }
    public void PlayOpen()
    {

        animator.SetTrigger("Open");
        AudioManager.PlayDoorAudio();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
