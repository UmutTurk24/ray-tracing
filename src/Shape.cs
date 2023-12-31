/// <summary>
/// A shape is an object that can be hit by a ray.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 16 November 2023

public abstract class Shape
{    

    // Define the material components
    private Vector _specular = new Vector(255f,255f,255f);
    private Vector _ambient = new Vector(30f,30f,30f); // Dark Gray
    private Vector _diffuse = new Vector(0f, 200f, 200f); // Default to cyan
    private float _shininess = 100f;

    public Vector S {
        get => _specular;
        set => _specular = value;
    }

    public Vector A {
        get => _ambient;
        set => _ambient = value;
    }

    public Vector D {
        get => _diffuse;
        set => _diffuse = value;
    }

    public float Shiny
    {
        get => _shininess;
        set
        {
            if (value <= 0f || value >= 128f)
                throw new ArgumentOutOfRangeException(nameof(Shiny), "Shininess must be between 0f and 128f.");
            _shininess = value;
        }
    }

    ///<summary> Determines if the shape object has been hit by the ray input.</summary>
    /// <param name="r">The ray.</param>
    /// <return> The intersection distiance from the ray origin. Return infinity if
    /// there is no intersection.  </return>
    public abstract float Hit(Ray r);
    ///<summary> Calculates the normal of the object at the given point on the
    /// object.</summary>
    /// <param name="p">A point on the object</param>
    /// <return> A vector of the normal of the object at that point. </return>
    public abstract Vector Normal(Vector p);

}

