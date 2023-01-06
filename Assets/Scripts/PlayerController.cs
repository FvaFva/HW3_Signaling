using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    private Transform _transform;
    private Rigidbody2D _body;
    private float _distanceToGroundForJump = 0000.1f;
    private readonly RaycastHit2D[] _downPlace = new RaycastHit2D[10];

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            bool isUnderFeetCollision  = _body.Cast(Vector2.down, _downPlace, _distanceToGroundForJump) != 0;

            if(isUnderFeetCollision && isGroundUnderFeet())
            {
                _body.AddForce(Vector2.up * _jumpForce);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            _transform.Translate(_speed * Time.deltaTime,0,0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _transform.Translate(-1 * _speed * Time.deltaTime, 0, 0);
        }   
    }

    private bool isGroundUnderFeet()
    {
        foreach (RaycastHit2D place in _downPlace)
        {
            if(place.transform.gameObject.GetComponent<Ground>())
                return true;
        }
        
        return false;
    }
}
