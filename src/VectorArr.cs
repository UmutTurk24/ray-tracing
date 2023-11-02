using System.Net.Mail;

/// 
/// Possible improvements to the Vector class
/// Use struct
/// Make the class immutable
public class Vector
{


    // Properties to access vector components
    
    // Properties to access vector components
    private float[] vec = new float[3];

    public Vector()
    {
        /// <summary>
        /// Initializes a vector to (0, 0, 0).
        /// </summary>
        /// <returns>A vector with components (0, 0, 0).</returns>

        vec[0] = 0;
        vec[1] = 0;
        vec[2] = 0;
    }

    public Vector(float x, float y, float z)
    {
        /// <summary>
        /// Initializes a vector with the specified components.
        /// </summary>
        /// <param name="x">The x component.</param>
        /// <param name="y">The y component.</param>
        /// <param name="z">The z component.</param>
        /// <returns>A vector with the specified components.</returns>

        vec[0] = x;
        vec[1] = y;
        vec[2] = z;
    }

    public float X
    {
        get { return vec[0]; }
        set { vec[0] = value; }
    }

    public float Y
    {
        get { return vec[1]; }
        set { vec[1] = value; }
    }

    public float Z
    {
        get { return vec[2]; }
        set { vec[2] = value; }
    }

    // Static methods for vector operations

    public static float Dot(Vector v, Vector l)
    {
        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="v">The first vector.</param>
        /// <param name="l">The second vector.</param>
        /// <returns>The dot product of the two vectors.</returns>

        return (v.X * l.X) + (v.Y * l.Y) + (v.Z * l.Z);
    }

    public static Vector Cross(Vector v, Vector l) => new Vector(
        /// <summary>
        /// Calculates the cross product of two vectors.
        /// </summary>
        /// <param name="v">The first vector.</param>
        /// <param name="l">The second vector.</param>
        /// <returns>The cross product of the two vectors.</returns>

        v.Y * l.Z - v.Z * l.Y,
        -(v.X * l.Z - v.Z * l.X),
        v.X * l.Y - v.Y * l.X);

    public static float GetAngle(Vector v, Vector l)
    {
        /// <summary>
        /// Calculates the angle between two vectors.
        /// </summary>
        /// <param name="v">The first vector.</param>
        /// <param name="l">The second vector.</param>
        /// <returns>The angle between the two vectors.</returns>

        return (float)Math.Acos(Dot(v, l) / (~v * ~l));
    }


    public static void Normalize(ref Vector vec){
        /// <summary>
        /// Normalizes a vector.
        /// </summary>
        /// <returns>A normalized vector.</returns>

        var vectorLength = ~vec;
        if (vectorLength != 0) {
            if (vec.X != 0) vec.X /= vectorLength;
            if (vec.Y != 0) vec.Y /= vectorLength;
            if (vec.Z != 0) vec.Z /= vectorLength;
        }

        
    }

    public static void Abs(ref Vector vec) {
        /// <summary>
        /// Computes the absolute values of a vector.
        /// </summary>
        /// <returns>A vector with absolute values.</returns>

        vec.X = Math.Abs(vec.X);
        vec.Y = Math.Abs(vec.Y);
        vec.Z = Math.Abs(vec.Z);
    }

    // Overloaded operators
    public static Vector operator *(float k, Vector v) => new Vector(
        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="k">The scalar.</param>
        /// <param name="v">The vector.</param>
        /// <returns>The product of the scalar and the vector.</returns>
        k * v.X, k * v.Y, k * v.Z);

    public static Vector operator *(Vector v, float k) => new Vector(
        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <param name="k">The scalar.</param>
        /// <returns>The product of the scalar and the vector.</returns>

        k * v.X, k * v.Y, k * v.Z);

    public static Vector operator +(Vector l, Vector v) => new Vector(
        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="l">The first vector.</param>
        /// <param name="v">The second vector.</param>
        /// <returns>The sum of the two vectors.</returns>

        l.X + v.X, l.Y + v.Y, l.Z + v.Z);

    public static Vector operator +(Vector v) => new Vector(
        /// <summary>
        /// Returns the vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns>The vector.</returns>
        v.X, v.Y, v.Z);


    public static Vector operator -(Vector v) => new Vector(
        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns>The negated vector.</returns>

        -1 * v.X, -1 * v.Y, -1 * v.Z);


    public static Vector operator -(Vector l, Vector v) => new Vector(
        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="l">The first vector.</param>
        /// <param name="v">The second vector.</param>
        /// <returns>The difference of the two vectors.</returns>

        l.X - v.X, l.Y - v.Y, l.Z - v.Z);

    // Compute the magnitude of a vector
    public static float operator ~(Vector v) =>
        /// <summary>
        /// Computes the magnitude of a vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns>The magnitude of the vector.</returns>

        (float)Math.Sqrt((v.X * v.X) + (v.Y * v.Y) + (v.Z * v.Z));


    public override string ToString()
    {
        /// <summary>
        /// Returns a string representation of the vector.
        /// </summary>
        /// <returns>A string representation of the vector.</returns>

        return "x: " + this.X.ToString() +
               " y: " + this.Y.ToString() +
               " z: " + this.Z.ToString();
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
            Vector p = (Vector)obj;
            return (this.X == p.X) && (this.Y == p.Y) && (this.Z == p.Z);
        }
    }

    public override int GetHashCode()
    {
        /// <summary>
        /// Returns the hash code for this vector.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>

        return HashCode.Combine(X, Y, Z);
    }
}
