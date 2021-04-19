using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    Animator tor;
    int playerHp = 100;
    int playerAttackk = 10;
    Text hpText;
    public Vector3 vector3;
    GameObject miss;
    bool initial = false;
    public Animator GetTor { get => tor; }
    public Text HpText { get => hpText; set => hpText = value; }
    public int PlayerHp { get => playerHp; set => playerHp = value; }
    public int PlayerAttackk { get => playerAttackk; set => playerAttackk = value; }
    GameController controller;
    public GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        tor = GetComponent<Animator>();
        vector3 = transform.position;
        hpText = transform.Find("Text").GetComponent<Text>();
        miss = transform.Find("Miss").gameObject;
        hpText.text = playerHp.ToString();
        controller = transform.parent.GetComponent<GameController>();
    }
    public void Miss()
    {
        miss.gameObject.SetActive(true);
        Invoke("MissFalse", 0.1f);
    }
    public void MissFalse()
    {
        miss.gameObject.SetActive(false);
        game.SetActive(false);
    }
    public void GameFalse()
    {
        game.GetComponent<Animator>().SetBool("jian", false);
        game.SetActive(false);
        controller.PlayerrAttack(playerAttackk * UnityEngine.Random.Range(1, 5));
    }
    /// <summary>
    ///角色攻击动画回调
    /// </summary>
    public void OnSpellattacks()
    {
        Debug.Log("发射飞剑");
        tor.SetBool("spellattacks", false);
        game.SetActive(true);
        game.GetComponent<Animator>().SetBool("jian", true);
        //Invoke("GameFalse", 3f);
    }
    /// <summary>
    ///角色攻击动画回调
    /// </summary>
    public void OnPlayerAttack()
    {
        Debug.Log("砍");
        tor.SetBool("attack", false);
        controller.PlayerrAttack(playerAttackk);
        initial = true;
    }
    // Update is called once per frame
    float time = 0;
    void Update()
    {
        if (initial)
        {

            time += Time.deltaTime;
            if (time > 2f)
            {
                if (Vector3.Distance(transform.position, vector3) > 1)
                { transform.position = Vector3.MoveTowards(transform.position, vector3, 300 * Time.deltaTime); }
                else
                { initial = false; }
            }
        }

    }
}
