using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Test : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody playerRb;

    private Actions_Test actions_Test;

    private int controlsAvaible;

    private Scene actualScene;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        actions_Test = new Actions_Test();
        actions_Test.Player.Enable();

        controlsAvaible = 0;
        // La verificacion de escenas para habilitar los controles deberia hacerse desde un GameManager
        actualScene = SceneManager.GetActiveScene();
        if (actualScene.name == "Level1")
        {
            controlsAvaible = 1;
        }
        else if (actualScene.name == "Level2")
        {
            controlsAvaible = 2;
        }
    }

    private void Update()
    {
        PlayerControls(controlsAvaible);
    }

    // Determina que controles tiene disponible el usuario segun el nivel
    private void PlayerControls(int controls)
    {
        switch (controls)
        {
            case 1: // Controles Abuso
                PlayerMovement();
                break;

            case 2: // Controles Mediacion
                PlayersAproach();
                break;

            default: break;
        }
    }

    private void PlayerMovement()
    {
        // Lee los datos obtenidos por las teclas vinculadas al movimiento y los almacena en una variable.
        Vector2 inputVector = actions_Test.Player.Movement.ReadValue<Vector2>(); // Controles = Flechas del teclado O Arrastrar el mouse por la pantalla.

        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y); // Lo cambio a un vector3 para poder mover el cubo sobre el eje X e Y.

        Vector3 velocity = movementDirection * speed; // Añadido de velocidad, su valor depende del puesto en el inspector
        playerRb.AddForce(velocity, ForceMode.Force);
    }

    private void PlayersAproach()
    {
        // Encontrar el objeto padre del protagonista.
        // Hacer que los dos circulos se acerquen al padre mientras se mantenga apretado.
        // Hacer que orbiten alrededor hasta que se deje de apretar.
        // Volver a sus posiciones originales de forma suave.
    }
}