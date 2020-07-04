using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static Text Score;
    public static Text Health;
    public static Text Dash;

    private void Start()
    {
        Score = GameObject.Find("Score").gameObject.GetComponent<Text>();
        Health = GameObject.Find("Health").gameObject.GetComponent<Text>();
        Dash = GameObject.Find("Dash").gameObject.GetComponent<Text>();
        PrintScore();
        PrintHealth();
        PrintDash();
    }


    public static void PrintScore()
    {
        Score.text = "Score: " + StatCharacterController.player.Score;
    }
    public static void PrintHealth()
    {
        Health.text = "Health: " + StatCharacterController.player.Health;
    }
    public static void PrintDash()
    {
        Dash.text = "Dash: " + StatCharacterController.player.Dash;
    }

}
