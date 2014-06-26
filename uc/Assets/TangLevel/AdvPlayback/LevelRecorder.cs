using System;

namespace TangLevel.Playback.Adv
{
  public class LevelRecorder
  {

    private LevelRecord record = new LevelRecord ();
    private bool recording = false;

    public static LevelRecorder NewInstance ()
    {

      LevelRecorder r = new LevelRecorder ();
      r.record.id = new Random ().Next (int.MaxValue);
      return r;
    }

    /// <summary>
    /// 防止外部通过这个构造器实例化
    /// </summary>
    private LevelRecorder(){
    }

    public void Start (Group attackGroup, Level level)
    {

      recording = true;
    }

    public void Stop ()
    {
      recording = false;
    }

    public void Save ()
    {

      Cache.advRecordTable.Add (record.id, record);

    }

    /// <summary>
    /// 接收移动通知
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    public void OnNavTo (int heroId, float x)
    {



    }

    /// <summary>
    /// 接收状态改变通知
    /// </summary>
    /// <param name="status">Status.</param>
    public void OnStatusChange (int heroId, HeroStatus status)
    {

    }

  }
}