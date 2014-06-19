using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

namespace TangGame.UI
{

	/// 英雄相关
	public class HeroCache
	{

		private static HeroCache mInstance;

		public static HeroCache instance {
			get {
				if (null == mInstance) {
					mInstance = new HeroCache ();
				}
				return mInstance; 
			}
		}

		/// <summary>
		/// 其它玩家的英雄数据
		/// </summary>
		private Dictionary<int, HeroBase> oHeroBeseTable = new Dictionary<int, HeroBase> ();
		/// <summary>
		/// 英雄数据数组 key，为configid
		/// </summary>
		private Dictionary<int, HeroBase> mHeroBeseTable = new Dictionary<int, HeroBase> ();

		public Dictionary<int, HeroBase> MHeroBeseTable {
			get {
				return mHeroBeseTable;
			}
		}


		/// <summary>
		/// 更新或添加我的英雄数据
		/// Updatas my hero base table.
		/// </summary>
		/// <param name="hn">Hn.</param>
		public bool UpdataMyHeroBaseTable (Net.HeroNet hn)
		{
			if (!mHeroBeseTable.ContainsKey (hn.configId)) {
				HeroBase hb = new HeroBase ();
				hb.Xml = GetHeroData (hn.configId);
				if (hb.Xml == null)
					return false;
				mHeroBeseTable.Add (hn.configId, hb);
			}
			mHeroBeseTable [hn.configId].Net = hn;
			return true;
		}

		/// <summary>
		/// 更新或添加我的英雄数据
		/// </summary>
		/// <param name="hd">Hd.</param>
		public void UpdataMyHeroBaseTable (HeroData hd)
		{
			if (!mHeroBeseTable.ContainsKey (hd.id)) {
				HeroBase hb = new HeroBase ();
				hb.Xml = hd;
				mHeroBeseTable.Add (hd.id, hb);
			}
		}

		/// <summary>
		/// 更新或添加其它玩家的英雄数据
		/// Updatas the other players hero base table.
		/// </summary>
		/// <param name="hn">Hn.</param>
		public void UpdataOtherPlayersHeroBaseTable (Net.HeroNet hn)
		{
			HeroBase hb;
			if (!mHeroBeseTable.ContainsKey (hn.id)) {
				hb = new HeroBase ();
				hb.Xml = GetHeroData (hn.configId);
				mHeroBeseTable.Add (hb.Net.id, hb);
			} else {
				hb = GetHeroByNetID (hn.id);
			}
			hb.Net = hn;

		}

		/// <summary>
		/// 根据配置id获得自己的英雄
		/// </summary>
		/// <returns>The hero config I.</returns>
		/// <param name="id">Identifier.</param>
		public HeroBase GetMyHeroByConfigID (int configId)
		{
			if (mHeroBeseTable.ContainsKey (configId)) {
				return mHeroBeseTable [configId];
			}
			return null;
		}

		/// <summary>
		/// Gets my hero by net I.
		/// 根据网络id获得自己的英雄
		/// </summary>
		/// <returns>The my hero by net I.</returns>
		/// <param name="netId">Net identifier.</param>
		public HeroBase GetMyHeroByNetID (int netId)
		{
			foreach (HeroBase h in mHeroBeseTable.Values) {
				if (h.Net != null && h.Net.id == netId)
					return h;
			}
			return null;
		}

		/// <summary>
		/// 根据网络id获取英雄
		/// 与GetHeroByNetID作用相同
		/// </summary>
		/// <returns>The hero.</returns>
		/// <param name="netId">Net identifier.</param>
		public HeroBase GetHero(int netId){
			return GetHeroByNetID (netId);
		}
		/// <summary>
		/// 根据网络id获取英雄
		/// </summary>
		/// <returns>The hero by net I.</returns>
		public HeroBase GetHeroByNetID (int netId)
		{
			if (oHeroBeseTable.ContainsKey (netId)) {
				return oHeroBeseTable [netId];
			}
			return GetMyHeroByNetID (netId);
		}

    /// 根据配置表id获取英雄
    public HeroBase GetHeroByID (int id)
    {
      if (mHeroBeseTable.ContainsKey (id)) {
        return mHeroBeseTable [id];
      }
      return null;
    }

		/// 灵魂石ID与英雄的关联
		private Dictionary<int, HeroData> soulStoneRelations = new Dictionary<int, HeroData> ();

		/// <summary>
		/// 添加灵魂石关联
		/// </summary>
		/// <param name="id">灵魂石ID</param>
		/// <param name="data">英雄数据</param>
		public void AddSoulStoneRelation (HeroData data)
		{
			if (!soulStoneRelations.ContainsKey (data.soul_rock_id)) {
				soulStoneRelations [data.soul_rock_id] = data;
			}
		}

		/// 获取灵魂石关联的英雄数据
		public HeroData GetSoulStoneRelation (int id)
		{
			if (soulStoneRelations.ContainsKey (id)) {
				return soulStoneRelations [id];
			}
			return null;
		}

		/// 获取英雄本地配置信息
		public HeroData GetHeroData (int id)
		{
			if (Config.heroXmlTable.ContainsKey (id)) {
				return Config.heroXmlTable [id];
			}
			return null;
		}

	}
}