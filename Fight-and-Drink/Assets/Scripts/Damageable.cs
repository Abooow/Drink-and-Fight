using UnityEngine;

public class Damagables : MonoBehaviour
{
    public CharacterStats Character;

    virtual protected void TakeDamage(float damage)
    {
    }

    virtual protected void OnDeath()
    {
    }
}
