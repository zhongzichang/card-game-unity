using UnityEngine;
using System.Collections;

namespace ClientDemoTest{

  public class ClientDemo : MonoBehaviour {

    private LoginService loginService = new LoginService ();
    private HeroService heroService = new HeroService ();
    private HttpsTestService httpsTestService = new HttpsTestService();

    private string msg = "...";

    void OnGUI(){

      GUILayout.Label (msg);
      if (GUILayout.Button ("login")) {
        loginService.sendLoginData("user", "pass", loginResponseHandler);
      }
      if (GUILayout.Button ("getHeroes")) {
        loginService.getHeroes (getHeroesResponseHandler);
      }
      if (GUILayout.Button ("equipItem")) {
        heroService.equipItem("", "", equipItemResponseHandler);
      }
      if (GUILayout.Button ("httpsGithub")) {
        msg = "";
        httpsTestService.httpsGithub (githubResponseHandler);
      }
      if (GUILayout.Button ("httpsLocal")) {
        msg = "";
        httpsTestService.httpsLocal (githubResponseHandler);
      }
      if (GUILayout.Button ("httpLocal")) {
        msg = "";
        httpsTestService.httpLocal (githubResponseHandler);
      }
  	}

    private void githubResponseHandler (GithubApiResult result)
    {
      msg = result.user_url;
      Debug.Log (result.user_url);
    }

    private void loginResponseHandler (LoginResult result)
    {
      Debug.Log (result.name);
    }

    private void getHeroesResponseHandler (HeroResult result)
    {
      HeroItem[] heroes = (HeroItem[])result.data;
      foreach (HeroItem hero in heroes) {
        Debug.Log (hero.name);
        foreach (SkillItem skill in hero.skillLevel) {
          Debug.Log(skill.name);
        }
      }
    }

    private void equipItemResponseHandler (EquipItemResult result)
    {
      Debug.Log (result.name);
    }

  }
}
