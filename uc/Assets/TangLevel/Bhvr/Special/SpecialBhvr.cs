using System;
using UnityEngine;

namespace TangLevel
{
  public abstract class SpecialBhvr : MonoBehaviour
  {
    public GameObject source;
    public GameObject target;
    public Skill skill;


    public abstract void Play();
  }
}