
/// <summary>
/// Scene class for collection of shapes
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 24 October 2023

public class Scene {
    private List<Shape> _shapes;
    public Scene() {
        _shapes = new List<Shape>();
    }

    public void AddShape(ref Shape s) {
        // Adds the shape to the scene
        _shapes.Add(s);
        
    }
}
