using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class XPHolderScript : MonoBehaviour
{
    public int pendingXP=0;
    public int finalXP=0;
    public int winXP = 100;

    public TextMeshProUGUI textDisplay;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();

    private void Start()
    {
        spriteRenderer =  GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (finalXP == 0 && pendingXP == 0)
        {
            textDisplay.text = $"Shoot asteroids to make them drop the seeds of life, getting hit will cost you.";
        }
        else if (pendingXP < 0)
        {
            textDisplay.text = $"Game Over. Try not to crash";
        }
        else if (finalXP == 0)
        {
            textDisplay.text = $"Collected seeds: {pendingXP} Deliver them to the planet to start the roots of life";
        }
        else if (finalXP < winXP)
        {
            textDisplay.text = $"Collected seeds: {pendingXP} Life Goal {finalXP} / {winXP}";
        }
        else
        {
            textDisplay.text = $"Congratulations , you have won. Collected seeds: {pendingXP} Life Goal {finalXP} / {winXP}";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        finalXP += pendingXP;
        pendingXP = 0;
        var amountDone = (int) Mathf.Floor(((float)finalXP) / ((float)winXP) * (sprites.Count - 1));
        if (amountDone >= sprites.Count)
        {
            amountDone = sprites.Count - 1;
        }
        //Debug.Log($"Setting amount done {amountDone}, {((float)finalXP) / ((float)winXP)} , {((float)finalXP) / ((float)winXP) * sprites.Count} ");
        spriteRenderer.sprite = sprites[amountDone];
    }
}
