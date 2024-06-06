using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private EntityStats playerStats;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<EntityStats>(); // Obtendo a referência para PlayerStats
    }

    void Update()
    {
        float moveSpeed = playerStats.baseSpeed; // Ajuste da velocidade de movimento do jogador

        float moveInput = Input.GetAxis("Horizontal"); 

        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); 

        rb.velocity = moveVelocity; // Aplicar a velocidade ao Rigidbody
    }

}