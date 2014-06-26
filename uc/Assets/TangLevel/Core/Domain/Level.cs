using UnityEngine;
using System;
using System.Collections.Generic;

namespace TangLevel
{
  [Serializable]
  public class Level
  {
    // -- 关卡属性 --

    #region Level Attributes

    /// <summary>
    /// 关卡ID
    /// </summary>
    public int id;
    /// <summary>
    /// 关卡名称
    /// </summary>
    public string name;
    /// <summary>
    /// 子关卡
    /// </summary>
    public SubLevel[] subLevels;
    /// <summary>
    /// 敌人的大招特写是否打开
    /// </summary>
    public bool defenseBigMoveCloseUp = false;

    /// <summary>
    /// 自动战斗是否可以设置
    /// </summary>
    public bool autoFightSetable = false;

    /// <summary>
    /// 是否自动战斗
    /// </summary>
    public bool autoFight = false;

    #endregion

    // -- 场景属性 --

    #region Scene Attributes

    #endregion

    #region Constructor

    public Level ()
    {
    }

    public Level (int id, string name)
    {
      this.id = id;
      this.name = name;
    }

    #endregion

    #region PublicMethods

    public Level ShallowCopy ()
    {
      return (Level)this.MemberwiseClone ();
    }

    public Level DeepCopy ()
    {
      Level other = (Level)this.MemberwiseClone ();

      other.subLevels = new SubLevel[ subLevels.Length ];
      for (int i = 0; i < other.subLevels.Length; i++) {
        other.subLevels [i] = subLevels [i].DeepCopy ();
      }

      return other;
    }

    public void DisableDefenseBigMoveCloseUp ()
    {
      foreach (SubLevel slvl in subLevels) {
        slvl.defenseGroup.DisableBigMoveCloseUp ();
      }
    }

    public void EnableDefenseBigMoveCloseUp ()
    {
      foreach (SubLevel slvl in subLevels) {
        slvl.defenseGroup.EnableBigMoveCloseUp ();
      }
    }

    #endregion
  }
}