using UnityEngine;
using System.Collections;
using TangGame.UI;

namespace TangGame{
	public class Global {

		/// UI使用的层
		public static int UILayer = 9;
		/// UI显示的父对象
		public static GameObject uiRoot;

		/// 道具正常边框的Sprite名称
		private static string[] PropsNormalFrame = new string[]{"equip_frame_white", "equip_frame_green", "equip_frame_blue", "equip_frame_purple", "equip_frame_orange"};
		/// 道具碎片边框的Sprite名称
		private static string[] PropsDebrisFrame = new string[]{"fragment_frame_white", "fragment_frame_green", "fragment_frame_blue", "fragment_frame_purple", "fragment_frame_orange"};
		/// <summary>
		/// 英雄图鉴边框
		/// </summary>
		private static string[] PictorialFrame = new string[]{"card_bg_white", "card_bg_green", "card_bg_blue", "card_bg_purple", "card_bg_orange"};
		/// <summary, >
		/// 获取道具边框的Sprite名称
		/// </summary>
		public static string GetPropFrameName(PropsType type, int upgrade){
			if(upgrade < 0){upgrade = 0;}
			if(upgrade > 4){upgrade = 4;}
			if(type == PropsType.DEBRIS){
				return PropsDebrisFrame[upgrade];
			}else{
				return PropsNormalFrame[upgrade];
			}
		}

		/// <summary>
		/// 获取英雄的品质边框
		/// </summary>
		/// <returns>英雄边框的Sprite名称</returns>
		/// <param name="upgrade">品质</param>
		public static string GetHeroIconFrame(int upgrade){
			return "hero_icon_frame_" + upgrade;
		}

		/// <summary>
		/// 获取英雄名称的品质边框
		/// </summary>
		/// <returns>英雄名称边框的Sprite名称</returns>
		/// <param name="upgrade">品质</param>
		public static string GetHeroNameFrame(int upgrade){
			return "herodetail_name_bg";//TODO + upgrade;
		}
		/// <summary>
		/// Gets the hero pictorial frame.
		/// 获取英雄图鉴边框
		/// </summary>
		/// <returns>The hero pictorial frame.</returns>
		/// <param name="upgrade">Upgrade.</param>
		public static string GetHeroPictorialFrame(int upgrade){
			return PictorialFrame[UpgradeToRank(upgrade)];
		}

		/// <summary>
		/// Upgrades to rank.
		/// 英雄品质转换
		/// </summary>
		/// <returns>The to rank.</returns>
		/// <param name="upgrade">Upgrade.</param>
		public static int UpgradeToRank (int upgrade)
		{
			float val = (float)Mathf.Sqrt ((float)(2 * upgrade + 0.25)) - (float)0.5;
			int rank = Mathf.CeilToInt (val);
			return rank;
		}
		/// <summary>
		/// Ranks to upgrade.
		/// 英雄品质转换
		/// </summary>
		/// <returns>The to upgrade.</returns>
		/// <param name="rank">Rank.</param>
		public static int RankToUpgrade(int rank){
			int val = rank * (rank - 1) / 2 + 1; //n(n-1)/2+1
			return val;
		}
		/// <summary>
		/// Gets the upgrade rem.
		/// 获取品质的余数
		/// </summary>
		/// <returns>The upgrade rem.</returns>
		public static int GetUpgradeRem(int upgrade){
			int rankTmp = Global.UpgradeToRank(upgrade);
			int upgradeTmp = Global.RankToUpgrade (rankTmp);
			return upgrade - upgradeTmp;
		}

		/// 获取转换英雄的星阶，考虑到界面只能显示5个星的问题，需要转换
		public static int GetHeroStar(int evolve){
			return evolve;
		}
	}
}