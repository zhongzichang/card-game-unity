using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using DragonBones.Objects;

namespace TangDragonBones
{
  public class Cache
  {
    // armature object
    public static Dictionary<string, List<Armature>> armatureTable = new Dictionary<string, List<Armature>>();

    // gameo object
    public static Dictionary<string, List<GameObject>> gobjTable = new Dictionary<string, List<GameObject>>();

    // atlas data
    public static Dictionary<string, AtlasData> atlasDataTable = new Dictionary<string, AtlasData>();

    // skeleton data
    public static Dictionary<string, SkeletonData> skeletonDataTable = new Dictionary<string, SkeletonData>();

    // texture
    public static Dictionary<string, Texture> textureTable = new Dictionary<string, Texture>();
  }
}

