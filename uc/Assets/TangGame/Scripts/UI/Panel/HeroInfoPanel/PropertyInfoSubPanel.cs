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
//		SetHeroInfo (data.);
//		SetHeroDescription(data.)
//		SetProperty(data.)
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
		void SetProperty (string str)
		{
			//FIXME 
		}
	}
}