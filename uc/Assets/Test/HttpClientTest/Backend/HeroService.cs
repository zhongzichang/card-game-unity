using UnityEngine;
using System.Collections;

using JsonFx.Json;

namespace Test{
  public class EquipItemResult{
    public int id;
    public string name;
  }

  public class HeroService {

    private JsonReader reader = new JsonReader ();
    private JsonWriter writer = new JsonWriter();

    public void equipItem(string heroId, string equipId, System.Action<EquipItemResult> responseHandler) {
      string endpoint = "equipItem";
      System.Action<string> handler = delegate(string jsonData){
        EquipItemResult result = reader.Read<EquipItemResult> (jsonData);
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