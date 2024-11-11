using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    int Score;
    string Color;
    bool ColorSwitch;
    Dictionary<string, int> RedDamages;
    Dictionary<string, int> BlueDamages;
    Dictionary<string, int> YellowDamages;
    GameObject[] LevelControllers;

    void Awake()
    {
        LevelControllers = GameObject.FindGameObjectsWithTag("LevelController");
        if (LevelControllers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        Score = 0;
        Color = "Red";
        ColorSwitch = true;
        RedDamages = new Dictionary<string, int>();
        BlueDamages = new Dictionary<string, int>();
        YellowDamages = new Dictionary<string, int>();
        RedDamages.Add("Red", 3);
        RedDamages.Add("Blue", 2);
        RedDamages.Add("Yellow", 1);
        BlueDamages.Add("Red", 1);
        BlueDamages.Add("Blue", 3);
        BlueDamages.Add("Yellow", 2);
        YellowDamages.Add("Red", 2);
        YellowDamages.Add("Blue", 1);
        YellowDamages.Add("Yellow", 3);
    }

    void Update()
    {
        
    }

    public Dictionary<string, int> GetDamages(string Color)
    {
        if (Color == "Red")
        {
            return RedDamages;
        }
        else if (Color == "Blue")
        {
            return BlueDamages;
        }
        else if (Color == "Yellow")
        {
            return YellowDamages;
        }
        return null;
    }

    public void AddScore(int PointsEarned)
    {
        Score += PointsEarned;
    }

    public string GetColor()
    {
        return Color;
    }

    public bool SetColor(string Color)
    {
        if (!ColorSwitch || this.Color == Color)
        {
            return false;
        }
        ColorSwitch = false;
        this.Color = Color;
        return true;
    }

    public int GetScore()
    {
        return Score;
    }
}
