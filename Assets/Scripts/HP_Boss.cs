using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HP_Boss : MonoBehaviour
{
    public float boss_hp;
    public float boss_hp_Cur;

    private bool isDie;
    public Image boss_hpBar_Front;
    public Image boss_hpBar_Back;
    public TextMeshProUGUI damageText1;
    public TextMeshProUGUI damageText2;
    public TextMeshProUGUI damageText3;
    private float NormalDamage;
    private float SmashDamage;
    private float CounterDamage;
    private string Normaldmg;
    private string Smashdmg;
    private string Counterdmg;

    private Animator bossAnim;

    private Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        isDie = false;
        boss = GetComponent<Boss>();
        bossAnim = GetComponent<Animator>();
        boss_hp_Cur = boss_hp;
    }
    void BossSyncBar()
    {
        boss_hpBar_Front.fillAmount = boss_hp_Cur / boss_hp;

        if (boss_hpBar_Back.fillAmount > boss_hpBar_Front.fillAmount)
        {
            boss_hpBar_Back.fillAmount = Mathf.Lerp(boss_hpBar_Back.fillAmount,
                boss_hpBar_Front.fillAmount, Time.deltaTime);
        }
    }
    void PlayerDamage()
    {
        NormalDamage = Random.Range(100, 200);
        SmashDamage = Random.Range(200, 300);
        CounterDamage = Random.Range(300, 400);

    }
    void BossDie()
    {
        if (boss_hp_Cur <= 0)
        {
            GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
        }
    }
    // Update is called once per frame
    void Update()
    {
        BossSyncBar();
        PlayerDamage();
        BossDie();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "HitBox_Enemy")
        {
            if (other.tag == "Col_NormalAtk")
            {
                boss_hp_Cur -= NormalDamage;
                Normaldmg = string.Format("{0}", NormalDamage);
                damageText1.text = Normaldmg;
                damageText1.gameObject.SetActive(true);


            }
            if (other.tag == "Col_SmashAtk")
            {
                boss_hp_Cur -= SmashDamage;
                Smashdmg = string.Format("{0}", SmashDamage);
                damageText2.text = Smashdmg;
                damageText2.gameObject.SetActive(true);


            }
            if (other.tag == "Col_CounterAtk")
            {
                boss_hp_Cur -= CounterDamage;
                Counterdmg = string.Format("{0}", CounterDamage);
                damageText3.text = Counterdmg;
                damageText3.gameObject.SetActive(true);

            }
        }
    }
}
