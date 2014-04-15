using DragonBones;
using DragonBones.Factorys;
using DragonBones.Animation;
using DragonBones.Objects;
using DragonBones.Display;
using DragonBones.Textures;
namespace TangDragonBones
{
  public class CharacterData
  {
    public SkeletonData skeletonData;
    public AtlasData atlasData;

    public CharacterData (SkeletonData skeletonData, AtlasData atlasData)
    {
      this.skeletonData = skeletonData;
      this.atlasData = atlasData;
    }
  }
}

