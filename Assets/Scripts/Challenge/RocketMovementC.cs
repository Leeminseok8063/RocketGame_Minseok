using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketMovementC : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 10f;
    private readonly float ROTATIONSPEED = 100f;

    //private Vector2 GroundToPlayerVector;
    private Vector2 frontVector;
    private Vector2 RotateVector;
    
    private float highScore = -1;
    private float degree = 90;

    public static Action<float> OnHighScoreChanged;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        FixDegree();
        Rotate(RotateVector.x);
        Debug.DrawLine(transform.position, transform.position + (Vector3)frontVector * 5f, Color.red);

        if (!(highScore < transform.position.y)) return;
        highScore = transform.position.y;
        OnHighScoreChanged?.Invoke(highScore);
    }

    private void FixDegree()
    {
        float rad = degree * Mathf.Deg2Rad;
        frontVector = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }

    public void ApplyMovement(float inputX)
    {
        RotateVector.x = inputX;
    }

    public void ApplyBoost()
    {
        _rb2d.AddForce(frontVector * SPEED, ForceMode2D.Impulse);
    }

    public float GetDegree()
    {
        return degree;
    }
    
    public Vector2 GetFrontVector() 
    { return frontVector; }

    private void Rotate(float inputX)
    {
        degree -= RotateVector.x * ROTATIONSPEED * Time.fixedDeltaTime;
        degree = Mathf.Clamp(degree, 10f, 160f);
        transform.rotation = Quaternion.Euler(0, 0, degree - 90);
    }
}