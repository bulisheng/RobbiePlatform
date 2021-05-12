using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnmyCont : MonoBehaviour
{
    public EnemyStates enemyStates;
    GameObject attactTarget;//攻击目标
    NavMeshAgent agent;
    Animator animator;
    CharacterStats characterStats;
    public bool isGuard;

    float speed;//记录初始移动速度
    //可视化距离
    [Header("Basic Settings")]
    public float sightRadius;

    [Header("Patrol state Settings")]
    public float patrorRange;

    bool isWalk;
    bool isChase;
    bool isFollow;

    Vector3 wayPoint;//随机巡逻点
    Vector3 startV3;

    public float lookatTime;//停留注视时间
    float remainLookAtTime;
    float lastAttackTime;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
        speed = agent.speed;
        startV3 = transform.position;
        remainLookAtTime = lookatTime;
    }

    void Start()
    {
        if (isGuard)
        {
            enemyStates = EnemyStates.GUARD;//站桩敌人
        }
        else
        {
            enemyStates = EnemyStates.PATROL;//巡逻敌人
            GetNewMayPint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SwichStates();
        SwichAnimation();
        lastAttackTime -= Time.deltaTime;
    }
    //状态机 切换状态
    void SwichStates()
    {
        if (FoundPlayer())
        {
            enemyStates = EnemyStates.CHASE;
        }
        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                agent.speed = speed;
                break;
            case EnemyStates.PATROL:
                isChase = false;
                agent.speed = speed * 0.5f;
                //agent.stoppingDistance  根据大小获取不同的停止距离
                if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
                {
                    isWalk = false;
                    if (remainLookAtTime > 0)
                    {
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else
                    {
                        GetNewMayPint();
                    }
                }
                else
                {
                    isWalk = true;
                    agent.destination = wayPoint;
                }
                break;
            case EnemyStates.CHASE:
                isChase = true;
                isWalk = false;
                agent.speed = speed;
                if (!FoundPlayer())
                {
                    isFollow = false;
                    if (remainLookAtTime > 0)
                    {
                        agent.destination = transform.position;
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else if (isGuard)
                    {
                        enemyStates = EnemyStates.GUARD;
                    }
                    else
                    {
                        enemyStates = EnemyStates.PATROL;
                    }

                }
                else
                {
                    isFollow = true;
                    agent.isStopped = false;
                    agent.destination = attactTarget.transform.position;
                }
                if (TargetInAttackRange() || TargetInSkillRange())
                {
                    isFollow = false;
                    agent.isStopped = true;
                    if (lastAttackTime < 0)
                    {
                        lastAttackTime = characterStats.attackData.coolDown;

                        //暴击判断
                        characterStats.isCriical = Random.value < characterStats.attackData.criticalChanece;
                        //攻击
                        Attack();
                    }
                }
                break;
            case EnemyStates.DEAD:
                break;
            default:
                break;
        }
    }
    void Attack()
    {
        transform.LookAt(attactTarget.transform);
        if (TargetInAttackRange())
        {
            animator.SetTrigger("Attack");
        }
        if (TargetInSkillRange())
        {
            animator.SetTrigger("Skill");
        }
    }
    bool TargetInAttackRange()
    {
        if (attactTarget != null)
        {
            return Vector3.Distance(attactTarget.transform.position, transform.position) <= characterStats.attackData.attackRange;
        }
        else
        {
            return false;
        }
    }
    bool TargetInSkillRange()
    {
        if (attactTarget != null)
        {
            return Vector3.Distance(attactTarget.transform.position, transform.position) <= characterStats.attackData.skillRange;
        }
        else
        {
            return false;
        }
    }
    //通过射线检测周围是否有敌人
    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach (var item in colliders)
        {
            if (item.CompareTag("Player"))
            {
                attactTarget = item.gameObject;
                return true;
            }
        }
        attactTarget = null;
        return false;
    }
    /// <summary>
    /// 设置动画
    /// </summary>
    void SwichAnimation()
    {
        animator.SetBool("Walk", isWalk);
        animator.SetBool("Chase", isChase);
        animator.SetBool("Follow", isFollow);
        animator.SetBool("Criical", characterStats.isCriical);
    }
    /// <summary>
    /// 画个圈圈 显示目标的范围
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
    /// <summary>
    /// 获取巡逻点
    /// </summary>
    void GetNewMayPint()
    {
        remainLookAtTime = lookatTime;
        float randomX = Random.Range(-patrorRange, patrorRange);
        float randomZ = Random.Range(-patrorRange, patrorRange);
        //获取随机坐标点
        Vector3 randomPoint = new Vector3(startV3.x + randomX, transform.position.y, startV3.z + randomZ);
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrorRange, 1) ? hit.position : transform.position;//获取随机坐标点是在导航网格上
    }
    // Start is called before the first frame update

}
