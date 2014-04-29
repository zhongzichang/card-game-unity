using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
	public class BagPropsInfoSubPanel : MonoBehaviour
	{

		public GameObject PropsInfoBg;
		public GameObject PropsInfoLabel;
		public GameObject PropsDescription;
		public GameObject PropInfoTable;
		// Use this for initialization
		void Start ()
		{
			this.UpPropsInfo ("物品描述，这是一个非常非常非常废话的描述！你可以忽略它！当然，忽略也是有代价的！！！！");
		}
		// Update is called once per frame
		void Update ()
		{
	
		}
		public void Flush(int propsId){

		}

		public void UpPropsInfo(string text){
			UILabel label = PropsInfoLabel.GetComponent<UILabel> ();
			label.text = text;
			Utils.TextBgAdaptiveSize( label, PropsInfoBg.GetComponent<UISprite>());
			PropInfoTable.GetComponent<UITable> ().repositionNow = true;
		}
	
	}
}