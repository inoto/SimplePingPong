using UnityEngine;
using Random = UnityEngine.Random;

namespace SimplePingPong
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ball : MonoBehaviour
    {
        public Color Color
        {
            get => _sprite.color;
            set => _sprite.color = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public float Scale
        {
            get => _transform.localScale.x;
            set => _transform.localScale = new Vector2(value, value);
        }

        Vector2 RandomDirection => new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-1f, 1f)).normalized;
        
        float speed;
        float newX;
        Vector2 direction;
        
        Rigidbody2D _rigidbody;
        Transform _transform;
        SpriteRenderer _sprite;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void Reset()
        {
            _transform.position = Vector3.zero;
            _rigidbody.velocity = Vector2.zero;
            
            Invoke("Run", 1);
        }

        void Run()
        {
            _rigidbody.velocity = RandomDirection * speed;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Racket"))
                return;
            
            newX = (transform.position.x - other.transform.position.x) / other.collider.bounds.size.x;
        
            direction = _transform.position.y > 0 ? new Vector2(newX, -1).normalized : new Vector2(newX, 1).normalized;
            _rigidbody.velocity = direction * speed;
        }
    }
}
