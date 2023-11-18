using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    public Character target;
    public Image healthBar;
    public Image lustBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = target.currentHealth / target.maxHealth;
        lustBar.fillAmount = target.currentLust / target.MaxLust;
    }
}
