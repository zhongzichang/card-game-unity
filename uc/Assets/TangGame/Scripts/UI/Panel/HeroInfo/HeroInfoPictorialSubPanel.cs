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
			SetTweenByOnClick ();
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
			//TODO 暂时没有名字图片资源
//			this.HeroName.GetComponent<UISprite> ().spriteName = cardHeroName;
		}

		/// <summary>
		/// Sets the type of the property.设置英雄属性类型
		/// </summary>
		void SetPropertyType (AttributeTypeEnum type)
		{
			this.HeroType.GetComponent<UISprite> ().spriteName = Global.GetHeroTypeIconName (type);
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

		void SetBackground (int upgrade)
		{
			Foreground.GetComponent<UISprite> ().color = Global.GetHeroUpgradeColor(upgrade);
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
		void SetTweenByOnClick(){
			UIPanel panel = GetComponent<UIPanel> ();
			float currentHeight = this.GetComponent<BoxCollider> ().size.y;
			float currentWidth = this.GetComponent<BoxCollider> ().size.x;
			float heightRate = panel.width / currentHeight;
			float widthRate = panel.height / currentWidth;
			float scaleRate = heightRate < widthRate ? heightRate : widthRate;

			TweenScale[] mScales = this.gameObject.GetComponents<TweenScale> ();
			foreach (TweenScale mScale in mScales) {
				if (mScale.tweenGroup == 2) {
					mScale.to = new Vector3 (scaleRate,heightRate,scaleRate);
				}
			}

			TweenPosition[] mPosis = this.gameObject.GetComponents<TweenPosition> ();
			foreach (TweenPosition mPosi in mPosis) {
				if (mPosi.tweenGroup == 2) {
					mPosi.to = new Vector3 (0,currentWidth/2 * scaleRate,0);
				}
			}

		}
		public IEnumerator BackDepth() {
			yield return new WaitForSeconds(0.2f);
			GetComponent<UIPanel> ().startingRenderQueue -= 1000;
			GetComponent<UIPanel> ().depth -= 1000;
		}


	}
}