using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using JsonFx.Json;

public class HeroResponse
{
  public HeroResult[] data;
}

public class HeroResult
{
  public int id;
  public string name;
  /// <summary>
  /// The config identifier.
  /// </summary>
  public int configId;
  /// <summary>
  /// 英雄等级
  /// </summary>
  public int level;
  /// <summary>
  /// 英雄经验
  /// </summary>
  public long exp;
  /// <summary>
  /// 英雄品质
  /// </summary>
  public short upgrade;
  /// <summary>
  /// 英雄星级
  /// </summary>
  public short evolve;
  /// <summary>
  /// 剩余的技能点
  /// </summary>
  public short skillCount;
  /// <summary>
  /// 最后一次升级技能的时间
  /// </summary>
  public long lastUpSkillTime;
  /// <summary>
  /// 英雄技能等级
  /// </summary>
  public string skillLevel;

  public int[] getSkills(){
    string[] skillStrs = this.skillLevel.Split (',');
    int [] skills = new int[skillStrs.Length];
    for (int i = 0; i < skillStrs.Length; i++) {
      skills [i] = int.Parse(skillStrs [i]);
    }
    return skills;
  }
  /// <summary>
  /// The equip.
  /// </summary>
  //	public string equip;
}

public class Test : MonoBehaviour
{
  private JsonReader reader = new JsonReader ();

  void OnGUI ()
  {
    if (GUILayout.Button ("reqeust")) {
      HTTP.Request http = new HTTP.Request ("get", "http://localhost:4004/hero/heroList?userId=1");
      http.AddHeader ("Accept", "application/json");
      http.Send (HandleResponse);
    }
  }

  void HandleResponse (HTTP.Request request)
  {
    HeroResponse response = reader.Read<HeroResponse> (request.response.Text);
    HeroResult[] heroes = response.data;
    foreach (HeroResult hero in heroes) {
      int[] skillIds = hero.getSkills ();
      foreach(int skillId in skillIds){
        Debug.Log (skillId);
      }
    }

  }
}
