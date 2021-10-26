using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitBox_Player : MonoBehaviour
{
    [Header("Player SOUNDS")]
    public AudioSource counterSound;

    public Animator playerAni;
    private int damageStep;
    
    void update()
    {
        damageStep = Random.Range(0, 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Col_EnemyAtk")
        {
            if (gameObject.tag == "HitBox_Player")
            {
                switch (damageStep)
                {
                    case 0:
                        playerAni.Play("Rigidity");
                        break;
                    case 1:
                        playerAni.Play("Rigidity_2");
                        break;
                }

            }
            if (gameObject.tag == "Defence")
            {
                playerAni.Play("Defence_hit");

            }
            if(gameObject.tag == "Parrying")
            {
                playerAni.Play("Counter");
            }
        }
        if(other.tag == "Col_EnemySmash")
        {
            if(gameObject.tag == "HitBox_Player")
            {
                playerAni.Play("Rigidity3");
             }
            if(gameObject.tag == "Defence")
            {
                playerAni.Play("Defence_hit");
            }
        }
    }
}
