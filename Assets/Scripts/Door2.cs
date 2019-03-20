using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    public int collect2;
    public int cry2;
    public Animator anim2;
    // Start is called before the first frame update
    void Start()
    {
        anim2 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        collect2 = SimpleCharacterControl.pianodoor;
        cry2 = SimpleCharacterControl.crystal;
        if (collect2 == 1 && cry2 == 4)
        {
            anim2.SetBool("Open", true);

        }
        else
        {
            anim2.SetBool("Open", false);
        }
    }
}