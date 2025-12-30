using UnityEngine;

namespace Study_Camera.CombatSystem
{
    // 해당 컴포넌트는 typeof(T)의 타입이 존재해야만 붙힐 수 있음 
    [RequireComponent(typeof(Collider))]
    public class HurtBox : MonoBehaviour , IHurtBox
    {
        public ICombatAgent Owner { get; private set; }
        public Collider Collider {get; private set;}

        public void Awake()
        {
            Collider = GetComponent<Collider>();
        }
        
        public void Initialize(ICombatAgent owner)
        {
            Owner = owner;
            CombatSystem.Instance.AddHurtBox(Collider, this );
        }
    }
}