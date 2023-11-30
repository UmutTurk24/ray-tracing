using System.Drawing;

public class Filter
{
    public static Color[,] GaussianFilter(Color[,] image, int sigma, int size)
    {
        /// <summary>
        /// Applies a Gaussian filter to the image.
        /// </summary>
        /// <param name="image">The image to apply the filter to.</param>
        /// <returns>The filtered image.</returns>

        // Create a new image to store the filtered image
        Color[,] filteredImage = new Color[image.GetLength(0), image.GetLength(1)];

        // Create the Gaussian kernel

        double[,] kernel = new double[size, size];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                double distanceX = x - (size - 1) / 2.0;
                double distanceY = y - (size - 1) / 2.0;
                kernel[x, y] = (1 / (2 * Math.PI * sigma * sigma)) * Math.Exp(-(distanceX * distanceX + distanceY * distanceY) / (2 * sigma * sigma));
            }
        }

        // Normalize the filter
        double sum = 0;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                sum += kernel[x, y];
            }
        }

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                kernel[x, y] /= sum;
            }
        }

        for (int i = 0; i < 3; i++) // Assuming RGB color channels
        {
            for (int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    filteredImage[x, y] = Convolve(image, kernel, x, y, i);
                }
            }
        }

        return filteredImage;
    }

    private static Color Convolve(Color[,] image, double[,] kernel, int x, int y, int channel)
    {
        int width = image.GetLength(0);
        int height = image.GetLength(1);
        int size = kernel.GetLength(0);

        double resultR = 0;
        double resultG = 0;
        double resultB = 0;

        int halfSize = size / 2;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int pixelX = Math.Min(Math.Max(x - halfSize + i, 0), width - 1);
                int pixelY = Math.Min(Math.Max(y - halfSize + j, 0), height - 1);

                resultR += image[pixelX, pixelY].R * kernel[i,j];
                resultG += image[pixelX, pixelY].G * kernel[i,j];
                resultB += image[pixelX, pixelY].B * kernel[i,j];


            }
        }

        return Color.FromArgb((int)resultR, (int)resultG, (int)resultB);
    }

    
}

static class ColorExtensions
{
    public static int GetChannel(this Color color, int channel)
    {
        switch (channel)
        {
            case 0:
                return color.R;
            case 1:
                return color.G;
            case 2:
                return color.B;
            default:
                throw new ArgumentOutOfRangeException(nameof(channel), "Channel index must be 0, 1, or 2.");
        }
    }
}