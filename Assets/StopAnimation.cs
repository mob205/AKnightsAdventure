using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimation : MonoBehaviour
{
    void StopParentAnimation()
    {
        SendMessageUpwards("StopAnimation");
    }
}
