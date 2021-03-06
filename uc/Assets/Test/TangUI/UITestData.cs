﻿using UnityEngine;
using System.Collections;
using TangGame.Net;

using Restful;

public class UITestData : MonoBehaviour
{
  private UserApi userApi = new UserApi ();
	// Use this for initialization
	void Start ()
	{
//		27 23 20
		this.SetAllPropsBase ();
    this.SetHeroBase (1,1,10,6,23,8,1004,0,11,4);
    this.SetHeroBase (2,4,40,8,20,15,1005,1,11,5);
    this.SetHeroBase (3,4,40,8,27,15,1005,1,11,5);
    this.SetHeroBase (4,4,40,8,14,15,1005,1,11,5);
		this.SetPropsBase (1015,50);
		this.SetPropsBase (1016,88);
		this.SetPropsBase (1017,5);
    string userId = "1";
    HeroApi.getHeroes (userId, getHeroesResponseHandler);
	}
	/// <summary>
	/// Gets the heroes response handler.
	/// </summary>
	/// <param name="result">Result.</param>
	private void getHeroesResponseHandler (HeroResult result)
	{
    HeroNetItem[] heroNetItems = (HeroNetItem[])result.data;
    foreach (HeroNetItem heroNetItem in heroNetItems) {
      HeroNet heroNet = heroNetItem.Data;
			TangGame.UI.HeroCache.instance.UpdataMyHeroBaseTable (heroNet);
		}
	}

  void SetHeroBase (int id,int evolve,int exp,int rank,int configId,
    int level, int equipId,int equipLv,int equipExp,int equipLocal)
	{

    if (TangGame.Config.heroXmlTable.ContainsKey (configId)) {

      TangGame.UI.HeroBase herobase = new TangGame.UI.HeroBase ();

      herobase.Xml = TangGame.Config.heroXmlTable[configId];
      herobase.Net = new TangGame.Net.HeroNet ();
      herobase.Net.configId = herobase.Xml.id;
      herobase.Net.id = id;
      herobase.Net.star = evolve;
      herobase.Net.exp = exp;
      herobase.Net.rank = rank;
      herobase.Net.level = level;
      herobase.Net.equipList = new TangGame.Net.EquipNet[6];
      TangGame.Net.EquipNet equip;
      equip = new TangGame.Net.EquipNet ();
      equip.configId = equipId;
      equip.enchantsLv = equipLv;
      equip.enchantsExp = equipExp;
      herobase.Net.equipList [equipLocal] = equip;
      herobase.Net.skillLevel = new int[5];
      for (int i = 0; i < herobase.Net.skillLevel.Length; i++) {
        herobase.Net.skillLevel [i] = 20;
      }

			TangGame.UI.HeroCache.instance.UpdataMyHeroBaseTable (herobase.Net);
			TangGame.UI.HeroCache.instance.UpdataMyHeroBaseTable (herobase.Xml);

		}

	}

	void SetAllPropsBase(){
		foreach (TangGame.Xml.PropsData item in TangGame.Config.propsXmlTable.Values) {
			//TODO 测试数据
			TangGame.UI.Props props = new TangGame.UI.Props ();
			TangGame.Net.PropsNet net = new TangGame.Net.PropsNet ();
			net.configId = item.id;
			if (item.id % 2 == 0)
				net.count = 20;
			else
				net.count = 5;
			props.data = item;
			props.net = net;
			if (item.id % 3 == 1)
				TangGame.UI.PropsCache.instance.propsTable.Add (item.id, props);
		}
	}
	void SetPropsBase(int id,int count){
		TangGame.UI.Props props;
		if (!TangGame.UI.PropsCache.instance.propsTable.ContainsKey (id)) {
			return;
		}
		props = TangGame.UI.PropsCache.instance.propsTable[id];
		TangGame.Net.PropsNet net = new TangGame.Net.PropsNet ();
		net.count = count;
		net.id = props.data.id;
		props.net = net;

	}
}
