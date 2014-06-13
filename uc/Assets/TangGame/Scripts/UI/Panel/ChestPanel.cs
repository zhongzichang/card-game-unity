using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  public class ChestPanel : ViewPanel {

    public const string NAME = "ChestPanel";

    public UIEventListener viewBtn1;
    public UIEventListener arrowBtn1;
    public TweenPosition tween1;
    public UILabel freeLabel1;
    public GameObject moneyGroup;

    public UIEventListener viewBtn2;
    public UIEventListener arrowBtn2;
    public TweenPosition tween2;
    public UILabel freeLabel2;
    public GameObject ingotGroup;

    public UIEventListener viewBtn3;
    public UIEventListener arrowBtn3;
    public TweenPosition tween3;

    void Start () {
      viewBtn1.onClick += ViewBtn1ClickHandler;
      arrowBtn1.onClick += ArrowBtn1ClickHandler;

      viewBtn2.onClick += ViewBtn2ClickHandler;
      arrowBtn2.onClick += ArrowBtn2ClickHandler;

      viewBtn3.onClick += ViewBtn3ClickHandler;
      arrowBtn3.onClick += ArrowBtn3ClickHandler;

      freeLabel1.gameObject.SetActive(false);
      freeLabel2.gameObject.SetActive(false);
    }

    private void ViewBtn1ClickHandler(GameObject go){
      tween1.PlayForward();
    }

    private void ArrowBtn1ClickHandler(GameObject go){
      tween1.PlayReverse();
    }

    private void ViewBtn2ClickHandler(GameObject go){
      tween2.PlayForward();
    }
    
    private void ArrowBtn2ClickHandler(GameObject go){
      tween2.PlayReverse();
    }

    private void ViewBtn3ClickHandler(GameObject go){
      tween3.PlayForward();
    }
    
    private void ArrowBtn3ClickHandler(GameObject go){
      tween3.PlayReverse();
    }
  }
}