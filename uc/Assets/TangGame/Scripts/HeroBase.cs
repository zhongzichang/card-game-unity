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



	/***
	 * 英雄唯一id
	 * */
	public long Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
	/***
	 * 英雄星级
	 * */
	public  int Evolve{
		get{ 
			return evolve;
			}
		set{
			evolve = value;
		}
	}
	/***
	 * 英雄位置
	 * */
	public HeroLocationEnum HeroLocation {
		get {
			return heroLocation;
		}
		set {
			heroLocation = value;
		}
	}
	/***
	 * 英雄配置id
	 * */
	public int ConfigId {
		get {
			return configId;
		}
		set{
			configId = value;
		}
	}
	/***
	 * 英雄名字
	 * */
	public string HeroName {
		get {
			return heroName;
		}
		set{
			heroName = value;
		}
	}
	/***
	 * 英雄等级
	 * */
	public int Level {
		get {
			return level;
		}
		set {
			level = value;
		}
	}
	/***
	 * 英雄品阶
	 */

	public HeroesRankEnum HeroesRank {
		get {
			return heroesRank;
		}
		set {
			heroesRank = value;
		}
	}
	/***
	 * 英雄头像
	 */
	public string HeroAvatar {
		get {
			return heroAvatar;
		}
		set{
			heroAvatar = value;
		}
	}
	/***
	 * 英雄属性类型
	 * */
	public HeroPropertyEnum HeroPropertyType {
		get {
			return heroPropertyType;
		}
		set{
			heroPropertyType = value;
		}
	}
	/***
	 * 英雄是否为解锁
	 * */
	public bool Islock {
		get {
			return islock;
		}
		set{
			islock = value;
		}
	}
	/***
	 * 碎片数量
	 * */
	public int FragmentsCount {
		get {
			return fragmentsCount;
		}
		set{
			fragmentsCount = value;
		}
	}
	/***
	 * 进化最大数量
	 * */
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
