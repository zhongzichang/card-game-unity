using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HeroBase
{
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
	//英雄星级
	private int evolve;
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
	private int score;
	private int exp;
	private int expMax;
	private string cardHeroName;
	private string cardName;
	private int skillNum;

	public Dictionary<int,SkillBase> skillbases = new Dictionary<int, SkillBase> ();
	/// <summary>
	/// Gets or sets the Experience.
	/// </summary>
	/// <value>The exp.</value>
	public int Exp {
		get {
			return exp;
		}
		set {
			exp = value;
		}
	}

	public int SkillNum {
		get {
			return skillNum;
		}
		set {
			skillNum = value;
		}
	}

	public string CardName {
		get { 
			return cardName;
		}
		set {
			cardName = value; 
		}
	}

	/// <summary>
	/// Gets or sets the name of the card hero.英雄图鉴上的名字图片
	/// </summary>
	/// <value>The name of the card hero.</value>
	public string CardHeroName {
		get {
			return cardHeroName;
		}
		set {
			cardHeroName = value;
		}
	}

	/// <summary>
	/// Gets or sets the Experience max.
	/// </summary>
	/// <value>The exp max.</value>
	public int ExpMax {
		get {
			return expMax;
		}
		set {
			expMax = value;
		}
	}

	/// <summary>
	/// Gets or sets the identifier.英雄唯一id
	/// </summary>
	/// <value>The identifier.</value>
	public long Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	/// <summary>
	/// Gets or sets the evolve.英雄星级
	/// </summary>
	/// <value>The evolve.</value>
	public  int Evolve {
		get { 
			return evolve;
		}
		set {
			evolve = value;
		}
	}

	/// <summary>
	/// Gets or sets the hero location.英雄位置
	/// </summary>
	/// <value>The hero location.</value>
	public HeroLocationEnum HeroLocation {
		get {
			return heroLocation;
		}
		set {
			heroLocation = value;
		}
	}

	/// <summary>
	/// Gets or sets the config identifier.英雄配置id
	/// </summary>
	/// <value>The config identifier.</value>
	public int ConfigId {
		get {
			return configId;
		}
		set {
			configId = value;
		}
	}

	/// <summary>
	/// Gets or sets the name of the hero.英雄名字
	/// </summary>
	/// <value>The name of the hero.</value>
	public string HeroName {
		get {
			return heroName;
		}
		set {
			heroName = value;
		}
	}

	/// <summary>
	/// Gets or sets the level.英雄等级
	/// </summary>
	/// <value>The level.</value>
	public int Level {
		get {
			return level;
		}
		set {
			level = value;
		}
	}

	/// <summary>
	/// Gets or sets the heroes rank.英雄品阶
	/// </summary>
	/// <value>The heroes rank.</value>
	public HeroesRankEnum HeroesRank {
		get {
			return heroesRank;
		}
		set {
			heroesRank = value;
		}
	}

	/// <summary>
	/// Gets or sets the hero avatar.英雄头像
	/// </summary>
	/// <value>The hero avatar.</value>
	public string HeroAvatar {
		get {
			return heroAvatar;
		}
		set {
			heroAvatar = value;
		}
	}

	/// <summary>
	/// Gets or sets the type of the hero property.英雄属性类型
	/// </summary>
	/// <value>The type of the hero property.</value>
	public HeroPropertyEnum HeroPropertyType {
		get {
			return heroPropertyType;
		}
		set {
			heroPropertyType = value;
		}
	}

	/// <summary>
	/// Gets or sets a value indicating whether this instance islock.英雄是否为解锁
	/// </summary>
	/// <value><c>true</c> if this instance islock; otherwise, <c>false</c>.</value>
	public bool Islock {
		get {
			return islock;
		}
		set {
			islock = value;
		}
	}

	/// <summary>
	/// Gets or sets the fragments count.碎片数量
	/// </summary>
	/// <value>The fragments count.</value>
	public int FragmentsCount {
		get {
			return fragmentsCount;
		}
		set {
			fragmentsCount = value;
		}
	}

	/// <summary>
	/// Gets or sets the fragments count max.进化最大数量
	/// </summary>
	/// <value>The fragments count max.</value>
	public int FragmentsCountMax {
		get {
			return fragmentsCountMax;
		}
		set {
			fragmentsCountMax = value;
		}
	}

	/// <summary>
	/// Gets or sets the score.战斗力
	/// </summary>
	/// <value>The score.</value>
	public int Score {
		get {
			return score;
		}
		set {
			score = value;
		}
	}
}

public enum HeroPropertyEnum
{
	NONE,
	STR,
	INT,
	AGI
}

public enum HeroLocationEnum
{
	NONE,
	BEFORE,
	MEDIUM,
	LATER
}

public enum HeroesRankEnum
{
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
