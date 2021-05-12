using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerColl : MonoBehaviour
{
    NavMeshAgent nav;
    Animator animator;
    CharacterStats characterStats;
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MonseManager.Instance.onMouseCliscked += MoveToTarget;
        MonseManager.Instance.onAttackCliscked +=AttackToTarget;
    }
    GameObject attackObj;
    float lastTime=0.5f;
    public void MoveToTarget(Vector3 vector)
    {
        nav.isStopped = false;
        StopAllCoroutines();
        nav.destination = vector;
    }
    private void AttackToTarget(GameObject attack)
    {
        if (attack!=null)
        {
            attackObj = attack;
            StartCoroutine(Move());
        }
      
    }
    IEnumerator Move()
    {
        nav.isStopped = false;
        transform.LookAt(attackObj.transform);

        while (Vector3.Distance(attackObj.transform.position,transform.position)> characterStats.attackData.attackRange)
        {
            nav.destination = attackObj.transform.position;
            yield return null;
        }
        nav.isStopped = true;
        if (lastTime<0)
        {
            animator.SetTrigger("Attack");
            lastTime = 0.5f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        SetAnimator();
        lastTime -= Time.deltaTime;
    }

    public void SetAnimator()
    {
        animator.SetFloat("Speed", nav.velocity.sqrMagnitude);
    }
}
