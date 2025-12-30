using System.Collections.Generic;
using UnityEngine;

namespace Study_Camera.CombatSystem
{
    public class BossBreathDetector : MonoBehaviour, IHitDetector
    {
        public ICombatAgent Owner { get; private set; }
        
        [field: SerializeField]public LayerMask DetectionLayer { get; private set; }
        [field: SerializeField] private Collider Collider { get; set; }
        private List<HurtBox> hitList = new List<HurtBox>();
        public void Initialize(ICombatAgent owner)
        {
           Owner = owner;
           Collider = GetComponent<Collider>();
        }

        public void EnableDetection()
        {
            Collider.enabled = true;
        }

        public void DisableDetection()
        {
            Collider.enabled = false;
            hitList.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.GetMask("Combat"))
            {
                if (CombatSystem.Instance.HasHurtBox(other) == false) return;
                
                HitInfo hitInfo = new HitInfo();
                hitInfo.hurtBox = CombatSystem.Instance.GetHurtBox(other);
                hitInfo.receiver = hitInfo.hurtBox.Owner;
                hitInfo.gameObject = other.gameObject;
                hitInfo.position = other.ClosestPoint(transform.position);
                // .ClosestPoint(Vector3 position)
                // 해당 함수는 position 위치에서 Collider의 가장 가까운 표면 점을 반환해 주는 함수
                   
                
                Owner.OnHitDetected(hitInfo); 
            }
        }
    }
}