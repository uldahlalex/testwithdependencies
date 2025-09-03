using System.ComponentModel.DataAnnotations;

public record CreatePetRequestDto
{
    [MinLength(2)]
    public string Name { get; set; }
    [Range(0,15)]
    public int Age { get; set; }
}