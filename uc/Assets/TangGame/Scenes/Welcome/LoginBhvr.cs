using UnityEngine;
using System.Collections;
using Restful;
using TangGame.Net;
using TangGame.UI;

namespace TangGame
{

  public class LoginBhvr : MonoBehaviour
  {

    public GameObject serverGroup;
    public GameObject loginGroup;
    public UIEventListener loginButton;
    public UIInput usernameInput;
    public UIInput passwordInput;

    // Use this for initialization
    void Start ()
    {
      // 检查用户是否已经登录
      AuthApi.Check (
        delegate(AuthResult result) {
          if (result.logined) {
            UserApi.GetMe(meHandle());
          } else {
            serverGroup.SetActive (false);
            loginGroup.SetActive (true);
          }
        }
      );

      loginButton.onClick += OnLogin;
    }

    private void OnLogin(GameObject loginButton){

      string username = usernameInput.value;
      string password = passwordInput.value;

      LoginApi.login (username, password, delegate(LoginResult result) {
        if( result.success ){
          UserApi.GetMe(meHandle());
        } else {
          Debug.Log(result.message);
        }
      });

    }

    private System.Action<UserNet> meHandle() {
      return delegate(UserNet user) {
        Debug.Log ("Hello " + user.nickname + " !");
        serverGroup.SetActive (true);
        loginGroup.SetActive (false);

        Account.instance.SetData(user);

      };
    }

  }
}