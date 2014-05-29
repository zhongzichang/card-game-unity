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

    public void Refresh(RewardItemData data){
      item = data;
      icon.spriteName = data.id;
      frame.spriteName = GetIconFrameName (data);
    }

    private string GetIconFrameName(RewardItemData data){
      // 装备
      return "item_frame_" + data.rank_color.ToString();
//      if (data.type == 1) {
//        if (data.rank_color == 1) {
//          return "equip_frame_white";
//        } else if (data.rank_color == 2) {
//          return "equip_frame_green";
//        } else if (data.rank_color == 3) {
//          return "equip_frame_blue";
//        } else if (data.rank_color == 4) {
//          return "equip_frame_purple";
//        }
//        return "equip_frame_orange";
//      }else{
//        if (data.rank_color == 1) {
//          return "fragment_frame_white";
//        }else if (data.rank_color == 2) {
//          return "fragment_frame_green";
//        }else if (data.rank_color == 3) {
//          return "fragment_frame_blue";
//        }else if (data.rank_color == 4) {
//          return "fragment_frame_purple";
//        }
//        return "fragment_frame_orange";
//      }
    }

    private void OnItemClicked(GameObject obj){
      Debug.Log ("OnItemClicked" + item.name );
    }
  }
}