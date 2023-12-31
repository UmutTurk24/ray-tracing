using System.Drawing;
/// <summary>
/// Represents an image with color data and provides methods to manipulate and save it.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 18 September 2023

public class TestImage
{
    // Two-dimensional array to store color data
    private Color[,] _image;
    // Gamma correction factor for image
    private double _gamma;
    // Width/Height of the image
    private int _width;
    public int Width
    {
        get { return _width; }
        set {
            /// <summary>
            /// Sets the width of the image to the specified value.
            /// </summary>
            /// <param name="value">The new width of the image.</param>
            /// <returns>void</returns>
            var newImage = new Color[value, _height];
            for (int i = 0; i < value; i++) for (int j = 0; j < _height; j++) newImage[i,j] = _image[i,j];
            _image = newImage;
            _width = value;
        }
    }
    private int _height;
    public int Height
    {
        get { return _height; }
        set {
            /// <summary>
            /// Sets the height of the image to the specified value.
            /// </summary>
            /// <param name="value">The new height of the image.</param>
            /// <returns>void</returns>
            var newImage = new Color[_width, value];
            for (int i = 0; i < _width; i++) for (int j = 0; j < value; j++) newImage[i, j] = _image[i, j];
            _image = newImage;
            _height = value; 
        }
    }


    public TestImage(int _width = 512, int _height = 512, double gamma = 1.8)
    {
        /// <summary>
        /// Initializes an image with the specified width, height and gamma correction factor.
        /// </summary>
        /// <param name="_width">The width of the image.</param>
        /// <param name="_height">The height of the image.</param>
        /// <param name="gamma">The gamma correction factor of the image.</param>
        /// <returns>An image with the specified width, height and gamma correction factor.</returns>

        this._gamma = gamma;
        _image = new Color[_width, _height];
        for (int i = 0; i < _width; i++) for (int j = 0; j < _height; j++)
            _image[i, j] = Color.Black;
        this._width = _width;
        this._height = _height;
    }
    
    // Method to set a pixel color at the specified position
    public void Paint(int i, int j, TestVector color, int alpha = 255) {
        /// <summary>
        /// Sets the color of the pixel at the specified position to the specified color.
        /// </summary>
        /// <param name="i">The x coordinate of the pixel.</param>
        /// <param name="j">The y coordinate of the pixel.</param>
        /// <param name="color">The color to set the pixel to.</param>
        /// <param name="alpha">The alpha value of the pixel.</param>
        /// <returns>void</returns>
        
        
        // Check if pixel is out of bounds
        if (i < 0 || i >= _width || j < 0 || j >= _height) return;

        // Clamp color values to [0, 255]
        color.X = (float) Math.Clamp(color.X, 0, 255);
        color.Y = (float) Math.Clamp(color.Y, 0, 255);
        color.Z = (float) Math.Clamp(color.Z, 0, 255);
    
        // Set the pixel color
        _image[i,j] = Color.FromArgb(alpha, (int)color.X, (int)color.Y, (int)color.Z);
    }


    public void SaveImage(string fileName)
    {
        /// <summary>
        /// Saves the image to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the image to.</param>
        /// <returns>void</returns>

        // Create a Bitmap object for the image
        Bitmap solution = 
            new(_width, _height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

        // Calculate the gamma correction factor
        var gammaFactor = 1 / _gamma;
        
        // Copy color data from the image array to the Bitmap with the Gamma Correction
        for (int i = 0; i < _width; i++) for (int j = 0; j < _height; j++) {
            var color = _image[i,j];

            // Console.WriteLine(Math.Pow(color.R/255, gammaFactor) * 255);
            // Console.WriteLine(Math.Pow(color.R/255, gammaFactor) * 255);
            // Console.WriteLine(Math.Pow(color.R/255, gammaFactor) * 255);
            
            solution.SetPixel(i,j, Color.FromArgb(
                color.A,
                (int) (Math.Pow(color.R/255.0, gammaFactor) * 255),
                (int) (Math.Pow(color.G/255.0, gammaFactor) * 255),
                (int) (Math.Pow(color.B/255.0, gammaFactor) * 255)
            ));
        }

        solution.Save(fileName);
    }

    // Override ToString method to provide a string representation of the image, for testing
    public override string ToString()
    {
        /// <summary>
        /// Returns a string representation of the image.
        /// </summary>
        /// <returns>A string representation of the image.</returns>

        var arrayRepresentation = "";
        for (int i = 0; i < _width; i++) 
        {
            for (int j = 0; j < _height; j++)
            {
                arrayRepresentation += _image[i,j].ToString() + " || ";
            }
            arrayRepresentation += "\n";
        }
        arrayRepresentation += "";
        return arrayRepresentation;
    }
}