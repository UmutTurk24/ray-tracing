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
        new Vector(0.0f, 0.0f, 85.0f), // Eye
        new Vector(0.0f, 0f, 0f), // Lookat
        new Vector(0.0f, 1f, 0f), // Up
        0.1f, 150f, 512, 512, -10f, 10f, -10f, 10f);

        //Build the scene
        Scene scene = new Scene();


        Shape t1 = new Sphere(new Vector(-80f, 100f, 46f), 20f);
        Shape t2 = new Sphere(new Vector(-60f, 100f, 48f), 20f);
        Shape t3 = new Sphere(new Vector(-40f, 100f, 50f), 20f);
        Shape t4 = new Sphere(new Vector(-20f, 100f, 50f), 20f);
        Shape t5 = new Sphere(new Vector(0f, 100f, 50f), 20f);
        Shape t6 = new Sphere(new Vector(20f, 100f, 50f), 20f);
        Shape t7 = new Sphere(new Vector(40f, 100f, 50f), 20f);
        Shape t8 = new Sphere(new Vector(60f, 100f, 48f), 20f);
        Shape t9 = new Sphere(new Vector(80f, 100f, 46f), 20f);

        Shape b1 = new Sphere(new Vector(-80f, -100f, 46f), 20f);
        Shape b2 = new Sphere(new Vector(-60f, -100f, 48f), 20f);
        Shape b3 = new Sphere(new Vector(-40f, -100f, 50f), 20f);
        Shape b4 = new Sphere(new Vector(-20f, -100f, 50f), 20f);
        Shape b5 = new Sphere(new Vector(0f, -100f, 50f), 20f);
        Shape b6 = new Sphere(new Vector(20f, -100f, 50f), 20f);
        Shape b7 = new Sphere(new Vector(40f, -100f, 50f), 20f);
        Shape b8 = new Sphere(new Vector(60f, -100f, 48f), 20f);
        Shape b9 = new Sphere(new Vector(80f, -100f, 46f), 20f);

        Shape l1 = new Sphere(new Vector(80f, -80f, 46f), 20f);
        Shape l2 = new Sphere(new Vector(80f, -60f, 48f), 20f);
        Shape l3 = new Sphere(new Vector(80f, -40f, 50f), 20f);
        Shape l4 = new Sphere(new Vector(80f, -20f, 50f), 20f);
        Shape l5 = new Sphere(new Vector(80f, 0f, 50f), 20f);
        Shape l6 = new Sphere(new Vector(80f, 20f, 50f), 20f);
        Shape l7 = new Sphere(new Vector(80f, 40f, 50f), 20f);
        Shape l8 = new Sphere(new Vector(80f, 60f, 48f), 20f);
        Shape l9 = new Sphere(new Vector(80f, 80f, 46f), 20f);

        Shape r1 = new Sphere(new Vector(-80f, -80f, 46f), 20f);
        Shape r2 = new Sphere(new Vector(-80f, -60f, 48f), 20f);
        Shape r3 = new Sphere(new Vector(-80f, -40f, 50f), 20f);
        Shape r4 = new Sphere(new Vector(-80f, -20f, 50f), 20f);
        Shape r5 = new Sphere(new Vector(-80f, 0f, 50f), 20f);
        Shape r6 = new Sphere(new Vector(-80f, 20f, 50f), 20f);
        Shape r7 = new Sphere(new Vector(-80f, 40f, 50f), 20f);
        Shape r8 = new Sphere(new Vector(-80f, 60f, 48f), 20f);
        Shape r9 = new Sphere(new Vector(-80f, 80f, 46f), 20f);


        Shape mid = new Sphere(new Vector(0f, 0f, 25f), 50f);
        mid.DiffuseColor = new Vector(255f, 255f, 0f);

        Shape p1 = new Plane(new Vector(0f,0f,1f), new Vector(0f,0f,60f));
        p1.DiffuseColor = new Vector(0f,20,20);

        scene.AddShape(ref t1);
        scene.AddShape(ref t2);
        scene.AddShape(ref t3);
        scene.AddShape(ref t4);
        scene.AddShape(ref t5);
        scene.AddShape(ref t6);
        scene.AddShape(ref t7);
        scene.AddShape(ref t8);
        scene.AddShape(ref t9);

        scene.AddShape(ref b1);
        scene.AddShape(ref b2);
        scene.AddShape(ref b3);
        scene.AddShape(ref b4);
        scene.AddShape(ref b5);
        scene.AddShape(ref b6);
        scene.AddShape(ref b7);
        scene.AddShape(ref b8);
        scene.AddShape(ref b9);

        scene.AddShape(ref l1);
        scene.AddShape(ref l2);
        scene.AddShape(ref l3);
        scene.AddShape(ref l4);
        scene.AddShape(ref l5);
        scene.AddShape(ref l6);
        scene.AddShape(ref l7);
        scene.AddShape(ref l8);
        scene.AddShape(ref l9);

        scene.AddShape(ref r1);
        scene.AddShape(ref r2);
        scene.AddShape(ref r3);
        scene.AddShape(ref r4);
        scene.AddShape(ref r5);
        scene.AddShape(ref r6);
        scene.AddShape(ref r7);
        scene.AddShape(ref r8);
        scene.AddShape(ref r9);
        
        scene.AddShape(ref mid);
        scene.AddShape(ref p1);



        c.RenderImage("test.bmp", scene);
    }
}