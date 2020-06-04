/// <summary>
/// Defines a weapon.
/// </summary>
public interface IWeapon
{
    string Name { get; set; }
    string Description { get; set; }
    int OrderIndex { get; set; }
    float FireRate { get; set; }

    void Shoot();
}
