﻿using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

namespace TangGame{

	public class PropTips : MonoBehaviour {

		/// 该对象实例
		private static PropTips mInstance;

		private PropTipsPanel panel;
		private TweenScale tween;
		private bool started;
		public Vector3 position;
		public int offset;
		public PropsBase prop;

		// Use this for initialization
		void Start () {
			Object obj = Resources.Load("Prefabs/Tips/PropTipsPanel", typeof(GameObject));
			GameObject go = GameObject.Instantiate(obj) as GameObject;
			go.transform.parent = this.gameObject.transform;
			go.transform.localScale = Vector3.one;
			panel = go.GetComponent<PropTipsPanel>();

			tween = this.gameObject.AddComponent<TweenScale>();
			tween.from = new Vector3(0, 0, 1);
			tween.duration = 0.1f;
			started = true;
			UpdateDisplay();
		}

		private void Open(){
			if(started){
				this.gameObject.SetActive(true);
				UpdateDisplay();
			}
		}

		/// 关闭隐藏Tips
		private void Close(){
			this.gameObject.SetActive(false);
			prop = null;
		}

		/// 更新数据的显示
		private void UpdateDisplay(){
			if(panel == null){return;}
			tween.ResetToBeginning();
			tween.Play();

			panel.SetProp(prop);

			this.gameObject.transform.position = this.position;
			Vector3 temp = this.gameObject.transform.localPosition;
			temp.y = temp.y + panel.height / 2 + offset + 2;
			this.gameObject.transform.localPosition = temp;
		}


		//=====================================================================================
		//=====================================================================================
		
		/// <summary>
		/// 显示Tips
		/// </summary>
		/// <param name="position">触发对象的世界坐标</param>
		/// <param name="offset">触发对象显示的偏移量，一般为高度</param>
		/// <param name="id">道具的ID</param>
		public static PropTips Show(Vector3 position, int offset, PropsBase prop){
			if(mInstance == null){
				GameObject go = new GameObject("PropTips");
				go.layer = Global.UILayer;
				go.transform.parent = Global.uiRoot.transform;
				mInstance = go.AddComponent<PropTips>();
				go.transform.localScale = Vector3.one;
				go.transform.localPosition = new Vector3(0, 0, -100);//Z值临时添加
			}
			mInstance.position = position;
			mInstance.offset = offset;
			mInstance.prop = prop;
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