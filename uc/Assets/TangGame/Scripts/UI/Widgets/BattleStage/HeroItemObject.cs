using UnityEngine;
using System.Collections;
using TangGame.Xml;

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

    private HeroItemData mData;
    public HeroItemData data{
      get { return mData; } 
      set { mData=value; } 
    }

    private bool showLevel = true;
    public bool ShowLevel {
      get { return showLevel; }
      set { showLevel = value; }
    }

    public string HeroId{
      get { return mData.id; } 
    }

    public void Refresh(HeroItemData data){
      this.mData = data;
      HeroData heroData = HeroCache.instance.GetHeroData(int.Parse(this.mData.id));
      if(heroData != null){
        icon.spriteName = heroData.avatar;
      }else{
        icon.spriteName = "";
      }
      iconFrame.spriteName = Global.GetHeroIconFrame(data.rank_color);
      if (showLevel) {
        level.text = data.level.ToString ();
      } else {
        level.enabled = false;
      }
      mp.fillAmount = data.mp / data.mpMax;
      hp.fillAmount = data.hp / data.hpMax;
      stars.maxPerLine = data.stars;
      tick.SetActive (false);

      stars.Reposition ();
    }

    public void UpdateMp(int mpValue){
      mp.fillAmount = mpValue *100 / data.mpMax;
      mp.gameObject.SetActive (true);
      if (data.mpMax == mpValue) {
        cast.gameObject.SetActive (true);
      } else {
        cast.gameObject.SetActive (false);
      }
    }

    public void ToggleActive(){
      gameObject.SetActive (!gameObject.activeSelf);
    }

    public void ToggleTick(){
      bool toggled = tick.activeSelf;
      if (toggled) {
        icon.color = ColorUtil.WHITE;
        iconFrame.color = ColorUtil.WHITE;
      } else {
        icon.color = ColorUtil.GRAY;
        iconFrame.color = ColorUtil.GRAY;
      }
      tick.SetActive (!toggled);
    }

  }
}