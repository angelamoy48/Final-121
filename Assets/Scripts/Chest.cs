using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            SimpleCharacterControl.chest = 1;
            SimpleCharacterControl.chestdoor = 1;
            SimpleCharacterControl.displayText = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            SimpleCharacterControl.displayText = false;
            SimpleCharacterControl.chest = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
