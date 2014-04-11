using UnityEngine;
using System.Collections;

public class HeroBase{
	//英雄id
	private long id;
	//英雄配置id
	private int configId;
	//英雄名字
	private string heroName;
	//英雄图标
	private string heroAvatar;
	//英雄等级
	private int level;
	//英雄品阶等级
	private HeroesRankEnum heroesRank;
	//英雄属性 力量／敏捷／智力
	private HeroPropertyEnum heroPropertyType;
	//英雄是否解锁
	private bool islock;
	//碎片当前数量
	private int fragmentsCount;
	//碎片当前最大数量
	private int fragmentsCountMax;
	//英雄位置类型，前中后
	private HeroLocationEnum heroLocation;

	public long Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	public HeroLocationEnum HeroLocation {
		get {
			return heroLocation;
		}
		set {
			heroLocation = value;
		}
	}

	public int ConfigId {
		get {
			return configId;
		}
		set{
			configId = value;
		}
	}

	public string HeroName {
		get {
			return heroName;
		}
		set{
			heroName = value;
		}
	}

	public int Level {
		get {
			return level;
		}
		set {
			level = value;
		}
	}

	public HeroesRankEnum HeroesRank {
		get {
			return heroesRank;
		}
		set {
			heroesRank = value;
		}
	}

	public string HeroAvatar {
		get {
			return heroAvatar;
		}
		set{
			heroAvatar = value;
		}
	}

	public HeroPropertyEnum HeroPropertyType {
		get {
			return heroPropertyType;
		}
		set{
			heroPropertyType = value;
		}
	}

	public bool Islock {
		get {
			return islock;
		}
		set{
			islock = value;
		}
	}

	public int FragmentsCount {
		get {
			return fragmentsCount;
		}
		set{
			fragmentsCount = value;
		}
	}

	public int FragmentsCountMax {
		get {
			return fragmentsCountMax;
		}
		set{
			fragmentsCountMax = value;
		}
	}
}

public enum HeroPropertyEnum{
	NONE,
	STR,
	INT,
	AGI
}
public enum HeroLocationEnum{
	NONE,
	BEFORE,
	MEDIUM,
	LATER
}
public enum HeroesRankEnum{
	NONE,
	WHITE,
	GREEN,
	GREEN1,
	BLUE,
	BLUE1,
	BLUE2,
	PURPLE,
	PURPLE1,
	PURPLE2,
	PURPLE3,
	ORANGE

}
