using UnityEngine;
using System.Collections;
using TangGame.UI;

namespace TangGame
{
	public class Global
	{
		/// UI使用的层
		public static int UILayer = 9;
		/// UI显示的父对象
		public static GameObject uiRoot;
		public static bool isDebugBuild = true;

		public static void Log (object message)
		{
			if (isDebugBuild) {
				Debug.Log (message);
			}
		}

		public static void LogWarning (object message)
		{
			if (isDebugBuild) {
				Debug.LogWarning (message);
			}
		}

		public static void LogError (object message)
		{
			if (isDebugBuild) {
				Debug.LogError (message);
			}
		}

		/// 道具正常边框的Sprite名称
		private static string[] PropsNormalFrame = new string[] {
			"item_frame_1",
			"item_frame_1",
			"item_frame_2",
			"item_frame_3",
			"item_frame_4",
			"item_frame_5"
		};
		/// 道具碎片边框的Sprite名称
		private static string[] PropsDebrisFrame = new string[] {
			"item_frame_1",
			"item_frame_1",
			"item_frame_2",
			"item_frame_3",
			"item_frame_4",
			"item_frame_5"
		};
		private static string[] mHeroTypeIcon = new string[]{ 
			"str_icon",
			"str_icon",
			"int_icon",
			"agi_icon"
		};
		private static Color32[] mColor32 = new Color32[]{
			new Color32 (171, 171, 171, 255),
			new Color32 (93, 255, 0, 255),
			new Color32 (0,192,255,255),
			new Color32 (236,43,228,255),
			new Color32(255,111,0,255)
		};
		/// <summary>
		/// Gets the color32 by rank.
		/// 根据品阶获取颜色
		/// </summary>
		/// <returns>The color32 by rank.</returns>
		/// <param name="rank">Rank.</param>
		public static Color32 GetColor32ByRank(int rank){
			if (rank > mColor32.Length) {
				return Color.gray;
			} else {
				return mColor32 [rank];
			}
		}
		/// <summary>
		/// Gets the hero type icon.
		/// 根据英雄类型获取名字资源
		/// </summary>
		/// <returns>The hero type icon.</returns>
		/// <param name="type">Type.</param>
		public static string GetHeroTypeIconName(AttributeTypeEnum type){
			return mHeroTypeIcon [(int)type];
		}

		/// <summary, >
		/// 获取道具边框的Sprite名称
		/// </summary>
		public static string GetPropFrameName (int upgrade){
			return GetPropFrameName (PropsType.NONE,upgrade);
		}
		/// <summary, >
		/// 获取道具边框的Sprite名称
		/// </summary>
		public static string GetPropFrameName (PropsType type, int upgrade)
		{
			if (upgrade < 0) {
				upgrade = 0;
			}
			if (upgrade > 4) {
				upgrade = 4;
			}
			if (type == PropsType.DEBRIS) {
				return PropsDebrisFrame [upgrade];
			} else {
				return PropsNormalFrame [upgrade];
			}
		}

		/// <summary>
		/// 获取英雄的品质边框
		/// </summary>
		/// <returns>英雄边框的Sprite名称</returns>
		/// <param name="upgrade">品质</param>
		public static string GetHeroIconFrame (int upgrade)
		{
			return "hero_icon_frame_" + upgrade;
		}

		/// <summary>
		/// 获取英雄名称的品质边框
		/// </summary>
		/// <returns>英雄名称边框的Sprite名称</returns>
		/// <param name="upgrade">品质</param>
		public static string GetHeroNameFrame (int upgrade)
		{
			return "herodetail_name_bg";//TODO + upgrade;
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
		public static int RankToUpgrade (int rank)
		{
			int val = rank * (rank - 1) / 2 + 1; //n(n-1)/2+1
			return val;
		}

		/// <summary>
		/// Gets the upgrade rem.
		/// 获取品质的余数
		/// </summary>
		/// <returns>The upgrade rem.</returns>
		public static int GetUpgradeRem (int upgrade)
		{
			int rankTmp = Global.UpgradeToRank (upgrade);
			int upgradeTmp = Global.RankToUpgrade (rankTmp);
			return upgrade - upgradeTmp;
		}

		/// 获取转换英雄的星阶，考虑到界面只能显示5个星的问题，需要转换
		public static int GetHeroStar (int evolve)
		{
			return evolve;
		}

    /// 获取StreamingAssets文件夹路径
    public static string GetStreamingAssetsPath(){
      string streamingAssetsPath = "";
      if(Application.platform == RuntimePlatform.IPhonePlayer){
        streamingAssetsPath = "file://" + Application.dataPath + "/Raw/";
      }else if(Application.platform == RuntimePlatform.Android){
        streamingAssetsPath = Application.streamingAssetsPath + "/";
      }else{
        streamingAssetsPath = "file://" + Application.dataPath + "/StreamingAssets/";
      }
      return streamingAssetsPath;
    }
	}
}