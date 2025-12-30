using UnityEngine;

namespace Study_Camera.CombatSystem
{
    public struct HitInfo
    {
        public ICombatAgent receiver;
        public Vector3 position;
        public HurtBox hurtBox;
        public int hitCount;
        public int parameter;
        
        public GameObject gameObject; //혹 몰라서 넣는것. 사실 안넣고 ICombatAgent를 더 넓게 잡아도 됩니다.
        
    }
    
    public interface ICombatAgent
    {
        void TakeDamage(int damage);
        void OnHitDetected(HitInfo hitInfo);
    }
    
}