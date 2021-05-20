using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 10f;
    public float maxHealth = 50f;
    public float maxShielding;
    public float score;

    //Locais
    float accelerationInput = 0f;
    float steeringInput = 0f;
    float rotationAngle = 0f;
    float velocityVsUp = 0f;

    Rigidbody2D carRigidBody2D = new Rigidbody2D();

    void Awake()
    {
        carRigidBody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        StopOrthogonalVelocity();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CarController controller = GetComponent<CarController>();
        if (other.CompareTag("Boost"))
        {
            controller.maxSpeed = 50.0f;
        }
        else if (other.CompareTag("Health"))
        {
            controller.maxHealth += 50f;
        }
        else if (other.CompareTag("Coin"))
        {
            controller.score += 10f;
        }
        else if (other.CompareTag("Shield"))
        {
            controller.maxShielding += 20f;
        }
    }
    void ApplyEngineForce()
    {
        CarController controller = GetComponent<CarController>();
       

        
        velocityVsUp = Vector2.Dot(transform.up, carRigidBody2D.velocity);
        if(velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }
        if (carRigidBody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }
        if (accelerationInput == 0)
        {
            carRigidBody2D.drag = Mathf.Lerp(carRigidBody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else carRigidBody2D.drag = 0;

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minSpeedForTurningFactor = (carRigidBody2D.velocity.magnitude / 8);
        minSpeedForTurningFactor = Mathf.Clamp01(minSpeedForTurningFactor);
        rotationAngle -= steeringInput * turnFactor * minSpeedForTurningFactor;


        carRigidBody2D.MoveRotation(rotationAngle);
    }

    void StopOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody2D.velocity, transform.right);

        carRigidBody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
