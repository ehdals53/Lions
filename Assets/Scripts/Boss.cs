using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{ 

    public Animator bossAni;
    public Transform target;
    public Transform enemyTr;
    public float bossSpeed;
    bool enableAct;
    int attackNumber;
    private HP_Boss hp_boss;

    void Start()
    {
        hp_boss = GetComponent<HP_Boss>();
        bossAni = GetComponent<Animator>();
        enableAct = true;
    }
    void RotateBoss()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);

        transform.rotation =
            Quaternion.Slerp(transform.localRotation,
                rot, 5 * Time.deltaTime);
    }
    void MoveBoss()
    {
        if ((target.position - transform.position).magnitude >= 4)
        {
            bossAni.SetBool("IsMove", true);
            transform.Translate(Vector3.forward * bossSpeed * Time.deltaTime, Space.Self);
        }
        if ((target.position - transform.position).magnitude < 4)
        {
            bossAni.SetBool("IsMove", false);
        }

    }
    void Update()
    {
        attackNumber = Random.Range(0, 6);

        if (enableAct)
        {
            RotateBoss();
            MoveBoss();
        }
    }
        
    void BossAtk()
    {
        if ((target.position - transform.position).magnitude < 4)
        {
            switch (attackNumber)
            {
                case 0:
                    bossAni.Play("BossAtk1");
                    break;
                case 1:
                    bossAni.Play("BossAtk2");
                    break;
                case 2:
                    bossAni.Play("BossAtk3");
                    break;
                case 3:
                    bossAni.Play("BossAtk4");
                    break;
                case 4:
                    bossAni.Play("BossAtk5");
                    break;
                case 5:
                    bossAni.Play("BossSmash");
                    break;
            }
        }
    }
    public void FreezeBoss()
    {
        enableAct = false;

    }
    void UnFreezeBoss()
    {
        enableAct = true;
    }
    public GameObject HitBox;

    void ChangeTag(string t)
    {
        HitBox.tag = t;
    }
}
