using System.Collections.Generic;
using UnityEngine;
using DragonBones;

namespace TangDragonBones
{
  public class Cache
  {
    public static Dictionary<string, List<GameObject>> gobjTable = new Dictionary<string, List<GameObject>>();

    public static Dictionary<string, AvatarData> characterDataTable = new Dictionary<string, AvatarData> ();
  }
}

