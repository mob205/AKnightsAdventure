using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    Image image;

    #region Sprite references
    public Sprite heartEmpty;
    public Sprite heartFourth;
    public Sprite heartHalf;
    public Sprite heartThreeFourths;
    public Sprite heartFull;
    #endregion

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {

    }

    public void SetSpriteNull()
    {
        image.color = Color.clear;
    }
    
    public void SetSprite(int value)
    {
        image.color = Color.white;
        switch (value)
        {
            case 0:
                image.sprite = heartEmpty;
                break;
            case 1:
                image.sprite = heartFourth;
                break;
            case 2:
                image.sprite = heartHalf;
                break;
            case 3:
                image.sprite = heartThreeFourths;
                break;
            case 4:
                image.sprite = heartFull;
                break;
        }
    }
    

}
