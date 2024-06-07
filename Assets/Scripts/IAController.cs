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
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        moveX = actions.ContinuousActions[0];
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float MoveX = 0;
        MoveX = Input.GetAxisRaw("Horizontal");
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = MoveX;
    }

    private void Update()
    {
        Vector3 movement = transform.right * moveX * IASpeed;
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
}
