using UnityEngine;
using System.Collections;

using Newtonsoft.Json;
using TangGame.Net;

namespace ClientDemoTest
{
  public class AuthResult
  {
    public bool logined;
  }

  public class AuthApi {

    public static void Check(System.Action<LoginResult> responseHandler) {

      string path = "/check";

      System.Action<string> handler = delegate(string jsonData){
        LoginResult result = JsonConvert.DeserializeObject<LoginResult>(jsonData);
        responseHandler (result);
      };


      RestApi.Instance.HttpGet (path, handler);
    }

  }
}