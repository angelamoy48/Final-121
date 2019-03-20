using UnityEngine;
using System.Collections;

public class End : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Application.Quit();
        }
    }
}
