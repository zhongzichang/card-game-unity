using System.Collections.Generic;
using UnityEngine;

namespace TangDragonBones
{
  public class Cache
  {
    public static Dictionary<string, List<GameObject>> gobjTable = new Dictionary<string, List<GameObject>> ();

    public static Dictionary<string, CharacterData> characterDataTable = new Dictionary<string, CharacterData> ();
    public static Dictionary<string, Texture> textureTable = new Dictionary<string, Texture> ();
  }
}

