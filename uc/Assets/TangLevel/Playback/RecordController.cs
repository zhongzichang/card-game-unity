using System;

namespace TangLevel
{
  public class RecordController
  {

    public static LevelRecord record = new LevelRecord ();

    public static void StartRecording ()
    {

      record.selfGroup = LevelContext.selfGroup;
      record.level = LevelContext.CurrentLevel;

    }

    public static void StopRecording ()
    {

    }
  }
}

