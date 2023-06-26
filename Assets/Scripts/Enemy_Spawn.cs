using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyTypes;

    [SerializeField]
    private Transform spawn;

    [SerializeField]
    private List<GameObject> enemiesList;

    [SerializeField, Tooltip("Dependiendo del nivel, el collider efectua un comportamiento distinto")]
    private int behaviour;

    private void Start()
    {
        enemiesList = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Genera un enemigo de inmediato cuando se ingresa al nivel
            Debug.Log("Entrando al siguiente concepto");
            for (int i = 0; i < 4; i++)
            {
                Spawn();
                // accedo al script del enemigo en la lista para seleccionar su comportamiento
                enemiesList[i].gameObject.GetComponent<Enemy>().variation = behaviour;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = enemiesList.Count - 1; i >= 0; i--)
            {
                if (enemiesList[i] != null)
                    Destroy(enemiesList[i]); // Elimina el objeto

                enemiesList.RemoveAt(i); // Elimina el objeto de la lista
            }
        }
    }

    private void Spawn()
    {
        int enemyType = Random.Range(0, 1);
        Quaternion spawnRotation = Quaternion.Euler(90, 180, 0);
        Vector3 spawnPosition = spawn.position + new Vector3(Random.Range(3f, 8f), 0, Random.Range(3f, 8f));
        // Añado los enemigos a una lista para despues poder tener una referencia para borrarlos.
        enemiesList.Add((GameObject)Instantiate(enemyTypes[1], spawnPosition, spawnRotation));
    }
}