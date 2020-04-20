using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float health;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveAmount;
    private Animator _animator;


    private void Start()
    {
        // Set equal to rigidbody that is attached to character
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // *** normalized will not move faster diagonally ***
        _moveAmount = moveInput.normalized * speed;

        // Detect if player is moving
        if (moveInput != Vector2.zero)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        // Don't forget fixedDeltaTime to make frame rate independent
        _rigidbody.MovePosition(_rigidbody.position + _moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }
}
