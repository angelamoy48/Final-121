using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position;
        transform.Translate(Vector3.back * 2);
        transform.Translate(Vector3.up * 2);
    }
}

