
/// <summary>
/// Controller for Hw4
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 15 November 2023
/// Time spent: ~5 hours
public class HW4Controller
{
    public static void Main()
    {
       
        // Perspective Test
        Camera c2 = new Camera(Camera.Projection.Perspective,
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
        scene2.AddShape(ref s3);
        scene2.AddShape(ref s2);
        scene2.AddShape(ref s1);
        // c2.RenderImage("SphereScene.bmp", scene2);
        

        // Orthographic Test
        Camera c1 = new Camera(Camera.Projection.Orthographic,
        new Vector(0.0f, 100.0f, 150.0f),
        new Vector(0.0f, 0f, 0f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 350f, 512, 512, -50, 50, -50f, 50f);

        Scene scene3 = new Scene();
        scene3.Light = new Vector(-50.0f,100.0f, 30.0f);
        Shape midSphere = new Sphere(new Vector(0.0f, 0.0f, 0.0f), 20f);
        scene3.AddShape(ref midSphere);
        // Random rnd = new Random();
        // for (int i = 0; i < 10; i ++) {
        //     for (int j = 0; j < 10; j++) {
        //         Shape sph = new Sphere(new Vector(-150.0f + (i * 40), -180.0f + (j * 40), 20.0f), 10f);
        //         Vector color = new Vector(rnd.Next(255), rnd.Next(255), rnd.Next(255));
        //         sph.D = color;
        //         scene3.AddShape(ref sph);
        //     }

        // }
        
        // scene3.Light = new Vector(-30.0f,180.0f, 250.0f);
        c1.RenderImage("Sphere-.bmp", scene3);



    }
}
