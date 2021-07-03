// Aaron Grincewicz ASGrincewicz@icloud.com 6/10/2021
using System;
public interface IDamageable
{
    public sbyte Lives { get; }
    public sbyte HP { get; }
    public bool IsPlayer { get; }

    public void Damage(sbyte hpDamage);
}
