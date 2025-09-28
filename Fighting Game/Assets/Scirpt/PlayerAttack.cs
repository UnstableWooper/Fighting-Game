using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float direction;
    public bool facingRight;
    
    public GameObject slashGameObject;
    public GameObject shieldGameobject;
    
    public bool usingShield;
    public float shieldBar;
    public float shieldBarDeplenish;
    public float shieldBarPer;
    public float shieldBarMax; 
    
    public float slashLength;
    private float _slashCooldown;
    
    public KeyCode slash;
    public KeyCode shield;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slashGameObject.SetActive(false);
        shieldGameobject.SetActive(false);
        shieldBarMax = 40f;
        shieldBarDeplenish = 0.25f;
        shieldBarPer = 0.005f;
        shieldBar = shieldBarMax;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        
        
        facingRight = GetComponent<PlayerMovement>().facingRight;
        if (facingRight) 
        { 
            direction = 0.5f;
        }else 
        if (!facingRight) 
        { 
            direction = -0.5f;
        }
        
        if (shieldBar >= 0)
        { 
            if(Input.GetKey(shield)) usingShield = true; 
            else usingShield = false; 
        }  
        else usingShield = false;

        if (usingShield)
        {
            shieldBar -= shieldBarDeplenish; 
            shieldGameobject.SetActive(true);
            shieldGameobject.transform.position = new Vector2(transform.position.x + direction, transform.position.y);
        }
        else
        {
            shieldGameobject.SetActive(false);
            StartCoroutine(ShieldRegen());
        }

        
        if (Input.GetKeyDown(slash))
        {
            if (_slashCooldown <= 0)
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

        slashGameObject.SetActive(true);
        yield return new WaitForSeconds(slashLength);
        slashGameObject.SetActive(false);
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
        yield break;
    }
}
