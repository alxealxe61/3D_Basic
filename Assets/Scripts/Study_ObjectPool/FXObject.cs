using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Study_Camera.Study_ObjectPool
{
    public class FxObject :  MonoBehaviour , IPoolAbleObject
    {
        public Action<IPoolAbleObject> ReturnToPoolMethod { get; set; }
        [field:SerializeField] public string Key { get; set; }
        
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        private void OnEnable()
        {
            _particleSystem.Play();
        }

        private void Update()
        {
            // 재생중일때는 패스
            if (_particleSystem.isPlaying) return;
            
            //풀로 되돌아가는 함수
            ReturnToPoolMethod.Invoke(this);
        }
        
    }
}