using UnityEngine;
using System.Collections;

//using Pathfinding.Serialization.JsonFx;
using Newtonsoft.Json;

namespace ClientDemoTest
{
  public class LoginRequest{
    public string username;
    public string password;
  }

  public class LoginResult
  {
    public int id;
    public string name;
  }

  public class HeroResult
  {
    public HeroItem[] data;
  }

  public class HeroItem
  {
    public string id;
    public string name;
    public string configId;
    public string level;
    public string exp;
    public string upgrade;
    public string evolve;
    public string skillCount;
    public string lastUpSkillTime;
    public SkillItem[] skillLevel;
  }

  public class SkillItem{
    public string id;
    public string name;
  }

  public class LoginService {


    public void sendLoginData(string username, string password, System.Action<LoginResult> responseHandler) {
      string endpoint = "echo";
      System.Action<string> handler = delegate(string jsonData){
  //      LoginResult result = JsonReader.Deserialize<LoginResult> (jsonData);
        LoginResult result = JsonConvert.DeserializeObject<LoginResult>(jsonData);
        responseHandler (result);
      };
      RestApi.Instance.HttpPost (endpoint, MakeLoginRequest (username, password), handler);
    }

    public void getHeroes(System.Action<HeroResult> responseHandler){
      string endpoint = "get_heroes";
      System.Action<string> handler = delegate(string jsonData){
        HeroResult result = JsonConvert.DeserializeObject<HeroResult> (jsonData);
  //      HeroResult result = JsonReader.Deserialize<HeroResult> (jsonData);
        responseHandler (result);
      };
      RestApi.Instance.HttpGet (endpoint, handler);
    }

    private string MakeLoginRequest(string username, string password){
      LoginRequest request = new LoginRequest ();
      request.username = username;
      request.password = password;

      return JsonConvert.SerializeObject (request);
  //    return JsonWriter.Serialize(request);
    }

  }
}