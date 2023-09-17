/// <summary>
/// Represents a three-dimensional vector and provides vector operations.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 18 September 2023
public class Vector
{
    // Private fields for vector components
    private float x;
    private float y;
    private float z;

    // Properties to access vector components
    public float X
    {
        get { return x; }
        set { x = value; }
    }

    public float Y
    {
        get { return y; }
        set { y = value; }
    }

    public float Z
    {
        get { return z; }
        set { z = value; }
    }

    // Default constructor initializes the vector to (0, 0, 0)
    public Vector()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
    }

    // Constructor with parameters to set the vector components
    public Vector(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    // Static methods for vector operations

    // Calculate the dot product of two vectors
    public static float Dot(Vector v, Vector l)
    {
        return (v.X * l.X) + (v.Y * l.Y) + (v.Z * l.Z);
    }

    // Calculate the cross product of two vectors
    public static Vector Cross(Vector v, Vector l) => new Vector(
        v.Y * l.Z - v.Z * l.Y,
        -(v.X * l.Z - v.Z * l.X),
        v.X * l.Y - v.Y * l.X);

    // Calculate the angle between two vectors
    public static float GetAngle(Vector v, Vector l)
    {
        return (float)Math.Acos(Dot(v, l) / (~v * ~l));
    }

    // Normalize a vector
    public static Vector Normalize(Vector v) => new Vector(
        v.X / ~v, v.Y / ~v, v.Z / ~v);

    // Compute the absolute values of a vector
    public static Vector Abs(Vector v) => new Vector(
        Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z));

    // Overloaded operators

    // Multiply a vector by a scalar
    public static Vector operator *(float k, Vector v) => new Vector(
        k * v.X, k * v.Y, k * v.Z);

    // Multiply a vector by a scalar
    public static Vector operator *(Vector v, float k) => new Vector(
        k * v.X, k * v.Y, k * v.Z);

    // Add two vectors
    public static Vector operator +(Vector l, Vector v) => new Vector(
        l.X + v.X, l.Y + v.Y, l.Z + v.Z);

    // Unary plus operator
    public static Vector operator +(Vector v) => new Vector(
        v.X, v.Y, v.Z);

    // Negate a vector
    public static Vector operator -(Vector v) => new Vector(
        -1 * v.X, -1 * v.Y, -1 * v.Z);

    // Subtract two vectors
    public static Vector operator -(Vector l, Vector v) => new Vector(
        l.X - v.X, l.Y - v.Y, l.Z - v.Z);

    // Compute the magnitude of a vector
    public static float operator ~(Vector v) =>
        (float)Math.Sqrt((v.X * v.X) + (v.Y * v.Y) + (v.Z + v.Z));

    // Override ToString method to provide a string representation of the vector
    public override string ToString()
    {
        return "x: " + this.X.ToString() +
               " y: " + this.Y.ToString() +
               " z: " + this.Z.ToString();
    }

    // Override Equals method to compare two vectors for equality
    public override bool Equals(object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        } else
        {
            Vector p = (Vector)obj;
            return (this.X == p.X) && (this.Y == p.Y) && (this.Z == p.Z);
        }
    }

    // Override GetHashCode method to generate a hash code for the vector
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
}
