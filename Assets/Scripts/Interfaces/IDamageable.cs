// Aaron Grincewicz ASGrincewicz@icloud.com 6/10/2021
using System;
public interface IDamageable
{
    public int Lives { get; }
    public int HP { get; }
    public bool IsPlayer { get; }

    public void Damage(int hpDamage);
}
