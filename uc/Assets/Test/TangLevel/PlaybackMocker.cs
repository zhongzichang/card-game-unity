using System;
using TP = TangLevel.Playback;

namespace TangLevel
{
  public class PlaybackMocker
  {
    public static TP.LevelRecord MockRecrod ()
    {

      TP.LevelRecord record = new TP.LevelRecord ();

      record.id = 1;
      //record.attackGroup = Mocker.MockGroup ();
      //record.level = Mocker.MockGrassLevel ();

      return record;

    }
  }
}

