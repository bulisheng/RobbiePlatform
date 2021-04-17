using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Playe : MonoBehaviour
{
    public int hp = 100;
    public int move = 50;
    public int attack = 10;
    public Text hptext;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
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
    // Update is called once per frame
    void Update()
    {
        hptext.text = hp.ToString();
    }
}
