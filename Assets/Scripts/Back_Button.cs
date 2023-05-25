using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_Button : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this); // Esto capaz hay que removerlo despues.
        gameObject.SetActive(false);
    }
}