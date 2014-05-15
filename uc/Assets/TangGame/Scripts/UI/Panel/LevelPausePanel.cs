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
		public UIEventListener soundOffBtn;
    public UIEventListener soundOnBtn;
		public TweenScale tween;
    public UISprite soundOffSprite;
		public UISprite soundOnSprite;

		private object mParam;

		void Awake(){
			continueBtn.onClick += ContinueBtnHandler;
			quitBtn.onClick += QuitBtnHandler;
      soundOffBtn.onClick += SoundBtnHandler;
      soundOnBtn.onClick += SoundBtnHandler;

			continueLabel.text = UIPanelLang.BATTLE_CONTINUE;
			quitLabel.text = UIPanelLang.BATTLE_QUIT;
			soundLabel.text = UIPanelLang.BATTLE_SOUND_OPEN;
		}

		void Start(){
			Open();
		}

		public object param{
			get{return mParam;}
			set{
				mParam = value;
				Open();
			}
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
        soundOnSprite.gameObject.SetActive(true);
        soundOffSprite.gameObject.SetActive(false);
			}else{
				soundLabel.text = UIPanelLang.BATTLE_SOUND_CLOSE;
        soundOnSprite.gameObject.SetActive(false);
        soundOffSprite.gameObject.SetActive(true);
			}
		}

	}
}