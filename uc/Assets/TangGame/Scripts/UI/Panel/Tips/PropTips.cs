using UnityEngine;
using System.Collections;

namespace TangGame{

	public class PropTips : MonoBehaviour {

		/// 该对象实例
		private static PropTips mInstance;

		private PropTipsPanel panel;
		private bool started;
		public Vector3 position;
		public int offset;
		public int id;

		// Use this for initialization
		void Start () {
			Object obj = Resources.Load("Prefabs/Tips/PropTipsPanel", typeof(GameObject));
			GameObject go = GameObject.Instantiate(obj) as GameObject;
			go.transform.parent = this.gameObject.transform;
			go.transform.localScale = Vector3.one;
			panel = go.GetComponent<PropTipsPanel>();
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
		}

		/// 更新数据的显示
		private void UpdateDisplay(){
			if(panel == null){return;}
			panel.effectLabel.text = "";
			panel.descLabel.text = "";
			panel.goldLabel.text = "";
			panel.CalculateHeight();
		}


		//=====================================================================================
		//=====================================================================================
		
		/// <summary>
		/// 显示Tips
		/// </summary>
		/// <param name="position">触发对象的世界坐标</param>
		/// <param name="offset">触发对象显示的偏移量，一般为高度</param>
		/// <param name="id">道具的ID</param>
		public static PropTips Show(Vector3 position, int offset, int id){
			if(mInstance == null){
				GameObject go = new GameObject("PropTips");
				go.layer = 9;
				go.transform.parent = null;
				mInstance = go.AddComponent<PropTips>();
				go.transform.localScale = Vector3.one;
				go.transform.localPosition = new Vector3(0, 0, -100);//Z值临时添加
			}
			mInstance.position = position;
			mInstance.offset = offset;
			mInstance.id = id;
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