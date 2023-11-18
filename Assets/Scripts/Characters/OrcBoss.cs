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

    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        if(triggerType == AnimationTriggerType.ENEMYPLAP)
        {
            AudioManager.Instance.sfxSource.clip = sexPlap;
            AudioManager.Instance.sfxSource.Play();
            GameManager.Instance.player.TakeDamage(false, 1f);
            //Play sound efect
            //Deal lust damage to player
        }
    }


    public void FuckedDamage(int damage)
    {
        GameManager.Instance.player.TakeDamage(false,damage);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameManager.Instance.gameState = GameManager.GameState.DIALOGUE;
            GameManager.Instance.dialogueManager.ChangeDialogue(healthLostDialogue);
            GameManager.Instance.dialogueManager.PlayDialogue();
            Debug.Log("Boss Dead");
        }
        else if(currentLust >= 100)
        {
            GameManager.Instance.gameState = GameManager.GameState.DIALOGUE;
            GameManager.Instance.dialogueManager.ChangeDialogue(lustLostDialogue);
            GameManager.Instance.dialogueManager.PlayDialogue();
            Debug.Log("Boss Horny");
        }
    }
}
