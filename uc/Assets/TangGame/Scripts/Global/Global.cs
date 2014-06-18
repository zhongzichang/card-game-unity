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

    /// <summary>
    /// 道具和英雄的颜色数组
    /// </summary>
		private static Color32[] mColor32 = new Color32[]{
      new Color32 (255, 255, 255, 255),
			new Color32 (171, 171, 171, 255),
			new Color32 (93, 255, 0, 255),
			new Color32 (0,192,255,255),
			new Color32 (236,43,228,255),
			new Color32(255,111,0,255)
    };

    /// <summary>
    /// 道具和英雄的颜色十六进制字串数组
    /// </summary>
    private static string[] mColorHex = new string[]{
      "FFFFFF",
      "ABABAB",
      "5DFF00",
      "00C0FF",
      "EC2BE4",
      "FF6F00"
    };

    /// 英雄品质阶段对应的当前的品阶值
    /// 1:1,0; 2:2,0; 3:2,1; 4:3,0; 5:3,1; 6:3,2; 7:4,0; 8:4,1; 9:4,2; 10:4,3 
    private static int[] HerorankNum = new int[]{0, 1, 2, 2, 3, 3, 3, 4, 4, 4, 4};
    
    /// 英雄品质阶段对应的当前+几的数量
    /// 1:1,0; 2:2,0; 3:2,1; 4:3,0; 5:3,1; 6:3,2; 7:4,0; 8:4,1; 9:4,2; 10:4,3 
    private static int[] HerorankAddNum = new int[]{0, 0, 0, 1, 0, 1, 2, 0, 1, 2, 3};

    
    //===================================================================================================
    //===================================================================================================


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
		public static string GetPropFrameName (int rank){
			return GetPropFrameName (PropsType.NONE,rank);
		}
		/// <summary, >
		/// 获取道具边框的Sprite名称
		/// </summary>
		public static string GetPropFrameName (PropsType type, int rank)
		{
			if (rank < 0) {
				rank = 0;
			}
			if (rank > 4) {
				rank = 4;
			}
			if (type == PropsType.DEBRIS) {
				return PropsDebrisFrame [rank];
			} else {
				return PropsNormalFrame [rank];
			}
		}

		/// <summary>
		/// 获取英雄的品质边框
		/// </summary>
		/// <returns>英雄边框的Sprite名称</returns>
		/// <param name="rank">品质</param>
		public static string GetHeroIconFrame (int rank)
		{
			rank = rank < 1 ? 1 : rank;
			return "hero_frame_" + rank;
		}

		/// <summary>
		/// 获取英雄名称的品质边框
		/// </summary>
		/// <returns>英雄名称边框的Sprite名称</returns>
		/// <param name="rank">品质</param>
		public static string GetHeroNameFrame (int rank)
		{
			return "herodetail_name_bg";//TODO + rank;
		}

    /// 根据品阶获取品阶的阶段颜色，如，白色（1），绿色（2），蓝色（3），紫色（4）
    public static Color32 GetHerorankColor(int rank){
      int index = GetHerorankNum(rank);
      return mColor32[index];
    }

    /// 根据品阶获取品阶的阶段颜色，如，白色（1），绿色（2），蓝色（3），紫色（4）
    public static string GetHerorankHexColor(int rank){
      int index = GetHerorankNum(rank);
      return mColorHex[index];
    }

    /// 根据品阶获取品阶的阶段值，如，白色（1），绿色（2），蓝色（3），紫色（4）
    public static int GetHerorankNum(int rank){
      if(rank < 0 || rank > HerorankNum.Length){
        return 0;
      }
      return HerorankNum[rank];
    }

		/// <summary>
		/// Gets the rank rem.
		/// 获取品质的余数，即当前品质的+几，如蓝色+2
		/// </summary>
		/// <returns>The rank rem.</returns>
		public static int GetHerorankRem (int rank)
		{
      if(rank < 0 || rank > HerorankAddNum.Length){
        return 0;
      }
      return HerorankAddNum[rank];
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

		#region====对象排序===

		/// <summary>
		/// 道具根据附魔点进行排序
		/// </summary>
		/// <returns>The sort by enchants ponts.</returns>
		/// <param name="propsA">Properties a.</param>
		/// <param name="propsB">Properties b.</param>
		static public int PropsSortByEnchantsPonts (Props propsA, Props propsB)
		{
			return propsA.data.enchant_points.CompareTo (propsB.data.enchant_points);
		}
		#endregion===========
	}
}