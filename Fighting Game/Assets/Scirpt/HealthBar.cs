using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class HealthBar : MonoBehaviour
{
    public GameObject winScreen;
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI winTextLable;
    
    public bool player1Dead;
    public bool player2Dead;
    
    public GameObject healthBarP1;
    public GameObject healthBarP2;
    public GameObject blockBarP1GameObject;
    public GameObject blockBarP2GameObject;
    
    public GameObject player1;
    public GameObject player2;

    public SpriteRenderer player1Color;
    public SpriteRenderer player2Color;

    public PlayerHealth health1;
    public PlayerHealth health2;
    public PlayerAttack attack1;
    public PlayerAttack attack2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winScreen.SetActive(false);
        health1 = player1.GetComponent<PlayerHealth>();
        health2 = player2.GetComponent<PlayerHealth>();
        attack1 = player1.GetComponent<PlayerAttack>();
        attack2 = player2.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attack1.stunned) player1Color.color = Color.yellow;
        else player1Color.color = Color.blue;
        if(attack2.stunned) player2Color.color = Color.yellow;
        else player2Color.color = Color.blue;
            
        if (health1.health <= 0)
        {
            player1Dead = true;
            StartCoroutine(Dead());
        };
        if (health2.health <= 0)
        {
            player2Dead = true;
            StartCoroutine(Dead());; 
        }
        
        float healthP1 = health1.health;
        float healthP2 = health2.health;
        float blockBarP1 = attack1.shieldBar;
        float blockBarP2 = attack2.shieldBar;
        
        Vector3 localScaleP1 = healthBarP1.transform.localScale;
        Vector3 localScaleP2 = healthBarP2.transform.localScale;
        Vector3 localBlockScaleP1 = blockBarP1GameObject.transform.localScale;
        Vector3 localBlockScaleP2 = blockBarP2GameObject.transform.localScale;
        
        localScaleP1.x = healthP1 * 0.01f;
        localScaleP2.x = healthP2 * 0.01f;
        localBlockScaleP1.x = blockBarP1 * 0.025f;
        localBlockScaleP2.x = blockBarP2 * 0.025f;

        healthBarP1.transform.localScale = localScaleP1;
        healthBarP2.transform.localScale = localScaleP2;
        blockBarP1GameObject.transform.localScale = localBlockScaleP1;
        blockBarP2GameObject.transform.localScale = localBlockScaleP2;
    }
    
    
    IEnumerator Dead()
    {
        if (player1Dead)
        {
            winScreen.SetActive(true);
            winTextLable.text = "Player 2 Wins";
        }
        if (player2Dead)
        {
            winScreen.SetActive(true);
            winTextLable.text = "Player 1 Wins";
        }
        yield break;
    }
    

    public void onButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
