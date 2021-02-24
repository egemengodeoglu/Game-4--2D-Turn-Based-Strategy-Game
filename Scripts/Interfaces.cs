using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void Die();
        bool IsDead { get; set; }

    }

    public interface IPushAble
    {
        void ApplyPushForce(Vector3 force);
    }

    public interface IReviveable
    {
        void Revive(int startHealth);
    }

    public interface IHealable
    {
        void Heal(int healAmount);
    }

    public interface IDamageDealer
    {
        void ApplyDamage(IDamageable obj);
    }
}
