using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideManager : MonoBehaviour
{
    public static bool  inside = false;
    public static bool outside = true;

    public static void HideToggle(bool isHide)
    {
        inside = isHide;
        outside = !isHide;
    }

 
}
