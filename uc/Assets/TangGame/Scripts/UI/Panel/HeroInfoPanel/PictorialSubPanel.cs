using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

namespace TangGame.UI
{
	public class PictorialSubPanel : MonoBehaviour
	{
		public GameObject HeroName;
		public GameObject HeroType;
		public GameObject Skill1;
		public GameObject Skill2;
		public GameObject Skill3;
		public GameObject Skill4;
		public GameObject StarList;
		public GameObject Background;
		public GameObject Texture;
		private HeroBase data;
		private bool isChecked = false;
		// Use this for initialization
		void Start ()
		{

		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		/// <summary>
		/// Refreshs the panel.刷新面板
		/// </summary>
		private void RefreshPanel ()
		{
			if (data == null) {
				return;
			}
			this.RefreshPanel (this.data);
		}

		/// <summary>
		/// Refreshs the panel.刷新面板的数据
		/// </summary>
		/// <param name="data">Data.</param>
		public void RefreshPanel (HeroBase data)
		{
			this.data = data;
			SetHeroName (data.Xml.name);
			SetPropertyType (data.Attribute_Type);
//		SetSkillGroup ();
			SetCardTexture (data.Xml.portrait);
			SetBackground ((int)data.Net.upgrade);
			SetStarList (data.Net.evolve);
		}

		void SetStarList (int count)
		{
			StarList.GetComponent<StarList> ().count = count;
			StarList.GetComponent<StarList> ().Flush ();
		}

		/// <summary>
		/// Sets the name of the hero.设置英雄名字
		/// </summary>
		void SetHeroName (string cardHeroName)
		{
			this.HeroName.GetComponent<UISprite> ().spriteName = "card_name_" + cardHeroName + "big";
		}

		/// <summary>
		/// Sets the type of the property.设置英雄属性类型
		/// </summary>
		void SetPropertyType (AttributeTypeEnum type)
		{
			string resName = "icon_str";
			switch (type) {
			case AttributeTypeEnum.STR:
				resName = "icon_str";
				break;
			case AttributeTypeEnum.INT:
				resName = "icon_int";
				break;
			case AttributeTypeEnum.AGI:
				resName = "icon_agi";
				break;
			}
			this.HeroType.GetComponent<UISprite> ().spriteName = resName;
		}

		void SetSkillGroup ()
		{
			//FIXME 修改界面上技能的图标
		}

		void SetBackground (int rank)
		{
			Background.GetComponent<UISprite> ().spriteName = "card_bg_" + HeroBase.GetRankColorStr(rank);
		}

		/// <summary>
		/// Sets the card texture 
		/// </summary>
		void SetCardTexture (string cardName)
		{
			Object card = Resources.Load ("Textures/SanguoUI/art/" + cardName);
			this.Texture.GetComponent<UITexture> ().mainTexture = card  as Texture2D;
		}

		void OnClick ()
		{
			isChecked = !isChecked;
			UIPlayTween pt = GetComponent<UIPlayTween> ();
			pt.Play (true);
			if (isChecked) {
				GetComponent<UIPanel> ().startingRenderQueue += 1000;
				GetComponent<UIPanel> ().depth += 1000;
			} else {
				StartCoroutine (BackDepth ());
			}
		}
		public IEnumerator BackDepth() {
			yield return new WaitForSeconds(0.2f);
			GetComponent<UIPanel> ().startingRenderQueue -= 1000;
			GetComponent<UIPanel> ().depth -= 1000;
		}


	}
}