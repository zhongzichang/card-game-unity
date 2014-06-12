using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 商店面板
  /// </summary>
  public class ShopPanel :ViewPanel {

    public const string NAME = "ShopPanel";

    public static string[] Strs = new string[]{"你的手指真灵活，我很喜欢","我会在每天9点，12点和18点更新商品","我是卖商品的","轻一点，点疼我了"};

    public ShopItem shopItem;
    public GameObject tips;
    public UILabel tipsLabel;
    public UILabel timeLabel;
    public UIEventListener refreshBtn;
    public UIEventListener tipsBtn;
    public UISprite tipsBackground;

    public List<ShopItem> items = new List<ShopItem>();

    private object mParam;
    private float tipsTime;
    private bool isShowTips;

    void Start(){
      Init();
      refreshBtn.onClick += RefreshBtnClickHandler;
      tipsBtn.onClick += TipsBtnClickHandler;
      tips.SetActive(false);
      this.started = true;
      UpdateData();
    }

    void Update(){

      if(!isShowTips){return;}
      if(tipsTime < 2){
        tipsTime += Time.deltaTime;
      }else{
        tipsBackground.alpha -= Time.deltaTime * 1;
        if(tipsBackground.alpha <= 0.001){
          isShowTips = false;
        }
      }
    }

    private void Init(){
      Vector3 localPosition = shopItem.transform.localPosition;
      Vector3 temp = localPosition;
      items.Add(shopItem);
      for(int i = 1; i < 6; i++){
        GameObject go = UIUtils.Duplicate(shopItem.gameObject, shopItem.transform.parent.gameObject);
        temp = localPosition;
        temp.x += (i % 3) * 278;
        temp.y -= (int)(i / 3) * 210;
        go.transform.localPosition = temp;
        ShopItem item = go.GetComponent<ShopItem>();
        items.Add(item);
      }

    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value;this.UpdateData();}
    }

    private void UpdateData(){
      if(!this.started){return;}
    }

    private void RefreshBtnClickHandler(GameObject go){

    }

    private void TipsBtnClickHandler(GameObject go){
      int index = Random.Range(0, Strs.Length);
      string str = Strs[index];
      tipsLabel.text = str;
      OpenTips();
      UpdateTips();
    }

    /// 更新Tips相关
    private void UpdateTips(){
      tipsBackground.width = tipsLabel.width + 20;
    }

    private void OpenTips(){
      tipsTime = 0;
      tipsBackground.alpha = 1;
      tips.SetActive(true);
      isShowTips = true;
    }
  }
}