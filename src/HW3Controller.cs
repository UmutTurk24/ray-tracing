/// <summary>
/// Controller for Hw2
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 28 September 2023
/// Time spent: ~3 hours

public class HW3Controller
{
    public static void Main()
    {
        //Setup the camera
        Camera c = new Camera(Camera.Projection.Perspective,
        new Vector(0.0f, 20.0f, 100.0f),
        new Vector(0.0f, 0f, 0f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 150f, 512, 512, -10f, 10f, -10f, 10f);

        //Build the scene
        Scene scene = new Scene();
        Shape s1 = new Sphere(new Vector(0.0f, 10.0f, 50.0f), 20f);
        Shape s2 = new Sphere(new Vector(50.0f, 15.0f, 10.0f), 30f);
        Shape s3 = new Sphere(new Vector(-60f, 30f, -10.0f), 60f);
        s2.DiffuseColor = new Vector(200f, 0f, 255f);
        s3.DiffuseColor = new Vector(0.0f, 255f, 0.0f);

        Shape p1 = new Plane();
        p1.DiffuseColor = new Vector(50f, 10f, 10f);
        // scene.AddShape(ref s1);
        scene.AddShape(ref p1);
        scene.AddShape(ref s3);
        scene.AddShape(ref s2);

        c.RenderImage("test.bmp", scene);
    }
}