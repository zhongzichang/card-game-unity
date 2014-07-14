using UnityEngine;
using System.Collections;

namespace TangGame.UI{

  public delegate void AlertVoidDelegate (GameObject go);

  /// <summary>
  /// 提示框
  /// </summary>
  public class AlertPanel : MonoBehaviour {

    public AlertVoidDelegate okDelegate;
    public AlertVoidDelegate cancelDelegate;

    public UILabel cancelBtnLabel;
    public UILabel okBtnLabel;
    public UIEventListener okBtn;
    public UIEventListener cancelBtn;
    public UILabel msgLabel;

    private int renderQueueIndex = 10000;

    // Use this for initialization
    void Start () {
      renderQueueIndex = Global.GetTipsPanelRenderQueueIndex();
      okBtn.onClick += OkBtnClickHandler;
      cancelBtn.onClick += CancelBtnClickHandler;
    }

    /// 设置提示
    public void SetAlert(string msg){
      UIPanel mPanel = this.GetComponent<UIPanel>();
      mPanel.renderQueue = UIPanel.RenderQueue.StartAt;
      mPanel.startingRenderQueue = renderQueueIndex;
      mPanel.depth = renderQueueIndex;
      this.msgLabel.text = msg;
      if(this.msgLabel.height > 26){
        this.msgLabel.pivot = UIWidget.Pivot.Left;
      }else{
        this.msgLabel.pivot = UIWidget.Pivot.Center;
      }
    }

    private void OkBtnClickHandler(GameObject go){
      if(okDelegate != null){
        okDelegate(go);
      }
    }

    private void CancelBtnClickHandler(GameObject go){
      if(cancelDelegate != null){
        cancelDelegate(go);
      }
    }


  }
}

