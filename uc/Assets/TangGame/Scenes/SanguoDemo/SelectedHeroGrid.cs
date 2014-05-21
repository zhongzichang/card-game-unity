using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI
{

  public class SelectedHeroGrid : MonoBehaviour {

    /// 排序
    public List<string> Sort(){
      Dictionary<int, string> temp = new Dictionary<int, string>();
      List<int> orders = new List<int>();
      for (int i = 0; i < this.transform.childCount; ++i){
        Transform t = this.transform.GetChild(i);
        HeroSelectedItem item = t.GetComponent<HeroSelectedItem>();
        orders.Add(item.data.order);
        if(temp.ContainsKey(item.data.order)){
          Global.LogError(">> SelectedHeroGrid Hero order is repeat. heroId = " + item.data.id);
        }
        temp[item.data.order] = item.data.id;
      }
      orders.Sort();
      List<string> result = new List<string>();
      for(int i = 0, length = orders.Count; i < length; i++){
        result.Add(temp[orders[i]]);
      }
      return result;
    }

  }

}