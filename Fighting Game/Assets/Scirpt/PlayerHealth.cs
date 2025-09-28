using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    
    public GameObject slash;
    public float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == slash)
        {
            health -= 10;
        }
    }
}
