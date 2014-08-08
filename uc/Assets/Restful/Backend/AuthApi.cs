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

  public class AuthApi
  {

    public static void Check (System.Action<AuthResult> responseHandler)
    {
      string path = "/check";
      RestApi.Instance.HttpGet (path, RestApi.Handle<AuthResult> (responseHandler));
    }

  }
}