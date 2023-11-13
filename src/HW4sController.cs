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

        // Shape p2 = new Plane();
        // scene2.AddShape(ref p2);
        scene2.AddShape(ref s3);
        scene2.AddShape(ref s2);
        scene2.AddShape(ref s1);
        c2.RenderImage("test.bmp", scene2);
        

        Camera c1 = new Camera(Camera.Projection.Orthographic,
        new Vector(0.0f, 20.0f, 80.0f),
        new Vector(0.0f, 0f, 0f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 150f, 512, 512, -10f, 10f, -10f, 10f);
        Scene scene3 = new Scene();
        Shape s4 = new Sphere(new Vector(0.0f, 20.0f, 20.0f), 20f);
        scene3.AddShape(ref s4);
        scene3.Light = new Vector(-30.0f,100.0f, 80.0f);
        c1.RenderImage("SphereArray.bmp", scene3);

        //Setup the camera
        // Camera c = new Camera(Camera.Projection.Perspective,
        // new Vector(0.0f, 0.0f, 85.0f), // Eye
        // new Vector(0.0f, 0f, 0f), // Lookat
        // new Vector(0.0f, 1f, 0f), // Up
        // 0.1f, 150f, 1512, 1512, -10f, 10f, -10f, 10f);

        // //Build the scene
        // Scene scene = new Scene();

        // Shape t1 = new Sphere(new Vector(-80f, 100f, 46f), 20f);
        // Shape t2 = new Sphere(new Vector(-60f, 100f, 48f), 20f);
        // Shape t3 = new Sphere(new Vector(-40f, 100f, 50f), 20f);
        // Shape t4 = new Sphere(new Vector(-20f, 100f, 50f), 20f);
        // Shape t5 = new Sphere(new Vector(0f, 100f, 50f), 20f);
        // Shape t6 = new Sphere(new Vector(20f, 100f, 50f), 20f);
        // Shape t7 = new Sphere(new Vector(40f, 100f, 50f), 20f);
        // Shape t8 = new Sphere(new Vector(60f, 100f, 48f), 20f);
        // Shape t9 = new Sphere(new Vector(80f, 100f, 46f), 20f);

        // Shape b1 = new Sphere(new Vector(-80f, -100f, 46f), 20f);
        // Shape b2 = new Sphere(new Vector(-60f, -100f, 48f), 20f);
        // Shape b3 = new Sphere(new Vector(-40f, -100f, 50f), 20f);
        // Shape b4 = new Sphere(new Vector(-20f, -100f, 50f), 20f);
        // Shape b5 = new Sphere(new Vector(0f, -100f, 50f), 20f);
        // Shape b6 = new Sphere(new Vector(20f, -100f, 50f), 20f);
        // Shape b7 = new Sphere(new Vector(40f, -100f, 50f), 20f);
        // Shape b8 = new Sphere(new Vector(60f, -100f, 48f), 20f);
        // Shape b9 = new Sphere(new Vector(80f, -100f, 46f), 20f);

        // Shape l1 = new Sphere(new Vector(80f, -80f, 46f), 20f);
        // Shape l2 = new Sphere(new Vector(80f, -60f, 48f), 20f);
        // Shape l3 = new Sphere(new Vector(80f, -40f, 50f), 20f);
        // Shape l4 = new Sphere(new Vector(80f, -20f, 50f), 20f);
        // Shape l5 = new Sphere(new Vector(80f, 0f, 50f), 20f);
        // Shape l6 = new Sphere(new Vector(80f, 20f, 50f), 20f);
        // Shape l7 = new Sphere(new Vector(80f, 40f, 50f), 20f);
        // Shape l8 = new Sphere(new Vector(80f, 60f, 48f), 20f);
        // Shape l9 = new Sphere(new Vector(80f, 80f, 46f), 20f);

        // Shape r1 = new Sphere(new Vector(-80f, -80f, 46f), 20f);
        // Shape r2 = new Sphere(new Vector(-80f, -60f, 48f), 20f);
        // Shape r3 = new Sphere(new Vector(-80f, -40f, 50f), 20f);
        // Shape r4 = new Sphere(new Vector(-80f, -20f, 50f), 20f);
        // Shape r5 = new Sphere(new Vector(-80f, 0f, 50f), 20f);
        // Shape r6 = new Sphere(new Vector(-80f, 20f, 50f), 20f);
        // Shape r7 = new Sphere(new Vector(-80f, 40f, 50f), 20f);
        // Shape r8 = new Sphere(new Vector(-80f, 60f, 48f), 20f);
        // Shape r9 = new Sphere(new Vector(-80f, 80f, 46f), 20f);



        // Shape p1 = new Plane(new Vector(0f,0f,1f), new Vector(0f,0f,60f));

        // scene.AddShape(ref t1);
        // scene.AddShape(ref t2);
        // scene.AddShape(ref t3);
        // scene.AddShape(ref t4);
        // scene.AddShape(ref t5);
        // scene.AddShape(ref t6);
        // scene.AddShape(ref t7);
        // scene.AddShape(ref t8);
        // scene.AddShape(ref t9);

        // scene.AddShape(ref b1);
        // scene.AddShape(ref b2);
        // scene.AddShape(ref b3);
        // scene.AddShape(ref b4);
        // scene.AddShape(ref b5);
        // scene.AddShape(ref b6);
        // scene.AddShape(ref b7);
        // scene.AddShape(ref b8);
        // scene.AddShape(ref b9);

        // scene.AddShape(ref l1);
        // scene.AddShape(ref l2);
        // scene.AddShape(ref l3);
        // scene.AddShape(ref l4);
        // scene.AddShape(ref l5);
        // scene.AddShape(ref l6);
        // scene.AddShape(ref l7);
        // scene.AddShape(ref l8);
        // scene.AddShape(ref l9);

        // scene.AddShape(ref r1);
        // scene.AddShape(ref r2);
        // scene.AddShape(ref r3);
        // scene.AddShape(ref r4);
        // scene.AddShape(ref r5);
        // scene.AddShape(ref r6);
        // scene.AddShape(ref r7);
        // scene.AddShape(ref r8);
        // scene.AddShape(ref r9);
        
        // scene.AddShape(ref mid);
        // scene.AddShape(ref p1);

        // c.RenderImage("test3.bmp", scene);


    }
}
