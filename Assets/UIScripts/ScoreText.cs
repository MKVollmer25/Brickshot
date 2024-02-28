using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TMP_Text ScoreDisplay;
    LevelController Cont;
    // Start is called before the first frame update
    void Start()
    {
        ScoreDisplay = GetComponent<TextMeshProUGUI>();
        Cont = GameObject.Find("LevelController").GetComponent<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay.text = "Score: " + Cont.GetScore();
    }
}
