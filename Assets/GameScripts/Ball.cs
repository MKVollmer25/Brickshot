using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject Paddle;
    LevelController Cont;
    public Rigidbody2D BallBody;
    int X_Dir;
    int Y_Dir;
    int SuperColor;
    bool Sticky;
    bool Stuck;
    bool Launched;
    float PaddlePos;
    Vector3 MouseIn;
    Vector2 MouseCoords;
    Vector3 Movement;
    Vector3 BallTarget;
    Renderer OuterBall;
    Renderer InnerBall;
    float Angle;
    float ClickAngle;
    float Speed;
    string BallColor;
    Color[] Red;
    Color[] Blue;
    Color[] Yellow;
    Color[] Colorless;
    Color[] Super;
    // Start is called before the first frame update
    void Start()
    {
        Movement = new Vector3(0, 0, 0);
        Angle = Mathf.PI / 2;
        ClickAngle = 0.0f;
        Speed = 0.0f;
        PaddlePos = 0.0f;
        X_Dir = 1;
        Y_Dir = 1;
        SuperColor = 0;
        Paddle = GameObject.Find("Paddle");
        Cont = GameObject.Find("LevelController").GetComponent<LevelController>();
        Sticky = false;
        Stuck = false;
        Launched = false;
        BallBody = GetComponent<Rigidbody2D>();
        BallTarget = gameObject.transform.position;
        OuterBall = gameObject.GetComponent<Renderer>();
        InnerBall = gameObject.transform.GetChild(0).GetComponent<Renderer>();
        Red = new Color[2];
        Blue = new Color[2];
        Yellow = new Color[2];
        Colorless = new Color[2];
        Super = new Color[2];
        Red[0] = new Color(0.8f, 0f, 0f, 1f);
        Red[1] = new Color(1f, 0f, 0f, 1f);
        Blue[0] = new Color(0f, 0f, 0.8f, 1f);
        Blue[1] = new Color(0f, 0f, 1f, 1f);
        Yellow[0] = new Color(0.8f, 0.8f, 0f, 1f);
        Yellow[1] = new Color(1f, 1f, 0f, 1f);
        Colorless[0] = new Color(0.5f, 0.5f, 0.5f, 1f);
        Colorless[1] = new Color(0.7f, 0.7f, 0.7f, 1f);
        Super[0] = new Color(0.8f, 0f, 0f, 1f);
        Super[1] = new Color(1f, 0f, 0f, 1f);
        SetBallColor("Super");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Speed = 6.5f;
            Stuck = false;
            if (!Launched)
            {
                MouseIn = Input.mousePosition;
                MouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (MouseCoords.x - gameObject.transform.position.x != 0) {
                    ClickAngle = Mathf.Atan2(Mathf.Abs(MouseCoords.y - gameObject.transform.position.y), Mathf.Abs(MouseCoords.x - gameObject.transform.position.x));
                    if (ClickAngle < (Mathf.PI / 8))
                    {
                        Angle = Mathf.PI / 8;
                    }
                    else
                    {
                        Angle = ClickAngle;
                    }
                    if (MouseCoords.x - gameObject.transform.position.x < 0)
                    {
                        X_Dir = -1;
                    }
                }
                else
                {
                    Angle = Mathf.PI / 2;
                }
                Launched = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Cont.SetColor("Red"))
            {
                SetBallColor("Red");
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (Cont.SetColor("Blue"))
            {
                SetBallColor("Blue");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Cont.SetColor("Yellow"))
            {
                SetBallColor("Yellow");
            }
        }
    }

    void FixedUpdate()
    {
        Movement.x = Mathf.Cos(Angle) * Speed * X_Dir;
        Movement.y = Mathf.Sin(Angle) * Speed * Y_Dir;
        BallBody.velocity = Movement;
        if (BallColor == "Super")
        {
            SuperColor = (SuperColor + 1) % 51;
            OuterBall.material.SetColor("_Color", Color.HSVToRGB(SuperColor / 50.0f, 1.0f, 0.8f));
            InnerBall.material.SetColor("_Color", Color.HSVToRGB(SuperColor / 50.0f, 1.0f, 1.0f));
        }
        if (Stuck)
        {
            BallTarget.x = Paddle.transform.position.x + PaddlePos;
            gameObject.transform.position = BallTarget;
        }
    }

    public void SetBallColor(string NewColor)
    {
        switch (NewColor)
        {
            case "Red":
                OuterBall.material.SetColor("_Color", Red[0]);
                InnerBall.material.SetColor("_Color", Red[1]);
                BallColor = "Red";
                break;
            case "Blue":
                OuterBall.material.SetColor("_Color", Blue[0]);
                InnerBall.material.SetColor("_Color", Blue[1]);
                BallColor = "Blue";
                break;
            case "Yellow":
                OuterBall.material.SetColor("_Color", Yellow[0]);
                InnerBall.material.SetColor("_Color", Yellow[1]);
                BallColor = "Yellow";
                break;
            case "Colorless":
                OuterBall.material.SetColor("_Color", Colorless[0]);
                InnerBall.material.SetColor("_Color", Colorless[1]);
                BallColor = "Colorless";
                break;
            case "Super":
                OuterBall.material.SetColor("_Color", Color.HSVToRGB(SuperColor / 50.0f, 1.0f, 0.8f));
                InnerBall.material.SetColor("_Color", Color.HSVToRGB(SuperColor / 50.0f, 1.0f, 1.0f));
                BallColor = "Super";
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        Vector2 CollisionNormal = CollisionInfo.GetContact(0).normal;
        Debug.Log(CollisionNormal);
        if (SideHit(CollisionNormal.x, CollisionNormal.y))
        {
            if (CollisionNormal.x < 0)
            {
                X_Dir = -1;
            }
            else
            {
                X_Dir = 1;
            }
        }
        else
        {
            if (CollisionNormal.y < 0)
            {
                Y_Dir = -1;
            }
            else
            {
                Y_Dir = 1;
            }
        }
        if (CollisionInfo.transform.name == "Paddle" && !SideHit(Mathf.Abs(CollisionNormal.x), Mathf.Abs(CollisionNormal.y)))
        {
            BallTarget = gameObject.transform.position;
            PaddlePos = gameObject.transform.position.x - CollisionInfo.transform.position.x;
            Angle = (Mathf.PI / 2) - (Mathf.Abs(PaddlePos) * 1.8f);
            if (gameObject.transform.position.x - CollisionInfo.transform.position.x > 0)
            {
                X_Dir = 1;
            }
            else
            {
                X_Dir = -1;
            }
            if (Sticky)
            {
                Speed = 0.0f;
                Stuck = true;
            }
        }
        if (CollisionInfo.gameObject.tag == "Brick" || CollisionInfo.gameObject.tag == "MetalBrick")
        {
            CollisionInfo.gameObject.GetComponent<Brick>().Damage(BallColor);
        }
    }
    
    void OnCollisionStay2D(Collision2D CollisionInfo)
    {
        Vector2 CollisionNormal = CollisionInfo.GetContact(0).normal;
        if (SideHit(CollisionNormal.x, CollisionNormal.y))
        {
            if (CollisionNormal.x > 0 && X_Dir != 1)
            {
                X_Dir = 1;
            }
            if (CollisionNormal.x < 0 && X_Dir != -1)
            {
                X_Dir = -1;
            }
        }
        else
        {
            if (CollisionNormal.y < 0)
            {
                Y_Dir = -1;
            }
            else if (CollisionNormal.y > 0)
            {
                Y_Dir = 1;
            }
        }
    }

    bool SideHit(float x, float y)
    {
        return Mathf.Abs(x) > Mathf.Abs(y);
    }

}
