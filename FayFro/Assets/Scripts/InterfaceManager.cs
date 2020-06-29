using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static Text score;
    public static Text health;
    public static Text dash;

    private void Start()
    {
        score = GameObject.Find("Score").gameObject.GetComponent<Text>();
        health = GameObject.Find("Health").gameObject.GetComponent<Text>();
        dash = GameObject.Find("Dash").gameObject.GetComponent<Text>();
        PrintScore();
        PrintHealth();
        PrintDash();
    }


    public static void PrintScore()
    {
        score.text = "Score: " + StatCharacterController.player.GetScore();
    }
    public static void PrintHealth()
    {
        health.text = "Health: " + StatCharacterController.player.GetHealth();
    }
    public static void PrintDash()
    {
        dash.text = "Dash: " + StatCharacterController.player.GetDash();
    }

}
