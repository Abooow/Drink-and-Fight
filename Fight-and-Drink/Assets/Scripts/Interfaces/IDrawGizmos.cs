/// <summary>
/// Used for debugging purposes.
/// </summary>
public interface IDrawGizmos
{
    bool DrawGizmos { get; set; }

    void OnDrawGizmos();
}