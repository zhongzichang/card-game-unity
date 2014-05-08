using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class BattleHeroPanel : MonoBehaviour {

    public SelectedHeroGrid selectedGrid;

    private void OnItemClicked(GameObject obj){
      HeroItemObject hero = (HeroItemObject)obj.GetComponent<HeroItemObject> (); 
      Debug.Log("OnItemClicked." + hero.HeroId);
    }
  }
}