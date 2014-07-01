using System.Collections.Generic;
using UnityEngine;
using TP = TangLevel.Playback;
namespace TangLevel
{
  public class Cache
  {

    public static Dictionary<string, Texture> textureTable = new Dictionary<string, Texture>();
    public static Dictionary<string, List<GameObject>> gobjTable = new Dictionary<string, List<GameObject>>();
    public static Dictionary<string, UnityEngine.Object> assetTable = new Dictionary<string, UnityEngine.Object>();
    public static Dictionary<int, TP.LevelRecord> recordTable = new Dictionary<int, TP.LevelRecord>();

  }
}

