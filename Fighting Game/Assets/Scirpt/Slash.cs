using System;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public GameObject enemyPlayer;
    public GameObject blockPlayer;
    
    private PlayerHealth _enemyHealth;

    private PlayerAttack _enemyAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyAttack = enemyPlayer.GetComponent<PlayerAttack>();
        _enemyHealth = enemyPlayer.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == enemyPlayer) _enemyHealth.Slashed();
        if (other.gameObject == blockPlayer)
        {
            _enemyAttack.Blocked();
            _enemyHealth.Blocked();
        }
    }
}
