using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public int _force = 500;
    public AudioClip _hurt;
    public AudioClip _jump;

    private bool _isAlive;
    private Vector2Int _forceVector;
    private Rigidbody2D _body;
    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        _forceVector = new Vector2Int(0, _force);
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _isAlive = true;
    }

    private void Update()
    {
        if (!_isAlive) return;

        if (!Input.GetKeyDown(KeyCode.Space)) return;

        _audioSource.clip = _jump;
        _audioSource.Play();
        _body.AddForce(_forceVector);
        _animator.SetBool("Jumped", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Ground"))
            _animator.SetBool("Jumped", false);

        if (!collision.collider.CompareTag("Obstacle")) return;
        
        print("Killed by: " + collision.collider.name);
        _isAlive = false;
        _audioSource.clip = _hurt;
        _audioSource.Play();
    }
}
