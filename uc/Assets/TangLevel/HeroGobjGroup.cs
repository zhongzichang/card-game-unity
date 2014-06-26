using System;
using System.Collections.Generic;
using UnityEngine;

namespace TangLevel
{
  /// <summary>
  /// 场景中的英雄团队
  /// </summary>
  public class HeroGobjGroup
  {

    private GroupStatus m_status;
    public GroupStatus Status{
      get{
        return m_status;
      }
      set{
        m_status = value;
      }
    }

    public List<GameObject> heros = new List<GameObject>();

  }
}

