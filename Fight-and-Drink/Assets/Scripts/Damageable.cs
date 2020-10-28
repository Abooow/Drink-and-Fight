using UnityEngine;

/// <summary>
/// Makes an object damageable.
/// </summary>
public class Damageable : MonoBehaviour
{
    public CharacterStats CharacterStats;

    /// <summary>
    /// Makes the object take a specified amount of damage.
    /// </summary>
    /// <param name="damage">The amount of damage to inflict.</param>
    public virtual void TakeDamage(float damage)
    {
        float damageMultiplier;
        if (CharacterStats.Armor >= 0) damageMultiplier = 100f / (100 + CharacterStats.Armor);
        else damageMultiplier = 2f - (100f / (100 - CharacterStats.Armor));

        CharacterStats.CurrentHealth -= damage * damageMultiplier;
        OnDamageTaken(damage * damageMultiplier);
        if (CharacterStats.CurrentHealth <= 0)
        {
            CharacterStats.CurrentHealth = 0;
            OnDeath();
        }
    }

    /// <summary>
    /// A method that is invoked once the object has taken damage.
    /// </summary>
    /// <param name="damage">The true damage that was inflicted.</param>
    virtual protected void OnDamageTaken(float damage)
    {
    }

    /// <summary>
    /// A method that is invoked once the objects health has reached 0.
    /// </summary>
    virtual protected void OnDeath()
    {
        CharacterStats.IsDead = true;
        Destroy(gameObject);
    }
}
