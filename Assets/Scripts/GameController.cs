﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}