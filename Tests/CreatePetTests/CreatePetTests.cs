using System.ComponentModel.DataAnnotations;
using Infrastructure.Postgres.Scaffolding;
using Service;

namespace Tests.CreatePetTests;

public class CreatePetTests(PetService petService, MyDbContext ctx)
{
    [Fact]
    public async Task CreatePet_ShouldBeAbleToSuccessfullyCreatePet_WhenNoValidationErrors()
    {
        //Arrange
        var name = "Bob";
        var validDto = new CreatePetRequestDto(name: name, age: 2);
        
        //Act
        var result = await petService.CreatePet(validDto);

        //Assert
        Assert.Equal(result.Name, name);
        Assert.Equal(ctx.Pets.First().Name, name);
    }
    
    [Theory]
    [InlineData("", 5)]
    [InlineData("asdsad", -1)]
    [InlineData("asdsad", 16)]
    [InlineData("22", 16)]
    public async Task CreatePet_ShouldThrowException_WhenValidationErrors(string name, int age)
    {
        var invaliDto = new CreatePetRequestDto(name: name, age: age);
        await Assert.ThrowsAnyAsync<ValidationException>(async() => await petService.CreatePet(invaliDto));
    }
}
