/// <summary>
/// Filter class for applying filters to images.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 1 December 2023

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
        /// 

        /// Retrieved the following implementation from https://medium.com/@akumar5/computer-vision-gaussian-filter-from-scratch-b485837b6e09
        /// 
        /// Gaussian Filter is implemented as follows
        /// 1. Create a new image to store the filtered image
        /// 2. Create the Gaussian kernel based on the following formula
        ///     kernel[x, y] = (1 / (2 * Math.PI * sigma * sigma)) * Math.Exp(-(distanceX * distanceX + distanceY * distanceY) / (2 * sigma * sigma))
        ///     where distanceX = x - (size - 1) / 2.0 and distanceY = y - (size - 1) / 2.0
        ///     and sigma is the standard deviation of the Gaussian distribution
        ///     and size is the size of the kernel
        /// 3. Normalize the filter by dividing each element by the sum of all elements in the kernel
        /// 4. For each color channel in the image, do the following:
        ///    4.1. For each pixel in the image, do the following:
        ///     4.1.1. Apply the kernel to the pixel
        ///     4.1.2. Store the result in the filtered image
        ///     4.1.3. Repeat for each color channel
        ///     4.1.4. Return the filtered image

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

/*
Extra information on the terminology used. 

Convolution:In image processing, convolution is like a sliding window operation. Imagine a small window (called a kernel or filter) that slides over each pixel in an image. At each position, it calculates a weighted sum of the pixel values under the window, producing a new pixel value.

Gaussian Filter: 
The Gaussian filter is a specific type of filter with a bell-shaped curve. It's like a smoothing tool that blurs an image. The idea is to give more weight to the center pixel of the window and less weight to the surrounding pixels. This helps in reducing noise and making the image look smoother.

How Gaussian Filter Works:
For each pixel in the image, the Gaussian filter considers a small neighborhood of pixels around it.
The weights assigned to these pixels follow a Gaussian distribution, meaning the center pixel gets the most weight, and the weights decrease as you move away from the center.
The filter then calculates a weighted sum of the pixel values in this neighborhood, producing a new pixel value.
The result is a smoothed image where abrupt changes in pixel values are reduced, resulting in a blur effect.

Standard Deviation (Sigma):
The standard deviation (σ) of the Gaussian filter controls the width of the bell-shaped curve. A larger σ means a wider curve, and a smaller 
σ means a narrower curve. This affects how much smoothing or blurring is applied.

Convolution Result:
The final pixel value after convolution is a combination of neighboring pixel values, with more emphasis on pixels closer to the center. This helps in reducing details and making the image look smoother.
*/