
/// <summary>
/// Sphere class for 3D Computer Graphics
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 24 October 2023

public class Sphere : Shape
{
    private float _radius;

    private Vector _center;


    // Get Setters
    public float Radius
    {
        get => _radius;
        set => _radius = value;
    }

    public Vector Center
    {
        get => _center;
        set => _center = value;
    }

    public Sphere(Vector center, float radius)
    {
        /// <summary>
        /// Initializes a sphere with the specified center and radius.
        /// </summary>
        /// <param name="center">The center of the sphere.</param>
        /// <param name="radius">The radius of the sphere.</param>
        /// <returns>A sphere with the specified center and radius.</returns>

        _center = center;
        _radius = radius;
    }

    public override float Hit(Ray r)
    {
        /// <summary>
        /// Determines if the sphere has been hit by the ray input.
        /// </summary>
        /// <param name="r">The ray.</param>
        /// <returns>The intersection distiance from the ray origin. Return infinity if
        /// there is no intersection.</returns>

        /// Sphere Ray Intersection Formula
        /// (d*d)t^2 + 2d(o-c)t + (o-c)(o-c) - r^2 = 0
        /// Quadratic Formula: ax^2 + bx + c = 0
        /// x = t, the distance from the ray origin to the intersection point
        /// a = d*d
        /// b = 2d(o-c)
        /// c = (o-c)(o-c) - r^2

        Vector oc = r.Origin - _center; // distance

        // float a = Vector.Dot(r.Direction, r.Direction);
        float a = Vector.Dot(r.Direction, r.Direction);
        float b = Vector.Dot(oc, r.Direction);
        float c = Vector.Dot(oc, oc) - (_radius * _radius);
        float discriminant = (b * b) - (a * c);

        if (discriminant < 0)
        {
            return float.PositiveInfinity;
        }
        else
        {
            float solution1 = (float)(-b - Math.Sqrt(discriminant)) / a;
            float solution2 = (float)(-b + Math.Sqrt(discriminant)) / a;

            return (solution1 < solution2) ? solution1 : solution2;
        }

    }

    public override Vector Normal(Vector p)
    {
        /// <summary>
        /// Calculates the normal of the sphere at the given point on the sphere.
        /// </summary>
        /// <param name="p">A point on the sphere.</param>
        /// <returns>A vector of the normal of the sphere at that point.</returns>

        /// A normal on a sphere can be determined as the vector that points from the sphere’s
        /// center to the intersection point.

        Vector normal = p - _center;
        Vector.Normalize(ref normal);
        return normal;
    }

    public override bool Equals(object obj)
    {
        /// <summary>
        /// Compares two vectors for equality.
        /// </summary>
        /// <param name="obj">The object to compare with the current vector.</param>
        /// <returns>True if the specified object is equal to the current vector; otherwise, false.</returns>
        /// <exception cref="NullReferenceException">The obj parameter is null.</exception>
        /// <exception cref="InvalidCastException">The obj parameter is not a Vector object.</exception>

        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        } else
        {
            Sphere p = (Sphere)obj;
            return (this._center == p.Center) && (this._radius == p.Radius);
        }
    }

    public override int GetHashCode()
    {
        /// <summary>
        /// Returns the hash code for this vector.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>

        return HashCode.Combine(_center, _radius);
    }

}