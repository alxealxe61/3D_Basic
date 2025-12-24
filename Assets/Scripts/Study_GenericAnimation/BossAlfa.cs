using System;
using System.Collections;
using UnityEngine;

public class BossAlfa : MonoBehaviour
{
    private static readonly int BREATH = Animator.StringToHash("Breath");
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    
    [SerializeField] private Transform firePoint;
    [SerializeField] private ParticleSystem breathEffect; 
    
    [SerializeField] private Collider ScratchCollider;
    [SerializeField] private Collider BreathCollider;
    
    private Animator Animator {get; set;}
    private bool isPlaying = false;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        
        // ParticleSystem?
        // 각종 연출과 효과에 사용되는 컴포넌트.
        // 입자를 이용한 각종 이펙트들이 사용된다고 생각하면 됩니다.
        // 몇백, 몇천개의 작은 입자들을 사용해서 화염이나 번개 같은
        // 그래픽효과를 만드는데에 사용이 됩니다.
        
        // .Play() => 파티클을 재생시키는 함수
        // .isPlaying => 재생중이니? 물어볼수있는 프로퍼티
        
        // 애니메이션이 다 종료 된 이후에 히트박스 콜라이더를 꺼야함
        // 근데 언제 꺼야할지 모름 코루틴 사용하면 솔직히 쉽긴한데 너무 땜빵 느낌임 
      
        EndBreath(); 
        EndScratch();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Animator.SetTrigger(ATTACK);
            StartScratch();
            Debug.Log("공격 시작");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Animator.SetTrigger(BREATH);
            StartBreath();
            Debug.Log("공격 시작");
        }
        
        if (isBreathing)
        {
            breathEffect.transform.position = firePoint.position;
        }
        
        if (isPlaying == true && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            EndBreath();
            EndScratch();
            Debug.Log("공격 종료");
        }
    }

    private bool isBreathing = false;
    
    private void StartBreath()
    {
        isPlaying = true;
        isBreathing = true;
        breathEffect.gameObject.SetActive(true);
        
        breathEffect.Play();
        StartCoroutine(Breath());
    }

    private IEnumerator Breath()
    {
        yield return new WaitForSeconds(2f);
        BreathCollider.enabled = true;
        // 6초 후 정지 
        
    }
    private void EndBreath()
    {
        isBreathing = false;
        BreathCollider.enabled = false;
        breathEffect.gameObject.SetActive(false);
        BreathCollider.GetComponent<Collider>().enabled = false;
        isPlaying = false;
    }

    private void StartScratch()
    {
        isPlaying = true;
        ScratchCollider.enabled = true;
    }

    private void EndScratch()
    {
        ScratchCollider.enabled = false;
        ScratchCollider.GetComponent<Collider>().enabled = false;
        isPlaying = false;
    }
}
