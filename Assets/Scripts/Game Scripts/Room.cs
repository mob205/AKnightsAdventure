﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public BoxCollider2D cameraBounds;
    private bool hasEnabled;
    public string roomName;
    public AudioClip music;

    //bool isDisabling;
    //public void Enable()
    //{
    //    hasEnabled = true;
    //    gameObject.SetActive(true);
    //    MusicPlayer.instance.PlayMusic(music);
    //}
    //public void Disable(float delay)
    //{
    //    if (isDisabling) { return; }
    //    StartCoroutine(delayedDisable(delay));
    //    isDisabling = true;
    //}
    //IEnumerator delayedDisable(float delay)
    //{
    //    hasEnabled = false;
    //    yield return new WaitForSeconds(delay);
    //    if (!hasEnabled)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //    isDisabling = false;
        
    //}
}
