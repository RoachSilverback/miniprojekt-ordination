namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;


[TestClass]
public class ordination_test
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
    public void AntalDage()
    {
        // Arrange
        Laegemiddel lm = service.GetLaegemidler().First();
        var ordination1 = new DagligSkæv(DateTime.Now, DateTime.Now.AddDays(5), lm);
        var ordination2 = new DagligSkæv(DateTime.Now, DateTime.Now.AddDays(1), lm);
        var ordination3 = new DagligSkæv(DateTime.Now, DateTime.Now.AddDays(0), lm);
        var ordination4 = new DagligSkæv(DateTime.Now, DateTime.Now.AddDays(-1), lm);

        // Act
        var result1 = ordination1.antalDage();
        var result2 = ordination2.antalDage();
        var result3 = ordination3.antalDage();
        var result4 = ordination4.antalDage();

        // Assert
        Assert.AreEqual(5, result1);
        Assert.AreEqual(1, result2);
        Assert.AreNotEqual(-1, result3);
        Assert.AreNotEqual(-1, result4);
    }
}