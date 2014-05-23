/// <summary>
/// Bag panel.
/// xbhuang 2014-4-28
/// </summary>
using UnityEngine;
using System.Collections;
using TangGame.UI;

namespace TangGame.UI
{
	public class BagPanel : MonoBehaviour
	{
    public const string NAME = "BagPanel";

		public GameObject BagInterfaceSubPanel;
		public GameObject BagPropsInfoSubPanel;
		// Use this for initialization
		void Start ()
		{
			DynamicBindUtil.BindScriptAndProperty (BagInterfaceSubPanel,BagInterfaceSubPanel.name);
			DynamicBindUtil.BindScriptAndProperty (BagPropsInfoSubPanel, BagPropsInfoSubPanel.name);
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
		void OnDisable(){
			this.BagPropsInfoSubPanel.SetActive (false);
		}
		/// <summary>
		/// Ups the bag properties info sub panel.
		/// 打开并且更新面板数据
		/// </summary>
		public void UpBagPropsInfoSubPanel(Props data){
			BagPropsInfoSubPanel.GetComponent<BagPropsInfoSubPanel> ().Flush (data);
		}
	}
}