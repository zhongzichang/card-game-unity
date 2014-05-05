using UnityEngine;
using System.Collections;

namespace TangLevel
{
  public class FillMonitor : MonoBehaviour
  {
    private UISprite sprite;

    void Start ()
    {
      sprite = GetComponent<UISprite> ();
    }

    public void OnChange (int val, int max)
    {

      if (max > 0) {

        float amount = ((float)val) / max;
        if (amount < 0)
          amount = 0;

        if (sprite != null && amount >= 0) {
          sprite.fillAmount = amount;
        }
      }
    }
  }
}