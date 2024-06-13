using UnityEngine;

public class Player : Entity
{
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Vector2 _clampedMovement;
    private float _dashMultiplier = 1.8f;
    private bool _isVerticalDashing;
    private bool _isHorizontalDashing;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        if (!CanMove) return;

        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _clampedMovement = Vector2.ClampMagnitude(_movement, 1);

        if (Input.GetKey(KeyCode.Space))
            Dash();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + (_clampedMovement * (Data.BaseSpeed * Time.fixedDeltaTime)));

        if (_isHorizontalDashing)
        {
            _isHorizontalDashing = false;
            _rb.AddForce(new Vector2(_rb.velocity.x * _dashMultiplier, _rb.velocity.y), ForceMode2D.Impulse);
        }

        if (_isVerticalDashing)
        {
            _isVerticalDashing = false;
            _rb.AddForce(new Vector2(_rb.velocity.x, _rb.velocity.y * _dashMultiplier), ForceMode2D.Impulse);
        }
    }

    private void Dash()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && !_isHorizontalDashing)
            _isHorizontalDashing = true;

        if (Input.GetAxisRaw("Vertical") != 0 && !_isVerticalDashing)
            _isVerticalDashing = true;
    }
}