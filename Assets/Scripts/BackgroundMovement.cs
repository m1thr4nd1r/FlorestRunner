using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMovement : MonoBehaviour
{
    public MovementSpeedType _movementSpeedType;
    public float _speed = 1f;
    public float _multiplier = 1f;

    Image _renderer;
    List<Transform> _brothers;

    private void Awake()
    {
        if (_movementSpeedType != null)
            _speed = _movementSpeedType.speed;

        _brothers = new List<Transform>();
        var children = transform.parent.gameObject.GetComponentsInChildren<Transform>();

        for (int i = 1; i < children.Length; i++)
            if (!children[i].name.Equals(name))
                _brothers.Add(children[i]);
                
        _renderer = GetComponent<Image>();
    }

    private void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);

        if (pos.x <= -Camera.main.pixelWidth)
        {
            var higherX = Mathf.NegativeInfinity;
            Transform right = null;
            foreach (var child in _brothers)
            {
                if (child.position.x <= higherX)
                    continue;
                
                higherX = child.position.x;
                right = child;
            }

            transform.position = new Vector2(right.position.x + _renderer.sprite.bounds.extents.x / 2 - 3f, transform.position.y);
        }

        var newPosition = transform.position;
        newPosition.x -= _speed * _multiplier * Time.deltaTime;
        transform.position = newPosition;
    }
}
