// using System.Drawing.Bitmap;
using System.Drawing;
// using System;

public class Image
{
    Color [,] image;
    private double gamma;
    private int width;
    public int Width
    {
        get { return width; }
        set { width = value; }
    }

    private int height;
    public int Height
    {
        get { return height; }
        set { height = value; }
    }

    public Image(int width = 512, int height = 512, double gamma = 1.8)
    {
        this.gamma = gamma;
        image = new Color[width, height];
    }
 
    public void Paint(int i, int j, Vector color, int alpha = 255) {
        image[i,j] = Color.FromArgb((int)color.X, (int)color.Y, (int)color.Z, alpha);
    }


    public void SaveImage(string fileName)
    {
        Bitmap solution = new(width, height,
            System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            
        solution.Save(fileName);

    }
}