using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI;

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

		bool mCanCall;

		public bool canCall {
			get {
				return soulStoneCount >= soulStoneCountMax;
			}
		}

		int soulStoneCount;
		int soulStoneCountMax;

		public int SoulStoneCount {
			get {
				return soulStoneCount;
			}
		}

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
			UpHeroAvatarSprite (data.Xml.avatar,hero.Islock);
			UpHeroName (data.Xml.name);
			Locked (data.Islock);
			UpLevel (data.Net.level);
			UpHeroType (data.Attribute_Type);
			UpProps (hero);
			SetStarList (data.Net.evolve);
			SetFragments (data);
			//			SetTag ();TODO
		}

		/// <summary>
		/// Sets the fragments.
		/// 设置碎片的条
		/// </summary>
		/// <param name="hero">Hero.</param>
		void SetFragments (HeroBase hero)
		{
			if (hero.Net.evolve != 0 || !hero.Islock) {
				return;
			}
			int rockId = hero.Xml.soul_rock_id;
			int evolve = hero.Xml.evolve;
			Dictionary<int, Props> pTable = PropsCache.instance.propsTable;
			if (pTable.ContainsKey (rockId)) {
				soulStoneCount = pTable [rockId].net.count;
			}
			if (evolve == 0) {
				evolve = 1;
			}
			soulStoneCountMax = Config.evolveXmlTable [evolve].val;

			this.UpHeroFragments (soulStoneCount,soulStoneCountMax);
		}

		/// <summary>
		/// Ups the tag.如果有装备或可以进阶则显示tag
		/// </summary>
		/// <param name="tagShow">If set to <c>true</c> tag show.</param>
		private void UpTag (bool tagShow)
		{
			this.HeroTag.SetActive (tagShow);
		}

		private void UpHeroType (AttributeTypeEnum propertyType)
		{
			this.HeroType.spriteName = Global.GetHeroTypeIconName (propertyType);
		}

		private void UpProps (HeroBase heroBase)
		{

			if (heroBase != null && !heroBase.Islock && heroBase.EquipBases != null) {
				Equip[] equips = heroBase.EquipBases;
				for (int i = 0; i < Props.Length; i++) {
					int len = heroBase.EquipBases.Length;
					if (len > i && equips [i] != null) {
						Props [i].spriteName = equips [i].data.icon;
						if (equips [i].net != null) {
							Props [i].GetComponentsInChildren<UISprite> () [0].enabled = true;
							Props [i].GetComponentsInChildren<UISprite> () [1].enabled = false;

						} else {
							Props [i].GetComponentsInChildren<UISprite> () [0].enabled = false;
							Props [i].GetComponentsInChildren<UISprite> () [1].enabled = true;
						}
					}
				}
			}
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
			this.HeroAvatarFrame.spriteName = Global.GetHeroIconFrame (heroRank);;
		}

		private void UpHeroFragments (int fragmentsCount, int fragmentsCountMax)
		{
			if (fragmentsCount >= fragmentsCountMax) {
				this.CountLabel.text = UIPanelLang.CALL_ME_MAYBE;
				this.ForegroundSprite.fillAmount = 1f;
			} else {
				this.CountLabel.text = fragmentsCount + "/" + fragmentsCountMax;
				this.ForegroundSprite.fillAmount = fragmentsCount / fragmentsCountMax;
			}
		}

		private void UpHeroAvatarSprite (string heroAvatar,bool isLock)
		{

			if (isLock) {
				heroAvatar += "_un";
			} 
			bool containSprite = false;
			foreach (UISpriteData srpiteData in HeroAvatarSprite.atlas.spriteList) {
				if (heroAvatar.Equals(srpiteData.name)) {
					containSprite = true;
				}
			}

			//TODO 仅仅供测试使用
			if (!containSprite) {
				heroAvatar = "hero_icon_un";
				Debug.LogWarning ("the hero can't find avatar name!");
			}
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
			starList.count = count;
			starList.Flush ();
		}
	}
}