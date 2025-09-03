using System.ComponentModel.DataAnnotations;
using Infrastructure.Postgres.Scaffolding;
using serversidevalidation.Entities;

namespace Service;


public class PetService(MyDbContext dbContext)
{
    public async Task<Pet> CreatePet(CreatePetRequestDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);
        var pet = new Pet(id: Guid.NewGuid().ToString(), createdAt: DateTime.UtcNow, age: dto.Age, name: dto.Name);
        await dbContext.Pets.AddAsync(pet);
        await dbContext.SaveChangesAsync();
        return pet;
    }
}