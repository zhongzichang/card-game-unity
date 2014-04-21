﻿using UnityEngine;
using System.Collections;

public class InterfaceSubPanel : MonoBehaviour {
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

	///6个道具栏
	public GameObject Props1;
	public GameObject Props2;
	public GameObject Props3;
	public GameObject Props4;
	public GameObject Props5;
	public GameObject Props6;

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
	private HeroBase data;
	///星级列表
	public GameObject StarList;




	private ArrayList propsSpirtes = new ArrayList();
	// Use this for initialization
	void Start () {
		propsSpirtes.Add (Props1.GetComponentInChildren<UISprite>());
		propsSpirtes.Add (Props2.GetComponentInChildren<UISprite>());
		propsSpirtes.Add (Props3.GetComponentInChildren<UISprite>());
		propsSpirtes.Add (Props4.GetComponentInChildren<UISprite>());
		propsSpirtes.Add (Props5.GetComponentInChildren<UISprite>());
		propsSpirtes.Add (Props6.GetComponentInChildren<UISprite>());

		SkillInfoButton.GetComponent<UIToggle> ().optionCanBeNone = true;
		PictorialButton.GetComponent<UIToggle> ().optionCanBeNone = true;
		UIEventListener.Get (PropertyInfoButton.gameObject).onClick += ToggleButtonOnClick;
		UIEventListener.Get (SkillInfoButton.gameObject).onClick += ToggleButtonOnClick;
		UIEventListener.Get (PictorialButton.gameObject).onClick += ToggleButtonOnClick;
	}


	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// Refreshs the panel.刷新面板
	/// </summary>
	private void RefreshPanel(){
		if (data == null) {
			return;
		}
		this.RefreshPanel (this.data);
	}
	/// <summary>
	/// Refreshs the panel.刷新面板的数据
	/// </summary>
	/// <param name="data">Data.</param>
	public void RefreshPanel(HeroBase data){
		this.data = data;
		this.SetHeroName (data.HeroName);
		this.SetHeroNameFrame ((int)data.HeroesRank);
		this.SetLevel (data.Level);
		this.SetScore (data.Score);
		this.SetExp (data.Exp,data.ExpMax);
		this.SetStarList (data.Evolve,true);
		this.SetHeroPackageSoulstone (data.FragmentsCount,data.FragmentsCountMax);
//		this.SetPropsList ();
		SetHeroType (data.HeroPropertyType);
	}
	private void SetHeroType(HeroPropertyEnum propertyType){
		string resName = "icon_str";
		switch(propertyType){
		case HeroPropertyEnum.STR:
			resName = "icon_str";
			break;
		case HeroPropertyEnum.INT:
			resName = "icon_int";
			break;
		case HeroPropertyEnum.AGI:
			resName = "icon_agi";
			break;
		}
		this.HeroType.GetComponent<UISprite>().spriteName = resName;
	}
	/// <summary>
	/// Sets the name of the hero.设置英雄名字
	/// </summary>
	void SetHeroName(string heroName){
		HeroNameLabel.GetComponent<UILabel> ().text = heroName;
	}

	/// <summary>
	/// Sets the hero name frame.设置英雄名字框阶级
	/// </summary>
	void SetHeroNameFrame(int rank){
		HeroNameFrame.GetComponent<UISprite> ().spriteName = "herodetail_name_frame_" + rank;
	}
	/// <summary>
	/// Sets the level.设置英雄等级
	/// </summary>
	void SetLevel(int level){
		LevelLabel.GetComponent<UILabel> ().text = level.ToString();
	}
	/// <summary>
	/// Sets the score.显示英雄战斗力
	/// </summary>
	void SetScore(int score){
		ScoreLabel.GetComponent<UILabel> ().text = score.ToString();
	}
	/// <summary>
	/// Sets the exp.设置英雄当前经验以及最大经验值
	/// </summary>
	void SetExp(long exp,long expMax){
		ExpLabel.GetComponent<UILabel> ().text = exp + " / " + expMax;
	}
	/// <summary>
	/// Sets the star list.设置英雄星级
	/// </summary>
	void SetStarList(int count,bool showBg){
		StarList starList = StarList.GetComponent<StarList> ();
		starList.count = count;
		starList.showBackground = showBg;
		starList.Flush ();
	}

	/// <summary>
	/// Sets the hero package soulstone.设置英雄碎片数量和比例条
	/// </summary>
	void SetHeroPackageSoulstone(int fragmentsCount,int fragmentsCountMax){
		HeroPackageSoulstoneCountLabel.GetComponent<UILabel> ().text = fragmentsCount + "/" + fragmentsCountMax;
		float fillAmount = (float)fragmentsCount / (float)fragmentsCountMax;
		if (fillAmount > 1)
			fillAmount = 1;
		HeroPackageSoulstoneForegroundSprite.GetComponent<UISprite> ().fillAmount = fillAmount;
	}
	/// <summary>
	/// Sets the properties list.设置道具穿戴
	/// </summary>
	void SetPropsList(){
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
	public int ClossOtherSubPanel(UIToggle tg){
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