using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_Player : MonoBehaviour
{
    public float mp;
    public float mp_Cur;

    public Image mpBar_Front;
    public Image mpBar_Back;

    public float dodge_Mp = 20.0f;
    public float backstep_Mp = 20.0f;
    public float sprint_Mp = 20.0f;
    public float regen_Mp = 10.0f;


    // Start is called before the first frame update
    public void Start()
    {
        mp_Cur = mp;

    }
    public void Syncbar()
    {
        mpBar_Front.fillAmount = mp_Cur / mp;

        if (mpBar_Back.fillAmount > mpBar_Front.fillAmount)
        {
            mpBar_Back.fillAmount = Mathf.Lerp(mpBar_Back.fillAmount, mpBar_Front.fillAmount, Time.deltaTime);

        }
        else
        {
            mpBar_Back.fillAmount = Mathf.Lerp(mpBar_Front.fillAmount, mpBar_Back.fillAmount, Time.deltaTime);
        }
    }
    public void MpRegen()
    {
        mp_Cur += Time.deltaTime * regen_Mp;

        if (mp_Cur > mp)
        {
            mp_Cur = mp;
        }
        
    }
    // Update is called once per frame
    public void Update()
    {
        Syncbar();
        MpRegen();
    }

    public void Dodge_MP()
    {
        mp_Cur -= dodge_Mp;
    }
    public void BackStep_MP()
    {
        mp_Cur -= backstep_Mp;
    }
    public void Sprint_MP()
    {
        mp_Cur -= sprint_Mp * Time.deltaTime;
    }

    /*
    void MpCost()
    {
        
        if (Input.GetButtonDown(dodgeButton) && (mp_Cur > dodge_Mp))
        {
            mp_Cur -= dodge_Mp;
        }
        if (Input.GetButton(sprintButton) && (mp_Cur > 0))
        {
            mp_Cur -= Time.deltaTime * sprint_Mp;
        }
        
    }
    */
}
