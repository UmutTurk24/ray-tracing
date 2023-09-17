using System.Drawing;
/// <summary>
/// Represents an image with color data and provides methods to manipulate and save it.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 18 September 2023

public class Image
{
    // Two-dimensional array to store color data
    Color[,] image;
    // Gamma correction factor for image
    private double gamma;
    // Width/Height of the image
    private int width;
    public int Width
    {
        get { return width; }
        set {
            // Create a new image array with the specified width, mimicing cropping
            var newImage = new Color[value, height];
            for (int i = 0; i < value; i++) for (int j = 0; j < height; j++) newImage[i,j] = image[i,j];
            image = newImage;
            width = value;
        }
    }
    private int height;
    public int Height
    {
        get { return height; }
        set {
            // Create a new image array with the specified height, mimicing cropping
            var newImage = new Color[width, value];
            for (int i = 0; i < width; i++) for (int j = 0; j < value; j++) newImage[i, j] = image[i, j];
            image = newImage;
            height = value; 
            }
    }

    // Constructor to initialize the image with specified width, height, and gamma
    public Image(int width = 512, int height = 512, double gamma = 1.8)
    {
        this.gamma = gamma;
        image = new Color[width, height];
        this.width = width;
        this.height = height;
    }
    // Method to set a pixel color at the specified position
    public void Paint(int i, int j, Vector color, int alpha = 255) {
        image[i,j] = Color.FromArgb((int)color.X, (int)color.Y, (int)color.Z, alpha);
    }


    public void SaveImage(string fileName)
    {
       // Create a Bitmap object for the image
        Bitmap solution = 
            new(width, height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

        // Copy color data from the image array to the Bitmap
        for (int i = 0; i < width; i++) for (int j = 0; j < height; j++) 
            solution.SetPixel(i,j, image[i,j]);

        solution.Save(fileName);
    }
    // Override ToString method to provide a string representation of the image, for testing
    public override string ToString()
    {
        var arrayRepresentation = "";
        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < height; j++)
            {
                arrayRepresentation += image[i,j].ToString() + " || ";
            }
            arrayRepresentation += "\n";
        }
        arrayRepresentation += "";
        return arrayRepresentation;
    }
}