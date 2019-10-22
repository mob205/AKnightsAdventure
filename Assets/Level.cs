using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public BoxCollider2D cameraBounds;

    private bool hasEnabled;

    public void Enable()
    {
        hasEnabled = true;
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void Disable(float delay)
    {
        StartCoroutine(delayedDisable(delay));
    }
    IEnumerator delayedDisable(float delay)
    {
        hasEnabled = false;
        yield return new WaitForSeconds(delay);
        if (!hasEnabled) { gameObject.SetActive(false); }
        
    }
}
