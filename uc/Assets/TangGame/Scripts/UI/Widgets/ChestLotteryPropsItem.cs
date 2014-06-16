using UnityEngine;
using System.Collections;
using TangGame.Xml;

namespace TangGame.UI{
  /// <summary>
  /// 宝箱抽奖界面的道具显示项
  /// </summary>
  public class ChestLotteryPropsItem : ViewItem {

    private static string[] EFFECT_NAME = new string[]{"", "chestBgEffect1", "chestBgEffect2", "chestBgEffect3", ""};

    public TweenPosition tweenPosition;
    public TweenRotation tweenRotation;

    public UISprite effectSprite;

    public UISprite frame;
    public UISprite icon;
    public UILabel numLabel;
    public UILabel nameLabel;

    private Vector2 mStartPosition = new Vector2(0, 220);
    private Vector2 mTargetPosition = new Vector2(0, -50);
    private PropsData propsData;

    public override void Start (){
      this.started = true;
      tweenPosition.eventReceiver = this.gameObject;
      tweenPosition.callWhenFinished = "OnFinishedHandler";
      effectSprite.gameObject.SetActive(false);
      nameLabel.gameObject.SetActive(false);
      UpdateData();
    }

    public override void UpdateData (){
      if(!this.started){return;}
      tweenPosition.from = this.mStartPosition;
      tweenPosition.to = this.mTargetPosition;
      tweenPosition.ResetToBeginning();
      tweenPosition.Play();
      tweenRotation.ResetToBeginning();
      tweenRotation.Play();

      ChestLotteryPropsData data = this.data as ChestLotteryPropsData;
      propsData = PropsCache.instance.GetPropsData(data.id);
      if(propsData == null){
        Global.LogError(">> ChestLotteryPropsItem Error, id = " + data.id);
        return;
      }
      this.frame.spriteName = Global.GetPropFrameName((PropsType)propsData.type, propsData.rank);
      this.icon.spriteName = propsData.Icon;
      this.nameLabel.text = propsData.name;
      this.numLabel.text = data.num > 1 ? ("" + data.num) : "";


    }

    private void OnFinishedHandler(UITweener tweener){
      if(propsData.rank > 0){
        effectSprite.spriteName = EFFECT_NAME[propsData.rank];
        effectSprite.gameObject.SetActive(true);
      }else{
        effectSprite.gameObject.SetActive(false);
      }

      nameLabel.gameObject.SetActive(true);
    }

    /// 移动的开始坐标
    public Vector2 startPosition{
      get{return this.mStartPosition;}
      set{this.mStartPosition = value;}
    }

    /// 移动的结束坐标
    public Vector2 targetPosition{
      get{return this.mTargetPosition;}
      set{this.mTargetPosition = value;}
    }
  }
}

