using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        // Membuat instance Singleton untuk Player
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Mengambil referensi dari komponen PlayerMovement dan Animator
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        playerMovement.Move();
    }

    private void LateUpdate()
    {
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}
