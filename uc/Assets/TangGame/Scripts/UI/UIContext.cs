// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using TangUI;
using System.Collections;

namespace TangGame
{
	public class UIContext
	{

		// ****************** panels *******************
		public const string MAIN_HEAD_PANEL_NAME = "MainHeadPanel";
		public const string MAIN_STATUS_PANEL_NAME = "MainStatusPanel";
		public const string MAIN_POPUP_PANEL_NAME = "MainPopupPanel";
		public const string HERO_VIEW_PANEL_NAME = "HeroViewPanel";
		/// <summary>
		/// HER o_ INF o_ PANE l_ NAM.
		/// 英雄队列面板
		/// </summary>
		public const string HERO_INFO_PANEL_NAME = "HeroInfoPanel";
		/// <summary>
		/// The BA g_ PANE l_ NAM.
		/// 背包面板
		/// </summary>
		public const string BAG_PANEL_NAME = "BagPanel";
		/// <summary>
		/// The property s_ DETAIL s_ PANE l_ NAM.
		/// 道具图鉴
		/// </summary>
		public const string PROPS_DETAILS_PANEL_NAME = "PropsDetailsPanel";

		/// <summary>
		/// 附魔面板
		/// </summary>
		public const string ENCHANTS_PANEL_NAME = "EnchantsPanel";
		/// <summary>
		/// The EQUI p_ INF o_ PANE l_ NAM.
		/// 装备信息面板
		/// </summary>
		public const string EQUIP_INFO_PANEL_NAME = "EquipInfoPanel";

		public const string HERO_ITEM_NAME = "HeroItem";
		public const string SKILL_ITEM_NAME = "SkillItem";
		public const string PROPS_ITEM_NAME = "PropsItem";
		public const string PANEL_BLOCK = "Block"; 

		/// <summary>
		/// The mgr center or center.
		/// </summary>
		public static UIPanelNodeManager mgrCoC; 
		/// <summary>
		/// The mgr Top right.
		/// </summary>
		public static UIPanelNodeManager mgrTR;
		/// <summary>
		/// The mgr Top or left.
		/// </summary>
		public static UIPanelNodeManager mgrTL;
		/// <summary>
		/// The mgr to top or top.
		/// </summary>
		public static UIPanelNodeManager mgrToT;

		/// UI管理
		public static UIPanelNodeManager manger;

		public static string getWidgetsPath(string weightName){
			return TangUI.Config.WIDGET_PATH + "/" + weightName;
		}
	}
}

