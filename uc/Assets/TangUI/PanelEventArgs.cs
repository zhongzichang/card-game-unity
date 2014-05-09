using System;

namespace TangUI
{
  public enum EventType{
    OnLoad
  }

  public class PanelEventArgs : EventArgs
  {

    private EventType eventType;

    public EventType EventType{
      get{
        return eventType;
      }
    }

    public PanelEventArgs (EventType eventType)
    {
      this.eventType = eventType;
    }

  }
}

