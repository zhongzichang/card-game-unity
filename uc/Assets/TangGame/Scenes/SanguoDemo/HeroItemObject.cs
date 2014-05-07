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

    private HeroItemData data;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public void Update(HeroItemData data){
      this.data = data;
      icon.spriteName = GetIconName(data);
      iconFrame.spriteName = GetIconFrameName(data);
      level.text = data.level.ToString();
      stars.maxPerLine = data.stars;
      stars.Reposition ();
    }

    public HeroItemData GetData(){
      return data;
    }

    public void Toggle(){
      Debug.Log ("Toggle");
      tick.SetActive (!tick.activeSelf);
    }

    public void Hide(){
      Debug.Log ("Hide");
      gameObject.SetActive (!gameObject.activeSelf);
    }

    private string GetIconName(HeroItemData data){
      return data.id;
    }

    private string GetIconFrameName(HeroItemData data){
      return string.Format("hero_icon_frame_{0}", data.rank);
    }
  }
}