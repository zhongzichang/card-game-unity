﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroItem : MonoBehaviour {

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

	private HeroBase data;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public HeroBase Data {
		get {
			return data;
		}
//		set {
//			data = value;
//		}
	}


	public void Flush(HeroBase hero){
		this.data = hero;
		
		//TODO next code is test ,need remove;
		data.HeroAvatar = this.HeroAvatarSprite.atlas.spriteList [this.Data.ConfigId].name;

		UpHeroRank ((int)data.HeroesRank);
		UpHeroAvatarSprite (data.HeroAvatar);
		UpHeroName (data.HeroName);
		Locked (data.Islock);
		UpLevel (data.Level);
		UpHeroType (data.HeroPropertyType);
		UpHeroFragments (data.FragmentsCount,data.FragmentsCountMax);
		SetStarList (data.Evolve);
	}

	private void UpHeroType(AttributeTypeEnum propertyType){
		string resName = "icon_str";
		switch(propertyType){
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
	private void UpProps(){
		//TODO upProps;
	}
	private void UpLevel(int level){
		this.Level.text = level.ToString();
	}
	private void UpHeroName(string heroName){
		this.HeroName.text = heroName;
	}
	private void UpHeroRank(int heroRank){
		string frameName;
		frameName = "hero_icon_frame_" + heroRank;
		this.HeroAvatarFrame.spriteName = frameName;
	}

	private void UpHeroFragments(int fragmentsCount, int fragmentsCountMax){
		this.CountLabel.text = fragmentsCount + "/" + fragmentsCount;
		this.ForegroundSprite.fillAmount = (float)data.FragmentsCount / (float)data.FragmentsCountMax;
	}

	private void UpHeroAvatarSprite(string heroAvatar){
		HeroAvatarSprite.spriteName = heroAvatar;
	}

	private void Locked(bool locked){
		PropsGrid.gameObject.SetActive (!locked);
		HeroPackageSoulstone.gameObject.SetActive (locked);
		Level.gameObject.SetActive (!locked);
	}
	void SetStarList(int count){
		starList.count = count;
		starList.Flush ();
	}
	                 


}
