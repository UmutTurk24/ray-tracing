

using System.Diagnostics;

/// <summary>
/// Controller for Hw2.5
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 18 October 2023
public class HW2sController
{
    /// <summary>
    ///  HOW TO USE
    ///  This controller expects Ray, Camera, Vector, Image classes, and their corresponding Test classes
    ///  Student submission: Ray, Corresponding Test Class: TestRay
    ///  
    ///  Running the file should produce tester_result.txt in the current directory
    /// </summary>
    static string filePath = "tester_result.txt";
    static int testCounter = 0;
    public static void Main() {
        
        CreateFiles(filePath);
        TestCameraSimpleConstructor();
        TestCameraConstructor();
        TestRayConstructor();
        TestOrthographicCalculation();
        TestPerspectiveCalculation();
    }

    static void CreateFiles(string filePath)
    {
        try
        {
            // Create the first file
            using (FileStream file = File.Create(filePath))
            {
                Console.WriteLine($"File 1 created at: {filePath}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void WriteToFile(string filePath, string content)
    {
        try
        {
            // Append the content to the file
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(content);
            }

            Console.WriteLine($"Data written to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
        }
    }

    static void WriteTestCase(string testDescription, string resultString) {

        string formattedTestDescription = $"/// TEST NUMBER {testCounter} /// \n" 
            + "TEST DESCRIPTION: " + testDescription + "\n"; 
        // Add the header and the reuslt for the test case
        WriteToFile(filePath, formattedTestDescription);
        WriteToFile(filePath, resultString);
        WriteToFile(filePath, "\n\n");
        testCounter ++;
    }

    static bool VectorEquals(Vector vec1, TestVector vec2) {
        return  vec1.X == vec2.X &&
                vec1.Y == vec2.Y &&
                vec1.Z == vec2.Z;

    }

    static void TestCameraSimpleConstructor() {

        Camera c1 = new Camera();
        TestCamera c2 = new TestCamera();

        // Verifying direction vectors are equal to each other
        string testDirectionDescription = "Verifying the direction construction for TestCameraSimpleConstructor";
        Vector student_u = c1.U;
        Vector student_v = c1.V;
        Vector student_w = c1.W;

        TestVector test_u = c2.U;
        TestVector test_v = c2.V;
        TestVector test_w = c2.W;

        bool areVectorsEqual = 
            VectorEquals(student_u,test_u) &&
            VectorEquals(student_v,test_v) &&
            VectorEquals(student_w,test_w);

        string resultDirectionString = 
            $"Are vectors equal: {areVectorsEqual}\n" +
            $"student_u '{student_u}' == test_u '{test_u}': RESULT {VectorEquals(student_u,test_u)}\n" +
            $"student_v '{student_v}' == test_v '{test_v}': RESULT {VectorEquals(student_v,test_v)}\n" +
            $"student_w '{student_w}' == test_w '{test_w}': RESULT {VectorEquals(student_w,test_w)}";
            
        WriteTestCase(testDirectionDescription, resultDirectionString);

        // Verifying the default values
        string testDefaultDescription = "Verifying the default values of constructor for TestCameraSimpleConstructor";

        Vector student_eye = c1.Eye;
        Vector student_lookAt = c1.LookAt;
        Vector student_up = c1.Up;
        float student_near = c1.Near;
        float student_far = c1.Far;
        float student_width = c1.Width;
        float student_height = c1.Height;
        float student_left = c1.Left;
        float student_right = c1.Right;
        float student_bottom = c1.Bottom;
        float student_top = c1.Top;

        TestVector test_eye = c2.Eye;
        TestVector test_lookAt = c2.LookAt;
        TestVector test_up = c2.Up;
        float test_near = c2.Near;
        float test_far = c2.Far;
        float test_width = c2.Width;
        float test_height = c2.Height;
        float test_left = c2.Left;
        float test_right = c2.Right;
        float test_bottom = c2.Bottom;
        float test_top = c2.Top;

        bool areDefaultValuesEqual =
            VectorEquals(student_eye,test_eye) &&
            VectorEquals(student_lookAt,test_lookAt) &&
            VectorEquals(student_up,test_up) &&
            student_near.Equals(test_near) &&
            student_far.Equals(test_far) &&
            student_width.Equals(test_width) &&
            student_height.Equals(test_height) &&
            student_left.Equals(test_left) &&
            student_right.Equals(test_right) &&
            student_bottom.Equals(test_bottom) &&
            student_top.Equals(test_top);

        string resultDefaultString =
            $"Are default values equal: {areDefaultValuesEqual}\n" +
            $"student_eye '{student_eye}' == test_eye '{test_eye}': {VectorEquals(student_eye, test_eye)}\n" +
            $"student_lookAt '{student_lookAt}' == test_lookAt '{test_lookAt}': {VectorEquals(student_lookAt, test_lookAt)}\n" +
            $"student_up '{student_up}' == test_up '{test_up}': {VectorEquals(student_up, test_up)}\n" +
            $"student_near '{student_near}' == test_near '{test_near}': {student_near.Equals(test_near)}\n" +
            $"student_far '{student_far}' == test_far '{test_far}': {student_far.Equals(test_far)}\n" +
            $"student_width '{student_width}' == test_width '{test_width}': {student_width.Equals(test_width)}\n" +
            $"student_height '{student_height}' == test_height '{test_height}': {student_height.Equals(test_height)}\n" +
            $"student_left '{student_left}' == test_left '{test_left}': {student_left.Equals(test_left)}\n" +
            $"student_right '{student_right}' == test_right '{test_right}': {student_right.Equals(test_right)}\n" +
            $"student_bottom '{student_bottom}' == test_bottom '{test_bottom}': {student_bottom.Equals(test_bottom)}\n" +
            $"student_top '{student_top}' == test_top '{test_top}': {student_top.Equals(test_top)}";

    WriteTestCase(testDefaultDescription, resultDefaultString);
    }
    static void TestCameraConstructor() {

        Camera c1 = new Camera(Camera.Projection.Perspective,
            new Vector(0.0f, 0.0f, 50.0f),
            new Vector(0.0f, 0.0f, 0.0f),
            new Vector(0.0f, 1.0f, 0.0f));

        TestCamera c2 = new TestCamera(TestCamera.Projection.Perspective,
            new TestVector(0.0f, 0.0f, 50.0f),
            new TestVector(0.0f, 0.0f, 0.0f),
            new TestVector(0.0f, 1.0f, 0.0f));

        // Verifying direction vectors are equal to each other
        string testDirectionDescription = "Verifying the direction construction for TestCameraConstructor";
        Vector student_u = c1.U;
        Vector student_v = c1.V;
        Vector student_w = c1.W;

        TestVector test_u = c2.U;
        TestVector test_v = c2.V;
        TestVector test_w = c2.W;

        bool areVectorsEqual = 
            VectorEquals(student_u,test_u) &&
            VectorEquals(student_v,test_v) &&
            VectorEquals(student_w,test_w);

        string resultDirectionString = 
            $"Are vectors equal: {areVectorsEqual}\n" +
            $"student_u '{student_u}' == test_u '{test_u}': RESULT {VectorEquals(student_u,test_u)}\n" +
            $"student_v '{student_v}' == test_v '{test_v}': RESULT {VectorEquals(student_v,test_v)}\n" +
            $"student_w '{student_w}' == test_w '{test_w}': RESULT {VectorEquals(student_w,test_w)}";
            
        WriteTestCase(testDirectionDescription, resultDirectionString);

        // Verifying the default values
        string testDefaultDescription = "Verifying the default values of constructor for TestCameraConstructor";

        Vector student_eye = c1.Eye;
        Vector student_lookAt = c1.LookAt;
        Vector student_up = c1.Up;
        float student_near = c1.Near;
        float student_far = c1.Far;
        float student_width = c1.Width;
        float student_height = c1.Height;
        float student_left = c1.Left;
        float student_right = c1.Right;
        float student_bottom = c1.Bottom;
        float student_top = c1.Top;

        TestVector test_eye = c2.Eye;
        TestVector test_lookAt = c2.LookAt;
        TestVector test_up = c2.Up;
        float test_near = c2.Near;
        float test_far = c2.Far;
        float test_width = c2.Width;
        float test_height = c2.Height;
        float test_left = c2.Left;
        float test_right = c2.Right;
        float test_bottom = c2.Bottom;
        float test_top = c2.Top;

        bool areDefaultValuesEqual =
            VectorEquals(student_eye,test_eye) &&
            VectorEquals(student_lookAt,test_lookAt) &&
            VectorEquals(student_up,test_up) &&
            student_near.Equals(test_near) &&
            student_far.Equals(test_far) &&
            student_width.Equals(test_width) &&
            student_height.Equals(test_height) &&
            student_left.Equals(test_left) &&
            student_right.Equals(test_right) &&
            student_bottom.Equals(test_bottom) &&
            student_top.Equals(test_top);

        string resultDefaultString =
            $"Are default values equal: {areDefaultValuesEqual}\n" +
            $"student_eye '{student_eye}' == test_eye '{test_eye}': {VectorEquals(student_eye, test_eye)}\n" +
            $"student_lookAt '{student_lookAt}' == test_lookAt '{test_lookAt}': {VectorEquals(student_lookAt, test_lookAt)}\n" +
            $"student_up '{student_up}' == test_up '{test_up}': {VectorEquals(student_up, test_up)}\n" +
            $"student_near '{student_near}' == test_near '{test_near}': {student_near.Equals(test_near)}\n" +
            $"student_far '{student_far}' == test_far '{test_far}': {student_far.Equals(test_far)}\n" +
            $"student_width '{student_width}' == test_width '{test_width}': {student_width.Equals(test_width)}\n" +
            $"student_height '{student_height}' == test_height '{test_height}': {student_height.Equals(test_height)}\n" +
            $"student_left '{student_left}' == test_left '{test_left}': {student_left.Equals(test_left)}\n" +
            $"student_right '{student_right}' == test_right '{test_right}': {student_right.Equals(test_right)}\n" +
            $"student_bottom '{student_bottom}' == test_bottom '{test_bottom}': {student_bottom.Equals(test_bottom)}\n" +
            $"student_top '{student_top}' == test_top '{test_top}': {student_top.Equals(test_top)}";

        WriteTestCase(testDefaultDescription, resultDefaultString);

    }

    static void TestRayConstructor() {
        Ray r1 = new Ray(new Vector(5, 10, 15), new Vector(10, 10, 100));
        TestRay r2 = new TestRay(new TestVector(5, 10, 15), new TestVector(10, 10, 100));

        string testRayConstructorDescription = "Verifying construction of Ray class and its attributes";

        bool areVectorsEqual = 
            VectorEquals(r1.Direction,r2.Direction) &&
            VectorEquals(r1.Origin,r2.Origin);
        
        Vector student_origin = r1.Origin;
        Vector student_direction = r1.Direction;
        TestVector tester_origin = r2.Origin;
        TestVector tester_direction = r2.Direction;
        
        string resultDirectionString = 
            $"Are vectors equal: {areVectorsEqual}\n" +
            $"student_origin '{student_origin}' == tester_origin '{tester_origin}': RESULT {VectorEquals(student_origin,tester_origin)}\n" +
            $"student_direction '{student_direction}' == tester_direction '{tester_direction}': RESULT {VectorEquals(student_direction,tester_direction)}";
        
        WriteTestCase(testRayConstructorDescription, resultDirectionString);
    }

    static void TestOrthographicCalculation() { 
        Camera c1 = new Camera(Camera.Projection.Orthographic,
            new Vector(5.0f, 5.0f, 5.0f),
            new Vector(10.0f, 10.0f, 10.0f),
            new Vector(0.0f, 5.0f, 0.0f));

        TestCamera c2 = new TestCamera(TestCamera.Projection.Orthographic,
            new TestVector(5.0f, 5.0f, 5.0f),
            new TestVector(10.0f, 10.0f, 10.0f),
            new TestVector(0.0f, 5.0f, 0.0f));
        
        // Verify the U and V vectors are normalized after construction
        string testUVDescription = "Verifying the U and V vectors for TestOrthographicCalculation";
        
        Vector student_u = c1.U;
        Vector student_v = c1.V;

        TestVector test_u = c2.U;
        TestVector test_v = c2.V;

        bool areVectorsEqual = 
            VectorEquals(student_u,test_u) &&
            VectorEquals(student_v,test_v);
        
        string testUResult = 
            $"Are vectors equal: {areVectorsEqual}\n" +
            $"student_u '{student_u}' == test_u '{test_u}': RESULT {VectorEquals(student_u,test_u)}\n" +
            $"student_v '{student_v}' == test_v '{test_v}': RESULT {VectorEquals(student_v,test_v)}";

        WriteTestCase(testUVDescription, testUResult);

        // Verify the origin calculation is correct at 4 random points
        string testOriginDescription = "Verifying the Origin Points for TestOrthographicCalculation";

        Vector student_eye = c1.Eye;
        TestVector test_eye = c2.Eye;
        float u1 = -5.555f;
        float u2 = 5.555f;
        float v1 = -5.555f;
        float v2 = 5.555f;

        Vector student_origin1 = student_eye + (student_u * u1) + (student_v * v1);
        Vector student_origin2 = student_eye + (student_u * u2) + (student_v * v1);
        Vector student_origin3 = student_eye + (student_u * u1) + (student_v * v2);
        Vector student_origin4 = student_eye + (student_u * u2) + (student_v * v2);

        TestVector test_origin1 = test_eye + (test_u * u1) + (test_v *v1);
        TestVector test_origin2 = test_eye + (test_u * u2) + (test_v *v1);
        TestVector test_origin3 = test_eye + (test_u * u1) + (test_v *v2);
        TestVector test_origin4 = test_eye + (test_u * u2) + (test_v *v2);

        bool areOriginsEqual = 
            VectorEquals(student_origin1,test_origin1) &&
            VectorEquals(student_origin2,test_origin2) &&
            VectorEquals(student_origin3,test_origin3) &&
            VectorEquals(student_origin4,test_origin4);

        string testOriginResult = 
            $"Are vectors equal: {areOriginsEqual}\n" +
            $"student_origin1 '{student_origin1}' == test_origin1 '{test_origin1}': RESULT {VectorEquals(student_origin1,test_origin1)}\n" +
            $"student_origin2 '{student_origin2}' == test_origin2 '{test_origin2}': RESULT {VectorEquals(student_origin2,test_origin2)}\n" +
            $"student_origin3 '{student_origin3}' == test_origin3 '{test_origin3}': RESULT {VectorEquals(student_origin3,test_origin4)}\n" +
            $"student_origin4 '{student_origin4}' == test_origin4 '{test_origin4}': RESULT {VectorEquals(student_origin4,test_origin4)}";
        
        WriteTestCase(testOriginDescription, testOriginResult);
    }

    static void TestPerspectiveCalculation() {

        Camera c1 = new Camera(Camera.Projection.Perspective,
            new Vector(5.0f, 5.0f, 5.0f),
            new Vector(10.0f, 10.0f, 10.0f),
            new Vector(0.0f, 5.0f, 0.0f));

        TestCamera c2 = new TestCamera(TestCamera.Projection.Perspective,
            new TestVector(5.0f, 5.0f, 5.0f),
            new TestVector(10.0f, 10.0f, 10.0f),
            new TestVector(0.0f, 5.0f, 0.0f));


        string testUVWDescription = "Verifying the U and V vectors for TestPerspectiveCalculation";
        
        Vector student_u = c1.U;
        Vector student_v = c1.V;
        Vector student_w = c1.W;

        TestVector test_u = c2.U;
        TestVector test_v = c2.V;
        TestVector test_w = c2.W;

        bool areVectorsEqual = 
            VectorEquals(student_u,test_u) &&
            VectorEquals(student_v,test_v) &&
            VectorEquals(student_w,test_w);
        
        string testUResult = 
            $"Are vectors equal: {areVectorsEqual}\n" +
            $"student_u '{student_u}' == test_u '{test_u}': RESULT {VectorEquals(student_u,test_u)}\n" +
            $"student_v '{student_v}' == test_v '{test_v}': RESULT {VectorEquals(student_v,test_v)}\n" +
            $"student_w '{student_w}' == test_w '{test_w}': RESULT {VectorEquals(student_w,test_w)}";
        
        WriteTestCase(testUVWDescription, testUResult);

        string testDirectionDescription = "Verifying the Direction Vector for TestPerspectiveCalculation";
        float u1 = -5.555f;
        float u2 = 5.555f;
        float v1 = -5.555f;
        float v2 = 5.555f;

        Vector student_direction1 = (student_u * u1) + (student_v * v1) - student_w;
        Vector student_direction2 = (student_u * u2) + (student_v * v1) - student_w;
        Vector student_direction3 = (student_u * u1) + (student_v * v2) - student_w;
        Vector student_direction4 = (student_u * u2) + (student_v * v2) - student_w;

        TestVector test_direction1 = (test_u * u1) + (test_v *v1) - test_w;
        TestVector test_direction2 = (test_u * u2) + (test_v *v1) - test_w;
        TestVector test_direction3 = (test_u * u1) + (test_v *v2) - test_w;
        TestVector test_direction4 = (test_u * u2) + (test_v *v2) - test_w;

        bool areDirectionEqual = 
            VectorEquals(student_direction1,test_direction1) &&
            VectorEquals(student_direction2,test_direction2) &&
            VectorEquals(student_direction3,test_direction3) &&
            VectorEquals(student_direction4,test_direction4);
        
        string testDirectionResult = 
            $"Are vectors equal: {areDirectionEqual}\n" +
            $"student_direction1 '{student_direction1}' == test_direction1 '{test_direction1}': RESULT {VectorEquals(student_direction1,test_direction1)}\n" +
            $"student_direction2 '{student_direction2}' == test_direction2 '{test_direction2}': RESULT {VectorEquals(student_direction2,test_direction2)}\n" +
            $"student_direction3 '{student_direction3}' == test_direction3 '{test_direction3}': RESULT {VectorEquals(student_direction3,test_direction3)}\n" +
            $"student_direction4 '{student_direction4}' == test_direction4 '{test_direction4}': RESULT {VectorEquals(student_direction4,test_direction4)}";

        WriteTestCase(testDirectionDescription, testDirectionResult);
        
    }

}