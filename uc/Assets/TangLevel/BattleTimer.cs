using System;
using UnityEngine;

namespace TangLevel
{
  public class BattleTimer :  MonoBehaviour
  {

    public static bool running;
    public static float time;
    public static int frameIndex;

    void Start(){
      running = false;
      time = 0;
      frameIndex = 0;
    }

    void Update ()
    {
      if (running) {
        time += Time.deltaTime;
        frameIndex++;
      }
    }

    /// <summary>
    /// 战斗开始时进行回调
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnBattleStart (object sender, EventArgs args)
    {
      running = true;
      time = 0;
      frameIndex = 0;
    }

    /// <summary>
    /// 战斗结束时进行回到
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnBattleOver (object sender, EventArgs args)
    {
      running = false;
    }


  }
}

