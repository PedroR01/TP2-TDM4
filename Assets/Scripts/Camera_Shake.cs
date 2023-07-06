using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    private void FixedUpdate() // Se actualiza en fixedUpdate por el uso de fisicas y para que no se lea simultaneamente con el movimiento del personaje.
    {
        Vector3 desiredPosition = target.position + offset; // Posicion de la camara a seguir con una distancia del objeto predefinida
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Actualiza la posicion de la camara de una forma suave y lineal. No funciona.
        transform.position = smoothedPosition;

        // transform.LookAt(target); // Mira en la direccion del objetivo. Hay que arreglarlo, al girar la camara se rompe el movimiento del personaje.
    }

    // Little shake when the player collides
    private void Shake()
    {
    }
}