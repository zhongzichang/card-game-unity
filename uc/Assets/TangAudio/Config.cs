using System;

namespace TangAudio
{
  public class Config
  {
    public static bool use_packed_res = false;
    /// <summary>
    /// 战斗背景资源路径
    /// </summary>
    public const string AUDIOS_PATH = "Audios";

    #region PublicMethods
    public static string AudioPath (string name)
    {
      return AUDIOS_PATH + Tang.Config.DIR_SEP + name;
    }
    #endregion
  }
}

