using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Movement : MonoBehaviour
{
    public GameObject pj;

    private Transform tf;

    private void Start()
    {
        tf = this.GetComponent<Transform>();
    }

    private void Update()
    {
        Seguimiento();
    }

    private void Seguimiento()
    {
        this.tf.position = new Vector3(pj.transform.position.x, tf.position.y, pj.transform.position.z);
        //tf.Translate(new Vector3(pj.transform.position.x, tf.position.y, pj.transform.position.z));
    }
}