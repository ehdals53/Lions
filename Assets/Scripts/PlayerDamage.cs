using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDamage : MonoBehaviour
{
    public float Player_hp = 2000.0f;
    public float Player_hp_Cur;
    public Image Player_hpBar_Front;
    public Image Player_hpBar_Back;
    public TextMeshProUGUI DamageText;  // 피격
    public TextMeshProUGUI DefenceText;    // 방어
    public TextMeshProUGUI CounterText;    // 반격
    private int damageStep;
    private float boss_NormalDamage;
    private float boss_SmashDamage;
    private string boss_Normaldmg;
    private string boss_Smashdmg;
    public bool isDie;
    private Animator anim;
    private readonly int die = Animator.StringToHash("PlayerDie");
    public GameObject player;

    [Header("Player Hit Sound")]
    public AudioSource hitSound_1;
    public AudioSource hitSound_2;
    public AudioSource dieSound;
    public AudioSource CounterSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isDie = false;
        Player_hp_Cur = Player_hp;

    }
    void PlayerSyncBar()
    {
        Player_hpBar_Front.fillAmount = Player_hp_Cur / Player_hp;

        if (Player_hpBar_Back.fillAmount > Player_hpBar_Front.fillAmount)
        {
            Player_hpBar_Back.fillAmount = Mathf.Lerp(Player_hpBar_Back.fillAmount,
                Player_hpBar_Front.fillAmount, Time.deltaTime);
        }
    }
    void ChangeTag(string t)
    {
        player.tag = t;
    }
    public void PlayerDie()
    {
        isDie = true;
        Debug.Log("PlayerDie!");
        dieSound.Play();
        anim.SetTrigger(die);
        GetComponent<MoveBehaviour>().enabled = false;
        GetComponent<BasicBehaviour>().enabled = false;
        GetComponent<AimBehaviourBasic>().enabled = false;
        GetComponent<Combo>().enabled = false;
        GetComponent<MP_Player>().enabled = false;

        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        boss.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);

    }
    void BossDamage()
    {
        boss_NormalDamage = Random.Range(100, 300);
        boss_SmashDamage = Random.Range(300, 500);
    }
    // Update is called once per frame
    void Update()
    {
        damageStep = Random.Range(0, 2);
        PlayerSyncBar();
        BossDamage();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Col_EnemyAtk")
        {
            if (gameObject.tag == "Player")
            {
                switch (damageStep)
                {
                    case 0:
                        anim.Play("Rigidity");
                        hitSound_1.Play();
                        Debug.Log("damage");
                        Player_hp_Cur -= boss_NormalDamage;
                        boss_Normaldmg = string.Format("{0}", boss_NormalDamage);
                        DamageText.text = boss_Normaldmg;
                        DamageText.gameObject.SetActive(true);
                        if (Player_hp_Cur <= 0.0f)
                        {
                            PlayerDie();
                        }
                        break;
                    case 1:
                        anim.Play("Rigidity_2");
                        hitSound_1.Play();
                        Debug.Log("damage");
                        Player_hp_Cur -= boss_NormalDamage;
                        boss_Normaldmg = string.Format("{0}", boss_NormalDamage);
                        DamageText.text = boss_Normaldmg;
                        DamageText.gameObject.SetActive(true);

                        if (Player_hp_Cur <= 0.0f)
                        {
                            PlayerDie();
                        }
                        break;
                }
            }
            if (gameObject.tag == "Defence")
            {
                anim.Play("Defence_hit");
                Debug.Log("Miss");
                DefenceText.text = "Miss";
                DefenceText.gameObject.SetActive(true);

            }
            if (gameObject.tag == "Parrying")
            {
                anim.Play("Counter");
                CounterSound.Play();
                Debug.Log("Counter");
                CounterText.text = "Counter";
                CounterText.gameObject.SetActive(true);

            }

        }
        if (other.tag == "Col_EnemySmash")
        {
            if (gameObject.tag == "Player")
            {
                anim.Play("Rigidity_3");
                hitSound_2.Play();
                Debug.Log("Critical");
                Player_hp_Cur -= boss_SmashDamage;
                boss_Smashdmg = string.Format("{0}", boss_SmashDamage);
                DamageText.text = boss_Smashdmg;
                DamageText.gameObject.SetActive(true);

                if (Player_hp_Cur <= 0.0f)
                {
                    PlayerDie();
                }

            }
            if (gameObject.tag == "Defence")
            {
                anim.Play("Defence_hit");
                Debug.Log("Miss");
                DefenceText.text = "Miss";
                DefenceText.gameObject.SetActive(true);

            }
            if (gameObject.tag == "Parrying")
            {
                anim.Play("Counter");
                CounterSound.Play();
                Debug.Log("Counter");
                CounterText.text = "Counter";
                CounterText.gameObject.SetActive(true);

            }
        }
    }
}
