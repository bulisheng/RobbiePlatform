using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Data ",menuName ="CharacterStats/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public int maxHelth;
    public int currHelth;
    public int baseDefence;
    public int currentDefence;
}
