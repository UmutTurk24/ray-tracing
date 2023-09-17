using System;


public class HW1Controller{

    public static bool TestSimpleConstructor() {
        var simpleVec = new Vector();
        var res = simpleVec.X == 0 &&
            simpleVec.Y == 0 &&
            simpleVec.Z == 0;
        return true == res;
    }

    public static bool TestCoolConstructor()
    {
        var simpleVec = new Vector(5,6,7);

        return simpleVec.X == 5 &&
            simpleVec.Y == 6 &&
            simpleVec.Z == 7;
    }

    static void Main()
    {
        Image myImage = new Image();
        myImage.SaveImage("./CoolImage");

        // b.MethodB();
    }
}