using System.Collections.Generic;
using UnityEngine;

namespace TangLevel
{
	public class Mocker
	{
		public static void Configure ()
		{
			Level level = MockGrassLevel ();
			if (!Config.levelTable.ContainsKey (level.id))
				Config.levelTable.Add (level.id, level);
		}

		public static Level MockGrassLevel ()
		{

			Level level = new Level ();
			level.id = 1;
			level.name = "Test Level 1";

			level.subLeves = new SubLevel[3];
			for (int i = 1; i < 4; i++) {
				SubLevel subLevel = new SubLevel ();
				subLevel.id = i;
				subLevel.index = i - 1;
				if (i == 1) {
					subLevel.resName = "lbg_grass1";
				} else if (i == 2) {
					subLevel.resName = "lbg_grass2";
				} else if (i == 3) {
					subLevel.resName = "lbg_grass3";
				}

				level.subLeves [subLevel.index] = subLevel;
				subLevel.enemyGroup = MockGroup ();

			}
			return level;
		}

		static int tmpG = 0;

		public static Group MockGroup (int count)
		{

			Group group = new Group ();
			group.heros = new Hero[ count];
			tmpG++;
			for (int j = 0; j < group.heros.Length; j++) {
				if (j == 0) {
					group.heros [j] = MockHeroZf ();
				} else if (j == 1) {
					group.heros [j] = MockHeroXc ();
				} else if (j == 2) {
					group.heros [j] = MockHeroZhangfei ();
				} else if (j == 3) {
					group.heros [j] = MockHeroHuatuo ();
				}
			}
			return group;
		}

		public static Group MockGroup (int[] heroIds)
		{
			Group group = new Group ();
			group.heros = new Hero[heroIds.Length];
			for (int i = 0; i < group.heros.Length; i++) {
				if (heroIds [i] == 1) {
					group.heros [i] = MockHeroZf ();
				} else if (heroIds [i] == 2) {
					group.heros [i] = MockHeroXc ();
				} else if (heroIds [i] == 3) {
					group.heros [i] = MockHeroZhangfei ();
				} else if (heroIds [i] == 4) {
					group.heros [i] = MockHeroHuatuo ();
				}
			}
			return group;
		}

		public static Group MockGroup ()
		{

			Group group = new Group ();
			int maxHeros = 5;
			group.heros = new Hero[ UnityEngine.Random.Range (4, maxHeros)];
			for (int j = 0; j < group.heros.Length; j++) {
				int id = UnityEngine.Random.Range (3, 5);
				if (id == 0)
					group.heros [j] = MockHeroZf ();
				else if (id == 1)
					group.heros [j] = MockHeroXc ();
				else if (id == 2) {
					group.heros [j] = MockHeroZhangfei ();
				} else if (id == 3) {
					//group.heros [j] = MockHeroHuatuo ();
          group.heros [j] = MockHeroZf ();
				} else if (id == 4) {
					group.heros [j] = MockHeroHuangjinbubing ();
				} else if (id == 5) {
					group.heros [j] = MockHeroHuangjingongjianbing ();
					//group.heros [j] = MockHeroXc ();
				}
			}
			return group;
		}

		static int tmp = 0;
		// mock hero zf -----------------------------------------------------

		#region zf

		public static Hero MockHeroZf ()
		{
			Hero hero = new Hero ();  
			hero.id = 1;
			hero.resName = "hero_zf";
			hero.maxHp = 100;
			hero.hp = 100;
			hero.maxMp = 100;
			hero.mp = 0;
			hero.sort = 3;//UnityEngine.Random.Range (1, 20);
			if (tmp % 2 == 0)
				hero.ai = new string[]{ "AutoFire" };
			else
				hero.ai = new string[]{ "AutoFire" };

			// skill
			List<Skill> skills = new List<Skill> ();
			skills.Add (MockZfAttackSkill ());
			skills.Add (MockBingHuaSkill ());
			skills.Add (MockYuehuaSkill ());
			//if( hero.id == 0)
			skills.Add (MockLuoshenSkill ());
			hero.skills = skills;
			hero.skillQueue = new int[]{ 0, 1, 2 };


			tmp++;
			return hero;
		}

		public static Skill MockZfAttackSkill ()
		{
			Skill skill = new Skill ();
			skill.effectors = new Effector[1];
			skill.effectors [0] = MockLineFlyEffector ();
			//skill.chargeClip = "binghua";
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 20;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockBingHuaSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockLineFlyEffector ();
			skill.chargeClip = "binghua0";
			skill.releaseClip = "binghua1";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 20;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockYuehuaSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockLineFlyEffector ();
			//skill.chargeClip = "";
			skill.releaseClip = "yuehua";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 20;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockLuoshenSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockLineFlyEffector ();
			//skill.chargeClip = "luoshen";
			skill.releaseClip = "luoshen";
			//skill.releaseSpecials = new string[2];
			//skill.releaseSpecials[0] = "Sprite_luoshen1";
			//skill.releaseSpecials[1] = "Sprite_luoshen2";
			skill.enable = true;
			skill.bigMove = true;
			skill.chargeTime = 2F;
			skill.distance = 20;
			skill.cd = 3;

			return skill;
		}
		// 线性飞行
		public static Effector MockLineFlyEffector ()
		{

			Effector effector = new Effector ();
			effector.specialName = "fx_binghua";

			Effector[] subEffectors = new Effector[1];
			subEffectors [0] = MockHitEffector ();
			effector.subEffectors = subEffectors;

			return effector;
		}
		// 命中
		public static Effector MockHitEffector ()
		{
			Effector effector = new Effector ();
			effector.specialName = "fx_binghuajizhong";
			return effector;
		}

		#endregion

		// mock hero xc -----------------------------------------------------

		#region xc

		public static Hero MockHeroXc ()
		{
			Hero hero = new Hero ();  
			hero.id = 2;
			hero.resName = "hero_xc";
			hero.maxHp = 300;
			hero.hp = 300;
			hero.maxMp = 100;
			hero.mp = 0;
			hero.sort = 1;//UnityEngine.Random.Range (1, 20);
			hero.ai = new string[]{ "AutoFire", "JumpRunning" };

			// skill
			List<Skill> skills = new List<Skill> ();
			skills.Add (MockXcAttackSkill ());
			skills.Add (MockQianjinzhuiSkill ());
			skills.Add (MockZhongjiSkill ());
			skills.Add (MockYemansicheSkill ());
			hero.skills = skills;
			hero.skillQueue = new int[]{ 0, 2, 3 };


			tmp++;
			return hero;
		}

		public static Skill MockXcAttackSkill ()
		{
			Skill skill = new Skill ();
			skill.effectors = new Effector[1];
			skill.effectors [0] = MockCommonHit ();
			//skill.chargeClip = "binghua0";
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 10;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockQianjinzhuiSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockQianjinzhuiEffector ();
			//skill.chargeClip = "qianjinzhui";
			skill.releaseClip = "qianjinzhui";
			skill.enable = true;
			skill.bigMove = true;
			skill.distance = 10;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockZhongjiSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockCommonHit ();
			skill.chargeClip = "zhongji";
			skill.releaseClip = "zhongji1";
			//skill.releaseSpecials = new string[2];
			//skill.releaseSpecials[0] = "Sprite_luoshen1";
			//skill.releaseSpecials[1] = "Sprite_luoshen2";
			skill.enable = true;
			skill.bigMove = false;
			skill.chargeTime = 2F;
			skill.distance = 10;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockYemansicheSkill ()
		{
			Skill skill = new Skill ();

			skill.releaseEffectors = new Effector[1];
			skill.releaseEffectors [0] = MockYemansicheEffector ();
			//skill.chargeClip = "yemansiche";
			skill.releaseClip = "yemansiche";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 10;
			skill.cd = 3;

			return skill;
		}
		// 千斤坠
		public static Effector MockQianjinzhuiEffector ()
		{

			Effector effector = new Effector ();
			effector.specialName = "fx_qianjinzhui";
			return effector;
		}

		public static Effector MockYemansicheEffector ()
		{

			Effector effector = new Effector ();
			effector.specialName = "fx_yemansiche";
			return effector;
		}

		#endregion

		// mock hero zhangfei -----------------------------------------------------

		#region zhangfei

		public static Hero MockHeroZhangfei ()
		{
			Hero hero = new Hero ();  
			hero.id = 3;
			hero.resName = "hero_zhangfei";
			hero.maxHp = 300;
			hero.hp = 300;
			hero.maxMp = 100;
			hero.mp = 0;
			hero.sort = 2;//UnityEngine.Random.Range (1, 20);
			hero.ai = new string[]{ "AutoFire" };

			// skill
			List<Skill> skills = new List<Skill> ();
			skills.Add (MockZhangfeiAttackSkill ());
			skills.Add (MockShenliSkill ());
			skills.Add (MockBaqiSkill ());
			skills.Add (MockDaheSkill ());
			hero.skills = skills;
			hero.skillQueue = new int[]{ 0, 2, 3 };


			tmp++;
			return hero;
		}

		public static Skill MockZhangfeiAttackSkill ()
		{
			Skill skill = new Skill ();
			skill.effectors = new Effector[1];
			skill.effectors [0] = MockCommonHit ();
			//skill.chargeClip = "binghua0";
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 10;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockShenliSkill ()
		{
			Skill skill = new Skill ();

      skill.targetType = Skill.TARGET_LOCKED;
			skill.effectors = new Effector[1];
			skill.effectors [0] = MockShenliEffect ();
			//skill.chargeClip = "shenli";
			skill.releaseClip = "shenli";
			skill.releaseSpecials = new string[1];
			skill.releaseSpecials [0] = "fx_shenli_skill";
			skill.enable = true;
			skill.bigMove = true;
			skill.distance = 20;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockBaqiSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockCommonHit ();
			skill.chargeClip = "baqi";
			skill.releaseClip = "baqi1";
			//skill.releaseSpecials = new string[2];
			//skill.releaseSpecials[0] = "Sprite_luoshen1";
			//skill.releaseSpecials[1] = "Sprite_luoshen2";
			skill.enable = true;
			skill.bigMove = false;
			skill.chargeTime = 2F;
			skill.distance = 10;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockDaheSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockDaheEffect ();
			skill.releaseClip = "dahe";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 10;
			skill.cd = 3;

			return skill;
		}

		public static Effector MockDaheEffect ()
		{


			Effector effector = new Effector ();
			effector.specialName = "fx_dahe";
			effector.subEffectors = new Effector[1];
			effector.subEffectors [0] = MockXuanyunEffect ();

			return effector;
		}

		public static Effector MockShenliEffect ()
		{


			Effector effector = new Effector ();
			effector.specialName = "fx_shenli";

			return effector;
		}

		#endregion

		// mock hero huatuo -----------------------------------------------------

		#region huatuo

		public static Hero MockHeroHuatuo ()
		{
			Hero hero = new Hero ();  
			hero.id = 4;
			hero.resName = "hero_ht";
			hero.maxHp = 120;
			hero.hp = 120;
			hero.maxMp = 100;
			hero.mp = 0;
			hero.sort = 2;//UnityEngine.Random.Range (1, 20);
			hero.ai = new string[]{ "AutoFire" };

			// skill
			List<Skill> skills = new List<Skill> ();
			//skills.Add (MockMediumCommonAttack ());
			skills.Add (MockHuatouAttack ());
			skills.Add (MockDuliSkill ());
			skills.Add (MockXumingSkill ());
			skills.Add (MockMiaoshouhuichunSkill());
			hero.skills = skills;
			hero.skillQueue = new int[]{ 0, 1, 2 ,3};
			tmp++;
			return hero;
		}

		public static Skill MockHuatouAttack ()
		{

			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockHuatuoAttackEffector ();
			//skill.chargeClip = "binghua0";
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 35;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockDuliSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockDuliEffector ();
			//skill.chargeClip = "binghua0";
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 35;
			skill.cd = 3;

			return skill;
		}
		public static Skill MockXumingSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockXumingEffector ();
			//skill.chargeClip = "binghua0";
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 35;
			skill.cd = 3;

			return skill;
		}

		public static Skill MockMiaoshouhuichunSkill ()
		{
			Skill skill = new Skill ();

			skill.effectors = new Effector[1];
			skill.effectors [0] = MockMiaoshouhuichunEffector ();
			//skill.chargeClip = "binghua0";
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = true;
			skill.distance = 35;
			skill.cd = 3;

			return skill;
		}
		// 续命
		public static Effector MockXumingEffector ()
		{
			Effector effector = new Effector ();
			effector.specialName = "fx_xuming";

			Effector[] subEffectors = new Effector[1];
			subEffectors [0] = MockCommonHit ();
			effector.subEffectors = subEffectors;

			return effector;
		}
		// 妙手回春
		public static Effector MockMiaoshouhuichunEffector ()
		{
			Effector effector = new Effector ();
			effector.specialName = "fx_miaoshouhuichun";

			Effector[] subEffectors = new Effector[1];
			subEffectors [0] = MockCommonHit ();
			effector.subEffectors = subEffectors;

			return effector;
		}
		// 毒理
		public static Effector MockDuliEffector ()
		{
			Effector effector = new Effector ();
			effector.specialName = "fx_duli";

			Effector[] subEffectors = new Effector[1];
			subEffectors [0] = MockCommonHit ();
			effector.subEffectors = subEffectors;

			return effector;
		}
		// 普通攻击作用器
		public static Effector MockHuatuoAttackEffector ()
		{
			Effector effector = new Effector ();
			effector.specialName = "fx_huatuogongji";

			Effector[] subEffectors = new Effector[1];
			subEffectors [0] = MockCommonHit ();
			effector.subEffectors = subEffectors;

			return effector;
		}

		#endregion

		// mock hero huangjinbubin -----------------------------------------------------

		#region huangjinbubin

		public static Hero MockHeroHuangjinbubing ()
		{
			Hero hero = new Hero ();  
			hero.id = 100;
			hero.resName = "hero_huangjinbubing";
			hero.maxHp = 120;
			hero.hp = 120;
			hero.maxMp = 100;
			hero.mp = 0;
			hero.sort = 2;//UnityEngine.Random.Range (1, 20);
			hero.ai = new string[]{ "AutoFire" };

			List<Skill> skills = new List<Skill> ();
			skills.Add (MockNearCommonAttack ());
			hero.skills = skills;
			hero.skillQueue = new int[]{ 0 };

			return hero;
		}

		#endregion

		// mock hero huangjingongjianbin -----------------------------------------------------

		#region huangjingongjianbing

		public static Hero MockHeroHuangjingongjianbing ()
		{
			Hero hero = new Hero ();  
			hero.id = 101;
			hero.resName = "hero_huangjingongjianbing";
			hero.maxHp = 120;
			hero.hp = 120;
			hero.maxMp = 100;
			hero.mp = 0;
			hero.sort = 2;//UnityEngine.Random.Range (1, 20);
			hero.ai = new string[]{ "AutoFire" };

			List<Skill> skills = new List<Skill> ();
			skills.Add (MockLongCommonAttack ());
			hero.skills = skills;
			hero.skillQueue = new int[]{ 0 };

			return hero;
		}

		#endregion

		#region commonfx

		public static Effector MockXuanyunEffect ()
		{

			Effector effector = new Effector ();
			effector.specialName = "fx_xuanyun";

			return effector;
		}

		#endregion

		#region common_heros

		public static Hero MockCommonHero (string resName)
		{
			Hero hero = new Hero ();  
			hero.id = 10000;
			hero.resName = resName;
			hero.maxHp = 120;
			hero.hp = 120;
			hero.maxMp = 100;
			hero.mp = 0;
			hero.sort = 2;
			hero.ai = new string[]{ "AutoFire" };

			// skill
			List<Skill> skills = new List<Skill> ();
			skills.Add (MockNearCommonAttack ());
			hero.skills = skills;
			hero.skillQueue = new int[]{ 0 };


			return hero;
		}

		public static Skill MockNearCommonAttack ()
		{

			Skill skill = new Skill ();
			skill.effectors = new Effector[1];
			skill.effectors [0] = MockCommonHit ();
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 7;
			skill.cd = 3;

			return skill;

		}

		public static Skill MockMediumCommonAttack ()
		{

			Skill skill = new Skill ();
			skill.effectors = new Effector[1];
			skill.effectors [0] = MockCommonHit ();
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 17;
			skill.cd = 3;

			return skill;

		}

		public static Skill MockLongCommonAttack ()
		{

			Skill skill = new Skill ();
			skill.effectors = new Effector[1];
			skill.effectors [0] = MockCommonHit ();
			skill.releaseClip = "attack";
			skill.enable = true;
			skill.bigMove = false;
			skill.distance = 27;
			skill.cd = 3;

			return skill;

		}

		public static Effector MockCommonHit ()
		{

			Effector effector = new Effector ();
			effector.specialName = "fx_jizhong";
			return effector;
		}

		#endregion
	}
}

