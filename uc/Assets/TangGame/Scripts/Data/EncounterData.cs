
using System.Xml;
using UnityEngine;

namespace TangGame.Xml
{
	public class EncounterData
	{ 
		/// 编号
		public int id;
		/// 遭遇英雄
		public string hero_ids;
		/// 次数限制
		public int constraint_num;
		/// 概率
		public float probability;
		/// 对话编号1
		public string dialogue_1;
		/// 对话编号2
		public string dialogue_2;
		/// 对话编号3
		public string dialogue_3;
	}
}