using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            SimpleCharacterControl.Coin1 = 1;
            SimpleCharacterControl.displayText = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            SimpleCharacterControl.displayText = false;
            SimpleCharacterControl.Coin1 = 0;
        }
    }
}
