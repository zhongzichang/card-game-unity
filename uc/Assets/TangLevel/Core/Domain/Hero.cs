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
using TGN = TangGame.Net;
using TGX = TangGame.Xml;

namespace TangLevel
{
  [Serializable]
  public class Hero
  {
    #region Events

    public delegate void HpChangeHandler (int val,int max);

    public delegate void MpChangeHandler (int val,int max);

    public HpChangeHandler raiseHpChange;
    public MpChangeHandler raiseMpChange;

    #endregion

    // --- 人物属性 ---

    #region Configration Attributes
    public int configId;
    public string name;
    public string resName;
    /// <summary>
    /// 英雄技能
    /// </summary>
    public Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
    /// <summary>
    /// 出手序列
    /// </summary>
    public int[] skillQueue;
    /// <summary>
    /// 英雄的 AI，可以有多个
    /// </summary>
    public string[] ai;
    /// <summary>
    /// 配置冷却时间
    /// </summary>
    public float configCd;
    #endregion

    #region Variable Attributes

    public int id;
    public int maxHp;
    public int maxMp;
    public float cd; // 冷却时间
    public float speedScale; // 动画速度缩放

    /// <summary>
    /// 出场顺序值
    /// </summary>
    public int sort;
    /// <summary>
    /// 等级
    /// </summary>
    public int grade;
    /// <summary>
    /// 出生点
    /// </summary>
    public Vector2 birthPoint = Vector2.zero;
    /// <summary>
    /// 战斗方向
    /// </summary>
    public BattleDirection battleDirection = BattleDirection.RIGHT;

    /// 物理攻击
    public float physicalAttack;
    /// 魔法强度
    public float magicPower;
    /// 物理护甲
    public float physicalDefense;
    /// 魔法抗性
    public float magicDefense;
    /// 物理暴击
    public float physicalCrit;
    /// 法术暴击
    public float magicCrit;
    /// 生命回复
    public float hpRecovery;
    /// 能量回复
    public float mpRecovery;
    /// 破甲值
    public float routPhysicalDefense;
    /// 无视魔抗，破魔值
    public float routMagicDefense;
    /// 吸血等级
    public float bloodSuckingGrade;
    /// 闪避
    public float eva;
    /// 治疗效果
    public float healEffect;
    // 能量消耗降低
    public float mp_consume_dec;

    private int m_hp;
    private int m_mp;

    #endregion

    #region Properties

    public int hp {
      get{ return m_hp; }
      set {
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

    public int mp {
      get{ return m_mp; }
      set {
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

    #endregion

    #region Constroctor

    public Hero ()
    {

    }

    #endregion

    #region Public Methods

    public Hero ShallowCopy ()
    {
      return (Hero)this.MemberwiseClone ();
    }

    #endregion

    #region HelperMethods
    /// <summary>
    /// 根据网络数据和配置文件生成自身英雄数据
    /// </summary>
    /// <param name="n">N.</param>
    /// <param name="d">D.</param>
    /// <param name="sd">Sd.</param>
    public static Hero Compose (TGN.HeroNet n, TGX.HeroData d, TGX.HeroSortData sd)
    {

      Hero h = new Hero ();

      // 配置值
      h.name = d.name;
      h.resName = d.avatar;
      //h.sort = sd.location;
      //h.skills = null;
      //h.skillQueue = null;
      //h.ai
      //maxHp;
      //maxMp;
      // 变化值
      h.id = n.id;
      h.birthPoint = Vector2.zero;
      h.battleDirection = BattleDirection.RIGHT;
      return h;
    }

    #endregion
  }
}

