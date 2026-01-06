using UnityEngine;

public class HumonoidAnim : MonoBehaviour
{
    private Animator HumonoidAnimation;
    private AnimEventReceiver HumonoidAnimEventReceiver;

    private bool isAttackAble = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        HumonoidAnimation = GetComponent<Animator>();
        HumonoidAnimEventReceiver = GetComponent<AnimEventReceiver>();
    }

    private void OnEnable()
    {
        HumonoidAnimEventReceiver.OnAnimationTriggerReceived += OnTriggerAnim;
    }

    private void OnDisable()
    {
        HumonoidAnimEventReceiver.OnAnimationTriggerReceived -= OnTriggerAnim;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isAttackAble && Input.GetKeyDown(KeyCode.Mouse0))
        {
            HumonoidAnimation.SetTrigger("Attack");
            isAttackAble = false;
        }
    }

    private void OnTriggerAnim(string parameter)
    {
        if (parameter.Equals("Input_Start"))
        {
            isAttackAble = true;
        }

        if (parameter.Equals("Input_End"))
        {
            isAttackAble = false;
        }
    }
}
