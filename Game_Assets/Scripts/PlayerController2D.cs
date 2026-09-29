using UnityEngine;

namespace EscapeTheHackerNetwork
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController2D : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        
        [Header("References")]
        [SerializeField] private Animator animator;

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Vector2 movement;
        private bool isMovementLocked = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb.gravityScale = 0f; // Top-down / 2D plane setup
            rb.freezeRotation = true;
        }

        private void Update()
        {
            if (isMovementLocked)
            {
                movement = Vector2.zero;
                return;
            }

            // Input reading (WASD or Arrow keys)
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized;

            // Flip sprite based on direction
            if (movement.x > 0.05f)
            {
                spriteRenderer.flipX = false;
            }
            else if (movement.x < -0.05f)
            {
                spriteRenderer.flipX = true;
            }

            // Animator parameter updates
            if (animator != null)
            {
                bool isWalking = movement.sqrMagnitude > 0.01f;
                animator.SetBool("isWalking", isWalking);
                animator.SetFloat("moveX", movement.x);
                animator.SetFloat("moveY", movement.y);
            }
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = movement * moveSpeed;
        }

        public void SetMovementLocked(bool locked)
        {
            isMovementLocked = locked;
            if (locked)
            {
                rb.linearVelocity = Vector2.zero;
                if (animator != null)
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }
    }
}
