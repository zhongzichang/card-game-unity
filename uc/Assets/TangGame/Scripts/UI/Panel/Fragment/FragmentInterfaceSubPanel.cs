/// <summary>
/// Fragment interface sub panel.
/// xbhuang 
/// 2014-5-10
/// </summary>
using UnityEngine;

namespace TangGame.UI
{
	public class FragmentInterfaceSubPanel : MonoBehaviour
	{
		/// <summary>
		/// 碎片信息面板，父面板初始化时会被赋值
		/// </summary>
		FragmentPropsInfoSubPanel mInfoPanel;

		/// <summary>
		/// The toggle all.
		/// 显示所有物品碎片的按钮对象
		/// </summary>
		public GameObject ToggleAll;
		/// <summary>
		/// The toggle equip.
		/// 显示所有装备碎片的按钮对象
		/// </summary>
		public GameObject ToggleEquip;
		/// <summary>
		/// The toggle scroll.
		/// 显示所有卷轴碎片的按钮对象
		/// </summary>
		public GameObject ToggleScroll;
		/// <summary>
		/// The properties table.
		/// 道具列表对象
		/// </summary>
		public GameObject PropsTable;


		void Start(){

		}
			
		void LoadAllScrollItem(){

		}
		///===================道具信息面板=================
		/// <summary>
		/// Gets or sets the info panel.
		/// </summary>
		/// <value>The info panel.</value>
		public FragmentPropsInfoSubPanel infoPanel {
			get {
				return mInfoPanel;
			}
			set {
				mInfoPanel = value;
			}
		}
	}
}