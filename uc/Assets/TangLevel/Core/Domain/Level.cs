using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  public class Level
  {

    // -- 关卡属性 --
    #region Level Attributes
    public int id;
    public string name;
    public SubLevel[] subLeves;
    #endregion

    // -- 场景属性 --
    #region Scene Attributes
    /// <summary>
    /// 我方小组
    /// </summary>
    public Group selfGroup;
    /// <summary>
    /// 敌方所有 GameObject
    /// </summary>
    public List<GameObject> enemyGobjs = new List<GameObject>();
    #endregion


    #region Constructor
    public Level(){
    }

    public Level(int id, string name){
      this.id = id;
      this.name = name;
    }
    #endregion

  }
}