using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using static UnityEditor.Experimental.GraphView.GraphView;

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

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
                Wall();
                break;

            case 2: // Los enemigos del nivel vanidad
                break;

            case 3: // Los enemigos del nivel desinteres
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
    }

    private void Wall()
    {
        float dist = ObjectiveDistance().magnitude;
        if (dist > 2) // Si el jugador esta lejos, lo miran
            Look();
        else // Cuando se acerca vuelven a formacion y le impiden pasar
            Ignore(); // hacer el modulo de formacion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && variation == 2) // Colision de desinteres
        {
            Ignore();
        }
    }
}