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

    public float DodgeMP = 20.0f;
    public float GuardMP = 20.0f;
    public float SprintMP = 20.0f;
    public float SkillMP = 20.0f;
    public float Skill_H_MP = 50.0f;

    public float RegenMP = 10.0f;


    // Start is called before the first frame update
    private void Start()
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
        mp_Cur += Time.deltaTime * RegenMP;

        if (mp_Cur > mp)
        {
            mp_Cur = mp;
        }
        
    }
    // Update is called once per frame
    private void Update()
    {
        Syncbar();
        MpRegen();
    }

    public void Dodge_MP()
    {
        if(mp_Cur > DodgeMP)
        {
            mp_Cur -= DodgeMP;
        }
    }
    public void Guard_MP()
    {
        if(mp_Cur > GuardMP)
        {
            mp_Cur -= GuardMP;
        }
    }
    public void Skill_MP()
    {
        if(mp_Cur > SkillMP)
        {
            mp_Cur -= SkillMP;
        }
    }
    public void Sprint_MP()
    {
        if (mp_Cur > 1.0f)
        {
            mp_Cur -= SprintMP * Time.deltaTime;
        }
    } 

    public void SKILL_H_MP()
    {
        if(mp_Cur > 50.0f)
        {
            mp_Cur -= Skill_H_MP;
        }
    }
}
