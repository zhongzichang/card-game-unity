using System;

namespace TangLevel.Playback
{
  public class RecordController
  {

    public static LevelRecord record = new LevelRecord ();

    public static void StartRecording ()
    {

      record.attackGroup = LevelContext.selfGroup;
      record.level = LevelContext.CurrentLevel;

    }

    public static void StopRecording ()
    {

    }
  }
}

