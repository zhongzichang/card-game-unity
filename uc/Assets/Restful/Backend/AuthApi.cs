using UnityEngine;
using System.Collections;

using Newtonsoft.Json;
using TangGame.Net;

namespace Restful
{
  public class AuthResult
  {
    public bool logined;
  }

  public class AuthApi {

    public static void Check(System.Action<AuthResult> responseHandler) {

      string path = "/check";

      System.Action<string> handler = delegate(string jsonData){
        AuthResult result = JsonConvert.DeserializeObject<AuthResult>(jsonData);
        responseHandler (result);
      };


      RestApi.Instance.HttpGet (path, handler);
    }

  }
}