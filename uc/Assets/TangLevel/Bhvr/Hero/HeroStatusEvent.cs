using System;

namespace TangLevel
{
  public class HeroStatusEvent : EventArgs
  {
    private HeroStatus m_status;

    public HeroStatus Status{
      get{
        return m_status;
      }
    }

    public HeroStatusEvent (HeroStatus status)
    {
      this.m_status = status;
    }

  }
}

