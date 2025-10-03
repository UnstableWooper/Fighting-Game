using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public LayerMask groundLayer;
    private bool _onGround;

    public KeyCode jumpInput;
    public KeyCode rightInput;
    public KeyCode leftInput;
    
    public float jumpForce;
    public float speed;
    
    public bool facingRight;

    private PlayerAttack _playerAttack;
    
    public GameObject player;
    public GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerAttack = player.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        RaycastHit2D ground = Physics2D.Linecast(transform.position, transform.position + transform.TransformDirection(Vector3.down) * 0.6f, groundLayer);
        if (ground.collider != null)
        {
            _onGround = true;
        }
        else
        {
            _onGround = false;
        }
        
        if (enemy.transform.position.x > transform.position.x)
        {
            facingRight = true;
            Vector3 localScale = transform.localScale;
            localScale.x = 1;
            transform.localScale = localScale;
        }
        else if(enemy.transform.position.x < transform.position.x)
        {
            facingRight = false;
            Vector3 localScale = transform.localScale;
            localScale.x = -1;
            transform.localScale = localScale;
        }
        
        if (Input.GetKeyDown(jumpInput) && !_playerAttack.stunned)
        {
            if (_onGround)
            {
                rb.AddForce(Vector3.up * jumpForce);
            }
        }
        if (Input.GetKey(rightInput)  && !_playerAttack.stunned)
        {
            rb.AddForce(Vector3.right * speed);
        }
        if (Input.GetKey(leftInput)  && !_playerAttack.stunned)
        {
            rb.AddForce(Vector3.left * speed);
        }
    }
}
