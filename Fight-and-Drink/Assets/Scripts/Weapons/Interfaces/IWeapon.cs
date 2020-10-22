/// <summary>
/// Defines a weapon.
/// </summary>
public interface IWeapon
{
    string Name { get; set; }
    string Description { get; set; }
    int OrderIndex { get; set; }
    bool CanAttack { get; set; }

    void Attack();
    void HandleAddWeapon(IWeapon other);
}
