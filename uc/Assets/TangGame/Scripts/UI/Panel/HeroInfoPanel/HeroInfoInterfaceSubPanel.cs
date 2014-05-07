﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.UI.Base;
using TDB = TangDragonBones;

namespace TangGame.UI
{
	public class HeroInfoInterfaceSubPanel : MonoBehaviour
	{
		///进化按钮
		public GameObject EvolveButton;
		///进阶按钮
		public GameObject UpgradeButton;
		///属性详情按钮
		public GameObject PropertyInfoButton;
		///技能面板按钮
		public GameObject SkillInfoButton;
		///图鉴按钮
		public GameObject PictorialButton;
		public GameObject AnimatorObj;
		public GameObject Background;
		///6个道具栏
		public GameObject PropsTable;
		///英雄名字
		public GameObject HeroNameLabel;
		///英雄名字框
		public GameObject HeroNameFrame;
		///英雄属性类型，敏，力，智
		public GameObject HeroType;
		///碎片数量条
		public GameObject HeroPackageSoulstoneForegroundSprite;
		///碎片数量的label
		public GameObject HeroPackageSoulstoneCountLabel;
		///角色经验label
		public GameObject ExpLabel;
		///角色等级label
		public GameObject LevelLabel;
		///角色战斗力label
		public GameObject ScoreLabel;
		///英雄数据
		private HeroBase heroBase;
		///星级列表
		public GameObject StarList;
		private TDB.DemoBhvr demoBhvr;
		private ArrayList equipObjects = new ArrayList ();
		// Use this for initialization
		void Start ()
		{

      SkillInfoButton.GetComponent<UIToggle> ().optionCanBeNone = true;
      PictorialButton.GetComponent<UIToggle> ().optionCanBeNone = true;
      PropertyInfoButton.GetComponent<UIToggle> ().optionCanBeNone = true;

      UIEventListener.Get (AnimatorObj.gameObject).onClick += AnimatorObjOnClick;
      UIEventListener.Get (PropertyInfoButton.gameObject).onClick += ToggleButtonOnClick;
      UIEventListener.Get (SkillInfoButton.gameObject).onClick += ToggleButtonOnClick;
      UIEventListener.Get (PictorialButton.gameObject).onClick += ToggleButtonOnClick;
    }

    void OnEnable ()
    {
      LoadAnimatorObj ("hero_zf"); //TODO  测试用的
      ///注册英雄创建成功监听
      TDB.DbgoManager.RaiseLoadedEvent += OnAvatarLoaded;
    }

    void OnDisable ()
    {
      if (demoBhvr != null && demoBhvr.gameObject != null) {
        TDB.DbgoManager.Release (demoBhvr.gameObject, true);
      }
      TDB.DbgoManager.RaiseLoadedEvent -= OnAvatarLoaded;
    }
    // Update is called once per frame
    void Update ()
    {
    }

    private void LoadAnimatorObj (string resName)
    {
      GameObject obj = GetAvatar (resName);
      if (obj == null) {
        TDB.DbgoManager.LazyLoad (resName);
      }

    }

    private void OnAvatarLoaded (object sender, TDB.ResEventArgs args)
    {
      GameObject obj = GetAvatar (args.Name);
    }


    private GameObject GetAvatar (string resName)
    {
      GameObject obj = TDB.DbgoManager.FetchUnused (resName);
      if (obj != null) {
        obj.transform.parent = AnimatorObj.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = new Vector3 (30, 30, 1);
        obj.SetActive (true);
        demoBhvr = obj.GetComponent<TDB.DemoBhvr> ();
        Renderer objRender = obj.transform.GetComponentInChildren<Renderer> ();
        objRender.material.renderQueue = GetComponent<UIPanel> ().startingRenderQueue + 50;
      }
      return obj;
    }

    /// <summary>
    /// Refreshs the panel.刷新面板
    /// </summary>
    private void RefreshPanel ()
    {
      if (heroBase == null) {
        return;
      }
      this.RefreshPanel (this.heroBase);
    }

		/// <summary>
		/// Refreshs the panel.
		/// 刷新面板的数据
		/// </summary>
		/// <param name="data">Data.</param>
		public void RefreshPanel (HeroBase heroBase)
		{
			this.heroBase = heroBase;
			this.SetHeroName (heroBase.Xml.name);
			this.SetHeroNameFrame ((int)heroBase.Net.upgrade);
			this.SetLevel (heroBase.Net.level);
			this.SetScore (heroBase.Score);
			this.SetExp (heroBase.Net.exp, Config.levelUpXmlTable [heroBase.Net.level].val);
			this.SetStarList (heroBase.Net.evolve, true);
			int packageCount = 0;
			if (BaseCache.propsBaseTable.ContainsKey (heroBase.Xml.soul_rock_id)) {
				packageCount = BaseCache.propsBaseTable [heroBase.Xml.soul_rock_id].Net.count;
			}
			int mPackageCountMax = -1;
			if (Config.evolveXmlTable.ContainsKey (heroBase.Net.evolve + 1)) {
				mPackageCountMax = Config.evolveXmlTable [heroBase.Net.evolve].val;
			}
			this.SetHeroPackageSoulstone (packageCount, mPackageCountMax);
			this.SetEquipList (heroBase);
			SetHeroType (heroBase.Attribute_Type);
		}

    private void SetHeroType (AttributeTypeEnum propertyType)
    {
      string resName = "str_icon";
      switch (propertyType) {
      case AttributeTypeEnum.STR:
        resName = "str_icon";
        break;
      case AttributeTypeEnum.INT:
        resName = "int_icon";
        break;
      case AttributeTypeEnum.AGI:
        resName = "agi_icon";
        break;
      }
      this.HeroType.GetComponent<UISprite> ().spriteName = resName;
    }

    /// <summary>
    /// Sets the name of the hero.设置英雄名字
    /// </summary>
    void SetHeroName (string heroName)
    {
      HeroNameLabel.GetComponent<UILabel> ().text = heroName;
    }

    /// <summary>
    /// Sets the hero name frame.设置英雄名字框阶级
    /// </summary>
    void SetHeroNameFrame (int rank)
    {
      HeroNameFrame.GetComponent<UISprite> ().spriteName = "herodetail_name_frame_" + rank; //TODO  需要修改
    }

    /// <summary>
    /// Sets the level.设置英雄等级
    /// </summary>
    void SetLevel (int level)
    {
      LevelLabel.GetComponent<UILabel> ().text = level.ToString ();
    }

    /// <summary>
    /// Sets the score.显示英雄战斗力
    /// </summary>
    void SetScore (int score)
    {
      ScoreLabel.GetComponent<UILabel> ().text = score.ToString ();
    }

    /// <summary>
    /// Sets the exp.设置英雄当前经验以及最大经验值
    /// </summary>
    void SetExp (long exp, long expMax)
    {
      ExpLabel.GetComponent<UILabel> ().text = exp + " / " + expMax;
    }

    /// <summary>
    /// Sets the star list.设置英雄星级
    /// </summary>
    void SetStarList (int count, bool showBg)
    {
      StarList starList = StarList.GetComponent<StarList> ();
      starList.count = count;
      starList.showBackground = showBg;
      starList.Flush ();
    }

    /// <summary>
    /// 切换动画
    /// </summary>
    /// <param name="obj">Object.</param>
    void AnimatorObjOnClick (GameObject obj)
    {
      if (demoBhvr != null) {
        demoBhvr.GotoAndPlayNext ();
      }
    }

		/// <summary>
		/// Sets the hero package soulstone.设置英雄碎片数量和比例条
		/// </summary>
		void SetHeroPackageSoulstone (int fragmentsCount, int fragmentsCountMax)
		{
			UILabel lab = HeroPackageSoulstoneCountLabel.GetComponent<UILabel> ();

			if (fragmentsCountMax == -1) {
				lab.text = UIPanelLang.FULL_STAR;
				return;
			} else {
				lab.text = fragmentsCount + "/" + fragmentsCountMax;
			}
			float fillAmount = (float)fragmentsCount / (float)fragmentsCountMax;
			if (fillAmount > 1)
				fillAmount = 1;
			HeroPackageSoulstoneForegroundSprite.GetComponent<UISprite> ().fillAmount = fillAmount;
		}

		/// <summary>
		/// Sets the properties list.
		/// 设置穿戴的道具
		/// </summary>
		void SetEquipList (HeroBase heroBase)
		{
			List<TangGame.Xml.PropsXml> equipsList = heroBase.Xml.CurrentEquipListByRank (heroBase.Net.upgrade);
			EquipItem[] items = PropsTable.GetComponentsInChildren<EquipItem> ();
			for (int i = 0; i < items.Length; i++) {
				if (items[i] == null)
					continue;
				items[i].Flush (heroBase,equipsList[i]);
			}
			//FIXME  道具相关的数据更新
		}
		//切换详细属性／图鉴／技能升级的效果按钮
		public void ToggleButtonOnClick (GameObject obj)
		{
			UIToggle tg = obj.GetComponent<UIToggle> ();
			if (tg == null) {
				return;
			}

      tg.GetComponent<UIPlayTween> ().Play (true);
      if (UIToggle.GetActiveToggle (tg.group) != null) {
        if (ClossOtherSubPanel (tg) == 0) {
          this.GetComponent<UIPlayTween> ().Play (true);
        }
      } else {
        this.GetComponent<UIPlayTween> ().Play (true);
      }

    }
    //关闭其它面板
    public int ClossOtherSubPanel (UIToggle tg)
    {
      int count = 0;
      foreach (UIToggle item in UIToggle.list) {
        if (item.group == tg.group && !item.value) {
          UIPlayTween pt = item.GetComponent<UIPlayTween> ();
          if (pt.tweenTarget.activeSelf) {
            pt.Play (false);
            count++;
          }
        }
      }
      return count;
    }
  }
}