using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class EquipStageDropItem : MonoBehaviour
	{

		public UISprite StageIcon;
		public UILabel StageNameLabel;
		public UILabel StageTagLabel;
		public Xml.LevelData stageData;

		public void Flush (Xml.LevelData stageData)
		{
			if (stageData != null) {
				this.stageData = stageData;
				this.StageIcon.spriteName = stageData.icon;
				this.StageNameLabel.text = stageData.name;
				Xml.MapData mapData = LevelCache.instance.GetMapData (stageData.map_id);
				this.StageTagLabel.text = string.Format(UIPanelLang.MAP_INDEX,mapData.index);
				//TODO 判断当前是否解锁
				if (true) {
					//TODO 获取当前以及试炼过的次数

				} else {

				}
			}
		}
	}
}