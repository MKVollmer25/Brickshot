using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    int HP;
    string BrickColor;
    public Type BrickType;
    LevelController Cont;
    SpriteRenderer BrickSprite;
    Sprite Cracked1;
    Sprite Cracked2;
    Dictionary<string, int> Damages;
    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
        BrickSprite = gameObject.GetComponent<SpriteRenderer>();
        Cracked1 = Resources.Load<Sprite>("Sprites/CrackedBrick1");
        Cracked2 = Resources.Load<Sprite>("Sprites/CrackedBrick2");
        Cont = GameObject.Find("LevelController").GetComponent<LevelController>();
        if (BrickType == Type.Red)
        {
            BrickColor = "Red";
        }
        if (BrickType == Type.Blue)
        {
            BrickColor = "Blue";
        }
        if (BrickType == Type.Yellow)
        {
            BrickColor = "Yellow";
        }
        if (BrickType == Type.Metal)
        {
            BrickColor = "Metal";
        }
        if (BrickColor != "Metal")
        {
            Damages = Cont.GetDamages(BrickColor);
        }
        else
        {
            Damages = new Dictionary<string, int>();
        }
    }

    public void Damage(string Color)
    {
        Debug.Log(BrickColor);
        int DamageDone;
        if (Color != "Rainbow")
        {
            if (BrickColor != "Metal")
            {
                if (Color != "Colorless")
                {
                    DamageDone = Damages[Color];
                }
                else
                {
                    DamageDone = 1;
                }
            }
            else
            {
                DamageDone = 0;
            }
        }
        else
        {
            DamageDone = 3;
        }
        if (DamageDone >= HP)
        {
            if (DamageDone == HP)
            {
                Cont.AddScore(DamageDone + 2);
            }
            else
            {
                Cont.AddScore(DamageDone + 1);
            }
            Destroy(this.gameObject);
        }
        else
        {
            Cont.AddScore(DamageDone);
            HP -= DamageDone;
            Crack();
        }
    }

    void Crack()
    {
        if (HP == 2)
        {
            BrickSprite.sprite = Cracked1;
        }
        else if (HP == 1)
        {
            BrickSprite.sprite = Cracked2;
        }
    }
}

public enum Type
{
    Red,
    Blue,
    Yellow,
    Metal
}
