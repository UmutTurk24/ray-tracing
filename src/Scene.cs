

using System.Collections;
using System.Transactions;

/// <summary>
/// Scene class for collection of shapes
/// </summary>
/// Class <c>Computer Graphics</c>
/// Author: Umut Turk
/// Date: 24 October 2023
public class Scene: IEnumerable<Shape> {
    private List<Shape> _shapes;
    public Vector Light;
    public Scene() {
        _shapes = new List<Shape>();
        Light = new Vector(0f, 0f, 0f);
    }

    public void AddShape(ref Shape s) {
        // Adds the shape to the scene
        _shapes.Add(s);
    }

    public IEnumerator<Shape> GetEnumerator()
    {
        foreach(var item in _shapes)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}


public class ShapeEnum : IEnumerator
{
    /// <summary>
    /// Enumerator for shapes
    /// </summary>
    public List<Shape> _shapes; 
    int position = -1;

    public ShapeEnum(List<Shape> list) {
        _shapes = list;
    }

    public bool MoveNext() 
    {
        position++;
        return (position < _shapes.Count);
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current
    {
        get
        {
            return Current;
        }
    }

    public Shape Current
    {
        get
        {
            try
            {
                return _shapes[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }
}