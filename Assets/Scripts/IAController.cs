using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.EventSystems;
using Google.Protobuf.Collections;
using System.Threading;

public class IAController : Agent
{
    float IASpeed = 15f;
    Rigidbody rb;
    public bool isTraining = false;
    float moveX;
    float currentSpeed;

    public RayPerceptionSensorComponent3D rayPerceptionSensor; // Referencia al componente del sensor


    public override void Initialize()
    {
        if (!isTraining)
        {
            MaxStep = 0;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rb.velocity);

    }


    public override void OnEpisodeBegin()
    {
        GameManager.instance.RestartGame();
        rb.velocity = Vector3.zero; // Reinicia la velocidad del rigidbody
        moveX = 0f; // Reinicia el movimiento
        currentSpeed = IASpeed; // Reinicia la velocidad
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var continuousActions = actions.ContinuousActions;
        if (continuousActions.Length >= 3)
        {
            moveX = continuousActions[0];
            float sprintAction = continuousActions[1];
            currentSpeed = sprintAction > 0.5f ? IASpeed * 2f : IASpeed;
            float KickAction = continuousActions[2];
           
            if(KickAction == 1)
            {
                KickBall();            
            }
        }
        else
        {
            Debug.LogWarning("Se esperaban al menos 2 acciones continuas.");
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float MoveX = 0;
        float sprintAction = Input.GetKey(KeyCode.LeftShift) ? 1f : 0f;
        float kickAction = Input.GetMouseButtonDown(0) ? 1f : 0f;

        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        if (continuousActions.Length >= 3)
        {
            continuousActions[0] = MoveX;
            continuousActions[1] = sprintAction;
            continuousActions[2] = kickAction;
        }
        else
        {
            Debug.LogWarning("El array de acciones continuas es demasiado pequeño.");
        }
    }
    private void Update()
    {
        Vector3 movement = transform.right * moveX * currentSpeed;
        Debug.Log("Current Speed: " + currentSpeed);
        rb.velocity = movement;
    }

    public void TakeGoal()
    {
        this.AddReward(-0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            this.AddReward(0.5f);
        }
    }

    void KickBall()
    {
        Debug.Log("KickBall 1");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);

        if (colliders.Length != 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Ball"))
                {
                    Vector3 forceDir = collider.transform.position - transform.position;
                    collider.GetComponent<Rigidbody>().AddForce(forceDir.normalized * 15f, ForceMode.Impulse);
                    this.AddReward(0.25f);
                }
            }
        }
    }
}
