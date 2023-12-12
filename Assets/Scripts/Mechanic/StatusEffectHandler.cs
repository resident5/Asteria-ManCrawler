using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    public Animator anim;
    public CanvasGroup canvasGroup;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {

    }

    public void TurnOnEffect(string effect)
    {
        canvasGroup.alpha = 1;
        anim.SetBool(effect, true);
    }

    public void TurnOffEffect(string effect)
    {
        anim.SetBool(effect, false);
        canvasGroup.alpha = 0;
    }

}
