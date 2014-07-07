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

      Mail mail = new Mail();
      mail.id = 1;
      mail.title = "邮件测试数据";
      mail.content = "爱词霸英语为广大网友提供在线翻译、在线词典、英语口语、英语学习资料、汉语词典,金山词霸下载等服务,致力于为您提供优质权威的在线英语服务,是5000万英语学习者的...";
      mail.sender = "竞技场军需官 阿宽";


      MailCache.instance.list.Add(mail);
      MailCache.instance.list.Add(mail);
      MailCache.instance.list.Add(mail);
      MailCache.instance.list.Add(mail);
      MailCache.instance.list.Add(mail);
      MailCache.instance.list.Add(mail);
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
        item.onClick += ItemClickHandler;
        items.Add(item);
        item.data = mail;
        position.y = position.y - 134;
      }
    }

    private void CloseBtnClickHandler(GameObject go){
      UIContext.mgrCoC.Back();
    }

    private void ItemClickHandler(ViewItem viewItem){
      UIContext.mgrCoC.LazyOpen(MailDetailsPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE, viewItem.data);
    }
  }
}