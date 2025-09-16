using Base;
using UnityEngine;

namespace Player
{
    public class Player : Entity
    {
        [SerializeField] private float dashMultiplier = 1.2f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 0.7f;

        private Rigidbody2D _rb;
        private Vector2 _movement;
        private Vector2 _clampedMovement;
        private Collider2D _collider;

        private bool _isDashing;
        private Vector2 _dashDirection;
        private float _dashTimer;
        private float _dashCooldownTimer;

        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<PolygonCollider2D>();
        }

        protected void Update()
        {
            if (!CanMove) return;

            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");
            _clampedMovement = Vector2.ClampMagnitude(_movement, 1f);

            if (_dashCooldownTimer > 0f)
                _dashCooldownTimer -= Time.unscaledDeltaTime;

            if (Input.GetKeyDown(KeyCode.Space) && !_isDashing && _dashCooldownTimer <= 0f)
                TryStartDash();

            if (!_isDashing) return;
            
            _dashTimer -= Time.unscaledDeltaTime;
            
            if (!(_dashTimer <= 0f)) return;
            
            _isDashing = false;
            _dashCooldownTimer = dashCooldown;
            _collider.enabled = true;
        }

        private void FixedUpdate()
        {
            Vector2 moveDir = _isDashing ? _dashDirection : _clampedMovement;
            float speed = Data.BaseSpeed * (_isDashing ? dashMultiplier : 1f);

            if (!(moveDir.sqrMagnitude > 0f)) return;
            
            Vector2 targetPos = _rb.position + moveDir * (speed * Time.fixedDeltaTime);
            _rb.MovePosition(targetPos);
        }

        private void TryStartDash()
        {
            if (_clampedMovement.sqrMagnitude <= 0f)
                return;

            _isDashing = true;
            _dashDirection = _clampedMovement.normalized;
            _dashTimer = dashDuration;
            _collider.enabled = false;
        }
    }
}