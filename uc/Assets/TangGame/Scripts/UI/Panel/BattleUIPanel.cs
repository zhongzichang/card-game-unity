using UnityEngine;
using System.Collections;
using TangGame.UI;
namespace TangGame{

	public class BattleUIPanel : MonoBehaviour {
		
		public UIEventListener pauseBtn;
		public UILabel countdownLabel;

		public GameObject pausePanel;
		public UILabel continueLabel;
		public UILabel quitLabel;
		public UILabel soundLabel;
		public UIEventListener continueBtn;
		public UIEventListener quitBtn;
		public UIEventListener soundBtn;


		private bool pause = false;
		private float time = 120;

		void Awake(){
			pauseBtn.onClick += PauseBtnHandler;
			continueBtn.onClick += ContinueBtnHandler;
			quitBtn.onClick += QuitBtnHandler;
			soundBtn.onClick += SoundBtnHandler;
			pausePanel.SetActive(false);

			continueLabel.text = UIPanelLang.BATTLE_CONTINUE;
			quitLabel.text = UIPanelLang.BATTLE_QUIT;
			soundLabel.text = UIPanelLang.BATTLE_SOUND_OPEN;
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
			pausePanel.SetActive(true);
		}

		public void ContinueBtnHandler(GameObject go){
			pause = false;
			pausePanel.SetActive(false);
		}

		public void QuitBtnHandler(GameObject go){
			
		}

		public void SoundBtnHandler(GameObject go){
			
		}

	}
}