using UnityEngine;
using System.Collections;

using Newtonsoft.Json;
using TangGame.Net;

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
    public HeroNet[] data;
  }

//  public class HeroItem
//  {
//    public string id;
//    public string name;
//    public string configId;
//    public string level;
//    public string exp;
//    public string upgrade;
//    public string evolve;
//    public string skillCount;
//    public string lastUpSkillTime;
//    public SkillItem[] skillLevel;
//  }
//
//  public class SkillItem{
//    public string id;
//    public string name;
//  }

  public class LoginService {


    public void login(string username, string password, System.Action<LoginResult> responseHandler) {
      string endpoint = "echo";
      System.Action<string> handler = delegate(string jsonData){
        LoginResult result = JsonConvert.DeserializeObject<LoginResult>(jsonData);
        responseHandler (result);
      };
      string path = "/login";
      RestApi.Instance.HttpPost (path, MakeLoginRequest (username, password), handler);
    }

    public void getHeroes(string userId, System.Action<HeroResult> responseHandler){
      string endpoint = "heroList";
      System.Action<string> handler = delegate(string jsonData){
        HeroResult result = JsonConvert.DeserializeObject<HeroResult> (jsonData);
        responseHandler (result);
      };

      // "/hero/heroList?userId=1"
      string path = "/hero/" + endpoint + "?userId=" + userId;
      RestApi.Instance.HttpGet (endpoint, handler);
    }

    private string MakeLoginRequest(string username, string password){
      LoginRequest request = new LoginRequest ();
      request.username = username;
      request.password = password;

      return JsonConvert.SerializeObject (request);
    }

  }
}