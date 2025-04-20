using System;
namespace Presentation.DTOs;

public class EnclosureDto
{
    public required int Type { get; set; }
    public required int Length { get; set; }
    public required int Width { get; set; }
    public required int Heigth { get; set; }
    public required int Quantity { get; set; }
}

