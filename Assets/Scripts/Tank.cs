using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
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
            SimpleCharacterControl.tank = 1;
            SimpleCharacterControl.tankdoor = 1;
            SimpleCharacterControl.displayText = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            SimpleCharacterControl.displayText = false;
            SimpleCharacterControl.tank = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
