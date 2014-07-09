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
    private int renderQueueIndex = 10000;

  	// Use this for initialization
  	void Start () {
      renderQueueIndex = Global.GetTipsPanelRenderQueueIndex();
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
      mPanel.startingRenderQueue = renderQueueIndex;
      this.msgLabel.text = mMsg;

      int width = this.msgLabel.width + 30;
      int height = this.msgLabel.height + 30;
      this.background.width = width;
      this.background.height = height;
    }

  }
}