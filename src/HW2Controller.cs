/// <summary>
/// Controller for Hw2
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 28 September 2023
/// Time spent: ~4 hours

public class HW2Controller
{
    public static void Main() {
        Camera c = new Camera();

        c.RenderImage("test.bmp");

        Camera c2 = new Camera(Camera.Projection.Perspective,
            new Vector(0.0f, 0.0f, 50.0f),
            new Vector(0.0f, 0.0f, 0.0f),
            new Vector(0.0f, 1.0f, 0.0f));
        
        c2.RenderImage("test2.bmp");
    }
}