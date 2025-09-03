using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public record CreatePetRequestDto
{
    public CreatePetRequestDto(string name, int age)
    {
        Name = name;
        Age = age;
    }

    [MinLength(2)]
    public string Name { get; set; }
    [Range(0,15)] [NotNull] [Required]
    public int Age { get; set; }
}