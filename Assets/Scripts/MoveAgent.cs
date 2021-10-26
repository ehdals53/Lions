using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int nextIdx;
    private readonly float patrolSpeed = 1.5f;
    private readonly float traceSpeed = 4.0f;
    private float damping = 1.0f;
    private NavMeshAgent agent;
    private Transform enemyTr;

    private bool _patrolling;
    public bool patrolling
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1.0f;
                MoveWayPoint();
            }
        }
    }
    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            damping = 7.0f;
            TraceTarget(_traceTarget);
        }
    }
    public float speed
    {
        get { return agent.velocity.magnitude; }
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.updateRotation = false;
        agent.speed = patrolSpeed;

        var group = GameObject.Find("WayPointGroup");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);

            nextIdx = Random.Range(0, wayPoints.Count);
        }
        MoveWayPoint();
    }
    void MoveWayPoint()
    {
        if (agent.isPathStale) return;

        agent.destination = wayPoints[nextIdx].position;
        agent.isStopped = false;
    }
    public void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;

        agent.destination = pos;
        agent.isStopped = false;

    }
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _patrolling = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(agent.isStopped == false)
        {
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
        if (!_patrolling) return;

        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            //nextIdx = ++nextIdx % wayPoints.Count;
            nextIdx = Random.Range(0, wayPoints.Count);

            MoveWayPoint();
        }
    }
}
