using UnityEngine;

public class GeneralMovement : MonoBehaviour
{
    public MovementSpeedType _movementSpeedType;
    public float _speed = 1f;
    public float _multiplier = 1f;

    bool _oneTimeOnly = true;
    bool _playerAlive = true;
    float _borderPositionX;
    Transform _player;
    GameController _gameController;

    private void Start()
    {
        if (_movementSpeedType != null)
            _speed = _movementSpeedType.speed;

        var obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            _player = obj.transform;

        var camera = Camera.main;
        _gameController = camera.GetComponent<GameController>();

        _borderPositionX = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0)).x;
    }

    private void Update()
    {
        var newPosition = transform.position;
        newPosition.x -= _speed * _multiplier * Time.deltaTime;
        transform.position = newPosition;

        if (transform.position.x < _player.position.x && _oneTimeOnly) 
            GivePoints();

        if (transform.position.x >= Camera.main.pixelWidth * -0.01f) return;
        
        transform.position = new Vector2(_borderPositionX * 1.1f, transform.position.y);
        _oneTimeOnly = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        print(name);
        _playerAlive = false;
        _gameController.PlayerKilled();
    }

    private void GivePoints()
    {
        if (!_playerAlive) return;

        _oneTimeOnly = false;
        _gameController.IncrementScore(1);
    }
}
