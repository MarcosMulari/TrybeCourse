namespace functions.Data.Structure.Triangle;

public class Triangle
{
    public Triangle(int baseLength, int height)
    {
        BaseLength = baseLength;
        Height = height;
    }

    public int BaseLength { get; set; }
    public int Height { get; set; }
}

public static class TriangleExtensions
{
    public static int GetBaseLength(this Triangle triangle) => triangle.BaseLength;
    public static int GetHeight(this Triangle triangle) => triangle.Height;
    public static double CalculateArea(this Triangle triangle) => (triangle.BaseLength * triangle.Height) / 2.0;
    public static Triangle UpdateBaseLength(this Triangle triangle, int newBaseLength)
    {
        triangle.BaseLength = newBaseLength;
        return triangle;
    }

    public static Triangle UpdateHeight(this Triangle triangle, int newHeight)
    {
        triangle.Height = newHeight;
        return triangle;
    }

    public static Triangle UpdateDimensions(this Triangle triangle, int newBaseLength, int newHeight)
    {
        triangle.BaseLength = newBaseLength;
        triangle.Height = newHeight;
        return triangle;
    }
}