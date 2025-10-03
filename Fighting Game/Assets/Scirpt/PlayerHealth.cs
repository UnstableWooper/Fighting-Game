using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    
    public GameObject slash;
    public GameObject enemy;

    private PlayerAttack _playerAttack;
    
    public float health;

    public float imortalityFrames;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerAttack = enemy.GetComponent<PlayerAttack>();
        health = 100;
    }

    void Update()
    {
        Debug.Log(imortalityFrames + gameObject.name);
        imortalityFrames -= Time.deltaTime;
        if (_playerAttack.usingShield == true) imortalityFrames = 0.01f;
    }

    public void Slashed()
    {
        if (imortalityFrames <= 0)
        {
            health -= 10;
            imortalityFrames = 2f;
        }
    }
    
    public void Blocked()
    {
        imortalityFrames = 2f;
    }
}
