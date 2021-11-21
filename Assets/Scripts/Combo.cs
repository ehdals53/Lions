using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator playerAnim;

    bool comboPossible;
    public int comboStep;
    bool inputSmash;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }
    public void ComboPossible()
    {
        comboPossible = true;
    }
    public void NextAtk()
    {
        if (!inputSmash)
        {
            if (comboStep == 2)
                playerAnim.Play("3Combo_2");
            if (comboStep == 3)
                playerAnim.Play("3Combo_3");

        }
        if (inputSmash)
        {
            if (comboStep == 1)
                playerAnim.Play("Skill_A");
            if (comboStep == 2)
                playerAnim.Play("Skill_A");
            if (comboStep == 3)
                playerAnim.Play("Skill_A");
        }
    }
    public void ResetCombo()
    {
        comboPossible = false;
        inputSmash = false;
        comboStep = 0;
    }
    void NormalAttack()
    {
        if(comboStep == 0)
        {
            playerAnim.Play("3Combo_1");
            comboStep = 1;
            return;
        }
        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }
    }
    void SmashAttack()
    {
        if (comboPossible)
        {
            comboPossible = false;
            inputSmash = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NormalAttack();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SmashAttack();
        }
        
    }
}
