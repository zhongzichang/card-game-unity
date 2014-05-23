using System;
using UnityEngine;
using TGX = TangGame.Xml;

namespace TangLevel
{
  [Serializable]
  public class Skill
  {
    #region Configration Attributes

    // -- 自身属性 --

    /// <summary>
    /// 编号
    /// </summary>
    public int id;

    /// <summary>
    /// 冷却时间
    /// </summary>
    public float cd;

    /// <summary>
    /// 起手动作时间
    /// </summary>
    public float chargeTime;

    /// <summary>
    /// 释放动作时间
    /// </summary>
    public float releaseTime;

    /// <summary>
    /// 是否大招
    /// </summary>
    public bool bigMove;

    /// <summary>
    /// 攻击距离
    /// </summary>
    public float distance;

    // 技能系统基本参数
    // 升级提升参数

    /// 目标类型
    public int targetType;

    /// <summary>
    /// 范围类型
    /// </summary>
    public int scopeType;

    /// <summary>
    /// 作用器
    /// </summary>
    public Effector[] effectors;


    /// <summary>
    /// 前摇作用器
    /// </summary>
    public Effector[] chargeEffectors;

    /// <summary>
    /// 后摇作用器
    /// </summary>
    public Effector[] releaseEffectors;

    /// <summary>
    /// 是否可打断
    /// </summary>
    public bool breakable;

    /// <summary>
    /// 永久增加属性
    /// </summary>
    public bool attributePersistence;

    /// <summary>
    /// 被替换释放的另一个技能ID
    /// </summary>
    public int replaceSkillId;

    /// <summary>
    /// 蓄力动画剪辑
    /// </summary>
    public string chargeClip;

    /// <summary>
    /// 释放动画剪辑
    /// </summary>
    public string releaseClip;

    /// <summary>
    /// 蓄力特效
    /// </summary>
    public string[] chargeSpecials;

    /// <summary>
    /// 释放特效
    /// </summary>
    public string[] releaseSpecials;

    /// <summary>
    /// 是否可以施展
    /// </summary>
    public bool enable = false;

    #endregion

    #region Public Methods
    public Skill ShallowCopy(){
      return (Skill)this.MemberwiseClone ();
    }
    #endregion

    #region Helper Methods

    public static Skill Compose(TGX.SkillData d){

      Skill s = new Skill ();

      s.id = d.id;
      s.cd = d.cool_time;
      s.chargeTime = d.boot_time;
      s.releaseTime = d.after_time;
      s.bigMove = d.isUltimate;
      s.distance = d.cast_range;
      s.targetType = d.target_type;
      s.scopeType = d.range_type;
      //effectors;
      s.breakable = d.being_interrupted_by_injuries;
      s.attributePersistence = d.is_attribute_addition;
      //s.chargeClip = null;
      //s.releaseClip = null;
      //s.chargeSpecials = d.singing_effect;
      //s.releaseSpecials = d.play_effect;
      //s.enable
      return s;

    }
    #endregion

  }
}

