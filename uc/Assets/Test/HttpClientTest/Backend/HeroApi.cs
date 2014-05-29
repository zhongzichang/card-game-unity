using UnityEngine;
using System.Collections;

using Newtonsoft.Json;

namespace ClientDemoTest
{
  public class EquipItemResult{
    public bool ok;
  }

  public class UpgradeRankResult{
    public bool ok;
  }

  public class UpgradeStarResult{
    public bool ok;
  }

  public class UpgradeSkillResult{
    public bool ok;
  }

  public class HeroApi {
   
    public void equipItem(string heroId, string equipId, System.Action<EquipItemResult> responseHandler) {
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

    public void upgradeRank (string heroId, System.Action<UpgradeRankResult> responseHandler){ 
      System.Action<string> handler = delegate(string jsonData){
        UpgradeRankResult result = JsonConvert.DeserializeObject<UpgradeRankResult> (jsonData);
        responseHandler (result);
      };
      string path = "/upgradeRank";
      RestApiParam param = new RestApiParam();
      param.AddField("heroId", heroId);
      RestApi.Instance.HttpPost (path, param, handler);
    }

    public void upgradeStar(string heroId, System.Action<UpgradeStarResult> responseHandler){ 
      System.Action<string> handler = delegate(string jsonData){
        UpgradeStarResult result = JsonConvert.DeserializeObject<UpgradeStarResult> (jsonData);
        responseHandler (result);
      };
      string path = "/upgradeStar";
      RestApiParam param = new RestApiParam();
      param.AddField("heroId", heroId);
      RestApi.Instance.HttpPost (path, param, handler);
    }

    public void upgradeSkill(string heroId, string skillId, System.Action<UpgradeSkillResult> responseHandler) {
      System.Action<string> handler = delegate(string jsonData){
        UpgradeSkillResult result = JsonConvert.DeserializeObject<UpgradeSkillResult> (jsonData);
        responseHandler (result);
      };
      string path = "/upgradeSkill";
      RestApiParam param = new RestApiParam();
      param.AddField("heroId", heroId);
      param.AddField("skillId", skillId);
      RestApi.Instance.HttpPost (path, param, handler);
    }
  }
}