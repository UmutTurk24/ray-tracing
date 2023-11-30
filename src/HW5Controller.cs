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

        c2.RenderSqImageParallel("SphereScene2.bmp", scene2, 10, 10);

        // Stopwatch stopwatch = new Stopwatch();

        // int totalTrials = 20;

        // List<Entry> results = new List<Entry>();
        // for (int numberOfThreads = 1; numberOfThreads < 10; numberOfThreads++)
        // {
        //     for (int sizeMultiplier = 1; sizeMultiplier < 10; sizeMultiplier++)
        //     {
        //         for (int raySampleMultiplier = 1; raySampleMultiplier < 10; raySampleMultiplier++)
        //         {
        //             for (int raySquareMultiplier = 1; raySquareMultiplier < 10; raySquareMultiplier++)
        //             {
        //                 for (int trialNum = 0; trialNum < totalTrials; trialNum++)
        //                 {
        //                     stopwatch.Start();
        //                     var cams = DefineTestCam(sizeMultiplier, raySampleMultiplier, raySquareMultiplier);
        //                     cams.Item1.RenderImageParallel("SphereScene2.bmp", cams.Item2, numberOfThreads);
        //                     stopwatch.Stop();

        //                     results.Add( new Entry {threadNum = numberOfThreads, 
        //                                             sampleray = raySampleMultiplier * 5,
        //                                             squaresize = raySquareMultiplier,
        //                                             time = stopwatch.ElapsedMilliseconds,
        //                                             width = 128*sizeMultiplier});
        //                     stopwatch.Reset();
        //                 }
        //             }
        //         }
        //     }

        //     Console.WriteLine("Current thread: {0}" + numberOfThreads);
        // }

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
