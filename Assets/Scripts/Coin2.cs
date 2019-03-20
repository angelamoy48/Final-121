using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin2 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            SimpleCharacterControl.Coin2 = 1;
            SimpleCharacterControl.displayText = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            SimpleCharacterControl.displayText = false;
            SimpleCharacterControl.Coin2 = 0;
        }
    }
}
