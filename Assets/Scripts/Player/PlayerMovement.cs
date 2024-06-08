using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private EntityStats playerStats;
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<EntityStats>(); // Obtendo a referência para PlayerStats
    }

    void Update()
    {
        if (canMove)
        {
            float moveSpeed = playerStats.baseSpeed; // Ajuste da velocidade de movimento do jogador

            float moveInputHorizontal = Input.GetAxisRaw("Horizontal");
            float moveInputVertical = Input.GetAxisRaw("Vertical");

            Vector2 moveVelocity = new Vector2(moveInputHorizontal * moveSpeed, moveInputVertical * moveSpeed); 

            rb.velocity = moveVelocity; // Aplicar a velocidade ao Rigidbody
        }
    }
  
}