using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;

    private void Start()
    {
        // Mengambil komponen Rigidbody2D dan mengatur nilai awal
        rb = GetComponent<Rigidbody2D>();

        // Kalkulasi kecepatan dan friksi berdasarkan waktu yang dibutuhkan untuk akselerasi dan berhenti
        moveVelocity = maxSpeed / timeToFullSpeed;
        moveFriction = moveVelocity / timeToFullSpeed;
        stopFriction = moveVelocity / timeToStop;
    }

    public void Move()
    {
        // Mendapatkan input dari pemain
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Mengatur arah gerakan berdasarkan input
        moveDirection = new Vector2(moveX, moveY).normalized;

        Vector2 velocity = rb.velocity;
        Vector2 targetVelocity = moveDirection * moveVelocity;
        Vector2 friction = GetFriction(velocity);

        // Memperbarui kecepatan berdasarkan friksi dan arah gerakan
        rb.velocity = Vector2.ClampMagnitude(velocity + targetVelocity - friction, maxSpeed.magnitude);
    }

    private Vector2 GetFriction(Vector2 velocity)
    {
        // Menghitung gesekan berdasarkan komponen kecepatan
        if (velocity.magnitude > stopClamp.magnitude)
        {
            return moveFriction * velocity.normalized;
        }
        return stopFriction * velocity.normalized;
    }

    private void MoveBound()
    {
        // Implementasikan batas-batas pergerakan jika diperlukan
    }

    public bool IsMoving()
    {
        // Mengembalikan true jika pemain sedang bergerak
        return rb.velocity.magnitude > stopClamp.magnitude;
    }
}