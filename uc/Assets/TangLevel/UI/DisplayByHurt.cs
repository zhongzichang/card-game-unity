using System;
using UnityEngine;

namespace TangLevel
{
  public class DisplayByHurt : MonoBehaviour
  {
    /// <summary>
    /// 缺省持续时间
    /// </summary>
    public const float DEFAULT_TIME = 5f;
    /// <summary>
    /// 显示持续时间
    /// </summary>
    private float continuedTime = DEFAULT_TIME;
    /// <summary>
    /// 剩余时间
    /// </summary>
    private float remain = 0f;

    void Start(){
      continuedTime = DEFAULT_TIME;
    }

    void Update ()
    {


      if (remain > 0) {

        remain -= Time.deltaTime;

      } else {

        gameObject.SetActive (false);

      }



    }

    /// <summary>
    /// 监控 HP 变化
    /// </summary>
    /// <param name="val">Value.</param>
    /// <param name="max">Max.</param>
    public void OnHpChange (int val, int max)
    {

      if (!gameObject.activeSelf) {
        // hp 变化后 ，显示 HP Bar
        gameObject.SetActive (true);
      }

      remain = continuedTime;

    }
  }
}

