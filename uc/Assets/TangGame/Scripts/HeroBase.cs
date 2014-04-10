using UnityEngine;
using System.Collections;

public class HeroBase{
	private long id;
	private int configId;
	private string heroName;
	private string heroAvatar;
	private int level;
	private HeroesRankEnum heroesRank;
	private HeroPropertyEnum heroPropertyType;
	private bool islock;
	private int fragmentsCount;
	private int fragmentsCountMax;
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
