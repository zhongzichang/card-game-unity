using UnityEngine;
using System.Collections;

namespace TangGame.UI{

  /// 战斗界面右上角显示获得资源面板
  public class LevelResourcePanel : MonoBehaviour {
    /// 获得金币
    public UILabel goldLabel;
    /// 获得道具数量
    public UILabel propsLabel;

    public TweenScale goldTween;
    public TweenScale propsTween;

    public int gold{
      set{
        goldLabel.text = "" + value;
        if(value > 0){
          goldTween.ResetToBeginning();
          goldTween.Play();
        }
      }
    }

    public int propsNum{
      set{
        propsLabel.text = "" + value;
        if(value > 0){
          propsTween.ResetToBeginning();
          propsTween.Play();
        }
      }
    }

    /*
    void OnGUI(){
      if(GUI.Button(new Rect(50, 50, 50, 50), "G")){
        gold = Random.Range(10, 5000);
      }

      if(GUI.Button(new Rect(150, 50, 50, 50), "P")){
        propsNum = Random.Range(0, 100);
      }
    }
    */

  }
}