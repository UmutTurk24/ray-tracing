using System.Reflection.Metadata.Ecma335;
using System;
using System.Diagnostics;

public class Vector
{
    // Get/Set Declarations and Attributes

    private float x;
    public float X {
        get { return x; }
        set { x = value; }
    }
    private float y;
    public float Y
    {
        get { return y; }
        set { y = value; }
    }

    private float z;
    public float Z
    {
        get { return z; }
        set { z = value; }
    }

    public Vector()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
    }
    public Vector(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }



    public static float Dot(Vector v, Vector l)
    {
        return (v.X * l.X) + (v.Y * l.Y) + (v.Z * l.Z);
    }
    public static Vector Cross(Vector v, Vector l) => new Vector(
            v.Z * l.Y - v.Y * l.Z,
            v.Z * l.X - v.X * l.Z,
            v.X * l.Y - v.Y * l.X);

    public static float GetAngle(Vector v, Vector l) {
        return (float)Math.Acos(Dot(v, l) / (~v * ~l));
    }   

    public static Vector Normalize(Vector v) => new Vector(
        v.X / ~v, v.Y / ~v, v.Z / ~v);
    public static Vector Abs(Vector v) => new Vector(
        Math.Abs(v.X) , Math.Abs(v.Y), Math.Abs(v.Z));

    // Overrided operators
    public static Vector operator *(float k, Vector v) => new Vector(
        k * v.X, k * v.Y, k * v.Z);
    public static Vector operator *(Vector v, float k) => new Vector(
        k * v.X, k * v.Y, k * v.Z);
    public static Vector operator +(Vector l, Vector v) => new Vector(
        l.X + v.X, l.Y + v.Y, l.Z + v.Z);
    public static Vector operator +(Vector v) => new Vector(
        v.X, v.Y, v.Z);
    public static Vector operator -(Vector v) => new Vector(
        -1 * v.X, -1 * v.Y, -1 * v.Z);
    public static Vector operator -(Vector l, Vector v) => new Vector(
        l.X - v.X, l.Y - v.Y, l.Z - v.Z);
    public static float operator ~(Vector v) => 
        (float)Math.Sqrt((v.X * v.X) + (v.Y * v.Y) + (v.Z + v.Z));

    // Overrided functions
    public override String ToString() {
        
        return "x: " + this.X.ToString() +
               " y: " + this.Y.ToString() +
               " z: " + this.Z.ToString();
    }
    public override bool Equals(Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Vector p = (Vector) obj;
            return (this.X == p.X) && (this.Y == p.Y) && (this.Z == p.Z);
        }
    }
}