using UnityEngine;
using System.Collections;

namespace TangGame.UI{

  /// <summary>
  /// 信息Tips，用于简短提示，文本过多请使用面板方式
  /// </summary>
	public class MsgTips : MonoBehaviour {

		/// 该对象实例
		private static MsgTips mInstance;

		private MsgTipsPanel panel;
    private TweenAlpha tween;
		private bool started;
		public Vector3 position;
    /// 信息文本
		public string msg;
    /// 时间计数
    private float time = 0;
    /// 面板关闭标示
    private bool closing;

		// Use this for initialization
		void Start () {
      Object obj = Resources.Load("Prefabs/Tips/MsgTipsPanel", typeof(GameObject));
			GameObject go = GameObject.Instantiate(obj) as GameObject;
			go.transform.parent = this.gameObject.transform;
			go.transform.localScale = Vector3.one;
      go.transform.localPosition = Vector3.zero;
      panel = go.GetComponent<MsgTipsPanel>();

			tween = this.gameObject.AddComponent<TweenAlpha>();
      tween.eventReceiver = this.gameObject;
      tween.callWhenFinished = "OnFinishedHandler";
      tween.from = 1;
      tween.to = 0;
			tween.duration = 0.5f;
      tween.enabled = false;
			started = true;
			UpdateDisplay();
		}

    void Update(){
      if(closing){return;}
      time += Time.deltaTime;
      if(time > 1.5f){
        Close();
      }
    }

		private void Open(){
			if(started){
        time = 0;
        closing = false;
				this.gameObject.SetActive(true);
        tween.enabled = false;
        tween.ResetToBeginning();
				UpdateDisplay();
			}
		}

		/// 关闭隐藏Tips
		private void Close(){
      closing = true;
      time = 0;
      tween.Play();
		}

		/// 更新数据的显示
		private void UpdateDisplay(){
			if(panel == null){return;}
      panel.msg = this.msg;
		}

    private void OnFinishedHandler(UITweener tweener){
        closing = false;
        this.gameObject.SetActive(false);
    }

		//=====================================================================================
		//=====================================================================================
		
		/// <summary>
		/// 显示Tips
		/// </summary>
    public static MsgTips Show(string msg){
			if(mInstance == null){
        GameObject go = new GameObject("MsgTips");
				go.layer = Global.UILayer;
        mInstance = go.AddComponent<MsgTips>();
			}
      mInstance.transform.parent = UICamera.current.transform.parent;
      mInstance.transform.localPosition = Vector3.zero;
      mInstance.transform.localScale = Vector3.one;

      mInstance.msg = msg;
			mInstance.Open();
			return mInstance;
		}

		/// 隐藏
		public static void Hiddle(){
			if(mInstance != null){
				mInstance.Close();
			}
		}
	}
}