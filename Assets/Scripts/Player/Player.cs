using UnityEngine;

public class Player : Entity
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

        Dash();
    }

    private void Dash()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetAxisRaw("Horizontal") is 1 or -1 && Input.GetAxisRaw("Vertical") is 1 or -1)
            _rb.AddForce(new Vector2(_rb.velocity.x * 1.1f, _rb.velocity.y), ForceMode2D.Impulse);
        else
        {
            if (Input.GetKey(KeyCode.Space) && Input.GetAxisRaw("Horizontal") is 1 or -1)
                _rb.AddForce(new Vector2(_rb.velocity.x * 1.8f, _rb.velocity.y), ForceMode2D.Impulse);

            if (Input.GetKey(KeyCode.Space) && Input.GetAxisRaw("Vertical") is 1 or -1)
                _rb.AddForce(new Vector2(_rb.velocity.x * 1.8f, _rb.velocity.y), ForceMode2D.Impulse);
        }
    }
}