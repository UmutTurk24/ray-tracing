
/// <summary>
/// A plane is a flat surface that extends infinitely in all directions.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 24 October 2023


class Plane : Shape
{
    private Vector _point;
    private Vector _normal;
    private Vector _color;
    public Vector Color
    {
        get => _color;
        set => _color = value;
    }

    public Plane() 
    {
        /// <summary>
        /// Initializes a plane with the specified normal and point.
        /// </summary>
        /// <returns> A plane with the specified normal and point. </returns>

        _color = new Vector(0f, 0f, 0f);
        // Set the position of the plane to the origin
        _point = new Vector(0, 0, 0);
        // Set the normal of the plane to the positive y direction
        _normal = new Vector(0, 1, 0);
    }
    public Plane(Vector normal, Vector point)
    {
        /// <summary>
        /// Initializes a plane with the specified normal and point.
        /// </summary>
        /// <param name="normal">The normal of the plane.</param>
        /// <param name="point">A point on the plane.</param>
        /// <returns>A plane with the specified normal and point.</returns>

        _color = new Vector(0f, 0f, 0f);
        _normal = normal;
        _point = point;
    }

    public override float Hit(Ray r)
    {
        /// Plane Ray Intersection Formula
        /// t = ((a - o) * n) / (n * l)
        /// t = distance from ray origin to intersection point
        /// n = normal to the plane
        /// a = point on the plane
        /// o = origin of the ray
        /// l = direction of the ray

        if (Vector.Dot(_normal, r.Direction) == 0) return float.PositiveInfinity;

        return Vector.Dot(_point - r.Origin, _normal) / Vector.Dot(_normal, r.Direction);
    }

    public override Vector Normal(Vector p)
    {
        return _normal;
    }

    public void DiffuseColor(Vector p)
    {   
        _color = p;
    }

    public override Vector GetColor()
    {
        return _color;
    }


}