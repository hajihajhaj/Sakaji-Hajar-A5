using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    public bool isLeft;

    public GameObject ball; 

    private float _speed = 0.1f;
    private float _maxY = 4.25f; // maximum Y position the paddle can reach

    // Start is called before the first frame update
    void Start()
    {
        if (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeft)
        {
            FollowBall();
        }
        else
        {
            // player control
            if (Input.GetKey(KeyCode.UpArrow)) MoveUp();
            if (Input.GetKey(KeyCode.DownArrow)) MoveDown();
        }
    }

    // AI control
    void FollowBall()
    {
        if (ball != null)
        {
            Vector3 direction = (ball.transform.position - transform.position).normalized;
            float targetY = Mathf.Clamp(transform.position.y + direction.y * _speed, -_maxY, _maxY);
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
    }

    void MoveUp()
    {
        if (transform.position.y < _maxY)
        {
            transform.Translate(Vector2.up * _speed);
        }
    }

    void MoveDown()
    {
        if (transform.position.y > -_maxY)
        {
            transform.Translate(-Vector2.up * _speed);
        }
    }
}
