
using System.Xml;
using UnityEngine;

namespace TangGame.Xml
{
	public class DialogueData
	{ 
		/// 对话编号
		public int id;
		/// 下一句对话编号
		public int next_dialogue_id;
		/// 内容字符串
		public string dialogue;
		/// 头像编号
		public string avatar_id;
		/// 音乐编号
		public int music_id;
		/// 音效编号
		public int sound_id;
	}
}