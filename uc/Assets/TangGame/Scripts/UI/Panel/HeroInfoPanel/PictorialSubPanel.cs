using UnityEngine;
using System.Collections;

public class PictorialSubPanel : MonoBehaviour {

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
	void Start () {
		//FIXME remove this code ,that only to test!
		SetCardTexture ("card_bg_big_2");
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
		SetHeroName (data.CardHeroName);
		SetPropertyType (data.HeroPropertyType);
//		SetSkillGroup ();
		SetCardTexture (data.CardName);
		SetBackground ((int)data.HeroesRank);
		SetStarList (data.Evolve);
	}
	void SetStarList(int count){
		StarList.GetComponent<StarList> ().count = count;
		StarList.GetComponent<StarList> ().Flush ();
	}
	/// <summary>
	/// Sets the name of the hero.设置英雄名字
	/// </summary>
	void SetHeroName(string cardHeroName){
		this.HeroName.GetComponent<UISprite>().spriteName = "card_name_"+ cardHeroName +"big";
	}
	/// <summary>
	/// Sets the type of the property.设置英雄属性类型
	/// </summary>
	void SetPropertyType(HeroPropertyEnum type){
		string resName = "icon_str";
		switch(type){
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

	void SetSkillGroup(){
		//FIXME 修改界面上技能的图标
	}
	void SetBackground(int rank){
		string bgColor = "";
		if (rank < (int)HeroesRankEnum.GREEN) {
			bgColor = "white";
		} else if (rank < (int)HeroesRankEnum.BLUE) {
			bgColor = "green";
		} else if (rank < (int)HeroesRankEnum.PURPLE) {
			bgColor = "blue";
		} else if (rank < (int)HeroesRankEnum.ORANGE) {
			bgColor = "purple";
		} else {
			bgColor  = "orange";
		}

		Background.GetComponent<UISprite> ().spriteName = "card_bg_" + bgColor;
	}
	/// <summary>
	/// Sets the card texture 
	/// </summary>
	void SetCardTexture(string cardName){
		Object card = Resources.Load ("Textures/SanguoUI/art/" + cardName);
		this.Texture.GetComponent<UITexture> ().mainTexture =  card  as Texture2D;
	}

	void OnClick(){
		isChecked = !isChecked;
		UIPlayTween pt = GetComponent<UIPlayTween> ();
		pt.Play (true);
	}

}
