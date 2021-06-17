using NUnit.Framework;
using static Data.Database;
using static Data.DataSearcher;
using System.Linq;

namespace Tests {
  [TestFixture]
  public class CoverageTest {
    
    [Test]
    public void Test_SearchBaseTable_CaseInsensitive() {
      string[] input = new string[] {"sEaRcH", "OrGaNiZaTiOnS", "nAmE", "eX"};
      
      ImportEntitiesFromJson();
      var resultTable = SearchBaseTable(tables["organizations.json"].content, input);
      Assert.AreEqual(4, resultTable.Count());
    }

    [Test]
    public void Test_SearchBaseTable_MultipleSearchFields() {
      string[] input = new string[] {"search", "organizations", "_id", "10", "tags", "west"};
      
      ImportEntitiesFromJson();
      var resultTable = SearchBaseTable(tables["organizations.json"].content, input);
      Assert.AreEqual(1, resultTable.Count());
    }

    [Test]
    public void Test_SearchBaseTable_EmptySearchFields() {
      string[] input = new string[] {"search", "users", "role", "%"};
      
      ImportEntitiesFromJson();
      var resultTable = SearchBaseTable(tables["organizations.json"].content, input);
      Assert.AreEqual(0, resultTable.Count());
    }

    [Test]
    public void Test_SearchAndOutputRelatedEntities() {
      string[] input = new string[] {"search", "organizations", "_id", "101"};
      
      ImportEntitiesFromJson();
      ValidateAndReturnSearchResults(input);
      Assert.AreEqual(4, relatedEntities.First().Count());
      Assert.AreEqual(4, relatedEntities.Last().Count());
    }
  }
}