using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  public class ShopSellPanel : ViewPanel {
    public const string NAME = "ShopSellPanel";

    public UILabel titleLabel;
    public UIEventListener closeBtn;
    public UISprite propsGroup;
    public SimplePropsNameItem simplePropsNameItem;
    public GameObject bottomGroup;
    public UILabel getLabel;
    public UILabel moneyLabel;
    public UIEventListener okBtn;
    public UILabel okBtnLabel;
    public UISprite background;

    void Start(){
      simplePropsNameItem.gameObject.SetActive(false);
      closeBtn.onClick += CloseBtnClickHandler;
      okBtn.onClick += OkBtnClickHandler;
      this.started = true;
      UpdateData();
    }

    public object param{
      get{return null;}
      set{
        UpdateData();
      }
    }

    protected override void UpdateData (){
      if(!this.started){return;}
      int count = 5;
      int height = 64 * Mathf.CeilToInt(count / 2f) + 20;
      propsGroup.height = height;

      Vector3 tempPosition = simplePropsNameItem.transform.localPosition;
      for(int i = 0; i < count; i++){
        GameObject go = UIUtils.Duplicate(simplePropsNameItem.gameObject, propsGroup.transform);
        go.transform.localPosition = tempPosition + new Vector3(250 * (i % 2), -(int)(i / 2) * 64, 0);
      }


      UIUtils.SetY(bottomGroup, -82 - height - 35);

      background.height = 82 + height + 155;
      UIUtils.SetY(background.gameObject, background.height / 2);
    }

    private void CloseBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }

    private void OkBtnClickHandler(GameObject go){
      
    }

  }
}

