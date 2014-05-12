using UnityEngine;
using System.Collections;


namespace TangGame.UI
{
  public class RewardItemObject : MonoBehaviour {
    public UISprite frame;
    public UISprite icon;

    private RewardItemData item;

    public void Start(){
      UIEventListener.Get (icon.gameObject).onClick += OnItemClicked;
    }

    public void Refresh(RewardItemData item){
      icon.spriteName = "374";
      frame.spriteName = "equip_frame_orange";
    }

    private void OnItemClicked(GameObject obj){
      Debug.Log ("OnItemClicked" + item.name );
    }
  }
}