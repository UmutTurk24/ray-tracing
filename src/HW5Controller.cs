using System.Diagnostics;
using System;
using System.Collections;
using System.Text;
using CsvHelper;
using System.Globalization;

/// <summary>
/// Controller for HW5
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 28 November 2023
public class HW5Controller
{
    public static void Main()
    {
        /// Do tests for 
        /// SqSphere-4 - DONE
        /// Origin-001-001
        /// Direction-5-5
        /// GSphere-3-9 - DONE
        /// SSphere-3-25 - DONE


        int totalTrials = 100;
        Stopwatch stopwatch = new Stopwatch();
        // double avgTime = 0;
        // for (int x = 0; x < totalTrials; x++)
        // {
        //     Camera c1 = new Camera(Camera.Projection.Orthographic,
        //     new Vector(0.0f, 0.0f, 150.0f),
        //     new Vector(0.0f, 0f, 0f),
        //     new Vector(0.0f, 1f, 0f),
        //     0.1f, 350f, 512, 512, -50f, 50f, -50f, 50f);

        //     Scene scene3 = new Scene();
        //     scene3.Light = new Vector(-40.0f,100.0f, 100.0f);
        //     Shape midSphere = new Sphere(new Vector(0.0f, 0.0f, 0.0f), 20f);
        //     scene3.AddShape(ref midSphere);

        //     stopwatch.Start();
        //     c1.RenderImageParallel("Gaussian.bmp", scene3, 9);
        //     stopwatch.Stop();
        //     avgTime += stopwatch.ElapsedMilliseconds;
        //     stopwatch.Reset();
        // }

        // Console.WriteLine(avgTime/totalTrials);
        

        // c1.RenderImageParallel("Gaussian.bmp", scene3, 10);


        // Stopwatch stopwatch = new Stopwatch();

        

        // List<Entry> results = new List<Entry>();
        for (int numberOfThreads = 1; numberOfThreads < 17; numberOfThreads++)
        {
            double avgTime = 0;
            for (int trialNum = 0; trialNum < totalTrials; trialNum++)
            {
                Camera c1 = new Camera(Camera.Projection.Orthographic,
                new Vector(0.0f, 0.0f, 150.0f),
                new Vector(0.0f, 0f, 0f),
                new Vector(0.0f, 1f, 0f),
                0.1f, 350f, 512, 512, -50f, 50f, -50f, 50f);

                Scene scene3 = new Scene();
                scene3.Light = new Vector(-40.0f,100.0f, 100.0f);
                Shape midSphere = new Sphere(new Vector(0.0f, 0.0f, 0.0f), 20f);
                scene3.AddShape(ref midSphere);

                stopwatch.Start();
                c1.RenderImageParallel("Gaussian.bmp", scene3, numberOfThreads);
                stopwatch.Stop();
                avgTime += stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();
            }
            avgTime /= totalTrials;
            Console.WriteLine("Current thread: {0}, avg_time: {1}", numberOfThreads, avgTime);
        }

        // using (var writer = new StreamWriter("./timeresults-size.csv"))
        // using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        // {
        //     csv.WriteRecords(results);
        // }

    }

    public class Entry
    {
        public int threadNum {get; set;}
        public int width {get; set;}
        public int sampleray {get; set;}
        public int squaresize {get; set;}
        public long time {get; set;}
    }

    public static (Camera, Scene) DefineTestCam(int sizeMultiplier, int raySampleMultiplier, int raySquareMultiplier)
    {
        // Perspective Test
        Camera c2 = new Camera(Camera.Projection.Perspective,
        new Vector(0.0f, 20.0f, 80.0f),
        new Vector(0.0f, 0f, 0f),
        new Vector(0.0f, 1f, 0f),
        0.1f, 150f, 128 * sizeMultiplier, 128 * sizeMultiplier, -10f, 10f, -10f, 10f);

        c2.SamplesPerPixel *= raySampleMultiplier;
        c2.AntialiasingSquareWidth *= raySquareMultiplier;

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

        return (c2, scene2);
    }
}
