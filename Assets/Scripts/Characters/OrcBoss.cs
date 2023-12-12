using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrcBoss : Character
{
    public Animator animator;

    public string[] attacks;
    public string[] grabAttacks;
    public string[] fuckAttacks;

    public TMP_FontAsset fontAsset;
    public float fontSize;

    int lastAttackNum = -1;
    int attackNum;

    public float nextAttackTime = 4f;

    public int grabStrength = 2;

    public enum AnimationTriggerType
    {
        ENEMYPLAP
    }

    public AudioClip sexPlap;
    public DialogueObject healthLostDialogue;
    public DialogueObject lustLostDialogue;

    public BossSpeech speech;

    public float lustDamage;
    public float damage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        currentLust = 0;
        lastAttackNum = -1;
    }
    public string Attack(string[] moveset)
    {
        attackNum = Random.Range(0, moveset.Length);

        while (lastAttackNum == attackNum)
        {
            attackNum = Random.Range(0, moveset.Length);
        }

        lastAttackNum = attackNum;

        return moveset[attackNum];
    }

    public void Grab(bool grab)
    {
        animator.SetBool("grab", grab);
    }

    public void Fuck(bool fuck)
    {
        animator.SetBool("fuck", fuck);
    }

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        if(triggerType == AnimationTriggerType.ENEMYPLAP)
        {
            AudioManager.Instance.sfxSource.clip = sexPlap;
            AudioManager.Instance.sfxSource.Play();
            GameManager.Instance.player.TakeDamage(false, lustDamage);
            //Play sound efect
            //Deal lust damage to player
        }
    }


    public void FuckedDamage(int damage)
    {
        GameManager.Instance.player.TakeDamage(false,damage);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StopAllCoroutines();
            GameManager.Instance.gameState = GameManager.GameState.DIALOGUE;
            GameManager.Instance.dialogueManager.ChangeDialogue(healthLostDialogue);
            GameManager.Instance.dialogueManager.PlayDialogue();
        }
        else if(currentLust >= 100)
        {
            StopAllCoroutines();
            GameManager.Instance.gameState = GameManager.GameState.DIALOGUE;
            GameManager.Instance.dialogueManager.ChangeDialogue(lustLostDialogue);
            GameManager.Instance.dialogueManager.PlayDialogue();
        }
    }
}
