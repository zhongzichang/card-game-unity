using UnityEngine;
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
		set {
			data = value;
		}
	}


	public void Flush(){
		UpHeroRank ();
		UpHeroAvatarSprite ();
		UpHeroName ();
		Locked ();
		UpLevel ();
		UpHeroType ();
		UpHeroFragments ();
	}

	private void UpHeroType(){
		string resName = "icon_str";
		switch(data.HeroPropertyType){
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
		this.HeroType.spriteName = resName;
	}
	private void UpProps(){
		//TODO upProps;
	}
	private void UpLevel(){
		this.Level.text = data.Level.ToString();
	}
	private void UpHeroName(){
		this.HeroName.text = data.HeroName;
	}
	private void UpHeroRank(){
		string frameName;
		frameName = "hero_icon_frame_" + (int)data.HeroesRank;
		this.HeroAvatarFrame.spriteName = frameName;
	}

	private void UpHeroFragments(){
		this.CountLabel.text = data.FragmentsCount + "/" + data.FragmentsCountMax;
		this.ForegroundSprite.fillAmount = (float)data.FragmentsCount / (float)data.FragmentsCountMax;
	}

	private void UpHeroAvatarSprite(){
		HeroAvatarSprite.spriteName = data.HeroAvatar;
	}

	private void Locked(){
		bool locked = data.Islock;
		PropsGrid.gameObject.SetActive (!locked);
		HeroPackageSoulstone.gameObject.SetActive (locked);
		Level.gameObject.SetActive (!locked);
	}


}
