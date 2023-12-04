using System.Collections;
using System.IO.Enumeration;
using System.Numerics;

/// <summary>
/// A camera is a device that captures images. It has a position, orientation, and
/// projection type (e.g., orthographic, perspective).
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 1 December 2023
public class Camera
{

    public enum Projection
    {
        Perspective,
        Orthographic
    }
    // Private fields for camera components
    private Vector _lookAt;
    private Vector _up;
    private Vector _eye;
    private float _near;
    private float _far;
    private int _width;
    private int _height;
    private float _left;
    private float _right;
    private float _top;
    private float _bottom;
    private Vector _u;
    private Vector _v;
    private Vector _w;
    private Projection _projection;

    // Get/Setters for camera components
    public Projection ProjectionType
    {
        get => _projection;
        set => _projection = value;
    }

    public Vector LookAt
    {
        get => _lookAt;
        set => _lookAt = value;
    }

    public Vector Up
    {
        get => _up;
        set => _up = value;
    }

    public Vector Eye 
    {
        get => _eye;
        set => _eye = value;
    }

    public float Near
    {
        get => _near;
        set => _near = value;
    }

    public float Far
    {
        get => _far;
        set => _far = value;
    }

    public int Width
    {
        get => _width;
        set => _width = value;
    }

    public int Height
    {
        get => _height;
        set => _height = value;
    }


    public float Left
    {
        get => _left;
        set => _left = value;
    }

    public float Right
    {
        get => _right;
        set => _right = value;
    }

    public float Bottom
    {
        get => _bottom;
        set => _bottom = value;
    }

    public float Top
    {
        get => _top;
        set => _top = value;
    }

    public Vector U
    {
        get => _u;
        set => _u = value;
    }

    public Vector V
    {
        get => _v;
        set => _v = value;
    }

    public Vector W
    {
        get => _w;
        set => _w = value;
    }

    public int SamplesPerPixel
    {
        get => _samplesPerPixel;
        set => _samplesPerPixel = value;
    }
    public int AntialiasingSquareWidth
    {
        get => _antialiasingSquareWidth;
        set => _antialiasingSquareWidth = value;
    }

    private float[,] _depthBuffer;

    // Regular Antialiasing Parameters
    private int _samplesPerPixel = 1; // Count of random samples for each pixel
    private int _antialiasingSquareWidth = 0; // Width of the square for antialiasing

    // Ray Partial Antialiasing Parameters
    private int _rayPartialBundleSize = 10; // The number of rays to be bundled for ray partial antialiasing
    private float _dxRay = 0.02f; // The factor for ray partial antialiasing in x
    private float _dyRay = 0.02f; // The factor for ray partial antialiasing in y
    private float _dxOrg = 0.0003f; // The factor for ray partial antialiasing in x
    private float _dyOrg = 0.0003f; // The factor for ray partial antialiasing in y

    public Camera()
    {
        /// <summary>
        /// Creates a new generic
        /// orthographic <c>Camera</c> centered at the origin. The generated image
        /// plane is 512 x 512 pixels and the viewing frustum is 2 x 2 units.
        /// </summary>
        /// <returns>A new generic orthographic <c>Camera</c> centered at the origin.</returns>

        _projection = Projection.Orthographic;
        _eye = new Vector(0, 0, 0);
        _lookAt = new Vector(0, 0, -1);
        _up = new Vector(0, 1, 0);
        _near = .1f;
        _far = 10f;
        _width = 512;
        _height = 512;
        _left = -1f;
        _right = 1f;
        _bottom = -1f;
        _top = 1f;

        // Calculate the u,v,w vectors
        CalculateCameraVectors();

        // Define the depth buffer - set all values to infinity
        SetupDepthBuffer();
    }


    public Camera(Projection projection, Vector eye, Vector lookAt, Vector up, float near = .1f, float far = 10f,
        int width = 512, int height = 512, float left = -1f, float right = 1f, float bottom = -1f, float top = 1f)
    {
        /// <summary>
        /// Creates a new <c>Camera</c> object with the specified parameters.
        /// </summary>
        /// <param name="projection">The projection type for the camera (e.g., Perspective, Orthographic).</param>
        /// <param name="eye">The position of the camera’s eye point in world coordinates.</param>
        /// <param name="lookAt">The location the camera is looking at in world coordinates.</param>
        /// <param name="up">The camera’s up vector.</param>
        /// <param name="near">The distance from the camera’s eye point to the near clipping plane.(default (.1). </param>
        /// <param name="far">The distance from the camera’s eye point to the far clipping plane.(default: 10).</param>
        /// <param name="width">The width of the camera’s viewport in pixels (default: 512).</param>
        /// <param name="height">The height of the camera’s viewport in pixels(default: 512).</param>
        /// <param name="left">The left boundary of the camera’s viewing frustum(default: -1.0).</param>
        /// <param name="right">The right boundary of the camera’s viewing frustum(default: 1.0).</param>
        /// <param name="bottom">The bottom boundary of the camera’s viewing frustum(default: -1.0).</param>
        /// <param name="top">The top boundary of the camera’s viewing frustum (default: 1.0).</param>
        /// </summary>
        /// <returns>A new <c>Camera</c> object with the specified parameters.</returns>
        
        _projection = projection;
        _eye = eye;
        _lookAt = lookAt;
        _up = up;
        _near = near;
        _far = far;
        _width = width;
        _height = height;
        _left = left;
        _right = right;
        _bottom = bottom;
        _top = top;

        // Calculate the u,v,w vectors
        CalculateCameraVectors();

        // Setup the Depth Buffer
        SetupDepthBuffer();
    }

    private void CalculateCameraVectors() {
        _w = _eye - _lookAt;
        Vector.Normalize(ref _w);
        _u = Vector.Cross(_up, _w);
        Vector.Normalize(ref _u);
        _v = Vector.Cross(_w, _u);
        Vector.Normalize(ref _v);
    }

    private void SetupDepthBuffer() {
        /// <summary>
        /// Sets up the depth buffer for the camera.
        /// </summary>
        /// <returns>void</returns>
        _depthBuffer = new float[_width, _height];
        for (int i = 0; i < _width; i++) 
        {
            for (int j = 0; j < _height; j++) 
            {
                _depthBuffer[i,j] = float.PositiveInfinity;
            }
        }
    }
    public void RenderImage(String fileName, Scene scene) {
        /// <summary>
        /// Renders the image and saves it to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the image to.</param>
        /// <returns>void</returns>

        Image image = new Image(_width, _height);

        Random random = new Random();

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                
                Vector antialiasedColor = AntialiasedColor(scene, random, i, j);
                // Set the color of the pixel
                image.Paint(i, j, antialiasedColor);
                
            }
        }
        image.SaveImage(fileName);
    }

    public void RenderImageParallel(String fileName, Scene scene, int numberOfThreads)
    {
        /// <summary>
        /// Renders the image and saves it to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the image to.</param>
        /// <param name="scene">The scene to render.</param>
        /// <param name="numberOfThreads">The number of threads to use for rendering.</param>

        Image image = new Image(_width, _height);
        ParallelOptions options = new ParallelOptions
        {
            MaxDegreeOfParallelism = numberOfThreads
        };

        Parallel.For(0, _width, options, i => 
        {
            Random random = new Random();
            for (int j = 0; j < _height; j++)
            {
                // Vector antialiasedColor = AntialiasedColor(scene, random, i, j);
                Vector antialiasedColor = RayPartialAntialiasedColor(scene, random, i, j);
                image.Paint(i, j, antialiasedColor);
            }
        });
        image.SaveImage(fileName);
    }

    public Vector RayPartialAntialiasedColor(Scene scene, Random random, int i, int j)
    {
        /// <summary>
        /// Calculates the antialiased color of the pixel with Ray Partial Antialiasing.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="random">The random number generator.</param>
        /// <param name="i">The x coordinate of the pixel.</param>
        /// <param name="j">The y coordinate of the pixel.</param>
        /// <returns>The antialiased color of the pixel.</returns>


        /// Tracing Ray Differentials
        /// https://graphics.stanford.edu/papers/trd/trd.pdf
        /// Homan Igehy


        // Calculate the original u,v values for ray partial antialiasing
        (float u, float v) = SpaceToPixelMapping(i,j);

        Vector accumulatedColor = new Vector(0,0,0);

        for (int k = 0; k < _rayPartialBundleSize; k++)
        {
            Ray randomRay;
            if (random.Next(0, 2) == 0) randomRay = CalculateRayDifferentialDirection(u, v, random);
            else randomRay = CalculateRayDifferentialOrigin(u, v, random);
            // randomRay = CalculateRayDifferentialDirection(u, v, random);

            Vector color = new Vector();

            foreach (Shape shape in scene)
            {
                float distance = shape.Hit(randomRay);

                // Check if the distance is less than the current distance in the depth buffer
                if (_depthBuffer[i, j] >= distance && distance > 0)
                {
                    _depthBuffer[i, j] = distance;
                    if (distance < _far && distance > _near)
                    {
                        color = CreatePixelColor(randomRay, scene, shape, distance);

                    }
                }
            }
            _depthBuffer[i, j] = float.PositiveInfinity;
            accumulatedColor += color;

        }

        return new Vector
                (accumulatedColor.X / _rayPartialBundleSize,
                accumulatedColor.Y / _rayPartialBundleSize,
                accumulatedColor.Z / _rayPartialBundleSize);
    }

    private Ray CalculateRayDifferentialDirection(float u, float v, Random random)
    {
        /// <summary>
        /// Calculates the ray differential in the direction.
        /// </summary>
        /// <returns>Ray</returns>


        // Calculate the ray differential
        int randomNumber = random.Next(0, 8);

        // float du = u + dx;
        // float dv = v + dy;
        float du = u;
        float dv = v;
        switch (randomNumber)
        {
            case 0:
                du += _dxRay;
                break;
            case 1:
                du -= _dxRay;
                break;
            case 2:
                dv += _dyRay;
                break;
            case 3:
                dv -= _dyRay;
                break;
            case 4:
                du += _dxRay;
                dv += _dyRay;
                break;
            case 5:
                du -= _dxRay;
                dv -= _dyRay;
                break;
            case 6:
                du -= _dxRay;
                dv += _dyRay;
                break;
            case 7:
                du += _dxRay;
                dv -= _dyRay;
                break;
        }
        
        if (_projection == Projection.Perspective)
        {
            // Calculate the direction of the ray for perspective projection
            Vector direction = (du * _u) + (dv * _v) - _w;
            return new Ray(_eye, direction);

        }
        else if (_projection == Projection.Orthographic)
        {
            // Calculate the direction of the ray for orthographic projection
            Vector origin = _eye + (u * _u) + (v * _v);
            Vector direction = -_w;
            return new Ray(origin, direction);

        }

        return new Ray(
            new Vector(0, 0, 0),
            new Vector(0, 0, 0)
        );
    }

    private Ray CalculateRayDifferentialOrigin(float u, float v, Random random)
    {
        /// <summary>
        /// Calculates the ray differential in the origin.
        /// </summary>
        /// <returns>Ray</returns>

        Vector jitteredEye = _eye;
        // Jitter the eye randomly in either x or y direction
        int randomNumber = random.Next(0, 6);

        switch (randomNumber)
        {
            case 0:
                jitteredEye.X += _dxOrg;
                break;
            case 1:
                jitteredEye.X -= _dxOrg;
                break;
            case 2:
                jitteredEye.Y += _dyOrg;
                break;
            case 3:
                jitteredEye.Y -= _dyOrg;
                break;
            case 4:
                jitteredEye.Z += _dxOrg;
                break;
            case 5:
                jitteredEye.Z -= _dxOrg;
                break;
        }
        
        if (_projection == Projection.Perspective)
        {
            // Calculate the direction of the ray for perspective projection
            Vector direction = (u * _u) + (v * _v) - _w;
            return new Ray(jitteredEye, direction);
        }
        else if (_projection == Projection.Orthographic)
        {
            // Calculate the direction of the ray for orthographic projection
            Vector origin = jitteredEye + (u * _u) + (v * _v);
            Vector direction = -_w;
            return new Ray(origin, direction);
        }

        return new Ray(
            new Vector(0, 0, 0),
            new Vector(0, 0, 0)
        );

    }
    public void RenderSqImageParallel(String fileName, Scene scene, int numberOfThreads, int antialiasingFactor)
    {
        /// <summary>
        /// Renders the image and saves it to the specified file with antialiasing technique defined below.
        /// </summary>
        /// <param name="fileName">The name of the file to save the image to.</param>
        /// <param name="scene">The scene to render.</param>
        /// <param name="numberOfThreads">The number of threads to use for rendering.</param>
        /// <param name="antialiasingFactor">The antialiasing factor.</param>

        /// Antialiasing Technique
        /// 1. Create a new image with width and height multiplied by the antialiasing factor
        /// 2. For each pixel in the new image, do the following:
        ///     2.1. For each subpixel in the pixel, do the following:
        ///         2.1.1. Calculate the color of the subpixel
        ///         2.1.2. Store the color of the subpixel
        ///     2.2. Average the colors of the subpixels
        ///     2.3. Store the color of the pixel
        /// 3. Create a new image with width and height divided by the antialiasing factor
        /// 4. For each pixel in the new image, do the following:
        ///     4.1. For each subpixel in the pixel, do the following:
        ///         4.1.1. Calculate the color of the subpixel
        ///         4.1.2. Store the color of the subpixel
        ///     4.2. Average the colors of the subpixels
        ///     4.3. Store the color of the pixel
        /// 5. Return the new image

        // Readjust the width and height
        _width  *= antialiasingFactor;
        _height *= antialiasingFactor;
        
        SetupDepthBuffer();

        // Create the color buffer
        Vector[,] colorBuffer = new Vector[_width, _height];
        Console.WriteLine(_height);

        ParallelOptions options = new ParallelOptions
        {
            MaxDegreeOfParallelism = numberOfThreads
        };

        Parallel.For(0, _width, options, i =>
        {
            Random random = new Random();
            for (int j = 0; j < _height; j++)
            {
                Vector antialiasedColor = AntialiasedColor(scene, random, i, j);
                colorBuffer[i,j] = antialiasedColor;
            }
        });

        // Readjust the width and height
        _width  /= antialiasingFactor;
        _height /= antialiasingFactor;

        SetupDepthBuffer();

        // Create the image
        Image image = new Image(_width, _height);

        Parallel.For(0, _width, options, i =>
        {
            for (int j = 0; j < _height; j++)
            {
                Vector color = new Vector();
                for (int k = 0; k < antialiasingFactor; k++)
                {
                    for (int l = 0; l < antialiasingFactor; l++) 
                    {
                        color += colorBuffer[(i * antialiasingFactor) + k, (j * antialiasingFactor)+ l];
                    }
                }

                // Average the pixel colors
                color.X = color.X / (int) Math.Pow(antialiasingFactor,2);
                color.Y = color.Y / (int) Math.Pow(antialiasingFactor,2);
                color.Z = color.Z / (int) Math.Pow(antialiasingFactor,2);

                image.Paint(i, j, color);
            }
        });

        image.SaveImage(fileName);
    }

    private Ray ConstructRay(float u, float v)
    {
        /// <summary>
        /// Constructs the ray given the u, v points
        /// </summary>
        /// <param name="u">The u coordinate of the pixel in space.</param>
        /// <param name="v">The v coordinate of the pixel in space.</param>
        /// <returns>The Ray with the corresponding projection</returns>
        if (_projection == Projection.Perspective)
        {
            // Calculate the direction of the ray for perspective projection
            Vector direction = (u * _u) + (v * _v) - _w;
            return new Ray(_eye, direction);
        }
        else if (_projection == Projection.Orthographic)
        {
            // Calculate the direction of the ray for orthographic projection
            Vector origin = _eye + (u * _u) + (v * _v);
            Vector direction = -_w;
            return new Ray(origin, direction);
        }

        return new Ray(
            new Vector(0,0,0),
            new Vector(0,0,0)
        );
    }

    private Vector CreatePixelColor(Ray ray, Scene scene, Shape shape, float distance)
    {
        /// <summary>
        /// Creates the color of the pixel.
        /// </summary>
        /// <param name="ray">The ray.</param>
        /// <param name="scene">The scene.</param>
        /// <param name="shape">The shape.</param>
        /// <param name="distance">The distance.</param>
        /// <returns>The color of the pixel.</returns>

        // Calculate the light color of the pixel
        // lightColor = ambientColor +
        // diffuseColor  * (lightDirection · normal) +
        // specularColor * (bisector · normal) ^ shininess  

        // Calculate the point of intersection
        Vector intersection = ray.Origin + ray.Direction * distance;

        Vector pointToEye;
        if (Camera.Projection.Orthographic == _projection)
        {
            pointToEye = -ray.Direction;
        }
        else // perspective
        {
            pointToEye = _eye - intersection;
        }
        Vector.Normalize(ref pointToEye);

        // Check if the intersection is in shadow
        Ray shadowRay = new Ray(intersection, scene.Light - intersection);
        foreach (Shape shadowShape in scene)
        {
            if (shape.Equals(shadowShape)) continue;
            float shadowDistance = shadowShape.Hit(shadowRay);
            if (shadowDistance < float.PositiveInfinity && shadowDistance > 0) {
                return new Vector(30,30,30); // Shadow
            }
        }

        // Calculate the light direction
        Vector lightDirection = scene.Light - intersection;
        Vector.Normalize(ref lightDirection);

        // Calculate the bisector h=bisector(v,l) = Normalized(v + l)
        Vector bisector = pointToEye + lightDirection;
        Vector.Normalize(ref bisector);

        // Calculate the Illumination from the source
        float I = 1f;   // For simplicty, let's call it 1.

        // Calculate the colour of the shape
        Vector shapeColor = shape.A + // Ambient Color
                            shape.D * I * Math.Max(0, Vector.Dot(lightDirection, shape.Normal(intersection))) + // Diffuse Color
                            shape.S * I * Math.Max(0, (float)Math.Pow(Vector.Dot(bisector, shape.Normal(intersection)), shape.Shiny)); // Specular Color
        
        return shapeColor;

    }

    private Vector AntialiasedColor(Scene scene, Random random, int i, int j)
    {
        /// <summary>
        /// Calculates the antialiased color of the pixel.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="random">The random number generator.</param>
        /// <param name="i">The x coordinate of the pixel.</param>
        /// <param name="j">The y coordinate of the pixel.</param>
        /// <returns>The antialiased color of the pixel.</returns>

        Vector accumulatedColor = new Vector(0,0,0);

        // Random ray sampling is done here
        for (int k = 0; k < _samplesPerPixel; k++) 
        {
            int randomI = random.Next(-_antialiasingSquareWidth, _antialiasingSquareWidth);
            int randomJ = random.Next(-_antialiasingSquareWidth, _antialiasingSquareWidth);

            randomI = Math.Clamp(randomI + i, 0, _width - 1);
            randomJ = Math.Clamp(randomJ + j, 0, _height - 1);

            (float randomU, float randomV) = SpaceToPixelMapping(randomI,randomJ);
            Ray randomRay = ConstructRay(randomU, randomV);

            Vector color = new Vector();

            foreach (Shape shape in scene)
            {
                float distance = shape.Hit(randomRay);

                // Check if the distance is less than the current distance in the depth buffer
                if (_depthBuffer[randomI, randomJ] >= distance && distance > 0)
                {
                    _depthBuffer[randomI, randomJ] = distance;
                    if (distance < _far && distance > _near) {
                        color = CreatePixelColor(randomRay, scene, shape, distance);
                        
                    }
                }
            }

            _depthBuffer[randomI, randomJ] = float.PositiveInfinity;
            accumulatedColor += color;
        }

        // Average the color
        return new Vector 
                (accumulatedColor.X / _samplesPerPixel,
                accumulatedColor.Y / _samplesPerPixel,
                accumulatedColor.Z / _samplesPerPixel);      
    }

    private (float, float) SpaceToPixelMapping(int i, int j)
    {
        /// <summary>
        /// Maps the pixel coordinates to the space coordinates.
        /// </summary>
        /// <param name="i">The x coordinate of the pixel.</param>
        /// <param name="j">The y coordinate of the pixel.</param>
        /// <returns>The space coordinates of the pixel.</returns>

        float mappedU = _left + (_right - _left) * i / _width;
        float mappedV = _bottom + (_top - _bottom) * j / _height;

        return (mappedU, mappedV);
    }
}