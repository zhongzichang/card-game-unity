using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemObject : MonoBehaviour{

    public UISprite icon;
    public UISprite iconFrame;
    public UILabel level;
    public UIGrid stars;
    public GameObject mp;
    public GameObject hp;
    public GameObject bucket;
    public GameObject tick;

    private HeroItemData heroData;
    public HeroItemData HeroData{
      get { return heroData; } 
      set { heroData=value; } 
    }
    public string HeroId{
      get { return heroData.id; } 
    }

    public void Refresh(HeroItemData data){
      HeroData = data;
      icon.spriteName = GetIconName(data);
      iconFrame.spriteName = GetIconFrameName(data);
      level.text = data.level.ToString();
      stars.maxPerLine = data.stars;
      stars.Reposition ();
    }

    public void ToggleActive(){
      gameObject.SetActive (!gameObject.activeSelf);
    }

    public void ToggleTick(){
      tick.SetActive (!tick.activeSelf);
    }

    private string GetIconName(HeroItemData data){
      return data.id;
    }

    private string GetIconFrameName(HeroItemData data){
      return string.Format("hero_icon_frame_{0}", data.rank);
    }
  }
}