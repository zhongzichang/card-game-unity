using DragonBones;
using DragonBones.Factorys;
using DragonBones.Animation;
using DragonBones.Objects;
using DragonBones.Display;
using DragonBones.Textures;
namespace TangDragonBones
{
  public class AvatarData
  {
    public SkeletonData skeletonData;
    public TextureAtlas textAtlas;

    public AvatarData (SkeletonData skeletonData, TextureAtlas textAtlas)
    {
      this.skeletonData = skeletonData;
      this.textAtlas = textAtlas;
    }
  }
}

