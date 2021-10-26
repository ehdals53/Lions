using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Col_PlayerAtk : MonoBehaviour
{
    public Combo combo;
    public string type_Atk;

    public int comboStep;

    public HitFeel hitFeel;
    public void OnEnable()
    { 
        comboStep = combo.comboStep;
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boss")
        {
            if(gameObject.tag == "Col_NormalAtk")
            {

                hitFeel.TimeStop();
            }
            if(gameObject.tag == "Col_SmashAtk")
            {

                hitFeel.TimeStop();
            }
            if(gameObject.tag == "Col_CounterAtk")
            {

                hitFeel.TimeStop();
            }

        }
    }
    */

}
