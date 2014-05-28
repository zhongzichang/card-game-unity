using UnityEngine;
using System.Collections;

//using Pathfinding.Serialization.JsonFx;
using Newtonsoft.Json;

namespace ClientDemoTest
{
  public class EquipItemResult{
    public int id;
    public string name;
  }

  public class HeroService {

    public void equipItem(string heroId, string equipId, System.Action<EquipItemResult> responseHandler) {
      string endpoint = "equipItem";
      System.Action<string> handler = delegate(string jsonData){
  //      EquipItemResult result = JsonReader.Deserialize<EquipItemResult> (jsonData);
        EquipItemResult result = JsonConvert.DeserializeObject<EquipItemResult> (jsonData);
        responseHandler (result);
      };
      RestApi.Instance.HttpPost (endpoint, MakeHeroRequest (), handler);
    }

    private string MakeHeroRequest(){
  //    LoginRequest request = new LoginRequest ();
  //    request.username = username;
  //    request.password = password;
  //
  //    return writer.Write(request);
      return "";
    }
  }
}