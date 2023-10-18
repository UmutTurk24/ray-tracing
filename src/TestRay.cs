
/// <summary>
/// A ray is a line with an origin and a direction.
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 28 September 2023

public class TestRay
{
    // Private fields for ray components
    private TestVector _origin;
    private TestVector _direction;

    // Get/Setters for ray components
    public TestVector Origin
    {
        get => _origin;
        set => _origin = value;
    }

    public TestVector Direction
    {
        get => _direction;
        set {
             _direction = value;
             TestVector.Normalize(ref _direction);

        }            
    }

    public TestRay(TestVector origin, TestVector direction)
    {
        /// <summary>
        /// Initializes a ray with the specified origin and direction.
        /// </summary>
        /// <param name="origin">The origin of the ray.</param>
        /// <param name="direction">The direction of the ray.</param>
        /// <returns>A ray with the specified origin and direction.</returns>

        _origin = origin;
        TestVector.Normalize(ref direction);
        _direction = direction;
    }

    public TestVector At(float t)
    {
        /// <summary>
        /// Returns the point on the ray at the specified distance.
        /// </summary>
        /// <param name="t">The distance from the origin of the ray.</param>
        /// <returns>The point on the ray at the specified distance.</returns>
        return _origin + t * _direction;
    }
}