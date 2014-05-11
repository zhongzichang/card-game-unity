/// <summary>
/// Properties base.
/// xbhuang 2014-4-28
/// </summary>
using UnityEngine;
using TangGame.Xml;
using TangGame.Net;

namespace TangGame.UI
{
	public class PropsBase{
		/// <summary>
		/// The xml.
		/// </summary>
		private PropsXml xml;
		/// <summary>
		/// The net.
		/// </summary>
		private PropsNet net;
		/// <summary>
		/// The count. 道具数量
		/// </summary>
		private int count;


		public int Count {
			get {
				return count;
			}
			set {
				count = value;
			}
		}

		public PropsXml Xml {
			get {
				return xml;
			}
			set {
				xml = value;
			}
		}

		public PropsNet Net {
			get {
				return net;
			}
			set {
				net = value;
			}
		}


	}
	///道具类型
	public enum PropsType{
		NONE,
		/// <summary>
		/// The EQUI.装备
		/// </summary>
		EQUIP,
		/// <summary>
		/// The DEBRI.碎片
		/// </summary>
		DEBRIS,
		/// <summary>
		/// The SCROLL.卷轴
		/// </summary>
		SCROLLS,
		/// <summary>
		/// The SOULROC.灵魂石
		/// </summary>
		SOULROCK,
		/// <summary>
		/// The EX.经验药水
		/// </summary>
		EXP,
		/// <summary>
		/// The ENCHAN.附魔材料
		/// </summary>
		ENCHANT,
		/// <summary>
		/// The valuable.
		/// 贵重物品
		/// </summary>
		VALUABLE
	}
}