﻿using UnityEngine;
using System.Collections;

public class UITestData : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
//		27 23 20
		this.SetAllPropsBase ();
		this.SetHeroBase (27,1,10,6,27,8,1004,0,11,4);
		this.SetHeroBase (23,4,40,8,23,15,1005,0,11,5);
		this.SetPropsBase (1015,50);
		this.SetPropsBase (1016,88);
		this.SetPropsBase (1017,5);
	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	void SetHeroBase (int id,int evolve,int exp,int upgrade,int heroId,int level,int equipId,int equipLv,int equipExp,int equipLocal)
	{

		TangGame.UI.HeroBase herobase;
		if (!TangGame.UI.HeroCache.instance.heroBeseTable.ContainsKey (id)) {
			return;
		}
		herobase = TangGame.UI.HeroCache.instance.heroBeseTable [id];
		herobase.Net = new TangGame.Net.HeroNet ();
		herobase.Net.configId = herobase.Xml.id;
		herobase.Net.evolve = evolve;
		herobase.Net.exp = exp;
		herobase.Net.upgrade = upgrade;
		herobase.Net.id = heroId;
		herobase.Net.level = level;
		herobase.Net.equipList = new TangGame.Net.EquipNet[6];
		TangGame.Net.EquipNet equip;
		equip = new TangGame.Net.EquipNet ();
		equip.configId = equipId;
		equip.enchantsLv = equipLv;
		equip.enchantsExp = equipExp;
		herobase.Net.equipList [equipLocal] = equip;
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