using Infrastructure.Postgres.Scaffolding;
using serversidevalidation.Entities;
using Service;

namespace Tests;

public class DeletePetTests(PetService petService, MyDbContext dbContext)
{

    /// <summary>
    /// Happy path test
    /// </summary>
    [Fact]
    public async Task DeletePet_ShouldSuccessfullyRemovePet()
    {
        //Arrange
        var id = Guid.NewGuid().ToString();
        var testPetToLookForAndDelete = new Pet(id: id, name: "Bob", createdAt: DateTime.UtcNow, age: 7);
        dbContext.Pets.Add(testPetToLookForAndDelete);
        await dbContext.SaveChangesAsync();
        Assert.Equal(dbContext.Pets.Count(), 1);

        
        //Act
        var result = await petService.DeletePet(id);

        //Assert
        Assert.Equal(result.Id, id);
        Assert.Equal(dbContext.Pets.Count(), 0);
    }

    /// <summary>
    /// Unhappy path test
    /// </summary>
    [Fact]
    public async Task DeletePet_ShouldThrowException_IfPetIsNotFound()
    {
        await Assert
            .ThrowsAnyAsync<KeyNotFoundException>(async () => await petService.DeletePet(""));
    }
}