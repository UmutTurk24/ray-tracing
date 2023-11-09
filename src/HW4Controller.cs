/// <summary>
/// Controller for Hw3
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 15 November 2023
/// Time spent: ~5 hours

public class HW4Controller
{
    public static void Main()
    {
       

        Camera c = new Camera(Camera.Projection.Perspective,
        new Vector(0.0f, 20.0f, 100.0f),
        new Vector(0.0f, 0f, 0f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 150f, 512, 512, -10f, 10f, -10f, 10f);
        //Build the scene
        Scene scene2 = new Scene();
        Shape s = new Sphere(new Vector(0.0f, 10.0f, 50.0f), 20f);
        Shape s2 = new Sphere(new Vector(50.0f, 15.0f, 10.0f), 30f);
        Shape s3 = new Sphere(new Vector(-60f, 30f, -10.0f), 60f);

        Shape p2 = new Plane();
        scene2.AddShape(ref p2);
        scene2.AddShape(ref s3);
        scene2.AddShape(ref s2);
        scene2.AddShape(ref s);
        c.RenderImage("test4.bmp", scene2);
    }
}
