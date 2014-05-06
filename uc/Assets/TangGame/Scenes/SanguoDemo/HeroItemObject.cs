using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemObject : MonoBehaviour{

    public UISprite icon;
    public UISprite iconFrame;
    public UILabel level;
    public UIGrid stars;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void UpdateHeroItem(HeroItemData data){
      icon.spriteName = data.icon;
      iconFrame.spriteName = data.iconFrame;
      level.text = data.level;
      stars.maxPerLine = data.stars;
      stars.Reposition ();
    }

//    void OnGUI(){
//      if (GUILayout.Button ("UpdateHeroItem")) {
//        HeroItemData data = new HeroItemData ();
//        data.icon = "BatRider";
//        data.iconFrame = "hero_icon_frame_2";
//        data.level = "56";
//        data.stars = 3;
//        UpdateHeroItem (data);
//      }
//      if (GUILayout.Button ("UpdateHeroItem")) {
//        HeroItemData data = new HeroItemData ();
//        data.icon = "Axe";
//        data.iconFrame = "hero_icon_frame_1";
//        data.level = "33";
//        data.stars = 4;
//        UpdateHeroItem (data);
//      }
//    }
  }
}