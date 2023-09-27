public class Camera
{
    enum Projection
    {
        Perspective,
        Orthographic
    }

    private Projection _projection;
    private Vector _position;
    private Vector _lookAt;
    private Vector _up;
    private Vector _eye;
    private float _near;
    private float _far;
    private float _width;
    private float _height;
    private float _left;
    private float _right;
    private float _top;
    private float _bottom;

    public Vector Position
    {
        get => _position;
        set => _position = value;
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

    private Vector Eye 
    {
        get => _eye;
        set => _eye = value;
    }


    public Projection Projection1
    {
        get => _projection;
        set => _projection = value;
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

    public float Width
    {
        get => _width;
        set => _width = value;
    }

    public float Height
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


    
    public Camera()
    {
        /// <summary>
        /// Creates a new generic
        /// orthographic <c>Camera</c> centered at the origin. The generated image
        /// plane is 512 x 512 pixels and the viewing frustum is 2 x 2 units.
        /// </summary>
        /// <returns>A new generic orthographic <c>Camera</c> centered at the origin.</returns>

        _projection = Projection.Orthographic;
        _position = new Vector(0, 0, 0);
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
    }


    public Camera(Projection projection, Vector eye, Vector lookAt, Vector up, float near = .1f, float far = 10f,
        float width = 512, float height = 512, float left = -1f, float right = 1f, float bottom = -1f, float top = 1f)
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
        _position = eye;
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
    }

    public RenderImage() {

        
    }

    private OrthographicRender() {
        // Find the vector from eye to the pixel
        /// o = eye
        /// u = width
        /// v = height
        /// up = camera's up vector
        /// w = vector from eye to lookAt
        /// u = find it by cross product of w and up

        // Set up the image to be saved
        Image image = new Image(_width, _height);

        Vector colorBlue = new Vector(128, 200, 255);
        Vector colorWhite = new Vector(255, 255, 255);

        // Find the vector -w from eye to the pixel
        Vector w = ((_lookAt - _eye).Normalize()) * (-1);
        Vector u = (w.Cross(_up)).Normalize();

        // Find the vector v from eye to the pixel
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // Find the relative pixel position
                // (int uPixel, int vPixel) =
                //     spaceToPixelMapping(_left, _right, _bottom, _top, _width, _height, i, j);
                
                // Find the origin of each ray 
                Vector origin = _eye + (i * u) + (j * _up);

                // Normalize the origin point
                origin = origin.Normalize();

                // Define the custom color
                Vector color = ((1 - origin.X) * colorWhite)
                    + (origin.X * colorBlue);

                // Set the color of the pixel
                image.Paint(i, j, color);
            }
        }
    }

    private PerspectiveRender()
    {
        // Find the vector from eye to the pixel
        /// ray origin -> _Eye
        /// ray direction -> uU + vV - W
        /// u = width
        /// v = height

        // Set up the image to be saved
        Image image = new Image(_width, _height);

        // Our lovely colors
        Vector colorBlue = new Vector(128, 200, 255);
        Vector colorWhite = new Vector(255, 255, 255);

        // Find the vector -w from eye to the pixel
        Vector w = ((_lookAt - _eye).Normalize()) * (-1);
        Vector u = (w.Cross(_up)).Normalize();

        // Find the vector v from eye to the pixel
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // Find the relative pixel position
                Vector direction = (i * u) + (j * _up) - w;

                // Define the custom color
                Vector color = (1.0 - j) * white + direction.Y * blue;

                // Set the color of the pixel
                image.Paint(i, j, color);
            }
        }
    }

    private spaceToPixelMapping(float left, float right, float bottom, float top, float width, float height, int i, int j)
    {
        int mappedU = left + (right - left) * (i) / width;
        int mappedV = bottom + (top - bottom) * (j) / height;

        return mappedU, mappedV;
    }
}