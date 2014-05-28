using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ClientDemoTest
{
  public interface IHeroService{
    bool equipItem(string heroId, string equipId);
  }

	[TestFixture]
  public class ClientDemoTest
	{
    [Test]
    public void equipedItemSuccessfully (){
      IHeroService heroService = GetHeroServiceMock (); 

      bool ret = heroService.equipItem ("heroId", "equipId");
      Assume.That (ret == false);
    }

    private IHeroService GetHeroServiceMock(){
      var mock = Substitute.For<IHeroService>();
      mock.equipItem ("heroId", "equipId").Returns (false);
      return mock;
    }
  }
}
