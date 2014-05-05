using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace TangGame
{
  public class BattleTxtPanelMediator : Mediator
  {
    private BattleTxtPanel panel;

    public BattleTxtPanelMediator (BattleTxtPanel panel)
    {
      this.panel = panel;
    }

    public override IList<string> ListNotificationInterests ()
    {
      return new List<string> (){ BattleCommand.BattleTxt };
    }

    public override void HandleNotification (INotification notification)
    {
      switch (notification.Name) {
      case BattleCommand.BattleTxt:
        this.panel.ShowBattleTxt (notification.Body as BattleTxt);
        break;
      }
    }
  }

  public class BattleTxtPanel : MonoBehaviour
  {
    public UILabel hurtLabel;
    public UILabel damageLabel;
    public UILabel textLabel;
    private BattleTxtPanelMediator mediator;

    void Awake ()
    {
      mediator = new BattleTxtPanelMediator (this);
      Facade.Instance.RegisterMediator (mediator);

      hurtLabel.gameObject.SetActive (false);
      damageLabel.gameObject.SetActive (false);
      textLabel.gameObject.SetActive (false);
    }

    public void ShowBattleTxt (BattleTxt txt)
    {
      UILabel label = null;
      if (txt.type == BattleTxtType.Hurt) {
        label = hurtLabel;
      } else if (txt.type == BattleTxtType.Text) {
        label = textLabel;
      }

      GameObject go = GameObject.Instantiate (label.gameObject) as GameObject;
      go.transform.parent = this.gameObject.transform;
      go.SetActive (true);
      go.transform.localScale = Vector3.one;
      BattleTxtItem item = go.AddComponent<BattleTxtItem> ();
      item.data = txt;
    }

    private void Show ()
    {
      BattleTxtType[] types = new BattleTxtType[]{ BattleTxtType.Hurt, BattleTxtType.Text };
      int index = Random.Range (0, 2);
      BattleTxtType type = types [index];
      int hp = 100;
      if (type == BattleTxtType.Hurt) {
        hp = Random.Range (50, 200);
        if (hp < 100) {
          hp = -hp;
        }
      } else if (type == BattleTxtType.Text) {
        hp = 1;
      }

      index = Random.Range (0, 100);

      BattleTxt txt = new BattleTxt ();
      txt.value = hp;
      txt.type = type;
      txt.crit = index > 50;
      mediator.SendNotification (BattleCommand.BattleTxt, txt);
    }

    void OnGUI ()
    {
      if (GUI.Button (new Rect (100, 100, 100, 50), "xue")) {
        Show ();
      }
    }
  }
}
