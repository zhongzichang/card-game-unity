using UnityEngine;
using System.Collections;
using Restful;

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
            serverGroup.SetActive (true);
            loginGroup.SetActive (false);
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
          serverGroup.SetActive (true);
          loginGroup.SetActive (false);
        } else {
          Debug.Log(result.message);
        }
      });

    }

  }
}