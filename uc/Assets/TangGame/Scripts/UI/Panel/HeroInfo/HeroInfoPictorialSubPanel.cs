using UnityEngine;
using System.Collections;
using TangGame.UI;

namespace TangGame.UI
{
	public class HeroInfoPictorialSubPanel : MonoBehaviour
	{
		public GameObject HeroName;
		public GameObject HeroType;
		public GameObject Skill1;
		public GameObject Skill2;
		public GameObject Skill3;
		public GameObject Skill4;
		public GameObject StarList;
		public GameObject Foreground;
		public GameObject Texture;
		private HeroBase data;
		private bool isChecked = false;
		GameObject[] skillGroup;
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
			SetSkillGroup (data.SkillBases);
			SetHeroName (data.Xml.name);
			SetPropertyType (data.Attribute_Type);
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

		void SetSkillGroup (SkillBase[] skillBases)
		{
			if (skillBases == null)
				return;

			skillGroup = new GameObject[4];
			skillGroup [0] = Skill1;
			skillGroup [1] = Skill2;
			skillGroup [2] = Skill3;
			skillGroup [3] = Skill4;
			for (int i = 0; i < skillBases.Length; i++) {
				if (skillBases [i] == null || skillGroup.Length < i) {
					return;
				}
				skillGroup [i].GetComponent<UISprite> ().spriteName = skillBases [i].Xml.skill_icon;
			}
		}

		void SetBackground (int rank)
		{
//			Foreground.GetComponent<UISprite> ().spriteName = "card_bg_" + HeroBase.GetRankColorStr(rank);
			Foreground.GetComponent<UISprite> ().color = HeroBase.GetRankColor ((RankEnum)rank);
		}

		/// <summary>
		/// Sets the card texture 
		/// </summary>
		void SetCardTexture (string cardName)
		{
			Object card = Resources.Load ("Textures/HeroPictorialTexture/" + cardName);
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