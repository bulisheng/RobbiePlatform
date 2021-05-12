using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    #region CharacterData_SO
    public CharacterData_SO characterData;
    public int MaxHelth
    {
        get
        {
            if (characterData != null)
                return characterData.maxHelth;
            else
            {
                return 0;
            }
        }
        set { characterData.maxHelth = value; }
    }
    public int CurrHelth
    {
        get
        {
            if (characterData != null)
                return characterData.currHelth;
            else
            {
                return 0;
            }
        }
        set { characterData.currHelth = value; }
    }
    public int BaseDefence
    {
        get
        {
            if (characterData != null)
                return characterData.baseDefence;
            else
            {
                return 0;
            }
        }
        set { characterData.baseDefence = value; }
    }
    public int CurrentDefence
    {
        get
        {
            if (characterData != null)
                return characterData.currentDefence;
            else
            {
                return 0;
            }
        }
        set { characterData.currentDefence = value; }
    }
    #endregion
    #region AttackData_SO
    public AttackData_SO attackData;
    [HideInInspector]
    public bool isCriical;
    public float AttackRange
    {
        get
        {
            if (attackData != null)
                return attackData.attackRange;
            else
            {
                return 0;
            }
        }
        set { attackData.attackRange = value; }
    }
    public float SkillRange
    {
        get
        {
            if (attackData != null)
                return attackData.skillRange;
            else
            {
                return 0;
            }
        }
        set { attackData.skillRange = value; }
    }
    public float CoolDown
    {
        get
        {
            if (attackData != null)
                return attackData.coolDown;
            else
            {
                return 0;
            }
        }
        set { attackData.coolDown = value; }
    }

    public int MinDamge
    {
        get
        {
            if (attackData != null)
                return attackData.minDamge;
            else
            {
                return 0;
            }
        }
        set { attackData.minDamge = value; }
    }
    public int MaxDamge
    {
        get
        {
            if (attackData != null)
                return attackData.maxDamge;
            else
            {
                return 0;
            }
        }
        set { attackData.maxDamge = value; }
    }
    public float CriticalMultipplier
    {
        get
        {
            if (attackData != null)
                return attackData.criticalMultipplier;
            else
            {
                return 0;
            }
        }
        set { attackData.criticalMultipplier = value; }
    }
    public float CriticalChanece
    {
        get
        {
            if (attackData != null)
                return attackData.criticalChanece;
            else
            {
                return 0;
            }
        }
        set { attackData.criticalChanece = value; }
    }
    #endregion

}
