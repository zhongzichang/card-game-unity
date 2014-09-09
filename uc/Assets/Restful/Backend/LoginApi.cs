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

    public static void login(LoginRequest request, System.Action<UserNet> responseHandler) {
      string path = "/auth/login";
      RestApi.Instance.HttpPost (path, request, RestApi.Handle<UserNet>(responseHandler));
    }

  }
}