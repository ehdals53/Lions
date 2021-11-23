using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigid;
    private Transform playerTr;
    private Transform enemyTr;
    private MoveAgent moveAgent;
    public bool isFire = false;
    public float AtkDist = 5.0f;
    private int attackNumber;
    private bool enableAct;
    private float nextFire = 0.0f;
    public float fireRate = 5.0f;
    private readonly float damping = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        moveAgent = GetComponent<MoveAgent>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackNumber = Random.Range(0, 6);

        if (isFire)
        {
            if (Time.time >= nextFire)
            {
                Fire();
                nextFire = Time.time + fireRate + Random.Range(0.0f, 1.0f);

            }
            if (enableAct)
            {
                Rotate();
            }
        }
    }
    void Rotate()
    {
        Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
        enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
    }
    void Fire()
    {
        float dist = Vector3.Distance(playerTr.position, enemyTr.position);

        if(dist <= AtkDist)
        {
            switch (attackNumber)
            {
                case 0:
                    animator.SetTrigger("Atk1");
                    break;
                case 1:
                    animator.SetTrigger("Atk2");

                    break;
                case 2:
                    animator.SetTrigger("Atk3");

                    break;
                case 3:
                    animator.SetTrigger("Atk4");

                    break;
                case 4:
                    animator.SetTrigger("Atk5");

                    break;
                case 5:
                    animator.SetTrigger("Atk6");

                    break;
            }
        }
    }
    void FreezeBoss()
    {
        enableAct = false;
    }
    void UnFreezeBoss()
    {
        enableAct = true;
    }
}
