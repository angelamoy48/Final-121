using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3 : MonoBehaviour
{
    public int collect3;
    public int cry3;
    public Animator anim3;
    // Start is called before the first frame update
    void Start()
    {
        anim3 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        collect3 = SimpleCharacterControl.pianodoor;
        cry3 = SimpleCharacterControl.crystal;
        if (collect3 == 1 && cry3 == 6)
        {
            anim3.SetBool("Open", true);

        }
        else
        {
            anim3.SetBool("Open", false);
        }
    }
}
