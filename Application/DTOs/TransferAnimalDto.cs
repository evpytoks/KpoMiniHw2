using System;
namespace Presentation.DTOs;

public class TransferAnimalDto
{
    public required Guid Animal { get; set; }
    public required Guid Enclosure { get; set; }
}
