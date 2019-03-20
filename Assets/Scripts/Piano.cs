using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
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
            SimpleCharacterControl.piano = 1;
            SimpleCharacterControl.pianodoor = 1;
            SimpleCharacterControl.displayText = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);
            SimpleCharacterControl.displayText = false;
            SimpleCharacterControl.piano = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
