using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

namespace TangGame.UI
{
	public class PropertyInfoSubPanel : MonoBehaviour
	{
		public GameObject HeroInfo;
		public GameObject HeroDescription;
		public GameObject Property;
		public GameObject property;
		private HeroBase data;
		// Use this for initialization
		void Start ()
		{
	
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void Flush (HeroBase data)
		{
			this.data = data;
			//FIXME 
			SetHeroInfo (data.Xml.hero_info);
			SetHeroDescription (data.Xml.hero_tip);
			SetProperty (data);
		}

		/// <summary>
		/// Sets the hero info.
		/// </summary>
		void SetHeroInfo (string info)
		{
			this.HeroInfo.GetComponent<UILabel> ().text = info;
		}

		/// <summary>
		/// Sets the hero description.
		/// </summary>
		void SetHeroDescription (string description)
		{
			this.HeroDescription.GetComponent<UILabel> ().text = description;
		}

		/// <summary>
		/// Sets the property.
		/// </summary>
		/// <param name="str">String.</param>
		void SetProperty (HeroBase herobase)
		{

		}
		void AddPropertyItem(string str, float num){
			GameObject obj = NGUITools.AddChild (Property.gameObject,property.gameObject);
//			obj
		}
	}
}