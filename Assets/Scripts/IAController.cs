using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.EventSystems;
using Google.Protobuf.Collections;
using System.Threading;
using Unity.VisualScripting;

public class IAController : Agent
{
    float IASpeed = 15f;
    Rigidbody rb;
    public bool isTraining = false;
    float moveX;
    float currentSpeed;

    List<GameObject> grabbedBalls = new List<GameObject>();

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
            float grabBallAction = continuousActions[3];
           
            if(KickAction == 1)
            {
                KickBall();            
            }

            if(grabBallAction == 1)
            {
                GrabBall();
            }
            else if (grabbedBalls.Count > 0)
            {
                ReleaseBall();
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
        float grabBallAction = Input.GetMouseButton(1) ? 1f : 0f;

        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        if (continuousActions.Length >= 3)
        {
            continuousActions[0] = MoveX;
            continuousActions[1] = sprintAction;
            continuousActions[2] = kickAction;
            continuousActions[3] = grabBallAction;
        }
        else
        {
            Debug.LogWarning("El array de acciones continuas es demasiado peque�o.");
        }
    }
    private void Update()
    {
        Vector3 movement = transform.right * moveX * currentSpeed;
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
        Collider[] colliders = Physics.OverlapSphere(transform.position + new Vector3(0,-0.5f,0), 2f);

        if (colliders.Length != 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Ball"))
                {
                    Vector3 forceDir = collider.transform.position - transform.position;
                    collider.GetComponent<Rigidbody>().AddForce(forceDir.normalized * 9f, ForceMode.Impulse);
                    this.AddReward(0.10f);
                }
            }
        }
    }

    void GrabBall()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.3f);

        if (colliders.Length != 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Ball") && !grabbedBalls.Contains(collider.gameObject))
                {
                    FixedJoint ballJoint = collider.AddComponent<FixedJoint>(); //Vamos a usar joints para sostener las pelotas
                    ballJoint.connectedMassScale = 0;
                    ballJoint.connectedBody = rb;
                    ballJoint.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    grabbedBalls.Add(ballJoint.gameObject);
                    if(grabbedBalls.Count == 0) StartCoroutine(RewardOnGrabbedBall());
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + new Vector3(0, -0.5f, 0), 2f);
    }

    void ReleaseBall()
    {
        if (grabbedBalls.Count > 0)
        {
            foreach (GameObject ball in grabbedBalls)
            {
                Vector3 ballDir = (ball.transform.position - transform.position).normalized;
                Rigidbody ballRB = ball.GetComponent<Rigidbody>();
                Destroy(ball.GetComponent<FixedJoint>());
                ballRB.velocity = Vector3.zero;
                ballRB.AddForce(ballDir * 11f, ForceMode.Impulse);
            }
            grabbedBalls.Clear();
            Debug.Log("Released ball " + name);
            StopAllCoroutines();
        }

    }

    IEnumerator RewardOnGrabbedBall()
    {
        yield return new WaitForSeconds(0.5f);
        this.AddReward(0.1f);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(0.1f);
        Debug.Log("Grab " + name);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(0.15f);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(0.15f);
        Debug.Log("Grab " + name);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(0.2f);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(0.15f);
        Debug.Log("Grab " + name);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(-0.15f);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(-0.2f);
        Debug.Log("Grab negative " + name);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(-0.2f);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(-0.3f);
        Debug.Log("Grab negative " + name);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(-0.3f);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(-0.4f);
        Debug.Log("Grab negative " + name);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(-0.4f);
        yield return new WaitForSeconds(0.5f);
        this.AddReward(-0.6f);
    }
}