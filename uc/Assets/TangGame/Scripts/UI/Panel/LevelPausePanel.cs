using UnityEngine;
using System.Collections;
using TangGame.UI;
using PureMVC.Patterns;

namespace TangGame{

	public class LevelPausePanel : MonoBehaviour {

		public GameObject group;
		public UILabel continueLabel;
		public UILabel quitLabel;
		public UILabel soundLabel;
		public UIEventListener continueBtn;
		public UIEventListener quitBtn;
		public UIEventListener soundBtn;
		public TweenScale tween;
		public UISprite soundSprite;

		void Awake(){
			continueBtn.onClick += ContinueBtnHandler;
			quitBtn.onClick += QuitBtnHandler;
			soundBtn.onClick += SoundBtnHandler;

			continueLabel.text = UIPanelLang.BATTLE_CONTINUE;
			quitLabel.text = UIPanelLang.BATTLE_QUIT;
			soundLabel.text = UIPanelLang.BATTLE_SOUND_OPEN;
		}

		void Start(){
			Open();
		}

		public void ContinueBtnHandler(GameObject go){
			Facade.Instance.SendNotification(BattleCommand.BattlePause);
		}

		public void QuitBtnHandler(GameObject go){
			
		}

		public void SoundBtnHandler(GameObject go){
			Setting.soundOn = !Setting.soundOn;
			UpdateSound();
		}

		public void Open(){
			UpdateSound();
			tween.ResetToBeginning();
			tween.Play();
		}

		/// 更新声音设置
		private void UpdateSound(){
			if(Setting.soundOn){
				soundLabel.text = UIPanelLang.BATTLE_SOUND_OPEN;
				soundSprite.spriteName = "SoundOn";
			}else{
				soundLabel.text = UIPanelLang.BATTLE_SOUND_CLOSE;
				soundSprite.spriteName = "SoundOff";
			}
		}

	}
}