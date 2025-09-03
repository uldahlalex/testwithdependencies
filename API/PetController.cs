using Microsoft.AspNetCore.Mvc;
using serversidevalidation.Entities;
using Service;

public class PetController(PetService petService) : ControllerBase
{
    [HttpPost(nameof(CreatePet))]
    public async Task<Pet> CreatePet([FromBody]CreatePetRequestDto dto)
    {
        return await petService.CreatePet(dto);
    }
}