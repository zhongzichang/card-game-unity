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

    #region Constants
    /// <summary>
    /// 秒转换为毫秒
    /// </summary>
    public const float SECOND_TO_MIL = 1000F;
    /// <summary>
    /// 地图右边界
    /// </summary>
    public const float RIGHT_BOUND = 70;
    /// <summary>
    /// 垂直中线
    /// </summary>
    public const float VERTICAL_CENTER_LINE = 32;
    /// <summary>
    /// 上边界
    /// </summary>
    public const float TOP_BOUND = 20;
    /// <summary>
    /// 水平中线
    /// </summary>
    public const float HORIZONTAL_CENTER_LINE = 18;
    /// <summary>
    /// 下边界
    /// </summary>
    public const float BOTTOM_BOUND = 16;
    /// <summary>
    /// 五条水平线
    /// </summary>
    public static readonly int[] HORIZONTAL_LINES = new int[]{ 16, 17, 18, 19, 20 };
    /// <summary>
    /// 英雄位置 Z 轴上限
    /// </summary>
    public const float HERO_POS_MIN_Z = -100;
    /// <summary>
    /// 缺省攻击的动画剪辑
    /// </summary>
    public const string DEFAULT_ATTACK_CLIP = "attack";
    /// <summary>
    /// 缺省投射标记
    /// </summary>
    public const string DEFAULt_CAST_LABEL = "cast";
    /// <summary>
    /// 人物比例还原
    /// </summary>
    public const string SCALE_RESUME_LABEL = "scale_resume";
    /// <summary>
    ///   是否使用打包后的资源
    /// </summary>
    public static bool use_packed_res = false;
    /// <summary>
    /// 战斗背景资源路径
    /// </summary>
    public const string GOBJS_PATH = "Prefabs/Gobjs";
    /// <summary>
    /// The DBF x_ PREFI.
    /// </summary>
    public const string DBFX_PREFIX = "dbfx_";
    /// <summary>
    /// 最大 HP
    /// </summary>
    public const int MAX_HP = 1000; 
    /// <summary>
    /// 公共CD
    /// </summary>
    public const float HERO_CD = 3F;
    /// <summary>
    /// 公共速度
    /// </summary>
    public const float HERO_SPEED = 1F;
    /// <summary>
    /// 列阵距离
    /// </summary>
    public const float EMBATTLE_DISTANCE = 10;
    #endregion

    #region Statics
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
    #endregion

    #region PublicMethods
    public static string GobjsPath (string name)
    {
      return GOBJS_PATH + Tang.Config.DIR_SEP + name;
    }
    #endregion
  }
}

