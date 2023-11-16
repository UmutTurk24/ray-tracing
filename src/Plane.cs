
/// <summary>
/// A plane is a flat surface that extends infinitely in all directions.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 16 November 2023


public class Plane : Shape
{
    private Vector _point;
    private Vector _normal;

    public Plane() 
    {
        /// <summary>
        /// Initializes a plane with the specified normal and point.
        /// </summary>
        /// <returns> A plane with the specified normal and point. </returns>

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

        _point = point;
        _normal = normal;
    }

    public override float Hit(Ray r)
    {
        /// <summary>
        /// Determines if the plane has been hit by the ray input.
        /// </summary>
        /// <param name="r">The ray.</param>
        /// <returns>The intersection distiance from the ray origin. Return infinity if
        /// there is no intersection.</returns>

        /// Plane Ray Intersection Formula
        /// t = ((a - o) * n) / (n * d)
        /// t = distance from ray origin to intersection point
        /// n = normal to the plane
        /// a = point on the plane
        /// o = origin of the ray
        /// d = direction of the ray

        if (Vector.Dot(_normal, r.Direction) == 0) return float.PositiveInfinity;
        return Vector.Dot(_point - r.Origin, _normal) / Vector.Dot(_normal, r.Direction);
    }

    public override Vector Normal(Vector p)
    {
        /// <summary>
        /// Returns the normal of the plane.
        /// </summary>
        /// <param name="p">A point on the plane.</param>
        /// <returns>The normal of the plane.</returns>

        return _normal;
    }

}