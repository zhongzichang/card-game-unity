using System;
using UnityEngine;

namespace TangLevel
{
  public class SkillWrapper
  {
    public Skill skill;
    public GameObject source;
    public GameObject target;

    public SkillWrapper ()
    {
    }

    public static SkillWrapper W(Skill skill, GameObject source, GameObject target){

      SkillWrapper e = new SkillWrapper ();
      e.skill = skill;
      e.source = source;
      e.target = target;
      return e;

    }
  }
}

