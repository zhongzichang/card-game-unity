using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemUpdateHandler
  {
    public delegate void UpdateToggle(string heroId);
    public delegate void UpdateMp(string heroId, int mp);

    public UpdateToggle updateToggle;
    public UpdateMp updateMp;
  }

  public class HeroStore {

    private Hashtable heroes = new Hashtable();
    private Hashtable handlers = new Hashtable();

    static private HeroStore _instance = null;
    private HeroStore(){}

    static public HeroStore Instance{
      get{
        if (_instance == null) {
            _instance = new HeroStore ();
          }
          return _instance;
        }
    }

    public void AddHero(HeroItemData hero){
      this.heroes [hero.id] = hero;
    }

    public bool HasHero(string heroId){
      return this.heroes.ContainsKey (heroId);
    }

    public HeroItemUpdateHandler GetUpdateHandler(string heroId){

      if (!this.handlers.ContainsKey(heroId)) {
        this.handlers [heroId] = new HeroItemUpdateHandler ();
      }
      return (HeroItemUpdateHandler)this.handlers [heroId];
    }

    public void UpdateToggle(string heroId){
      if (!this.handlers.ContainsKey (heroId))
        return;
      HeroItemUpdateHandler handler = (HeroItemUpdateHandler)this.handlers [heroId];
      handler.updateToggle (heroId);
    }

    private HeroItemData GetHeroById(string heroId){
      return (HeroItemData)this.heroes [heroId];
    }
  }
}
