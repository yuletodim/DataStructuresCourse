namespace QuadTreeImplementation.Core
{
    /// <summary>
    /// Implemented by classes which can be stored in the QuadTree 
    /// </summary>
    public interface IBoundable
    {
        Rectangle Bounds { get; set; }
    }
}
