using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

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
			return "herodetail_name_frame_" + upgrade;
		}
	}
}