using System.Collections;
using System.Numerics;

/// <summary>
/// A camera is a device that captures images. It has a position, orientation, and
/// projection type (e.g., orthographic, perspective).
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 16 November 2023
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

    private float[,] _depthBuffer;
    private int _samplesPerPixel = 1; // Count of random samples for each pixel
    private int _antialiasingSquareWidth = 0; // Width of the square for antialiasing
    private int _numberOfThreads = 4; // Number of threads to use for rendering

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

        // Set the thread number
        _numberOfThreads = (int)Math.Floor(Math.Sqrt(_width));

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

        // Set the thread number
        _numberOfThreads = (int)Math.Floor(Math.Sqrt(_width));
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

    private ArrayList CalculateColors(int threadIndex, Scene localScene)
    {
        int numberOfThreads = _numberOfThreads;
        // Create a list of columns
        ArrayList colorList = new ArrayList();
        
        Random random = new Random();
        // Last thread gets the rest
        if (threadIndex == _numberOfThreads - 1)
        {
            for (int j = (int)Math.Floor((double)threadIndex * _width / _numberOfThreads); j < _width; j++)
            {
                // Create a colors array
                Vector[] colors = new Vector[_height];
                for (int k = 0; k < _height; k++)
                {
                    // Add the calculated color to the colors array
                    colors[k] = AntialiasedColor(localScene, random, j, k);
                }
                // Append the colors array to the colorList
                colorList.Add(colors);
            }
        }
        else
        {
            
            for (int j = (int)Math.Floor((double)threadIndex * _width / numberOfThreads); j < (threadIndex + 1) * _width / numberOfThreads; j++)
            {
                // Create a colors array
                Vector[] colors = new Vector[_height];
                for (int k = 0; k < _height; k++)
                {
                    // Add the calculated color to the colors array
                    colors[k] = AntialiasedColor(localScene, random, j, k);
                }
                // Append the colors array to the colorList
                colorList.Add(colors);
            }

        }

        return colorList;


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