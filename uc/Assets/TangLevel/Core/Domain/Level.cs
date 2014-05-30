﻿using UnityEngine;
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
    public SubLevel[] subLeves;
    /// <summary>
    /// 敌人的大招特写是否打开
    /// </summary>
    public bool enemyBigMoveCloseUp = false;

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

      other.subLeves = new SubLevel[ subLeves.Length ];
      for (int i = 0; i < other.subLeves.Length; i++) {
        other.subLeves [i] = subLeves [i].DeepCopy ();
      }

      return other;
    }

    public void DisableEnemyBigMoveCloseUp ()
    {
      foreach (SubLevel slvl in subLeves) {
        slvl.enemyGroup.DisableBigMoveCloseUp ();
      }
    }

    public void EnableEnemyBigMoveCloseUp ()
    {
      foreach (SubLevel slvl in subLeves) {
        slvl.enemyGroup.EnableBigMoveCloseUp ();
      }
    }

    #endregion
  }
}