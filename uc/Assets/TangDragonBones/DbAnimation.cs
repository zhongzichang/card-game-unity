using UnityEngine;
using System.Collections;
using DragonBones;
using DragonBones.Animation;
using DragonBones.Display;
using DragonBones.Factorys;
using DragonBones.Objects;
using DragonBones.Textures;

namespace TangDragonBones
{
  /// <summary>
  /// DragonBones Animation Component
  /// </summary>
  public class DbAnimation : MonoBehaviour
  {
    private static UnityFactory factory = new UnityFactory ();
    public string armatureName;
    private Armature armature;

    #region MonoBehaviour

    // Use this for initialization
    void Start ()
    {
      if (Cache.skeletonDataTable.ContainsKey (armatureName) &&
          Cache.atlasDataTable.ContainsKey (name) &&
          Cache.textureTable.ContainsKey (name)) {

        SkeletonData skeletonData = Cache.skeletonDataTable [name];
        if (factory.GetSkeletonData (name) == null) {
          factory.AddSkeletonData (skeletonData, skeletonData.Name);
        }

        AtlasData atlasData = Cache.atlasDataTable [name];
        Texture texture = Cache.textureTable [name];
        if (factory.GetTextureAtlas (name) == null) {
          factory.AddTextureAtlas (new TextureAtlas (texture, atlasData));
        }

        // build armature
        armature = factory.BuildArmature (name, null, name);

        // make child
        GameObject armatureGobj = (armature.Display as UnityArmatureDisplay).Display;
        armatureGobj.name = "body";
        armatureGobj.transform.parent = transform;
        armatureGobj.transform.localPosition = Vector3.zero;
        armatureGobj.transform.localRotation = Quaternion.identity;

      }

	
    }
    // Update is called once per frame
    void Update ()
    {
	
    }

    void OnDisable ()
    {
      //Debug.Log ("disable");
      if (armature != null)
        WorldClock.Clock.Remove (armature);
    }

    void OnEnable ()
    {
      if (armature != null) {
        WorldClock.Clock.Add (armature);
      }
    }

    void OnDestroy ()
    {
      //Debug.Log ("destroy");
      if (armature != null)
        armature.Dispose ();
    }

    #endregion
  }
}
