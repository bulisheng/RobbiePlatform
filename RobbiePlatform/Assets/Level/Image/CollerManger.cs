using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollerManger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playes;
    public List<GameObject> foes = new List<GameObject>();
    Vector3 initial;
    Vector3 finitial;
    Playe playe1;
    RectTransform playe1Transform;
    RectTransform fTransform;
    public Text tip;
    void Start()
    {
        playe1 = playes.GetComponent<Playe>();
        playe1Transform= playes.GetComponent<RectTransform>();
        fTransform = foes[0].GetComponent<RectTransform>();
        initial = playe1Transform.position;
        finitial = fTransform.position;
        tip.text = "战斗开始";
    }
    bool playeratt = true;
    bool gameopen = true;
    // Update is called once per frame
    void Update()
    {
        if (gameopen)
        {
            if (playeratt)
            {
                foes[0].transform.position = finitial;
                Foe f = foes[0].GetComponent<Foe>();
                playe1.Init(true);
                playe1.Attack(false);
                if (f.hp > 0)
                {
               
                    if (Vector2.Distance(new Vector2(playe1Transform.position.x, playe1Transform.position.y), new Vector2(fTransform.position.x, fTransform.position.y)) > 50f
                  )
                    {
                        playe1.Move(true);
                        playe1Transform.position = Vector3.MoveTowards(playe1Transform.position, fTransform.position, 150 * Time.deltaTime);
                    }
                    else
                    {
                      
                        int rndom= Random.Range(0, 5);
                        if (rndom>2)
                        {
                            tip.text = "玩家攻击了怪物";
                            f.hp = f.hp - playe1.attack;
                        }
                        else
                        {
                            tip.text = "怪物闪避";
                        }
                        playe1Transform.position = playe1Transform.position;
                   
                        playe1.Move(false);
                        playe1.Attack(true);
                        playeratt = false;
                    }
                    if (f.hp <= 0)
                    {
                        tip.text = "玩家获胜";
                        gameopen = false;
                    }
                }
            }
            else
            {
       
                playe1Transform.position = initial;
                Foe f = foes[0].GetComponent<Foe>();
                f.Init(true);
                f.Attack(false);
                if (playe1.hp > 0)
                {
                    if (Vector2.Distance(new Vector2(fTransform.position.x, fTransform.position.y), new Vector2(playe1Transform.position.x, playe1Transform.position.y)) > 50)
                    {
                        f.Move(true);
                        fTransform.position = Vector3.MoveTowards(fTransform.position, playe1Transform.position, 150 * Time.deltaTime);
                    }
                    else
                    {
                        fTransform.position = fTransform.position;
                        f.Move(false);
                        f.Attack(true);
                        int rndom = Random.Range(0, 5);
                        if (rndom > 2)
                        {
                            tip.text = "怪物攻击了玩家";
                            playe1.hp = playe1.hp - f.attack;
                        }
                        else
                        {
                            tip.text = "玩家闪避";
                        }
                    
                        playeratt = true;
                    }
                    if (playe1.hp <= 0)
                    {
                        tip.text = "怪物获胜";
                        gameopen = false;
                    }
                }
            }
        }


    }
}
