using UnityEngine;

public class PlayerMovement : Entity
{
    private Rigidbody2D _rb;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        if (!CanMove) return;

        float moveSpeed = Data.BaseSpeed; // Ajuste da velocidade de movimento do jogador

        float moveInputHorizontal = Input.GetAxisRaw("Horizontal");
        float moveInputVertical = Input.GetAxisRaw("Vertical");

        Vector2 moveVelocity = new Vector2(moveInputHorizontal * moveSpeed, moveInputVertical * moveSpeed);

        _rb.velocity = moveVelocity; // Aplicar a velocidade ao Rigidbody
    }
}