using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TangGame.Xml;

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
      UIEventListener.Get(shopItem.gameObject).onClick += ItemClickHandler;
      items.Add(shopItem);
      for(int i = 1; i < 6; i++){
        GameObject go = UIUtils.Duplicate(shopItem.gameObject, shopItem.transform.parent.gameObject);
        temp = localPosition;
        temp.x += (i % 3) * 278;
        temp.y -= (int)(i / 3) * 210;
        go.transform.localPosition = temp;
        ShopItem item = go.GetComponent<ShopItem>();
        UIEventListener.Get(go).onClick += ItemClickHandler;
        items.Add(item);
      }

    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value;this.UpdateData();}
    }

    private void UpdateData(){
      if(!this.started){return;}

      List<Goods> list = new List<Goods>();
      int count = 0;
      foreach(PropsData data in Config.propsXmlTable.Values){
        Goods goods = new Goods();
        goods.data = data;
        goods.isSell = count / 2 == 1;
        goods.num = count + 1;
        count ++;
        list.Add(goods);
        if(count >= 6){
          break;
        }
      }
      for(int i = 0; i < items.Count; i++){
        items[i].data = list[i];
      }
    }

    /// 刷新按钮点击处理
    private void RefreshBtnClickHandler(GameObject go){
      Alert.Show("显示一批新货物需要消耗50钻石是够继续？（今日已刷新0次）", RefreshCallback, null);
    }

    /// 刷新提示回调处理
    private void RefreshCallback(AlertType type, object param){
      Global.Log(type);
    }

    private void TipsBtnClickHandler(GameObject go){
      int index = Random.Range(0, Strs.Length);
      string str = Strs[index];
      tipsLabel.text = str;
      OpenTips();
      UpdateTips();
    }

    private void ItemClickHandler(GameObject go){
      ShopItem item = go.GetComponent<ShopItem>();
      Goods goods = item.data as Goods;
      if(!goods.isSell){
        UIContext.mgrCoC.LazyOpen(ShopBuyPanel.NAME, TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE, item.data);
      }
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