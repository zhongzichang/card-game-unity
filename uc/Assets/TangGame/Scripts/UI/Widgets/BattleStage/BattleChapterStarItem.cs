using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// 地图章节里面的星级对象
  public class BattleChapterStarItem : ViewItem {

    /// 坐标
    private static Vector3[] Num1 = new Vector3[]{new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0)};
    private static Vector3[] Num2 = new Vector3[]{new Vector3(-18, 0, 0), new Vector3(18, 0, 0), new Vector3(0, 0, 0)};
    private static Vector3[] Num3 = new Vector3[]{new Vector3(-36, 0, 0), new Vector3(0, 0, 0), new Vector3(36, 0, 0)};

    /// 图标对象列表
    public UISprite[] list = new UISprite[]{};

    public override void Start (){
      started = true;
      UpdateData();
    }

    public override void UpdateData (){
      if(!started){return;}
      int num = (int)(this.data);
      Vector3[] positions = Num1;
      if(num == 0){
        positions = Num1;
      }else if(num == 1){
        positions = Num1;
      }else if(num == 2){
        positions = Num2;
      }else if(num == 3){
        positions = Num3;
      }

      for(int i = 0; i < list.Length; i++){
        list[i].gameObject.transform.localPosition = positions[i];
        if(i < num){
          list[i].gameObject.SetActive(true);
        }else{
          list[i].gameObject.SetActive(false);
        }
      }

    }
    
  }
}