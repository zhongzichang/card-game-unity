using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 修改头像面板
  /// </summary>
  public class ChangeAvatarPanel : ViewPanel {
    public const string NAME = "ChangeAvatarPanel";

    public GameObject avatarGroup;
    public GameObject changeAvatarItem;
    public UILabel titleLabel;
    public UIEventListener maskBtn;


    void Start(){
      changeAvatarItem.SetActive(false);
      maskBtn.onClick += MaskBtnClickHandler;
      UpdateData();
    }

    protected override void UpdateData (){
      Vector3 temp = changeAvatarItem.transform.localPosition;
      int col = 5;
      for(int i = 0; i < 30; i++){
        GameObject go = UIUtils.Duplicate(changeAvatarItem, avatarGroup);
        go.transform.localPosition = temp + new Vector3((i % col) * 150, -(int)(i / 5) * 150, 0);
        UIEventListener.Get(go).onClick += ItemClickHandler;
      }
    }

    private void ItemClickHandler(GameObject go){

    }

    private void MaskBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }


  }
}

