﻿using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class SpecialTest : MonoBehaviour
  {

    public GameObject source;
    public GameObject target;

    private string spriteName = "Sprite_binghua";


    // Use this for initialization
    void Start ()
    {

      GobjManager.RaiseLoadEvent += OnBingHuaLoad;
      GobjManager.LazyLoad (spriteName);
	
    }

    private void OnBingHuaLoad (object sender, LoadResultEventArgs args)
    {
      if (args.Name == spriteName) {
        GobjManager.RaiseLoadEvent -= OnBingHuaLoad;
        if (args.Success) {
          GameObject gobj = GobjManager.FetchUnused (spriteName);

          SkillSpecialBhvr bhvr = gobj.GetComponent<SkillSpecialBhvr> ();
          if (bhvr != null) {
            //bhvr.source = source;
            //bhvr.target = target;
            //bhvr.skill = Mocker.MockBingHuaSkill ();
          }
          gobj.SetActive (true);
        }
      }
    }
  }
}