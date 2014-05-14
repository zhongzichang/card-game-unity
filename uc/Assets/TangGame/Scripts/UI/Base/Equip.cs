/// <summary>
/// xbhuang 2014-4-28
/// </summary>
using UnityEngine;
using TangGame.Xml;
using TangGame.Net;

namespace TangGame.UI
{
	public class Equip : Props{

		private EquipNet mNet;


		public EquipNet net {
			get {
				return this.mNet;
			}
			set {
				this.mNet = value;
			}
		}
	}
}