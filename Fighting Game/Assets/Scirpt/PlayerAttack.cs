using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float direction;
    private bool _facingRight;
    
    public GameObject slashGameObject;
    public GameObject shieldGameObject;
    
    public GameObject playerEnemy;
    
    public bool usingShield;
    public float shieldBar;
    public float shieldBarDeplenish;
    public float shieldBarPer;
    public float shieldBarMax;

    public bool stunned;
    
    public float slashLength;
    private float _slashCooldown;

    public Rigidbody2D rb;

    private PlayerAttack _playerEnemyAttackScript;
    private PlayerHealth _playerEnemyHealthScript;
    
    public LayerMask shieldLayer;
    
    public KeyCode slash;
    public KeyCode shield;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerEnemyAttackScript = playerEnemy.GetComponent<PlayerAttack>();
        slashGameObject.transform.position = new Vector2(112, 112);
        shieldGameObject.transform.position = new Vector2(111,111);
        shieldBarMax = 40f;
        shieldBarDeplenish = 0.25f;
        shieldBarPer = 0.005f;
        shieldBar = shieldBarMax;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _facingRight = GetComponent<PlayerMovement>().facingRight;
        if (_facingRight) 
        { 
            direction = 0.5f;
        }else 
        if (!_facingRight) 
        { 
            direction = -0.5f;
        }
        
        if (shieldBar >= 0 && !stunned)
        { 
            if(Input.GetKey(shield)) usingShield = true; 
            else usingShield = false; 
        }  
        else usingShield = false;

        if (usingShield)
        {
            shieldBar -= shieldBarDeplenish; 
            shieldGameObject.transform.position = new Vector2(transform.position.x + direction, transform.position.y);
        }
        else
        {
            shieldGameObject.transform.position = new Vector2(111,111);
            StartCoroutine(ShieldRegen());
        }

        
        if (Input.GetKeyDown(slash))
        {
            if (_slashCooldown <= 0 && !stunned)
            {
                slashGameObject.transform.position = new Vector2(transform.position.x + direction, transform.position.y);
                StartCoroutine(Slash());
                _slashCooldown = 2;
            }
        }
        _slashCooldown -= Time.deltaTime;
    }

    IEnumerator Slash()
    {
        slashGameObject.transform.position = new Vector2(transform.position.x + direction, transform.position.y);
        yield return new WaitForSeconds(slashLength);
        slashGameObject.transform.position = new Vector2(111, 111);
    }

    IEnumerator ShieldRegen()
    {
        yield return new WaitForSeconds(1);
        while (shieldBar < shieldBarMax)
        {
            yield return new WaitForSeconds(0.1f);
            shieldBar += shieldBarPer;
        }
        if (shieldBar > shieldBarMax)
        {
            shieldBar = shieldBarMax;
        }
    }

    public void Blocked()
    {
        shieldBar -= shieldBarDeplenish * 40f;
        if ( shieldBar < 0)
        {
            shieldBar = 0;
            IsStunned();
        }
    }

    private void IsStunned()
    {
        if (shieldBar >= shieldBarMax)
        {
          stunned = false;
        }else{
            stunned = true;
        }
    }
}
