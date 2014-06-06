using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 邮件面板
  /// </summary>
  public class MailPanel : ViewPanel {
    public const string NAME = "MailPanel";

    public UILabel titleLabel;
    public MailItem mailItem;
    public UIScrollView scrollView;
    public UIEventListener closeBtn;

    private object mParam;
    private bool started;
    private List<MailItem> items = new List<MailItem>();

    void Start(){
      closeBtn.onClick += CloseBtnClickHandler;
      titleLabel.text = UIPanelLang.MAILBOX;
      this.started = true;
      this.mailItem.gameObject.SetActive(false);

      MailCache.instance.list.Add(new Mail());
      MailCache.instance.list.Add(new Mail());
      MailCache.instance.list.Add(new Mail());
      MailCache.instance.list.Add(new Mail());
      MailCache.instance.list.Add(new Mail());
      MailCache.instance.list.Add(new Mail());
      this.UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value; UpdateData();}
    }


    private void UpdateData(){
      if(!this.started){return;}
      foreach(MailItem item in items){
        GameObject.Destroy(item);
      }
      items.Clear();

      GameObject go = null;
      Vector3 position = mailItem.transform.localPosition;
      foreach(Mail mail in MailCache.instance.list){
        go = UIUtils.Duplicate(mailItem.gameObject, mailItem.transform.parent.gameObject);
        go.transform.localPosition = position;
        MailItem item = go.GetComponent<MailItem>();
        items.Add(item);
        item.data = mail;
        position.y = position.y - 134;
      }
    }

    private void CloseBtnClickHandler(GameObject go){

    }

    private void ItemClickHandler(ViewItem viewItem){
      UIContext.mgrCoC.LazyOpen(MailDetailsPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, viewItem.data);
    }
  }
}