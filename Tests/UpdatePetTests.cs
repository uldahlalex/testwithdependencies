using Infrastructure.Postgres.Scaffolding;
using serversidevalidation.Entities;
using Service;

namespace Tests;

public class UpdatePetTests(PetService service, MyDbContext dbContext)
{
    [Fact]
    public async Task Success()
    {
        //Arrange = Everything that must exist beforehand and objects to use
        var id = Guid.NewGuid().ToString();
        var testPetToLookForAndDelete = new Pet(id: id, name: "Bob", createdAt: DateTime.UtcNow, age: 7);
        dbContext.Pets.Add(testPetToLookForAndDelete);
        await dbContext.SaveChangesAsync();
        Assert.Equal(dbContext.Pets.Count(), 1);
        var dto = new CreatePetRequestDto("New name", 2);

        //Act
        var result = await service.UpdatePet(dto, id);
        
        //Assert
        var petInDb = dbContext.Pets.First();
        Assert.Equal(petInDb.Id, id);
        Assert.Equal("New name", petInDb.Name);
        Assert.Equal("New name", result.Name);
        Assert.Equal(dbContext.Pets.Count(), 1);

    }

    [Fact]
    public async Task Failure()
    {
        
    }
}