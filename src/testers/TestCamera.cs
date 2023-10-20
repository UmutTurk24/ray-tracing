using System.Drawing;
using System.Security.Authentication.ExtendedProtection;
/// <summary>
/// A camera is a device that captures images. It has a position, a viewing direction, and an up vector.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 28 September 2023
public class TestCamera
{

    public enum Projection
    {
        Perspective,
        Orthographic
    }
    // Private fields for camera components
    private TestVector _lookAt;
    private TestVector _up;
    private TestVector _eye;
    private float _near;
    private float _far;
    private int _width;
    private int _height;
    private float _left;
    private float _right;
    private float _top;
    private float _bottom;
    private TestVector _u;
    private TestVector _v;
    private TestVector _w;
    private Projection _projection;

    // Get/Setters for camera components
    public Projection ProjectionType
    {
        get => _projection;
        set => _projection = value;
    }

    public TestVector LookAt
    {
        get => _lookAt;
        set => _lookAt = value;
    }

    public TestVector Up
    {
        get => _up;
        set => _up = value;
    }

    public TestVector Eye 
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

    public TestVector U
    {
        get => _u;
        set => _u = value;
    }

    public TestVector V
    {
        get => _v;
        set => _v = value;
    }

    public TestVector W
    {
        get => _w;
        set => _w = value;
    }
    public TestCamera()
    {
        /// <summary>
        /// Creates a new generic
        /// orthographic <c>TestCamera</c> centered at the origin. The generated image
        /// plane is 512 x 512 pixels and the viewing frustum is 2 x 2 units.
        /// </summary>
        /// <returns>A new generic orthographic <c>TestCamera</c> centered at the origin.</returns>

        _projection = Projection.Orthographic;
        _eye = new TestVector(0, 0, 0);
        _lookAt = new TestVector(0, 0, -1);
        _up = new TestVector(0, 1, 0);
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
    }


    public TestCamera(Projection projection, TestVector eye, TestVector lookAt, TestVector up, float near = .1f, float far = 10f,
        int width = 512, int height = 512, float left = -1f, float right = 1f, float bottom = -1f, float top = 1f)
    {
        /// <summary>
        /// Creates a new <c>TestCamera</c> object with the specified parameters.
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
        /// <returns>A new <c>TestCamera</c> object with the specified parameters.</returns>
        
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
    }

    private void CalculateCameraVectors() {
        _w = _eye - _lookAt;
        TestVector.Normalize(ref _w);
        _v = _up;
        TestVector.Normalize(ref _v);
        _u = TestVector.Cross(_v, _w);
        TestVector.Normalize(ref _u);
    }

    public void RenderImage(String fileName) {
        /// <summary>
        /// Renders the image and saves it to the specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the image to.</param>
        /// <returns>void</returns>
        TestImage image;
        
        if (_projection == Projection.Orthographic) 
        {
            image = OrthographicRender();
        } else if (_projection == Projection.Perspective) 
        {
            image = PerspectiveRender();
        } else {
            return;
        }

        image.SaveImage(fileName);
    }

    private TestImage OrthographicRender() {
        /// <summary>
        /// Renders the image using orthographic projection.
        /// </summary>
        /// <returns>The rendered image.</returns>

        // Set up the image to be saved
        TestImage image = new TestImage(_width, _height);

        // Magical colors
        TestVector colorBlue = new TestVector(128, 200, 255);
        TestVector colorWhite = new TestVector(255, 255, 255);

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                // Translate the pixel coordinates to the space coordinates
                (float u, float v) = spaceToPixelMapping(i,j);

                // Find the origin of each ray and normalize
                TestVector origin = _eye + (u * _u) + (v * _v);
                TestVector.Normalize(ref origin);

                // Define the ray
                TestRay ray = new TestRay(origin, -_w);

                // Define the custom color
                TestVector color = ( (float)(1.0 - ray.Origin.X) * colorWhite)
                    + (ray.Origin.X * colorBlue);   

                // Set the color of the pixel
                image.Paint(i, j, color);
            }
        }
        return image;
    }

    private TestImage PerspectiveRender()
    {
        /// <summary>
        /// Renders the image using perspective projection.
        /// </summary>
        /// <returns>The rendered image.</returns>

        // Set up the image to be saved
        TestImage image = new TestImage(_width, _height);

        // Magical colors
        TestVector colorBlue = new TestVector(128, 200, 255);
        TestVector colorWhite = new TestVector(255, 255, 255);


        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                // Translate the pixel coordinates to the space coordinates
                (float u, float v) = spaceToPixelMapping(i,j);

                // Find the direction of the ray and define the ray
                TestVector direction = (u * _u) + (v * _v) - _w;
                TestRay ray = new TestRay(_eye, -direction);

                // Define the custom color
                TestVector color = ((float) (1.0 - ray.Direction.Y) * colorWhite) + (ray.Direction.Y * colorBlue);

                // Set the color of the pixel
                image.Paint(i, j, color);
            }
        }

        return image;
    }

    private (float, float) spaceToPixelMapping(int i, int j)
    {
        /// <summary>
        /// Maps the pixel coordinates to the space coordinates.
        /// </summary>
        /// <param name="i">The x coordinate of the pixel.</param>
        /// <param name="j">The y coordinate of the pixel.</param>
        /// <returns>The space coordinates of the pixel.</returns>

        float mappedU = _left + (_right - _left) * ((float) (i)) / _width;
        float mappedV = _bottom + (_top - _bottom) * ((float) (j)) / _height;

        return (mappedU, mappedV);
    }
}