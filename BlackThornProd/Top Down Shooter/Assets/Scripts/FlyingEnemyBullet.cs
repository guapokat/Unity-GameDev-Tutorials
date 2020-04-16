using UnityEngine;

public class FlyingEnemyBullet : MonoBehaviour
{
    private Player _playerScript;
    private Vector2 _targetPosition;

    public float speed;
    public int damage;

    private void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _targetPosition = _playerScript.transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _targetPosition) > .1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
