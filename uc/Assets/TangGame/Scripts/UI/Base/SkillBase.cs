using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI.Base
{
	public class SkillBase{
		private SkillXml xml;

		public SkillXml Xml {
			get {
				return xml;
			}
			set {
				xml = value;
			}
		}

		private int level;

		public int Level {
			get {
				return level;
			}
			set {
				level = value;
			}
		}

		/// <summary>
		/// 判断技能是否解锁
		/// </summary>
		/// <value><c>true</c> if this instance is lock; otherwise, <c>false</c>.</value>
		public bool IsLock {
			get {
				if (level <= 0) {
					return true;
				} else {
					return false;
				}
			}
		}
	}
}