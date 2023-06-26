using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 targetScale = new Vector3(2f, 2f, 2f);
    [SerializeField] private float duration = 2f;
    [SerializeField] private float returnDuration = 1f;

    private Vector3 initialScale;
    private float timer;
    private bool isScaling;

    private void Start()
    {
        initialScale = transform.localScale;
        timer = 0f;
        isScaling = false;
    }

    private void Update()
    {
        if (!isScaling)
        {
            // Smoothly return to initial scale
            if (timer < returnDuration)
            {
                timer += Time.deltaTime;
                float t = timer / returnDuration;
                Vector3 currentScale = Vector3.Lerp(targetScale, initialScale, t);
                currentScale.z = transform.localScale.z; // Maintain the initial Y scale
                transform.localScale = currentScale;
            }
            return;
        }

        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = duration;
            isScaling = false;
        }

        float t2 = timer / duration;
        Vector3 currentScale2 = Vector3.Lerp(initialScale, targetScale, t2);
        currentScale2.z = transform.localScale.z; // Maintain the initial Y scale

        transform.localScale = currentScale2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Acoso") // Aumenta la masa en el nivel de acoso = menos velocidad
            this.GetComponent<Rigidbody>().mass = 8;
        else if (other.gameObject.layer == 3) // Activa las luces en vanidad
            other.GetComponent<Light>().enabled = true;

        if (other.gameObject.name == "Big")
        {
            isScaling = true;
            timer = 0f;
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
            isScaling = false;
            timer = 0f;
            transform.localScale = initialScale;
        }
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