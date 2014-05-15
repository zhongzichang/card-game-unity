// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;
namespace TangLevel
{
  [Serializable]
  public class Hero
  {

    public delegate void HpChangeHandler (int val, int max);
    public HpChangeHandler raiseHpChange;

    public delegate void MpChangeHandler(int val, int max);
    public MpChangeHandler raiseMpChange;

    // --- 人物属性 ---
    #region Character Attributes
    /// <summary>
    /// The identifier.
    /// </summary>
    public int id;
    /// <summary>
    /// The name.
    /// </summary>
    public string name;
    /// <summary>
    /// 资源名称
    /// </summary>
    public string resName;
    /// <summary>
    /// 力量
    /// </summary>
    public int strength;
    /// <summary>
    /// 智力
    /// </summary>
    public int intelligence;
    /// <summary>
    /// 敏捷
    /// </summary>
    public int agility;
    /// <summary>
    /// 最大生命值
    /// </summary>
    public int maxHp;
    /// <summary>
    /// 生命值
    /// </summary>
    private int m_hp;
    public int hp {
      get{ return m_hp; }
      set{
        if (value < 0) {
          m_hp = 0;
        } else if (value > maxHp) {
          m_hp = maxHp;
        } else {
          m_hp = value;
        }
        if (raiseHpChange != null) {
          raiseHpChange (m_hp, maxHp);
        }
      }
    }
    /// <summary>
    /// 最大能量值
    /// </summary>
    public int maxMp;
    /// <summary>
    /// 能量值
    /// </summary>
    private int m_mp;
    public int mp {
      get{ return m_mp; }
      set{
        if (value < 0) {
          m_mp = 0;
        } else if (value > maxMp) {
          m_mp = maxMp;
        } else {
          m_mp = value;
        }
        if (raiseMpChange != null) {
          raiseMpChange (m_mp, maxMp);
        }
      }
    }

    /// <summary>
    /// 等级
    /// </summary>
    public int grade;

    /// <summary>
    /// 战斗前布阵所站的位置 - 前，中，后
    /// </summary>
    public BattlePos battlePos;

    /// <summary>
    /// 攻击距离
    /// </summary>
    public int attackDistance;
    /// <summary>
    /// 英雄的 AI，可以有多个
    /// </summary>
    public string[] ai;
    /// <summary>
    /// 英雄技能
    /// </summary>
    public List<Skill> skills;

    /// <summary>
    /// 出手序列
    /// </summary>
    public int[] skillQueue;

    #endregion


    // --- 场景属性 ---
    #region Scene Attribute
    /// <summary>
    /// 出生点
    /// </summary>
    public Vector2 birthPoint = Vector2.zero;
    /// <summary>
    /// 战斗方向
    /// </summary>
    public BattleDirection battleDirection = BattleDirection.RIGHT;
    #endregion


    #region Constroctor
    public Hero ()
    {
    }
    #endregion


    #region PublicMethods

    public Hero ShallowCopy ()
    {
      return (Hero)this.MemberwiseClone ();
    }

    #endregion
  }
}

