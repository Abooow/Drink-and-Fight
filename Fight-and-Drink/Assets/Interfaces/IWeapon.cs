/// <summary>
/// 
/// </summary>
public interface IWeapon
{
    string Name { get; set; }
    string Description { get; set; }
    float FireRate { get; set; }

    void Shoot();
}
