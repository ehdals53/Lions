using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDamage : MonoBehaviour
{
    public float boss_hp = 5000.0f;
    public float boss_hp_Cur;
    public Image boss_hpBar_Front;
    public Image boss_hpBar_Back;
    public TextMeshProUGUI NormalDmgText1;
    public TextMeshProUGUI SmashDmgText2;
    public TextMeshProUGUI CounterDmgText3;
    public TextMeshProUGUI HyperSkillDmgText;

    private float NormalDamage;
    private float SmashDamage;
    private float CounterDamage;
    private float HyperSkillDamage;

    private string Normaldmg;
    private string Smashdmg;
    private string Counterdmg;
    private string HyperSkilldmg;

    void Start()
    {
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
    void BossDie()
    {
        if (boss_hp_Cur <= 0.0f)
        {
            GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
        }
    }
    void PlayerDamage()
    {
        NormalDamage = Random.Range(100, 200);
        SmashDamage = Random.Range(200, 300);
        CounterDamage = Random.Range(200, 400);
        HyperSkillDamage = Random.Range(300, 500);

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
        if (gameObject.tag == "Boss")
        {
            if (other.tag == "Col_NormalAtk")
            {
                boss_hp_Cur -= NormalDamage;
                Normaldmg = string.Format("{0}", NormalDamage);
                NormalDmgText1.text = Normaldmg;
                NormalDmgText1.gameObject.SetActive(true);

            }
            if (other.tag == "Col_SmashAtk")
            {
                boss_hp_Cur -= SmashDamage;
                Smashdmg = string.Format("{0}", SmashDamage);
                SmashDmgText2.text = Smashdmg;
                SmashDmgText2.gameObject.SetActive(true);

            }
            if (other.tag == "Col_CounterAtk")
            {
                boss_hp_Cur -= CounterDamage;
                Counterdmg = string.Format("{0}", CounterDamage);
                CounterDmgText3.text = Counterdmg;
                CounterDmgText3.gameObject.SetActive(true);
                
            }
            if(other.tag == "Col_HyperAtk")
            {
                boss_hp_Cur -= HyperSkillDamage;
                HyperSkilldmg = string.Format("{0}", HyperSkillDamage);
                HyperSkillDmgText.text = HyperSkilldmg;
                HyperSkillDmgText.gameObject.SetActive(true);

            }
        }
    }
}
