using UnityEngine;
using System.Collections;

public class SkillBase {
	private int id;
	private int configId;
	private int skillLv;
//	private SkillConfig config;

	public int SkillLv {
		get {
			return skillLv;
		}
		set {
			skillLv = value;
		}
	}

	public int ConfigId {
		get {
			return configId;
		}
		set {
			configId = value;
		}
	}

	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
}
