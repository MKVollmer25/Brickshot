using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D BallBody;
    int X_Dir;
    int Y_Dir;
    Vector3 Movement;
    Renderer OuterBall;
    Renderer InnerBall;
    float Angle;
    float Speed;
    string Color;
    Color[] Red;
    Color[] Blue;
    Color[] Yellow;
    // Start is called before the first frame update
    void Start()
    {
        Movement = new Vector3(0, 0, 0);
        Angle = Mathf.PI/3;
        Speed = 6.5f;
        X_Dir = 1;
        Y_Dir = 1;
        BallBody = GetComponent<Rigidbody2D>();
        OuterBall = gameObject.GetComponent<Renderer>();
        InnerBall = gameObject.transform.GetChild(0).GetComponent<Renderer>();
        Red = new Color[2];
        Blue = new Color[2];
        Yellow = new Color[2];
        Red[0] = new Color(0.8f, 0f, 0f, 1f);
        Red[1] = new Color(1f, 0f, 0f, 1f);
        Blue[0] = new Color(0f, 0f, 0.8f, 1f);
        Blue[1] = new Color(0f, 0f, 1f, 1f);
        Yellow[0] = new Color(0.8f, 0.8f, 0f, 1f);
        Yellow[1] = new Color(1f, 1f, 0f, 1f);
        Color = "Red";
        SetBallColor(Color);
    }

    void FixedUpdate()
    {
        Movement.x = Mathf.Cos(Angle) * Speed * X_Dir;
        Movement.y = Mathf.Sin(Angle) * Speed * Y_Dir;
        BallBody.velocity = Movement;
    }

    public void SetBallColor(string NewColor)
    {
        switch (NewColor)
        {
            case "Red":
                OuterBall.material.SetColor("_Color", Red[0]);
                InnerBall.material.SetColor("_Color", Red[1]);
                Color = "Red";
                break;
            case "Blue":
                OuterBall.material.SetColor("_Color", Blue[0]);
                InnerBall.material.SetColor("_Color", Blue[1]);
                Color = "Blue";
                break;
            case "Yellow":
                OuterBall.material.SetColor("_Color", Yellow[0]);
                InnerBall.material.SetColor("_Color", Yellow[1]);
                Color = "Yellow";
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        Vector2 CollisionNormal = CollisionInfo.GetContact(0).normal;
        if (SideHit(CollisionNormal.x, CollisionNormal.y))
        {
            X_Dir *= -1;
        }
        else
        {
            Y_Dir *= -1;
        }
        if (CollisionInfo.transform.name == "Paddle" && !SideHit(Mathf.Abs(CollisionNormal.x), Mathf.Abs(CollisionNormal.y)))
        {
            Angle = (Mathf.PI / 2) + (Mathf.Abs(gameObject.transform.position.x - CollisionInfo.transform.position.x) * 1.8f);
            if (gameObject.transform.position.x - CollisionInfo.transform.position.x > 0)
            {
                X_Dir = -1;
            }
            else
            {
                X_Dir = 1;
            }
        }
        if (CollisionInfo.gameObject.tag == "Brick")
        {
            CollisionInfo.gameObject.GetComponent<Brick>().Damage(Color);
        }
    }

    void OnCollisionStay2D(Collision2D CollisionInfo)
    {
        Vector2 CollisionNormal = CollisionInfo.GetContact(0).normal;
        Debug.Log(CollisionNormal);
        if (SideHit(CollisionNormal.x, CollisionNormal.y))
        {
            if (CollisionNormal.x > 0 && X_Dir != -1)
            {
                Debug.Log("Ball stuck on wall");
                X_Dir = -1;
            }
            if (CollisionNormal.x < 0 && X_Dir != 1)
            {
                Debug.Log("Ball stuck on wall");
                X_Dir = 1;
            }
        }
        else
        {
            if (CollisionNormal.y < 0)
            {
                //Y_Dir = -1;
            }
            else
            {
                //Y_Dir = 1;
            }
        }
    }

    bool SideHit(float x, float y)
    {
        return Mathf.Abs(x) > Mathf.Abs(y);
    }

}
