using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum Attck
{
    NoAttck,
    PlayerAttck,
    PlayerSpellattacks,
    FoeAttack
}
public class GameController : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    Player player;
    Foe foe;
    Attck attck;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject1.GetComponent<Player>();
        foe = gameObject2.GetComponent<Foe>();
        attck = Attck.NoAttck;
    }
    public void OnAttacksClick()
    {
        if (Vector3.Distance(foe.transform.position, foe.vector3) < 10 && Vector3.Distance(player.transform.position, player.vector3) < 10)
        {
            attck = Attck.PlayerAttck;
        }

    }
    public void OnSpellattacksClick()
    {
        if (Vector3.Distance(foe.transform.position, foe.vector3) < 10 && Vector3.Distance(player.transform.position, player.vector3) < 10)
        {
            player.GetTor.SetBool("spellattacks", true);
            attck = Attck.PlayerSpellattacks;
        }
    }

    public void PlayerrAttack(int playerAttackk)
    {
        foe.FoeHp = foe.FoeHp - playerAttackk;
        if (foe.FoeHp <= 0)
        {
            foe.HpText.text = "死亡";
            return;
        }
        else
        {
            int random = UnityEngine.Random.Range(0, 10);
            if (random > 2)
            {
                foe.HpText.text = foe.FoeHp.ToString();
            }
            else
            {
                foe.Miss();
            }
            attck = Attck.FoeAttack;
        }

    }
    float time = 0;
    // Update is called once per frame
    void Update()
    {
        #region  玩家普通攻击
        if (attck == Attck.PlayerAttck)
        {
            if (Vector3.Distance(player.transform.position, foe.transform.position) > 400f)
            {

                player.transform.position = Vector3.MoveTowards(player.transform.position, foe.transform.position, 150 * Time.deltaTime);
            }
            else
            {
                player.GetTor.SetBool("attack", true);
                attck = Attck.NoAttck;
            }

        }
        #endregion
        #region  玩家法术攻击
        if (attck == Attck.PlayerSpellattacks)
        {
            player.GetTor.SetBool("spellattacks", true);
            attck = Attck.NoAttck;
        }
        #endregion
        #region  怪物普通攻击
        if (attck == Attck.FoeAttack)
        {
            time += Time.deltaTime;
            if (time > 2f)
            {
                if (Vector3.Distance(player.transform.position, player.vector3) < 10f)
                {
                    if (Vector3.Distance(player.transform.position, foe.transform.position) > 400f
    )
                    {

                        foe.transform.position = Vector3.MoveTowards(foe.transform.position, player.transform.position, 150 * Time.deltaTime);
                    }
                    else
                    {
                        player.PlayerHp = player.PlayerHp - foe.PlayerAttackk;
                        if (player.PlayerHp <= 0)
                        {
                            player.HpText.text = "死亡";
                            return;
                        }
                        else
                        {
                            int random = UnityEngine.Random.Range(0, 10);
                            foe.GetTor.SetBool("foeattack", true);
                            if (random > 2)
                            {
                                player.HpText.text = player.PlayerHp.ToString();
                            }
                            else
                            {
                                foe.Miss();
                            }
                        }
                        attck = Attck.NoAttck;
                    }
                }
            }
        }
        #endregion
    }
}
