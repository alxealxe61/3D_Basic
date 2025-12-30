using UnityEngine;

namespace Study_Camera.CombatSystem
{
    public interface IHurtBox
    {
        public ICombatAgent Owner { get; }
        
        public Collider Collider { get; }

        public void Awake();

        public void Initialize(ICombatAgent owner);
    }
}