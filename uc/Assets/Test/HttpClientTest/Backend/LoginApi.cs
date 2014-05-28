using UnityEngine;
using System.Collections;

using Newtonsoft.Json;
using TangGame.Net;

namespace ClientDemoTest
{
  public class LoginResult
  {
    public string userId;
  }

  public class LoginApi {

    public void login(string username, string password, System.Action<LoginResult> responseHandler) {
      string endpoint = "echo";
      System.Action<string> handler = delegate(string jsonData){
        LoginResult result = JsonConvert.DeserializeObject<LoginResult>(jsonData);
        responseHandler (result);
      };
      string path = "/login";
      RestApiParam param = new RestApiParam ();
      param.AddField ("username", username);
      param.AddField ("password", password);
      RestApi.Instance.HttpPost (path, param, handler);
    }

  }
}