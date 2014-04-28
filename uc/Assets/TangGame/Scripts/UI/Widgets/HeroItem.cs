using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI.Base;
namespace TangGame.UI
{
	public class HeroItem : MonoBehaviour
	{
		public UISprite HeroAvatarFrame;
		public UISprite HeroAvatarSprite;
		public UILabel HeroName;
		public UILabel Level;
		public GameObject HeroPackageSoulstone;
		public UILabel CountLabel;
		public UISprite ForegroundSprite;
		public UISprite HeroType;
		public UIGrid PropsGrid;
		public UISprite[] Props;
		public StarList starList;
		public GameObject HeroTag;
		private HeroBase data;
		// Use this for initialization
		void Start ()
		{

		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		public HeroBase Data {
			get {
				return data;
			}
//		set {
//			data = value;
//		}
		}

		public void Flush (HeroBase hero)
		{
			this.data = hero;
			UpHeroRank ((int)data.Net.upgrade);
			UpHeroAvatarSprite (data.Xml.avatar);
			UpHeroName (data.Xml.name);
			Locked (data.Islock);
			UpLevel (data.Net.level);
			UpHeroType (data.Attribute_Type);
			//			UpHeroFragments (data.FragmentsCount, data.FragmentsCountMax);//需要道具来支撑
			SetStarList (data.Net.evolve);
			//			SetTag ();TODO
		}
		/// <summary>
		/// Ups the tag.如果有装备或可以进阶则显示tag
		/// </summary>
		/// <param name="tagShow">If set to <c>true</c> tag show.</param>
		private void UpTag(bool tagShow){
			this.HeroTag.SetActive (tagShow);
		}
		private void UpHeroType (AttributeTypeEnum propertyType)
		{
			string resName = "icon_str";
			switch (propertyType) {
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
			this.HeroType.spriteName = resName;
		}

		private void UpProps ()
		{
			//TODO upProps;
		}

		private void UpLevel (int level)
		{
			this.Level.text = level.ToString ();
		}

		private void UpHeroName (string heroName)
		{
			this.HeroName.text = heroName;
		}

		private void UpHeroRank (int heroRank)
		{
			string frameName;
			frameName = "hero_icon_frame_" + heroRank;
			this.HeroAvatarFrame.spriteName = frameName;
		}

		private void UpHeroFragments (int fragmentsCount, int fragmentsCountMax)
		{
			this.CountLabel.text = fragmentsCount + "/" + fragmentsCount;
			this.ForegroundSprite.fillAmount = fragmentsCount / fragmentsCountMax;
		}

		private void UpHeroAvatarSprite (string heroAvatar)
		{
			HeroAvatarSprite.spriteName = heroAvatar;
		}

		private void Locked (bool locked)
		{
			PropsGrid.gameObject.SetActive (!locked);
			HeroPackageSoulstone.gameObject.SetActive (locked);
			Level.gameObject.SetActive (!locked);
		}

		void SetStarList (int count)
		{
			starList.spriteN = "head_star_icon";
			starList.count = count;
			starList.Flush ();
		}
	}
}