using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class MovingPlayerScript : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private Transform Target;
    [SerializeField] private Transform Player;
    Animator anim;
    NavMeshAgent Agent;


    void Start()
    {
        anim = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateUpAxis = false;
        Agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    private void Move()
    {
        if (Target != null)
        {
            Agent.SetDestination(Target.position);
            Agent.speed = Speed;
            anim.SetBool("Run", true);
        }
        if (Vector2.Distance(Player.position, Target.position) <= 0.1f)
        {
            anim.SetBool("Run", false);
        }
    }
}
