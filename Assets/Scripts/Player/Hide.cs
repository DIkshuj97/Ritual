using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public bool isHide = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Hide")
        {
            isHide = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hide")
        {
            isHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hide")
        {
            isHide = false;
        }
    }
}
