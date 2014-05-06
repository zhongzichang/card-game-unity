using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemObject : MonoBehaviour{

    public UISprite icon;
    public UISprite iconFrame;
    public UILabel level;
    public UIGrid stars;
    public GameObject tick;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void Update(HeroItemData data){
      this.name = data.icon;
      icon.spriteName = data.icon;
      iconFrame.spriteName = data.iconFrame;
      level.text = data.level;
      stars.maxPerLine = data.stars;
      stars.Reposition ();
    }

    public void Toggle(){
      tick.SetActive (!tick.activeSelf);
    }

    public void Hide(){
      gameObject.SetActive (!gameObject.activeSelf);
    }

//    void OnGUI(){
//      if (GUILayout.Button ("UpdateHeroItem")) {
//        HeroItemData data = new HeroItemData ();
//        data.icon = "BatRider";
//        data.iconFrame = "hero_icon_frame_2";
//        data.level = "56";
//        data.stars = 3;
//        Bind (data);
//      }
//      if (GUILayout.Button ("ToggleTick")) {
//        ToggleTick ();
//      }
//    }

  }
}