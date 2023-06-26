using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyTypes;

    [SerializeField]
    private Transform spawn;

    private List<GameObject> enemiesList = new List<GameObject>();

    [SerializeField, Tooltip("Dependiendo del nivel, el collider efectua un comportamiento distinto")]
    private int behaviour;

    private bool hasSpawned = false; // Flag condicional para ver si ya se instanciaron

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hasSpawned)
        {
            // Genera un enemigo de inmediato cuando se ingresa al nivel
            Debug.Log("Entrando al siguiente concepto");

            int numberOfEnemies; // Especifica la cantidad de enemigos que se quieren instanciar
            if (behaviour == 0)
                numberOfEnemies = 10;
            else if (behaviour != 1)
                numberOfEnemies = 5;
            else
                numberOfEnemies = 8;

            for (int i = 0; i < numberOfEnemies; i++)
            {
                GameObject enemy = Spawn();
                // accedo al script del enemigo en la lista para seleccionar su comportamiento
                enemy.GetComponent<Enemy>().variation = behaviour;
            }

            hasSpawned = true; // En verdadero una vez finalizada la instanciacion
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject enemy in enemiesList)
            {
                Destroy(enemy); // Elimina el objeto
            }
            enemiesList.Clear(); // Elimina el objeto de la lista
            hasSpawned = false;
        }
    }

    private GameObject Spawn()
    {
        int enemyType = Random.Range(0, enemyTypes.Length);
        Quaternion spawnRotation = Quaternion.Euler(90, 180, 0);

        Vector3 spawnOffset;
        Vector3 spawnPosition;

        if (behaviour != 1)
        {
            spawnOffset = new Vector3(Random.Range(3f, 8f), 0, Random.Range(3f, 8f));
            spawnPosition = spawn.position + Vector3.right * spawnOffset.x + Vector3.forward * spawnOffset.z;
        }
        else
        {
            float columnSpacing = 2f; // Especifica el espacio entre triangulos en la columna
            spawnOffset = new Vector3(2f, 0, -12f);
            spawnPosition = spawn.position + Vector3.forward * (columnSpacing * enemiesList.Count) + spawnOffset;
        }

        GameObject enemy = Instantiate(enemyTypes[enemyType], spawnPosition, spawnRotation);

        // Añado los enemigos a una lista para despues poder tener una referencia para llamarlos o borrarlos.
        enemiesList.Add(enemy);
        return enemy;
    }
}