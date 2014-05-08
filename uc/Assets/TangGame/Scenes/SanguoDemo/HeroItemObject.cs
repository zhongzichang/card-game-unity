using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemObject : MonoBehaviour{

    public UISprite icon;
    public UISprite iconFrame;
    public UILabel level;
    public UIGrid stars;
    public UISprite mp;
    public UISprite hp;
    public GameObject bucket;
    public GameObject tick;
    public UISprite cast;

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
      mp.fillAmount = data.mp / data.mpMax;
      hp.fillAmount = data.hp / data.hpMax;
      stars.maxPerLine = data.stars;
      stars.Reposition ();
    }

    public void UpdateMp(int mpValue){
      mp.fillAmount = mpValue *100 / HeroData.mpMax;
      mp.gameObject.SetActive (true);
      if (HeroData.mpMax == mpValue) {
        cast.gameObject.SetActive (true);
      } else {
        cast.gameObject.SetActive (false);
      }
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