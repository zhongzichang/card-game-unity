using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场调整界面中的英雄项
  /// </summary>
  public class ArenaAdjustHeroItem : ViewItem {

    public UISprite frame;
    public UISprite icon;
    public UISprite star;
    public UILabel levelLabel;

    public override void Start (){
      this.started = true;
      UpdateData();
    }

    public override void UpdateData (){
      ArenaHero hero = this.data as ArenaHero;
      this.index = hero.sortOrder;

      if(!this.started){return;}
      HeroBase heroBase = hero.heroBase;
      if(heroBase == null){
        Global.LogError(">> ArenaAdjustHeroItem heroBase is null, id = " + hero.id);
        return;
      }

      frame.spriteName = Global.GetHeroIconFrame(heroBase.Net.rank);
      star.width = Global.GetHeroStar(heroBase.Net.star) * 16;
      icon.spriteName = heroBase.Xml.avatar;
      levelLabel.text = heroBase.Net.level.ToString();
    }

    public override void UpdateSelected (){
      if(this.selected){
        frame.color = Color.gray;
        icon.color = Color.gray;
      }else{
        frame.color = Color.white;
        icon.color = Color.white;
      }
    }
  }
}