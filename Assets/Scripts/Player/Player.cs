using UnityEngine;

public class Player : Entity
{
    private Rigidbody2D _rb;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!CanMove) return;

        float moveSpeed = Data.BaseSpeed;
        Vector2 moveVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed);

        _rb.velocity = moveVelocity;

        if (Input.GetKey(KeyCode.Space))
        {
            Dash();
        }
    }

    private void Dash()
    {
        float dashMultiplier = 1.8f;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            _rb.AddForce(new Vector2(_rb.velocity.x * dashMultiplier, _rb.velocity.y), ForceMode2D.Impulse);
        }

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            _rb.AddForce(new Vector2(_rb.velocity.x, _rb.velocity.y * dashMultiplier), ForceMode2D.Impulse);
        }
    }
}