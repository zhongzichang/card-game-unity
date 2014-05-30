using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI
{
	public class SoulStoneDropPanel : MonoBehaviour
	{
		/// <summary>
		/// The avatar frame.
		/// 英雄头像框
		/// </summary>
		public GameObject AvatarFrame;
		/// <summary>
		/// The avatar icon.
		/// 英雄头像图标
		/// </summary>
		public GameObject AvatarIcon;
		/// <summary>
		/// The back button.
		/// 返回按钮
		/// </summary>
		public GameObject BackButton;
		/// <summary>
		/// The back label.
		/// 返回按钮的文字
		/// </summary>
		public GameObject BackLabel;
		/// <summary>
		/// The name label.
		/// 英雄的名字文字
		/// </summary>
		public GameObject NameLabel;
		/// <summary>
		/// 灵魂石数量
		/// </summary>
		public GameObject NumLabel;
		/// <summary>
		/// 灵魂石头掉落关卡的组
		/// </summary>
		public GameObject StageDropGrid;
		/// <summary>
		/// The stage drop item.
		/// 灵魂石掉落关卡项
		/// </summary>
		public GameObject StageDropItem;
		/// <summary>
		/// The title label.
		/// 面板标题
		/// </summary>
		public GameObject TitleLabel;
		PropsRelationData propsRelationData = null;
		/// <summary>
		/// The stage drop item list.
		/// </summary>
		List<GameObject> stageDropItemList = new List<GameObject>();
		/// <summary>
		/// The are my parameter.
		/// </summary>
		private object mParam;

		public object param {
			get { 
				return mParam;
			}
			set {
				mParam = value;
			}
		}

		void Start ()
		{
			SetBackLabel ("返回");
			SetTitleLabel ("获得途径：");
			SetNumLabel ("[c0ffff]未解锁[-]");
		}

		public void Flush (HeroBase herobase)
		{
			propsRelationData = PropsCache.instance.GetPropsRelationData (herobase.Xml.soul_rock_id);
			SetAvatarFrame (herobase.Net.rank);
			SetAvatarIcon (herobase.Xml.avatar);
			SetNameLabel (herobase.Xml.name);
			for (int i = 0; i < propsRelationData.levels.Count; i++) {
				AddStageDropItem (propsRelationData.levels[i],i);
			}
		}

		public void SetAvatarFrame (int rank)
		{
			AvatarFrame.GetComponent<UISprite> ().spriteName 
			= Global.GetHeroIconFrame (rank);
		}

		public void SetAvatarIcon (string avatarName)
		{
			AvatarIcon.GetComponent<UISprite> ().spriteName = avatarName;
		}

		public void SetNameLabel (string text)
		{
			NameLabel.GetComponent<UILabel> ().text = text;
		}

		public void SetNumLabel (string text)
		{
			NumLabel.GetComponent<UILabel> ().text = text;
		}

		public void SetTitleLabel (string text)
		{
			TitleLabel.GetComponent<UILabel> ().text = text;
		}

		public void SetBackLabel (string text)
		{
			BackLabel.GetComponent<UILabel> ().text = text;
		}

		public void AddStageDropItem (Xml.LevelData levelData,int index)
		{
			GameObject stageDropItem;
			if (stageDropItemList.Count < index) {
				stageDropItem = NGUITools.AddChild (StageDropGrid, StageDropItem);
				stageDropItemList.Add (stageDropItem);
			} else {
				stageDropItem = stageDropItemList [index];
			}
			if(!stageDropItem.activeSelf)
				stageDropItem.SetActive (true);
			SoulStoneStageDropItem itemScript = stageDropItem.GetComponent<SoulStoneStageDropItem> ();
			itemScript.Flush (levelData);
		}
		/// <summary>
		/// Disables the stage drop item list.
		/// </summary>
		public void DisableStageDropItemList(){
			foreach(GameObject o in stageDropItemList){
				o.SetActive (false);
			}
		}
	}
}