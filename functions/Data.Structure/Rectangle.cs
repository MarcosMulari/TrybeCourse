
namespace functions.Data.Structure.Rectangle;
public class Rectangle
{
    public Rectangle(int height, int width)
    {
        Height = height;
        Width = width;
    }

    public int Height { get; set; }
    public int Width { get; set; }
}
public static class RectangleExtensions
{
    public static int GetHeight(this Rectangle rectangle) => rectangle.Height;
    public static int GetWidth(this Rectangle rectangle) => rectangle.Width;
    public static int CalculateArea(this Rectangle rectangle) => rectangle.Width * rectangle.Height;
    public static Rectangle UpdateHeight(this Rectangle rectangle, int newHeight)
    {
        rectangle.Height = newHeight;
        return rectangle; 
    }

    public static Rectangle UpdateWidth(this Rectangle rectangle, int newWidth)
    {
        rectangle.Width = newWidth;
        return rectangle;
    }
    public static Rectangle UpdateDimensions(this Rectangle rectangle, int newHeight, int newWidth)
    {
        rectangle.Height = newHeight;
        rectangle.Width = newWidth;
        return rectangle;
    }
}