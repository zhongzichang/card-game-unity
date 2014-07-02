using System;

namespace TangLevel.Playback
{
  public class LevelRecorder
  {

    private bool isRecording = false;
    private LevelRecord record;

    public LevelRecorder ()
    {
    }

    public bool IsRecording{
      get{
        return isRecording;
      }
    }

    public void Start(){
      //ecord = new LevelRecord ();
      isRecording = true;
    }

    public void Stop(){
      isRecording = false;
    }

    public void Pause(){
      isRecording = false;
    }

    public void Resume(){
      isRecording = true;
    }

    public void Save(){
      Cache.recordTable.Add (record.id, record);
    }

  }
}

