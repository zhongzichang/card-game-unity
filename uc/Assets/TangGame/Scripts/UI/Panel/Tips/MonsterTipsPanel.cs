using UnityEngine;
using System.Collections;
using TangGame.Xml;

namespace TangGame.UI{
  public class MonsterTipsPanel : MonoBehaviour {

		/// 基本高度，不包含效果和简介文本
		private const int BASE_HEIGHT = 102;

		public UISprite background;
		public UISprite frame;
		public UISprite icon;
		public UILabel nameLabel;
		public UILabel levelLabel;
		public UILabel descLabel;

		/// TIPS高度，包含基本高度和文本高度
		private int mHeight = 0;
    private int renderQueueIndex = 10000;

		// Use this for initialization
		void Start () {
			CalculateHeight();
      renderQueueIndex = Global.GetTipsPanelRenderQueueIndex();
		}

		public int height{
			get{return this.mHeight;}
		}


		public void CalculateHeight(){
			mHeight = BASE_HEIGHT;

			if(!string.IsNullOrEmpty(descLabel.text)){
				mHeight += descLabel.height;
			}
			background.height = mHeight;
			this.gameObject.transform.localPosition = new Vector3(-165, mHeight / 2, 0);
		}

		/// 设置道具
    public void SetData(MonsterData data, bool isBoss){
      UIPanel mPanel = this.GetComponent<UIPanel>();
      mPanel.renderQueue = UIPanel.RenderQueue.StartAt;
      mPanel.startingRenderQueue = renderQueueIndex;
      mPanel.depth = renderQueueIndex;
      this.descLabel.text = data.desc;
      this.nameLabel.text = data.name;
      this.frame.spriteName = Global.GetHeroIconFrame(data.rank);
      this.icon.spriteName = data.avatar;
      string levelStr = "LV." + data.level;
      levelStr += isBoss ? " [FF0000]BOSS[-]" : "";
      this.levelLabel.text = levelStr;
      this.CalculateHeight();
      /*
      HeroData data = HeroCache.instance.GetHero(hero.id);
      if(data == null){
        Global.LogError(">> HeroTipsPanel heroData not found. hero id = " + hero.id);
        return;
      }

      this.frame.spriteName = Global.GetHeroIconFrame(hero.rank);
      this.icon.spriteName = data.avatar;
      this.nameLabel.text = data.name;

      string levelStr = "LV." + hero.level;
      levelStr += hero.boss ? " [FF0000]BOSS[-]" : "";
      this.levelLabel.text = levelStr;

      this.descLabel.text = data.hero_tip;

			this.CalculateHeight();*/
		}
	}
}