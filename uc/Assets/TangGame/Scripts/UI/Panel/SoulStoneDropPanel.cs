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
				if (mParam is HeroBase) {
					Flush (mParam as HeroBase);
				}
			}
		}

		void Start ()
		{
			SetBackLabel (UIPanelLang.BACK);
			SetTitleLabel (UIPanelLang.GET_WAY);
			SetPromptLabel ("该灵魂石不在副本掉落");
		}
		public GameObject PromptLabel;
		public void Flush (HeroBase herobase)
		{
			propsRelationData = PropsCache.instance.GetPropsRelationData (herobase.Xml.soul_rock_id);
			SetAvatarFrame (herobase.Net.rank);
			SetAvatarIcon (herobase.Xml.avatar);
			SetNameLabel (herobase.Xml.name);
			int count = 0;
			int countMax = 0;
			Props props = PropsCache.instance.GetProps (herobase.Xml.soul_rock_id);
			if (props != null) {
				count = props.net.count;
			}
			if(Config.evolveXmlTable.ContainsKey(herobase.Net.star + 1)){
				countMax = Config.evolveXmlTable [herobase.Net.star + 1].val;
			}
			SetNumLabel (count,countMax);
			DisableStageDropItemList ();
			if (propsRelationData != null) {
				PromptLabel.SetActive (false);
				for (int i = 0; i < propsRelationData.levels.Count; i++) {
					AddStageDropItem (propsRelationData.levels [i], i);
				}
			} else {
				PromptLabel.SetActive (true);
			}
			this.StageDropGrid.GetComponent<UIGrid> ().repositionNow = true;
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

		public void SetNumLabel (int count,int countMax)
		{
			if (0 != countMax) {
				NumLabel.GetComponent<UILabel> ().text = string.Format ("({0}/{1})", count, countMax);
			} else {
				//TODO 星级已满
			}
		}

		public void SetTitleLabel (string text)
		{
			TitleLabel.GetComponent<UILabel> ().text = text;
		}
		public void SetPromptLabel(string text){
			PromptLabel.GetComponent<UILabel> ().text = text;
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
				UIEventListener.Get (stageDropItem.gameObject).onClick += OnStageDropItemClick;
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
		void OnStageDropItemClick(GameObject obj){
			SoulStoneStageDropItem itemScript = obj.GetComponent<SoulStoneStageDropItem> ();
			if (itemScript != null) {
				BattleChaptersPanelData data = new BattleChaptersPanelData ();
				data.stage = itemScript.stageData.id;
				data.type = StageType.Guide;
				UIContext.mgrCoC.LazyOpen(BattleChaptersPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.ADDSTATUS, data);
			}
		}
	}
}