// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System.Collections.Generic;

namespace TangLevel
{
  public class Config
  {
    /// <summary>
    ///   是否使用打包后的资源
    /// </summary>
    public static bool use_packed_res = false;

    /// <summary>
    /// 战斗背景资源路径
    /// </summary>
    public const string BATTLE_BG_PATH = "Prefabs/Level/BattleBg";


    /// <summary>
    /// 特效资源路径
    /// </summary>
    public const string SPECIAL_PATH = "Specials";
    public static string SpecialPath(string name){
      return SPECIAL_PATH + Tang.Config.DIR_SEP + name;
    }


    /// <summary>
    /// 关卡表
    /// </summary>
    public static Dictionary<int, Level> levelTable = new Dictionary<int, Level> ();
    /// <summary>
    /// 技能表
    /// </summary>
    public static Dictionary<int, Skill> skillTable = new Dictionary<int, Skill> ();
    /// <summary>
    /// 作用器表
    /// </summary>
    public static Dictionary<int, Effector> effectorTable = new Dictionary<int, Effector> ();
  }
}

