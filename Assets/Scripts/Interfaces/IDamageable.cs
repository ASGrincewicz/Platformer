// Aaron Grincewicz ASGrincewicz@icloud.com 6/10/2021
public interface IDamageable
{
    /// <summary>
    /// <value>Returns the object's current lives.</value>
    /// </summary>
    public int Lives { get; }
    /// <summary>
    /// <value>Returns current hit points.</value>
    /// </summary>
    public int HP { get; }
    /// <summary>
    /// <value>Checks whether object is the player and not AI.</value>
    /// </summary>
    public bool IsPlayer { get; }
    /// <summary>
    /// Passes damage to the object.
    /// </summary>
    /// <param name="hpDamage"></param>
    public void Damage(int hpDamage);
}
