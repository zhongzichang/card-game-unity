using UnityEngine;
using System.Collections;

namespace Test{
  public class ClientTest : MonoBehaviour {

    private LoginService loginService = new LoginService ();
    private HeroService heroService = new HeroService ();

    void OnGUI(){
      if (GUILayout.Button ("login")) {
        loginService.sendLoginData("user", "pass", loginResponseHandler);
      }
      if (GUILayout.Button ("getHeroes")) {
        loginService.getHeroes (getHeroesResponseHandler);
      }
      if (GUILayout.Button ("equipItem")) {
        heroService.equipItem("", "", equipItemResponseHandler);
      }
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
