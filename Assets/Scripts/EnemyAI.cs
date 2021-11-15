using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }
    public State state = State.PATROL;

    private Transform playerTr;
    private Transform enemyTr;
    private Animator animator;
    public float attackDist = 5.0f;
    public float traceDist = 10.0f;
    public bool isDie = false;
    public GameObject Portal;
    private WaitForSeconds ws;
    private MoveAgent moveAgent;
    private EnemyFire enemyFire;
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashOffset = Animator.StringToHash("Offset");
    private readonly int hashWalkSpeed = Animator.StringToHash("WalkSpeed");
    private readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTr = player.GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        enemyFire = GetComponent<EnemyFire>();
        animator = GetComponent<Animator>();
        ws = new WaitForSeconds(0.3f);
        animator.SetFloat(hashOffset, Random.Range(0.0f, 1.0f));
        animator.SetFloat(hashWalkSpeed, Random.Range(1.0f, 1.2f));
        Portal.SetActive(false);

    }
    void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }
    IEnumerator CheckState()
    {
        while (!isDie)
        {
            if (state == State.DIE) yield break;

            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            if(dist <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (dist <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }
            yield return ws;
        }
    }
    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                case State.PATROL:
                    enemyFire.isFire = false;
                    moveAgent.patrolling = true;
                    animator.SetBool(hashMove,true);
                    break;
                case State.TRACE:
                    enemyFire.isFire = false;
                    moveAgent.traceTarget = playerTr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    moveAgent.Stop();
                    animator.SetBool(hashMove, false);
                    if (enemyFire.isFire == false)
                    {
                        enemyFire.isFire = true;
                    }
                    break;
                case State.DIE:
                    isDie = true;
                    enemyFire.isFire = false;
                    moveAgent.Stop();
                    animator.SetTrigger(hashDie);
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<CapsuleCollider>().enabled = false;
                    Portal.SetActive(true);
                    break;

            }
        }
    }
 
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(hashSpeed, moveAgent.speed);
    }
    public void OnPlayerDie()
    {
        moveAgent.Stop();
        enemyFire.isFire = false;
        StopAllCoroutines();

        animator.SetTrigger(hashPlayerDie);
    }
}
