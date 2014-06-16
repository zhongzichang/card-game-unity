/// <summary>
/// xbhuang 2014-4-28
/// </summary>
using UnityEngine;
using TangGame.Xml;

namespace TangGame.UI
{
	public class SkillBase{
		private SkillData xml;

		public SkillData Xml {
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
	public enum SkillType{
		NONE,
		NormalAttack
	}
}