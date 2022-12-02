namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;
using static shared.Util;

[TestClass]
public class DagligSkævTest
{

private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database"); 
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    //Metode / Scenarie / Forventet resultat
    //Test af metoden doegnDosis med scenariet at give en dosis udenfor 24 timers intervallet med forventet output 
    //Test af metoden doegnDosis med scenariet at opgive en negativ værdi for antalEnheder af Dosis.
    public void doegnDosis_Test()
    {
        //Arrange
        Laegemiddel lm = service.GetLaegemidler().First();
        List <Dosis> dosis = new List<Dosis> {new Dosis(DateTime.Now.AddHours(10),5)};
        
        var dagligSkæv = new DagligSkæv{startDen = DateTime.Now, slutDen = DateTime.Now.AddDays(10), laegemiddel = lm, doser = dosis};
       
        //Act
        var result = dagligSkæv.doegnDosis();

        //Assert
        Assert.AreEqual(5, result);
        
    }
}