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

    public void OnPreloadCompleted(){
      AuthCheck ();
    }

    public void AuthCheck(){

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

      LoginApi.login (new LoginRequest(username, password), delegate(UserNet user) {
        if( user != null ){
          Debug.Log ("Hello " + user.nickname + " !");
          serverGroup.SetActive (true);
          loginGroup.SetActive (false);
          Account.instance.SetData(user);
        } else {
          Debug.Log(user);
        }
      });

    }

    private System.Action<UserNet> meHandle() {
      return delegate(UserNet user) {
        serverGroup.SetActive (true);
        loginGroup.SetActive (false);
        Account.instance.SetData(user);

      };
    }

  }
}