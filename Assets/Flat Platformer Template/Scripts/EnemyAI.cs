using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    SpriteRenderer spriteRenderer;
    GameObject defendObject;
    GameObject attackObject;
    Vector3 playerRelativeDirection;
    Vector3 defendObjectRelativeDirection;
    public HealthSystem enemyHealth;

    float distanceToPlayer;
    float distanceToDefendObject;
    private float chaseRange = 10;
    public float attackRange;
    public float speed;
    public float attackObjectDistance;
    private float _startScale;
    private float lastAttackTime;
    public float attackDelay;
    public float damage = 10;

    void Start()
    {
        enemyHealth = gameObject.GetComponent<HealthSystem>();
        enemyHealth.SetHp(20);
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        defendObject = GameObject.FindGameObjectWithTag("DefendObject");
        _startScale = transform.localScale.x;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        distanceToDefendObject = Vector3.Distance(transform.position, defendObject.transform.position);
        playerRelativeDirection = player.transform.InverseTransformPoint(transform.position);
        defendObjectRelativeDirection = defendObject.transform.InverseTransformPoint(transform.position);

        Chase(distanceToPlayer, distanceToDefendObject);
        if (enemyHealth.GetCurrentHealth() <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Attack(float distanceToTarget, GameObject target)
    {
        if (Time.time > lastAttackTime + attackDelay)
        {
                Debug.Log("Atacking player/cart");
                target.GetComponent<HealthSystem>().Damage(damage);
                lastAttackTime = Time.time;
        }
    }

    private void HandleFacingToPlayer(Vector3 playerRelativeDirection, bool attackingObject)
    {
        if (attackingObject)
        {
            if (defendObjectRelativeDirection.x >= 0)
                transform.localScale = new Vector3(_startScale, _startScale, 1);
            else
                transform.localScale = new Vector3(-_startScale, _startScale, 1);
        } else if (playerRelativeDirection.x >= 0)
            transform.localScale = new Vector3(_startScale, _startScale, 1);
        else if(playerRelativeDirection.x <= 0)
            transform.localScale = new Vector3(-_startScale, _startScale, 1);
    }

    private void Chase(float distanceToPlayer, float distanceToDefendObject)
    {
        if (distanceToPlayer < chaseRange && !(distanceToPlayer <= attackRange) && distanceToDefendObject > attackObjectDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
            HandleFacingToPlayer(playerRelativeDirection, false);
        }
        else if (distanceToPlayer <= attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position, 0);
            Attack(distanceToPlayer, player);
            HandleFacingToPlayer(playerRelativeDirection, false);
        } else
        {
            ChaseDefendObject(distanceToDefendObject);
            if(distanceToDefendObject <= attackObjectDistance)
                Attack(distanceToPlayer, defendObject);
            HandleFacingToPlayer(playerRelativeDirection, true);
        }
    }

    private void ChaseDefendObject(float distanceToDefendObject)
    {
        if(distanceToDefendObject > attackObjectDistance)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(defendObject.transform.position.x, transform.position.y), speed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, transform.position, 0);

    }
}
