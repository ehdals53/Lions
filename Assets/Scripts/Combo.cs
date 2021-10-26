using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator playerAnim;

    bool comboPossible;
    public int comboStep;
    bool inputSmash;

    [Header("Player SOUNDS")]
    public AudioSource Normal_1;
    public AudioSource Normal_2;
    public AudioSource Normal_3;
    public AudioSource Smash_1;
    public AudioSource Smash_2;
    public AudioSource Smash_3;
    public AudioSource swordSound;
    public AudioSource smashSound;


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
            {
                swordSound.Play();
                Normal_2.Play();
                playerAnim.Play("Normal_Atk2");

            }
            if (comboStep == 3)
            {
                swordSound.Play();
                Normal_3.Play();
                playerAnim.Play("Normal_Atk3");

            }
        }
        if (inputSmash)
        {
            if (comboStep == 1)
            {
                smashSound.Play();
                Smash_1.Play();

                playerAnim.Play("Smash_Atk1");
            }
            if (comboStep == 2)
            {
                smashSound.Play();
                Smash_2.Play();

                playerAnim.Play("Smash_Atk2");
            }
            if (comboStep == 3)
            {
                smashSound.Play();
                Smash_3.Play();

                playerAnim.Play("Smash_Atk3");
            }
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

        if (comboStep == 0)
        {
            playerAnim.Play("Normal_Atk1");
            comboStep = 1;
            swordSound.Play();
            Normal_1.Play();
           
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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            NormalAttack();
        
        if (Input.GetMouseButtonDown(1))
            SmashAttack();

    }
}
