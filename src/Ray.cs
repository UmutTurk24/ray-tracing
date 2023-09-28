
/// <summary>
/// A ray is a line with an origin and a direction.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 28 September 2023

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