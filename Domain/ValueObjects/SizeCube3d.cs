using System;
namespace Domain.ValueObjects;

public class SizeCube3d
{
    public double Length_ { get; }
    public double Width_ { get; }
    public double Height_ { get; }

    public SizeCube3d(double length, double width, double height)
    {
        if (length < 0 || width < 0 || height < 0)
        {
            throw new ArgumentException("Параметры не могут быть отрицательными.");
        }

        Length_ = length;
        Width_ = width;
        Height_ = height;
    }
}
