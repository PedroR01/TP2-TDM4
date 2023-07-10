using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 targetScale = new Vector3(2f, 2f, 2f);
    [SerializeField] private float duration = .5f;
    [SerializeField] private float returnDuration = .5f;

    private Vector3 initialScale;
    private Coroutine scalingCoroutine;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Acoso") // Aumenta la masa en el nivel de acoso = menos velocidad
            this.GetComponent<Rigidbody>().mass = 8;
        else if (other.gameObject.layer == 3) // Activa las luces en vanidad
            other.GetComponent<Light>().enabled = true;

        if (other.gameObject.name == "Big")
        {
            if (scalingCoroutine != null)
                StopCoroutine(scalingCoroutine);

            scalingCoroutine = StartCoroutine(ScaleCharacter(targetScale, duration));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Acoso")
            this.GetComponent<Rigidbody>().mass = 3;
        else if (other.gameObject.layer == 3)
            other.GetComponent<Light>().enabled = false;

        if (other.gameObject.name == "Big")
        {
            if (scalingCoroutine != null)
                StopCoroutine(scalingCoroutine);

            scalingCoroutine = StartCoroutine(ScaleCharacter(initialScale, returnDuration));
        }
    }

    private IEnumerator ScaleCharacter(Vector3 target, float time)
    {
        float timer = 0f;
        Vector3 initialScale = transform.localScale;

        while (timer < time)
        {
            timer += Time.deltaTime;
            float t = timer / time;
            Vector3 newScale = Vector3.Lerp(initialScale, target, t);
            newScale.y = initialScale.y; // Maintain initial Y scale
            transform.localScale = newScale;
            yield return null;
        }

        transform.localScale = target;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
            GetComponent<Renderer>().material.color = Color.cyan;
    }

    private void OnCollisionExit(Collision other)
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}