namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;
using System.Linq;

[TestClass]
public class PNTest
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
    public void GivDosis()
    {
        // Arrange
        Laegemiddel lm = service.GetLaegemidler().First();
        var pN = new PN(DateTime.Now, DateTime.Now.AddDays(5), 5, lm);

        // Act
        var result1 = pN.givDosis(new Dato { dato =  DateTime.Now});
        var result2 = pN.givDosis(new Dato { dato =  DateTime.Now.AddDays(10)});
        var result3 = pN.givDosis(new Dato { dato =  DateTime.Now.AddDays(2)});
        var result4 = pN.givDosis(new Dato { dato =  DateTime.Now.AddDays(-10)});

        // Assert
        Assert.IsTrue(result1);
        Assert.IsFalse(result2);
        Assert.IsTrue(result3);
        Assert.IsFalse(result4);

    }
}
