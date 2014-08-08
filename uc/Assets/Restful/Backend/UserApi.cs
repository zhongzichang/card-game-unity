using UnityEngine;
using System.Collections;
using TangGame.Net;

namespace Restful
{

  public class UserApi
  {

    public static void GetMe (System.Action<UserNet> responseHandler)
    {
      string path = "/users/me";
      RestApi.Instance.HttpGet (path, RestApi.Handle<UserNet>(responseHandler));
    }


  }
}
