using System.Diagnostics;
using System;


/// <summary>
/// Controller for HW5
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 28 November 2023
/// Time spent: ~5 hours
public class HW5Controller
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

        Stopwatch stopwatch = new Stopwatch();

        int totalTrials = 100;

        for (int numberOfThreads = 1; numberOfThreads < 2; numberOfThreads++)
        {
            long milsecAverage = 0;
            for (int numTrials = 0; numTrials < totalTrials; numTrials++)
            {
                stopwatch.Start();
                c2.RenderImageParallel("SphereScene2.bmp", scene2, numberOfThreads);
                stopwatch.Stop();

                milsecAverage += stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();
                Console.WriteLine("Trial {0} is done", numTrials);
            }

            Console.WriteLine("Number of Threads: {0}, Average Mills: {1}", numberOfThreads, milsecAverage/totalTrials);
            stopwatch.Reset();
        }

        // Console.Error.WriteLine("Executing sequential loop...");
        // stopwatch.Start();
        // c2.RenderImage("SphereScene1.bmp", scene2);
        // stopwatch.Stop();
        // Console.Error.WriteLine("Sequential loop time in milliseconds: {0}", stopwatch.ElapsedMilliseconds);

        // stopwatch.Reset();

        // Console.Error.WriteLine("Executing parallel loop...");
        // stopwatch.Start();
        // c2.RenderImageParallel("SphereScene2.bmp", scene2);
        // stopwatch.Stop();
        // Console.Error.WriteLine("Parallel loop time in milliseconds: {0}", stopwatch.ElapsedMilliseconds);


    }
}
