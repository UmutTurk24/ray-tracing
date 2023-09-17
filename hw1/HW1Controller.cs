using System;
using System.Runtime.InteropServices;
/// <summary>
/// Testing Class for Vector and Image Classes
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 18 September 2023
/// Time spent: 4 hours
/// Collaborators: C# Documentation and ChatGPT :3
/// Feedback: 
public class HW1Controller
{
    public static bool TestSimpleConstructor() 
    {
        var simpleVec = new Vector();

        return simpleVec.X == 0 &&
            simpleVec.Y == 0 &&
            simpleVec.Z == 0;
    }
    public static bool TestConstructor()
    {
        var simpleVec = new Vector(5,6,7);

        return simpleVec.X == 5 &&
            simpleVec.Y == 6 &&
            simpleVec.Z == 7;
    }
    public static bool TestDot()
    {
        var vec1 = new Vector(1, 2, 3);
        var vec2 = new Vector(4, 5, 6);
        return 32 == Vector.Dot(vec1, vec2);
    }
    public static bool TestEquals() 
    {
        var vec1 = new Vector(1, 2, 3);
        var vec2 = new Vector(4, 5, 6);
        var vec3 = new Vector(4, 5, 6);

        return !vec1.Equals(vec2) && 
            vec2.Equals(vec3) && 
            vec3.Equals(vec2) &&
            !vec3.Equals(vec1);
    }
    public static bool TestCross()
    {
        var vec1 = new Vector(1, 2, 3);
        var vec2 = new Vector(1, 5, 7);

        return Vector.Cross(vec1, vec2).Equals(new Vector(-1, -4, 3));
    }
    public static bool TestAbs()
    {
        var vec1 = new Vector(-1, 0, -1);

        return Vector.Abs(vec1).Equals(new Vector(1, 0, 1));
    }
    public static bool TestNormalize()
    {
        var vec1 = new Vector(3, 4, 0);

        return Vector.Normalize(vec1).Equals(new Vector((float)3/5, (float)4/5, 0));
    }
    public static bool TestVectorScaling()
    {
        var vec1 = new Vector(1, 2, 3);

        return (5 * vec1).Equals(new Vector(5,10,15)) &&
            (vec1 * 5).Equals(new Vector(5, 10, 15));
    }
    public static bool TestVectorAddition()
    {
        var vec1 = new Vector(1, 2, 3);
        var vec2 = new Vector(1, 2, 3);

        return (vec1 + vec2).Equals(new Vector(2,4,6));
    }
    public static bool TestFlip()
    {
        var vec1 = new Vector(1, 2, 3);

        return (-vec1).Equals(new Vector(-1, -2, -3));
    }
    public static bool TestVectorSubtraction()
    {
        var vec1 = new Vector(1, 2, 3);

        return (-vec1).Equals(new Vector(-1, -2, -3));
    }
    public static bool TestMagnitude()
    {
        var vec1 = new Vector(3, 4, 0);
        return (~vec1) == 5;
    }
    public static bool TestGetAngle() 
    {
        var vec1 = new Vector(1, 1, 0);
        var vec2 = new Vector(1, 0, 0);

        return Vector.GetAngle(vec1, vec2) ==
            (float)Math.Acos(1 / (1 * Math.Sqrt(2)));
    }
    static void Main()
    {
        var myImage = new Image(4, 4);

        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) myImage.Paint(i,j,new Vector(i,j,0));
        Console.WriteLine(myImage);


    }
}