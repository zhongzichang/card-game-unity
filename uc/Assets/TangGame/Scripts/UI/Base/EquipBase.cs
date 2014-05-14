/// <summary>
/// xbhuang 2014-4-28
/// </summary>
using UnityEngine;
using TangGame.Xml;
using TangGame.Net;

namespace TangGame.UI
{
	public class EquipBase : Props{

		private EquipNet net;

		public EquipNet Net {
			get {
				return net;
			}
			set {
				net = value;
			}
		}
	}
}