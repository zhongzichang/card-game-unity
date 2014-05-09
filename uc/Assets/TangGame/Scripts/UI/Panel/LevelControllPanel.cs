using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace TangGame{

	public class LevelControllPanelMediator : Mediator
	{
		private LevelControllPanel panel;
		
		public LevelControllPanelMediator (LevelControllPanel panel)
		{
			this.panel = panel;
		}
		
		public override IList<string> ListNotificationInterests ()
		{
			return new List<string> (){ BattleCommand.BattlePause };
		}
		
		public override void HandleNotification (INotification notification)
		{
			switch (notification.Name) {
			case BattleCommand.BattlePause:
				this.panel.Play();
				break;
			}
		}
	}

	public class LevelControllPanel : MonoBehaviour {
		
		public UIEventListener pauseBtn;
		public UILabel countdownLabel;

		private bool pause = false;
		private float time = 120;

		private LevelControllPanelMediator mediator;

		void Awake(){
			mediator = new LevelControllPanelMediator (this);
			Facade.Instance.RegisterMediator (mediator);
			pauseBtn.onClick += PauseBtnHandler;
		}

		void Update(){
			if (!pause) {
				time -= Time.deltaTime;
				if(time >= 0){
					ShowCountdownTime();
				}
			}
		}

		private void ShowCountdownTime(){
			int minute = (int)(time / 60f);
			int second = (int)(time % 60f);
			string minuteStr = minute < 10 ? ("0" + minute) : minute.ToString ();
			string secondStr = second < 10 ? ("0" + second) : second.ToString ();
			countdownLabel.text = minuteStr + ":" + secondStr;
		}
	
		public void PauseBtnHandler(GameObject go){
			pause = true;
			TangGame.UIContext.manger.LazyOpen("LevelPausePanel", TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.SPRITE);
		}

		public void Play(){
			pause = false;
			TangGame.UIContext.manger.Back();
		}

	}
}