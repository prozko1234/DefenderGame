﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    public float WalkSpeed;
    public float JumpForce;
    public AnimationClip _walk, _jump;
    public Animation _Legs;
    public Transform _Blade, _GroundCast;
    public Camera cam;
    public bool mirror;
    public Vector2 _lastPosition;
    public HealthSystem playerHealth;
    public Transform attackPoint;
    public float attackRadius = 0.5f;
    public LayerMask enemyLayers;

    public float attackPower;
    private bool _canJump, _canWalk;
    private bool _isWalk, _isJump;
    private float rot, _startScale;
    private Rigidbody2D rig;
    private Vector2 _inputAxis;
    private RaycastHit2D _hit;
    private Animator anim;

	void Start ()
    {
        playerHealth = gameObject.GetComponent<HealthSystem>();
        playerHealth.SetHp(100);
        anim = _Blade.gameObject.GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        _startScale = transform.localScale.x;
	}

    void Update()
    {
        _inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_hit = Physics2D.Linecast(new Vector2(_GroundCast.position.x, _GroundCast.position.y + 0.2f), _GroundCast.position))
        {
            if (!_hit.transform.CompareTag("Player"))
            {
                _lastPosition = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
                _canJump = true;
                _canWalk = true;
            }
        }
        else _canJump = false;

        if (_inputAxis.y > 0 && _canJump)
        {
            _canWalk = false;
            _isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        Vector3 dir = cam.ScreenToWorldPoint(Input.mousePosition) - _Blade.transform.position;
        dir.Normalize();

        if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x + 0.2f)
            mirror = false;
        if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x - 0.2f)
            mirror = true;

        if (!mirror)
        {
            rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localScale = new Vector3(_startScale, _startScale, 1);
            //_Blade.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }
        if (mirror)
        {
            rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            transform.localScale = new Vector3(-_startScale, _startScale, 1);
            //_Blade.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
        }

        if (_inputAxis.x != 0)
        {
            rig.velocity = new Vector2(_inputAxis.x * WalkSpeed * Time.deltaTime, rig.velocity.y);

            if (_canWalk)
            {
                _Legs.clip = _walk;
                _Legs.Play();
            }
        }

        else
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }

        if (_isJump)
        {
            rig.AddForce(new Vector2(0, JumpForce));
            _Legs.clip = _jump;
            _Legs.Play();
            _canJump = false;
            _isJump = false;
        }
    }
        
    public bool IsMirror()
    {
        return mirror;
    }

    public void Attack()
    {
        if(_canJump && _canWalk)
        {
            anim.SetTrigger("Attack");
            anim.SetBool("IsMirror", false);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);
            foreach(Collider2D enemy in hitEnemies)
            {
                var enemyHealth = enemy.GetComponent<HealthSystem>();
                enemyHealth.Damage(attackPower);
                Debug.Log("we hit " + enemy.name);
            }
            Debug.Log("Player attacked");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
