using System;

namespace TangLevel.Playback
{
  /// <summary>
  /// 关卡记录
  /// </summary>
  public class LevelRecord
  {
    public int id;
    public int frameRate; // 每秒多少帧
    public Group attackGroup; // 进攻方
    public Level level; // 防守方
    public Timeline timeline; // 时间线
    public string version = "1.0.0";

    public LevelRecord ()
    {

    }
  }
}

