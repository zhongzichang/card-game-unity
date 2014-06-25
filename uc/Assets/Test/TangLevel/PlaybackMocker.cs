using System;

namespace TangLevel
{
  public class PlaybackMocker
  {
    public static LevelRecord MockRecrod ()
    {

      LevelRecord record = new LevelRecord ();

      record.id = 1;
      record.selfGroup = Mocker.MockGroup ();
      record.level = Mocker.MockGrassLevel ();

      return record;

    }
  }
}

