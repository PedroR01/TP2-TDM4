using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [HideInInspector]
    public int variation;

    [SerializeField, Range(2, 20)]
    private float speed, distMin, distMax;

    [SerializeField]
    private Material[] mat = new Material[2];

    private Rigidbody rb;

    //private float rotationSpeed = 90f;
    private float smoothTime = 1f;

    private Quaternion targetRotation;
    private Quaternion initialRotation;
    private Quaternion currentRotation;
    private float timer;
    private bool isRotating;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        initialRotation = transform.rotation;
        currentRotation = transform.rotation;
        timer = 0f;
        isRotating = false;
    }

    private void FixedUpdate()
    {
        Behaviour();
    }

    private void Behaviour()
    {
        switch (variation)
        {
            case 0: // Los enemigos del nivel acoso
                Look();
                Follow();
                break;

            case 1: // Los enemigos del nivel xenofobia
                this.gameObject.layer = 6;
                Wall();
                this.GetComponent<Rigidbody>().mass = 20;
                this.GetComponent<Rigidbody>().drag = 20;
                this.GetComponent<Rigidbody>().angularDrag = 20;
                break;

            case 2: // Los enemigos del nivel vanidad
                break;

            case 3: // Los enemigos del nivel desinteres
                Ignore();
                break;

            default:
                break;
        }
    }

    private void Look()
    {
        Vector3 direccion = player.position - this.transform.position;
        direccion.y = -90;

        Quaternion rotacion = Quaternion.LookRotation(direccion);
        transform.rotation = rotacion;
    }

    private void Follow()
    {
        // codigo para reducir o aumentar la velocidad y moverse en base a la distancia con su objetivo.
        if (canAttack())
        {
            float dist = ObjectiveDistance().magnitude;
            Vector3 objectiveVector = ObjectiveDistance();
            Vector3 velocity = objectiveVector * speed * Time.deltaTime;
            if (dist < distMin)
                GetComponent<Renderer>().material = mat[1];
            else if (dist < distMax)
            {
                GetComponent<Renderer>().material = mat[0];
                rb.AddForce(velocity, ForceMode.Force);
                // añadir corrutina para que cada 2 segundos tenga un impulso de velocidad
            }
        }
    }

    private bool canAttack()
    {
        return true;
    }

    private Vector3 ObjectiveDistance()
    {
        Vector3 vectorToObjective = player.position - this.transform.position;

        return vectorToObjective;
    }

    private void Ignore()
    {
        // Codigo para voltearse o alinearse (dependiendo del nivel) cuando se acerque el protagonista.
        Debug.Log("Ignorando");
        float dist = ObjectiveDistance().magnitude;

        if (dist < 5 && !isRotating)
            StartRotation();

        if (isRotating)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, timer / smoothTime);
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, t);

            if (timer >= smoothTime && dist > 5)
            {
                timer = 0f;
                isRotating = false;
                currentRotation = targetRotation;
            }
        }
    }

    public void StartRotation()
    {
        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * 180f);
        currentRotation = transform.rotation;
        timer = 0f;
        isRotating = true;
    }

    private void Wall()
    {
        float dist = ObjectiveDistance().magnitude;
        if (dist > 2) // Si el jugador esta lejos, lo miran
            Look();
        // Cuando se acerca vuelven a formacion y le impiden pasar
        // hacer el modulo de formacion
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && this.variation == 1)
        {
            Rigidbody playerRb = other.gameObject.GetComponent<Rigidbody>();

            // Calculate the direction from the enemy to the player
            Vector3 pushDirection = other.transform.position - transform.position;
            pushDirection.Normalize();

            // Apply an impulse force to the player in the opposite direction
            playerRb.AddForce(pushDirection * 70f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}