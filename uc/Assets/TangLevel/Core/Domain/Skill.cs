using System;
using UnityEngine;

namespace TangLevel
{
  public class Skill
  {
    #region SelfAttributes

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
    public float attackA;

    /// <summary>
    /// 释放动作时间
    /// </summary>
    public float attackB;

    /// <summary>
    /// 起手是否循环
    /// </summary>
    public bool attackALoop;

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
    /// 作用类型和次数
    /// </summary>
    public Effect[] effects;

    /// <summary>
    /// 作用器序列
    /// </summary>
    public EffectQueue effectQueue;

    /// <summary>
    /// 作用器
    /// </summary>
    public Effector effector;

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
    /// 动画剪辑名称
    /// </summary>
    public string animationClipName;

    /// <summary>
    /// 特效名称
    /// </summary>
    public string specialName;

    /// <summary>
    /// 技能图标
    /// </summary>
    public string icon;

    /// <summary>
    /// 文字描述
    /// </summary>
    public string desc;

    /// <summary>
    /// 黄字描述
    /// </summary>
    public string yellowDesc;

    #endregion

    #region PublicMethods
    public Skill ShallowCopy(){
      return (Skill)this.MemberwiseClone ();
    }
    #endregion

  }
}

