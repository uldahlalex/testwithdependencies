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
    
    //Delete
    public async Task<Pet> DeletePet(string id)
    {
        var pet = dbContext.Pets.FirstOrDefault(p => p.Id == id) ??
            throw new KeyNotFoundException("Pet was not found");
        dbContext.Pets.Remove(pet);
        await dbContext.SaveChangesAsync();
        return pet;
    }
    
    
    //Update
    public async Task<Pet> UpdatePet(CreatePetRequestDto dto, string id)
    {
        //Find the pet in the DB
        var pet = dbContext.Pets.FirstOrDefault(p => p.Id == id) ??
                  throw new KeyNotFoundException("Pet was not found");        
        //Overwrite the properties you wish to update
        pet.Age = dto.Age;
        pet.Name = dto.Name;

        //Save the changes and return the pet
        dbContext.SaveChangesAsync();
        return pet;
    }
}