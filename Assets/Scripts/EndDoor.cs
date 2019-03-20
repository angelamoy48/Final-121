using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    public int collect4;
    public int cry4;
    public Animator anim4;
    // Start is called before the first frame update
    void Start()
    {
        anim4 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        collect4 = SimpleCharacterControl.tankdoor;
        cry4 = SimpleCharacterControl.crystal;
        if (collect4 == 1 && cry4 == 7)
        {
            anim4.SetBool("Open", true);

        }
        else
        {
            anim4.SetBool("Open", false);
        }
    }
}
