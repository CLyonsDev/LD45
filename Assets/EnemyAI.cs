using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameEvent EventToCallOnHitPlayer;
    private float thinkRate = 0.2f;

    private float attackDistance = 2.15f;
    private float attackRate = 0.65f;
    private float damageDelay = 0.33f;
    private float attackTimer = 0.0f;
    private Vector2 randomPauseTime = new Vector2(0f, 0.55f);
    private bool canAttack = true;

    private Transform playerTransform;

    private NavMeshAgent agent;
    private Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent.enabled = false;
        agent.updateRotation = false;
    }

    private void Start()
    {
        //InitAI();
    }

    public void InitAI()
    {
        anim = GetComponentInChildren<Animator>();
        agent.enabled = true;
        StartCoroutine(Think());
    }

    public void SetAnimator(Animator a)
    {
        anim = a;
    }

    public void StopAI()
    {
        StopAllCoroutines();
        agent.enabled = false;
    }

    private IEnumerator AttackPlayer()
    {
        Debug.Log("Attack");
        anim.SetTrigger("Punch");
        // TODO: Random int for different attack animations?

        yield return new WaitForSeconds(damageDelay);
        EventToCallOnHitPlayer.Raise();
    }

    private IEnumerator Think()
    {
        while(true)
        {
            //Debug.Log(Vector3.Distance(transform.position, playerTransform.position));

            anim.SetFloat("Motion", agent.velocity.normalized.magnitude);
            agent.transform.LookAt(playerTransform);

            if (canAttack == false)
            {
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackRate)
                {
                    canAttack = true;
                }
            }

            if (Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
            {
                if (canAttack)
                {
                    canAttack = false;
                    attackTimer = 0.0f;
                    yield return new WaitForSeconds(Random.Range(randomPauseTime.x, randomPauseTime.y));
                    StartCoroutine(AttackPlayer());

                }
            }
            else
            {
                agent.SetDestination(playerTransform.position);
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
