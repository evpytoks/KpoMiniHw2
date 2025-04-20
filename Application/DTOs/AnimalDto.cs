using System;
namespace Presentation.DTOs;

public class AnimalDto
{
    public required string Name { get; set; }
    public required int AnimalType { get; set; }
    public required string BirthDate { get; set; }
    public required int Sex { get; set; }
    public required int FavouriteFood { get; set; }
    public required int Status { get; set; }
    public required Guid Enclosure { get; set; }
}
