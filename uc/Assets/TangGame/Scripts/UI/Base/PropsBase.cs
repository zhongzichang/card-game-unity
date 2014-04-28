/// <summary>
/// Properties base.
/// xbhuang 2014-4-28
/// </summary>
using UnityEngine;
using TangGame.Xml;
using TangGame.Net;

namespace TangGame.UI.Base
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
}