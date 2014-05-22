using UnityEngine;
using System.Collections;

namespace TangGame
{
  public class BattleTxtSpriteItem : ViewItem
  {
    private float duration = 1.8f;
    private float time = 0;
    private bool init = false;
    private UISprite sprite;
    private Vector3 movePosition;
    private Vector3 mScale = Vector3.one;
    private int step = 0;
    private float scaleSpeed = 0.3f;
    private float tempTime = 0;
    /// 文字中间停顿等待时间
    private float waitTime = 0.15f;

    void Update ()
    {
      if (init) {
        time += Time.deltaTime;
        if (time >= duration) {
          time = -1000;
          GameObject.Destroy (this.gameObject);
        }

        if (step == 0) {
          if (this.gameObject.transform.localScale != mScale) {
            Vector3 temp = this.gameObject.transform.localScale;
            temp = Vector3.MoveTowards (temp, mScale, scaleSpeed);
            this.gameObject.transform.localScale = temp;
          } else {
            step = 1;
          }
        } else if (step == 1) {
          tempTime += Time.deltaTime;
          if (tempTime > waitTime) {
            step = 2;
          }
        } else if (step == 2) {
          if (movePosition != this.gameObject.transform.localPosition) {
            Vector3 temp = this.gameObject.transform.localPosition;
            temp = Vector3.MoveTowards (temp, movePosition, 2f);
            this.gameObject.transform.localPosition = temp;
          }
          sprite.alpha -= Time.deltaTime * 1.5f;

          if (sprite.alpha <= 0) {
            step = 3;
          }
        }
      }
    }

    public override void Start ()
    {
      this.started = true;
      sprite = this.GetComponent<UISprite> ();
      UpdateData ();
    }

    public override void UpdateData ()
    {
      if (!this.started) {
        return;
      }
      BattleTxt txt = data as BattleTxt;
     
			sprite.spriteName = "battletext_kill_blue";

      Vector3 tempPosition = UICamera.mainCamera.ScreenToWorldPoint (txt.position);
      this.transform.position = tempPosition;
      this.transform.localScale = Vector3.zero;
      movePosition = this.transform.localPosition;
      movePosition.z = -10;
			movePosition.y += 30;
      this.transform.localPosition = movePosition;

      if (txt.crit) {
        mScale = new Vector3 (1.4f, 1.4f, 1);
        scaleSpeed *= 2;
      }
      init = true;
    }

    void OnDisable ()
    {
      GameObject.Destroy (this.gameObject);
    }
  }
}
