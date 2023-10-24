public abstract class Shape
{    
    ///<summary> Determins if the shape object has been hit by the ray input.</summary>
    /// <param name="r">The ray.</param>
    /// <return> The intersection distiance from the ray origin. Return infinity if
    /// there is no intersection.  </return>
    public abstract float Hit(Ray r);
    ///<summary> Calculates the normal of the object at the given point on the
    /// object.</summary>
    /// <param name="p">A point on the object</param>
    /// <return> A vector of the normal of the object at that point. </return>
    public abstract Vector Normal(Vector p);

    public abstract void DiffuseColor(Vector p);
}

