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

    public TextMeshProUGUI textTop;
    public TextMeshProUGUI textBottom;
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
            textTop.text = $"Provide the roots of life for the planet. ";
            textBottom.text = $"Shoot asteroids to make them drop the seeds.";
        }
        else if (pendingXP < 0)
        {
            textTop.text = $"Game Over. Try not to crash";
            textBottom.text = $"Press esc or start to exit";
        }
        else if (finalXP == 0)
        {
            textTop.text = $"Collected seeds: {pendingXP:D3} Deliver them to the planet";
            textBottom.text = $"Getting hit will cost you undeposited seeds";
        }
        else if (finalXP < winXP)
        {
            textTop.text = $"Collected seeds: {pendingXP:D3} Life Goal {finalXP:D3} / {winXP}";
            textBottom.text = $"Getting hit without any seeds will kill you.";
        }
        else
        {
            textTop.text = $"Congratulations , you have won. Keep playing for high score.";
            textBottom.text = $"Collected seeds: {pendingXP:D3} Life Goal {finalXP:D3}";
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
