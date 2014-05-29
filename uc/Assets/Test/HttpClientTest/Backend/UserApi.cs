using UnityEngine;
using System.Collections;

using TangGame.Net;
using Newtonsoft.Json;

namespace ClientDemoTest
{
  public class HeroResult
  {
    public HeroNetItem[] data;
//    public HeroNet[] item;
  }

  public class HeroNetItem{
    public string id;
//    public int configId;
//    public int level;
//    public long exp;
//    public int rank;
//    public int evolve;
//    public int skillCount;
//    public long lastUpSkillTime;
//    public int[] skillLevel;
//    public EquipNet[] equipList;

    public HeroNet Data{
      get{ 
        HeroNet hero = new HeroNet ();
        hero.id = int.Parse (this.id);
        return hero;
      }
    }
  }

  public class UserApi
	{
    public void getHeroes(string userId, System.Action<HeroResult> responseHandler){
      System.Action<string> handler = delegate(string jsonData){
        HeroResult result = JsonConvert.DeserializeObject<HeroResult> (jsonData);
        responseHandler (result);
      };

      // "/hero/heroList?userId=1"
      string endpoint = "heroList";
      string path = "/hero/" + endpoint + "?userId=" + userId;
      RestApi.Instance.HttpGet (path, handler);
    }
	}
}
