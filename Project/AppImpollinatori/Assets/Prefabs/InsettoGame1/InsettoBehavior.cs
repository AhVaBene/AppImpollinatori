using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsettoBehavior : MonoBehaviour
{
    bool toLeft;
    int impollinatore;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x == 3.3f)
        {
            transform.Rotate(new Vector3(0, 0, 90));
            toLeft = true;
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -90));
            toLeft = false;
        }
        impollinatore = UnityEngine.Random.Range(0, 69) % 7;
        //Aglais urticae(0) = Farfalla
        //Apis(1) = Ape
        //Bombus(2) = Ape
        //Cetonia(3) = Coleottero
        //Iphiclides(4) = Farfalla
        //Autographa(5) = Falena
        //Syrphus(6) = Mosca
        spriteRenderer.sprite = spriteArray[impollinatore];
    }

    // Update is called once per frame
    void Update()
    {
        if(!Game1Controller.isGameOver)
        {
            if (toLeft)
            {
                transform.position = new Vector2(
                transform.position.x - 2f * Time.deltaTime,
                transform.position.y);
            }
            else
            {
                transform.position = new Vector2(
                transform.position.x + 2f * Time.deltaTime,
                transform.position.y);
            }
        }
        

        if (transform.position.x < -3.4 || transform.position.x > 3.4 || Game1Controller.isGameOver)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        //BELLIS(0) = APE // COLEOTTERO // FARFALLA DIURNA
        //SOLANUM(1) = APE // BOMBO
        if (!Game1Controller.isGameOver)
        {
            if (ChoseFlower.FlowerChosen == 0)
            {
                if (impollinatore != 5 && impollinatore != 6)
                {
                    OnClickCorretto();
                }
                else
                {
                    OnClickErrato();
                }
            }
            if (ChoseFlower.FlowerChosen == 1)
            {
                if (impollinatore == 1 || impollinatore == 2)
                {
                    OnClickCorretto();
                }
                else
                {
                    OnClickErrato();
                }
            }
                
            Destroy(gameObject);
        }
            
    }

    private void OnClickCorretto()
    {
        Game1Controller.score++;
        GameAudioController.PlayClip(clips[0]);
        if(ChoseFlower.FlowerChosen == 0)
        {
            TimerScript.TimeLeft += 1;
        }
        else
        {
            TimerScript.TimeLeft += 3;
        }
        
    }

    private void OnClickErrato()
    {
        GameAudioController.PlayClip(clips[1]);
        if (ChoseFlower.FlowerChosen == 0)
        {
            TimerScript.TimeLeft -= 5;
        }
        else
        {
            TimerScript.TimeLeft -= 2;
        }
        
    }
}
