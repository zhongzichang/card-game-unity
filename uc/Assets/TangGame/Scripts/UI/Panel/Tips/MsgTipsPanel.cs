using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 信息提示面板
  /// </summary>
  public class MsgTipsPanel : ViewPanel {

    public UISprite background;
    public UILabel msgLabel;

    private string mMsg;

  	// Use this for initialization
  	void Start () {
      this.started = true;
      this.UpdateMsg();
  	}

    public string msg{
      get{return this.mMsg;}
      set{this.mMsg = value;UpdateMsg();}
    }

    private void UpdateMsg(){
      if(!this.started){return;}
      UIPanel mPanel = this.GetComponent<UIPanel>();
      mPanel.renderQueue = UIPanel.RenderQueue.StartAt;
      mPanel.startingRenderQueue = 10002;
      this.msgLabel.text = mMsg;

      int width = this.msgLabel.width + 30;
      int height = this.msgLabel.height + 30;
      this.background.width = width;
      this.background.height = height;
    }

  }
}