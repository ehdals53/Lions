using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HP_Player : MonoBehaviour
{
    public float player_hp;
    public float player_hp_Cur;
    private float h;
    private float v;
    public Image player_hpBar_Front;
    public Image player_hpBar_Back;
    public TextMeshProUGUI message; // 피격
    public TextMeshProUGUI message1;    // 방어
    public TextMeshProUGUI message2;    // 반격

    BasicBehaviour basicBehaviour;
    private float boss_NormalDamage;
    private float boss_SmashDamage;
    private string Normaldmg;
    private string Smashdmg;

    // Start is called before the first frame update
    void Start()
    {
        basicBehaviour = GetComponent<BasicBehaviour>();
        player_hp_Cur = player_hp;
    }
    void PlayerSyncBar()
    {
        player_hpBar_Front.fillAmount = player_hp_Cur / player_hp;

        if (player_hpBar_Back.fillAmount > player_hpBar_Front.fillAmount)
        {
            player_hpBar_Back.fillAmount = Mathf.Lerp(player_hpBar_Back.fillAmount,
                player_hpBar_Front.fillAmount, Time.deltaTime);
        }
    }
    void BossDamage()
    {
        boss_NormalDamage = Random.Range(100, 300);
        boss_SmashDamage = Random.Range(300, 500);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerSyncBar();
        BossDamage();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Col_EnemyAtk")
        {
            if(gameObject.tag == "HitBox_Player")
            {
                Debug.Log("damage");
                player_hp_Cur -= boss_NormalDamage;
                Normaldmg = string.Format("{0}", boss_NormalDamage);
                message.text = Normaldmg;
                message.gameObject.SetActive(true);
            }
            if (gameObject.tag == "Defence")
            {
                Debug.Log("Miss");
                message1.text = "Miss";
                message1.gameObject.SetActive(true);

            }
            if (gameObject.tag == "Parrying")
            {
                Debug.Log("Counter");
                message2.text = "Counter";
                message2.gameObject.SetActive(true);
            }

        }
        if (other.tag == "Col_EnemySmash")
        {
            if(gameObject.tag == "HitBox_Player")
            {
                Debug.Log("Critical");
                player_hp_Cur -= boss_SmashDamage;
                Smashdmg = string.Format("{0}", boss_SmashDamage);
                message.text = Smashdmg;
                message.gameObject.SetActive(true);
            }
            if (gameObject.tag == "Defence")
            {
                Debug.Log("Miss");
                message1.text = "Miss";
                message1.gameObject.SetActive(true);
            }
            if (gameObject.tag == "Parrying")
            {
                Debug.Log("Counter");
                message2.text = "Counter";
                message2.gameObject.SetActive(true);
            }
        }
    }
}
