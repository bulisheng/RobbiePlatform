using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Foe : MonoBehaviour
{
    Animator tor;
    int foeHp = 100;
    int foeAttackk = 10;
    Text hpText;
    GameObject miss;
    public Animator GetTor { get => tor; }
    public Text HpText { get => hpText; set => hpText = value; }
    public int FoeHp { get => foeHp; set => foeHp = value; }
    public int PlayerAttackk { get => foeAttackk; set => foeAttackk = value; }

    public Vector3 vector3;
    // Start is called before the first frame update
    void Start()
    {
        tor = GetComponent<Animator>();
        vector3 = transform.position;
        hpText = transform.Find("Text").GetComponent<Text>();
        hpText.text = foeHp.ToString();
        miss = transform.Find("Miss").gameObject;
    }
    public void Miss()
    {
        miss.gameObject.SetActive(true);
        Invoke("MissFalse", 1f);
    }
    public void MissFalse()
    {
        miss.gameObject.SetActive(false);
    }
    bool initial = false;
    /// <summary>
    ///怪物攻击动画回调
    /// </summary>
    public void OnFoeAttack()
    {
        Debug.Log("怪物攻击");
        tor.SetBool("foeattack", false);
        initial = true;
    }
    float time = 0;
    // Update is called once per frame
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
