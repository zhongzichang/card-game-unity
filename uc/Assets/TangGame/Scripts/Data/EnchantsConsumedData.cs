using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;

namespace TangGame.Xml
{
	public class EnchantsConsumedData
	{ 
		/// 品质
		public int rank;
		/// 每级消耗金币数组
		public string exp_spend;
		/// 每点消耗金币
		public int gold_spend;
		/// 每点消耗砖石
		public int gem_spend;

		/// <summary>
		/// Gets the exp spend.
		/// 获取等级为lv的附魔需要多少经验
		/// </summary>
		/// <returns>The exp spend.</returns>
		/// <param name="lv">Lv.</param>
		public int GetExpSpend(int lv){
			int[] exps = GetExpSpends ();
			if (exps.Length  >= lv) {
				return  exps[lv-1];
			} else {
				return 0;
			}
		}

		/// <summary>
		/// Gets the exp spends.
		/// 获取经验消耗集合
		/// </summary>
		/// <returns>The exp spends.</returns>
		public int[] GetExpSpends(){
			string[] strs = (string[])Utils.SplitStrByBraces (exp_spend).ToArray (typeof(string));	
			return Utils.SplitStrByCommaToInt (strs [0]);
		}
		/// <summary>
		/// Gets the lv max.
		/// 获取当前阶级最大附魔值
		/// </summary>
		/// <returns>The lv max.</returns>
		public int GetLvMax(){
			if (GetExpSpends () [0] == 0) {
				return 0;
			}
			return GetExpSpends ().Length;
		}
		/// <summary>
		/// Gets to max exp.
		/// 获取当前升级到满级需要多少经验
		/// </summary>
		/// <returns>The to max exp.</returns>
		public int GetToMaxExp(int lv,int exp){
			int expCurrent = 0;
			int expMax= 0;
			int[] expSpends = GetExpSpends ();
			foreach(int expSpend in expSpends){
				expMax += expSpend;
			}
			for(int i=0;i<lv;i++){
				expCurrent+=expSpends[i];
			}
			expCurrent += exp;
			int val = expMax - expCurrent;
			return val;
		}
	}

	[XmlRoot ("root")]
	[XmlLate("enchants_consumed")]
	public class EnchantsConsumedRoot
	{
		[XmlElement ("value")]
		public List<EnchantsConsumedData> items = new List<EnchantsConsumedData> ();

		public static void LateProcess (object obj)
		{
			EnchantsConsumedRoot root = obj as EnchantsConsumedRoot;
			foreach (EnchantsConsumedData item in root.items) {
				Config.enchantsConsumedXmlTable [item.rank] = item;
			}
		}
	}
}