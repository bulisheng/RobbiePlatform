using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Attack ", menuName = "CharacterStats/Attack")]
public class AttackData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public float attackRange;//攻击范围
    public float skillRange;//技能范围
    public float coolDown;//冷却时间
    public int minDamge;//最小攻击力
    public int maxDamge;//最大攻击力

    public float criticalMultipplier;//暴击加成
    public float criticalChanece;//暴击几率
}
