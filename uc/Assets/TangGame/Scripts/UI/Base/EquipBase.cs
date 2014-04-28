/// <summary>
/// xbhuang 2014-4-28
/// </summary>
using UnityEngine;
using TangGame.Xml;
using TangGame.Net;

namespace TangGame.UI.Base
{
	public class EquipBase : PropsBase{

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