using UnityEngine;
using System.Collections;
using TangGame.Xml;
using TangGame.UI;

namespace TangGame.UI
{
	public class SVPropsItem : PropsItem
	{
		public GameObject Vernier;
		public GameObject Arrow;
		/// <summary>
		/// The is checked.
		/// 当前被选中
		/// </summary>
		private bool isChecked;
		public bool IsChecked {
			get {
				return isChecked;
			}
			set {
				isChecked = value;
				Vernier.gameObject.SetActive (isChecked);
			}
		}
		public void Flush(PropsXml xml){
			data = new PropsBase ();
			data.Xml = xml;
			this.Flush (data);
		}
	}
}