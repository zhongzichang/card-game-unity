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
    public GameObject greenLabel;
	public GameObject orangeLabel;
	public GameObject redLabel;
	public GameObject yellowLabel;
	public GameObject textSprite;
    private BattleTxtPanelMediator mediator;

    void Awake ()
    {
      mediator = new BattleTxtPanelMediator (this);
      Facade.Instance.RegisterMediator (mediator);

	  greenLabel.SetActive (false);
			orangeLabel.SetActive (false);
			redLabel.SetActive (false);
			yellowLabel.SetActive (false);
			textSprite.SetActive (false);
    }

    public void ShowBattleTxt (BattleTxt txt)
    {
      GameObject label = null;
		if (txt.type == BattleTxtType.Hurt) {
			if(txt.value < 1){
				label = greenLabel;
			}else if(txt.self){
				label = redLabel;
			}else{
				label = orangeLabel;
			}
		} else if (txt.type == BattleTxtType.Text) {
			label = textSprite;
		}else if (txt.type == BattleTxtType.Energy) {
			label = yellowLabel;
		}

      GameObject go = GameObject.Instantiate (label) as GameObject;
      go.transform.parent = this.gameObject.transform;
      go.SetActive (true);
      go.transform.localScale = Vector3.one;
		if(txt.type == BattleTxtType.Text){
			BattleTxtSpriteItem item = go.AddComponent<BattleTxtSpriteItem> ();
			item.data = txt;
		}else{
			BattleTxtItem item = go.AddComponent<BattleTxtItem> ();
			item.data = txt;
		}
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
			txt.position = new Vector3(100 , 100,0);
      mediator.SendNotification (BattleCommand.BattleTxt, txt);
    }

		/*void OnGUI(){
			if(GUI.Button(new Rect(50, 50, 50, 50), "T")){
				Show();
			}
		}*/
  }
}
