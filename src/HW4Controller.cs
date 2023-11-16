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
       

        Camera c2 = new Camera(Camera.Projection.Orthographic,
        new Vector(0.0f, 20.0f, 80.0f),
        new Vector(0.0f, 0f, 0f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 150f, 512, 512, -10f, 10f, -10f, 10f);
        //Build the scene
        Scene scene2 = new Scene();
        scene2.Light = new Vector(-30.0f,100.0f, 80.0f);
        Shape s1 = new Sphere(new Vector(0.0f, 20.0f, 50.0f), 20f);
        Shape s2 = new Sphere(new Vector(-50.0f, 25.0f, 40.0f), 20f);
        Shape s3 = new Sphere(new Vector(60f, 40f, -10.0f), 70f);

        s1.D = new Vector(0, 0, 255f);
        s2.D = new Vector(0, 255f, 0f);
        s3.D = new Vector(255f, 0f, 0f);

        Shape p2 = new Plane();
        scene2.AddShape(ref p2);
        // scene2.AddShape(ref s3);
        scene2.AddShape(ref s2);
        scene2.AddShape(ref s1);
        // c2.RenderImage("SphereArray.bmp", scene2);
        

        // Orthographic Test
        Camera c1 = new Camera(Camera.Projection.Orthographic,
        new Vector(0.0f, 100.0f, 150.0f),
        new Vector(0.0f, 0f, 0f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 150f, 512, 512, -100f, 100f, -100f, 100f);
        Scene scene3 = new Scene();
        Shape s4 = new Sphere(new Vector(0.0f, 20.0f, 20.0f), 20f);
        scene3.AddShape(ref s4);
        scene3.Light = new Vector(-30.0f,100.0f, 80.0f);
        c1.RenderImage("test.bmp", scene3);



    }
}
