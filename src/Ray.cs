using System.Drawing;
/// <summary>
/// Represents an image with color data and provides methods to manipulate and save it.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 18 September 2023

public class Ray
{
    
    private Vector _origin;
    private Vector _direction;

    public Vector Origin
    {
        get => _origin;
        set => _origin = value;
    }

    public Vector Direction
    {
        get => _direction;
        set => _direction = value;
    }

    public Ray(Vector origin, Vector direction)
    {
        _origin = origin;
        _direction = direction;
    }

    public Vector At(float t)
    {
        return _origin + t * _direction;
    }
}