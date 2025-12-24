using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CapsuleDoll : MonoBehaviour
{
    [SerializeField] private Renderer CapsuleDollRenderer;
    [SerializeField] private Collider CapsuleDollCollider;

   
    private void OnTriggerEnter (Collider others)
    {
        StartCoroutine(RespawnCoroutine());
        Debug.Log("아야");
    }
    
    private IEnumerator RespawnCoroutine()
    {
        // airPlaneRenderer가 갖고있는 Material의 Color값을 조정하여
        // 깜빡거리는 효과를 준다.

        CapsuleDollCollider = GetComponent<Collider>();
        
        float sumTime = 0.0f;
        Material material = CapsuleDollRenderer.material;

        Color originalColor = material.color;
        Color targetColor = Color.red;

        CapsuleDollCollider.enabled = false;
        
        while (sumTime < 0.5f)
        {
            sumTime += Time.deltaTime;

            float t = sumTime;
            material.color = Color.Lerp(originalColor, targetColor, t);

            yield return null;
        }

        CapsuleDollCollider.enabled = true;
        
        material.color = originalColor;
        Debug.Log("빨강");
    }
}
