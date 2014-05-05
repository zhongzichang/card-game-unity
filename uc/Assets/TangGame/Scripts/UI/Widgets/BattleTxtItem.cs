using UnityEngine;
using System.Collections;

namespace TangGame
{
  public class BattleTxtItem : ViewItem
  {
    private float duration = 1.8f;
    private float time = 0;
    private bool init = false;
    private UILabel label;
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
          label.alpha -= Time.deltaTime * 1.5f;
          Color color = label.effectColor;
          color.a -= Time.deltaTime * 1f;
          label.effectColor = color;

          if (label.alpha <= 0) {
            step = 3;
          }
        }
      }
    }

    public override void Start ()
    {
      this.isStart = true;
      label = this.GetComponent<UILabel> ();
      UpdateData ();
    }

    public override void UpdateData ()
    {
      if (!this.isStart) {
        return;
      }
      BattleTxt txt = data as BattleTxt;
      if (txt.type == BattleTxtType.Hurt) {
        if (txt.value > 0) {
          label.text = "-" + Mathf.Abs (txt.value);
        } else {
          label.text = "+" + Mathf.Abs (txt.value);
          label.color = Color.green;
        }
      } else if (txt.type == BattleTxtType.Text) {
        label.color = new Color (0, 0, 180);
        txt.crit = false;
        label.text = "击杀奖励\n+300能量";
        waitTime = 0.4f;
      }
      Vector3 tempPosition = UICamera.mainCamera.ScreenToWorldPoint (txt.position);
      this.transform.position = tempPosition;
      this.transform.localScale = Vector3.zero;
      movePosition = this.transform.localPosition;
      movePosition.z = -10;
      movePosition.y = 0;

      if (txt.type == BattleTxtType.Text) {//文字初始坐标要往上一点
        movePosition.y += 30;
      }

      this.transform.localPosition = movePosition;

      if (txt.type != BattleTxtType.Text) {//文字不移动
        movePosition.y += 80;
      }

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
