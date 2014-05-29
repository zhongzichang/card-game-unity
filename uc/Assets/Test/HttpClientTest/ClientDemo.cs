using UnityEngine;
using System.Collections;

using TangGame.Net;

namespace ClientDemoTest{

  public class ClientDemo : MonoBehaviour {

    private LoginApi loginApi = new LoginApi ();
    private UserApi userApi = new UserApi ();
    private HeroApi heroApi = new HeroApi ();
    private TestApi testApi = new TestApi();

    private string msg;

    void Start(){
      msg = "click these function:";
      RestApi.Instance.Host = "http://localhost:4004";
    }

    void OnGUI(){

      GUILayout.Label (msg);
      if (GUILayout.Button ("login")) {
        loginApi.login("user", "pass", loginResponseHandler);
      }
      if (GUILayout.Button ("getHeroes")) {
        string userId = "1";
        userApi.getHeroes (userId, getHeroesResponseHandler);
      }
      if (GUILayout.Button ("equipItem")) {
        System.Action<EquipItemResult> equipItemResponseHandler = delegate(EquipItemResult result){
          Debug.Log(result.ok);
        };
        heroApi.equipItem("heroId", "equipId", equipItemResponseHandler);
      }
      if (GUILayout.Button ("rankrank_color")) {
        System.Action<rankrank_colorResult> rankrank_colorResponseHandler = delegate(rankrank_colorResult result){
          Debug.Log(result.ok);
        };
        heroApi.rankrank_color("heroId", rankrank_colorResponseHandler);
      }
      if (GUILayout.Button ("rankStar")) {
        System.Action<rankStarResult> rankStarResponseHandler = delegate(rankStarResult result){
          Debug.Log(result.ok);
        };
        heroApi.rankStar("heroId", rankStarResponseHandler);
      }
      if (GUILayout.Button ("rankSkill")) {
        System.Action<rankSkillResult> rankSkillResponseHandler = delegate(rankSkillResult result){
          Debug.Log(result.ok);
        };
        heroApi.rankSkill("heroId", "skillId", rankSkillResponseHandler);
      }

      if (GUILayout.Button ("httpsGithub")) {
        msg = "";
        testApi.httpsGithub (githubResponseHandler);
      }
      if (GUILayout.Button ("httpsLocal")) {
        msg = "";
        testApi.httpsLocal (githubResponseHandler);
      }
      if (GUILayout.Button ("httpLocal")) {
        msg = "";
        testApi.httpLocal (githubResponseHandler);
      }
  	}

    private void githubResponseHandler (GithubApiResult result)
    {
      msg = result.user_url;
      Debug.Log (result.user_url);
    }

    private void loginResponseHandler (LoginResult result)
    {
      Debug.Log (result.userId);
    }

    private void getHeroesResponseHandler (HeroResult result)
    {
      HeroNetItem[] heroes = (HeroNetItem[])result.data;
      foreach (HeroNetItem hero in heroes) {
        Debug.Log (hero.id);
      }
    }

  }
}
