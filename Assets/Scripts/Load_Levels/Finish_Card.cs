using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Card : MonoBehaviour
{
    [SerializeField] private GameObject card;
    private Load_Next_Level readyToEnd;

    private void Start()
    {
        readyToEnd = this.GetComponent<Load_Next_Level>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (readyToEnd.isActiveAndEnabled)
            card.SetActive(true);
        //card.enabled = true;
    }
}