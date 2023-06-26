using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
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