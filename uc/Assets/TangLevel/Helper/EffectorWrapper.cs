using System;
using UnityEngine;

namespace TangLevel
{
  public class EffectorWrapper
  {
    public Effector effector;
    public Skill skill;
    public GameObject source;
    public GameObject target;

    private EffectorWrapper () : base()
    {
    }

    public static EffectorWrapper W(Effector effector, Skill skill, GameObject source, GameObject target){

      EffectorWrapper e = new EffectorWrapper ();
      e.effector = effector;
      e.skill = skill;
      e.source = source;
      e.target = target;
      return e;

    }
  }
}

