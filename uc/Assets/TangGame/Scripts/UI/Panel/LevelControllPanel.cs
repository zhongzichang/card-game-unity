using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace TangGame{

  /// 战斗界面的左上角暂停控制界面
	public class LevelControllPanel : MonoBehaviour {

		public UIEventListener pauseBtn;
		public UILabel countdownLabel;

		private bool mPause = false;
		private float mTime = 120;

		void Update(){
			if (!mPause) {
				mTime -= Time.deltaTime;
				if(mTime >= 0){
					ShowCountdownTime();
				}
			}
		}

		private void ShowCountdownTime(){
			int minute = (int)(mTime / 60f);
			int second = (int)(mTime % 60f);
			string minuteStr = minute < 10 ? ("0" + minute) : minute.ToString ();
			string secondStr = second < 10 ? ("0" + second) : second.ToString ();
			countdownLabel.text = minuteStr + ":" + secondStr;
		}

    public bool pause{
      get{return this.mPause;}
      set{this.mPause = value;}
    }

    public float time{
      get{return this.mTime;}
      set{this.mTime = value;}
    }
	
	}
}