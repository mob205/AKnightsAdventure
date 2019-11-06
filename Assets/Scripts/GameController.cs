using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    enum States
    {

    }

    public static GameController instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
