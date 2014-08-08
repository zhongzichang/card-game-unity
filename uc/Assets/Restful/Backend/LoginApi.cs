using UnityEngine;
using System.Collections;

using Newtonsoft.Json;
using TangGame.Net;

namespace Restful
{
  public class LoginResult
  {
    public bool success;
    public string message;
  }

  public class LoginApi {

    public static void login(string username, string password, System.Action<LoginResult> responseHandler) {
      string endpoint = "echo";
      string path = "/login";
      RestApiParam param = new RestApiParam ();
      param.AddField ("username", username);
      param.AddField ("password", password);
      RestApi.Instance.HttpPost (path, param, RestApi.Handle<LoginResult>(responseHandler));
    }

  }
}