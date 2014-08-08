using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using TangGame.Net;

namespace Restful
{

  public class HeroResult
  {
    public HeroNetItem[] data;
  }

  public class HeroNetItem
  {
    public string id;

    public HeroNet Data {
      get { 
        HeroNet hero = new HeroNet ();
        hero.id = int.Parse (this.id);
        return hero;
      }
    }
  }

  public class EquipItemResult{
    public bool ok;
  }

  public class rankrank_colorResult{
    public bool ok;
  }

  public class rankStarResult{
    public bool ok;
  }

  public class rankSkillResult{
    public bool ok;
  }

  public class HeroApi {

    public static void getHeroes (string userId, System.Action<HeroResult> responseHandler)
    {

      string endpoint = "heroList";
      string path = "/hero/" + endpoint + "?userId=" + userId;
      RestApi.Instance.HttpGet (path, RestApi.Handle<HeroResult>(responseHandler));
    }
   
    public static void equipItem(string heroId, string equipId, System.Action<EquipItemResult> responseHandler) {
      System.Action<string> handler = delegate(string jsonData){
        EquipItemResult result = JsonConvert.DeserializeObject<EquipItemResult> (jsonData);
        responseHandler (result);
      };
      string path = "/equipItem";
      RestApiParam param = new RestApiParam();
      param.AddField("heroId", heroId);
      param.AddField("equipId", equipId);
      RestApi.Instance.HttpPost (path, param, handler);
    }

    public static void rankrank_color (string heroId, System.Action<rankrank_colorResult> responseHandler){ 
      System.Action<string> handler = delegate(string jsonData){
        rankrank_colorResult result = JsonConvert.DeserializeObject<rankrank_colorResult> (jsonData);
        responseHandler (result);
      };
      string path = "/rankrank_color";
      RestApiParam param = new RestApiParam();
      param.AddField("heroId", heroId);
      RestApi.Instance.HttpPost (path, param, handler);
    }

    public static void rankStar(string heroId, System.Action<rankStarResult> responseHandler){ 
      System.Action<string> handler = delegate(string jsonData){
        rankStarResult result = JsonConvert.DeserializeObject<rankStarResult> (jsonData);
        responseHandler (result);
      };
      string path = "/rankStar";
      RestApiParam param = new RestApiParam();
      param.AddField("heroId", heroId);
      RestApi.Instance.HttpPost (path, param, handler);
    }

    public static void rankSkill(string heroId, string skillId, System.Action<rankSkillResult> responseHandler) {
      System.Action<string> handler = delegate(string jsonData){
        rankSkillResult result = JsonConvert.DeserializeObject<rankSkillResult> (jsonData);
        responseHandler (result);
      };
      string path = "/rankSkill";
      RestApiParam param = new RestApiParam();
      param.AddField("heroId", heroId);
      param.AddField("skillId", skillId);
      RestApi.Instance.HttpPost (path, param, handler);
    }
  }
}