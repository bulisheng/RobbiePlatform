using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Foe : MonoBehaviour
{
    public int hp = 100;
    public int move = 40;
    public int attack = 1;
    private Animator animator;
    public Text hptext;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        hptext.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        hptext.text = hp.ToString();
    }
    public void Move(bool start)
    {
        animator.SetBool("move", start);
    }
    public void Attack(bool start)
    {
        animator.SetBool("attack", start);
    }
    public void Init(bool start)
    {
        animator.SetBool("init", start);
    }
}
