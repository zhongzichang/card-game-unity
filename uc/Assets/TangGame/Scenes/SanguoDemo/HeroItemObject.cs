using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemObject : MonoBehaviour{

    public Transform id;
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
      this.name = GetSortingName(data);
      id.name = data.id;
      icon.spriteName = GetIconName(data);
      iconFrame.spriteName = GetIconFrameName(data);
      level.text = data.level.ToString();
      stars.maxPerLine = data.stars;
      stars.Reposition ();
    }

    public void Toggle(){
      Debug.Log ("Toggle");
      tick.SetActive (!tick.activeSelf);
    }

    public void Hide(){
      Debug.Log ("Hide");
      gameObject.SetActive (!gameObject.activeSelf);
    }

    // This sorting name is for hero list
    private string GetSortingName(HeroItemData data){
      return string.Format("{0:D3}-{1:D2}-{2:D2}", data.level, data.rank, data.stars);
    }

    private string GetIconName(HeroItemData data){
      return data.id;
    }

    private string GetIconFrameName(HeroItemData data){
      return string.Format("hero_icon_frame_{0}", data.rank);
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