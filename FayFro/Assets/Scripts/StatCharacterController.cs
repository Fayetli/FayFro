using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private int score;

    private int maxHealth;
    private int health;

    private int maxDash;
    private int dash;

    //Score
    public int GetScore() { return score; }
    public void AddScore(int addScore) { score += addScore; }

    //Dash
    public int GetDash() { return dash; }
    public int GetMaxDash() { return maxDash; }
    public void AddDash(int addDash) { dash += addDash; }
    public void AddMaxDash(int addMaxDash) { maxDash += addMaxDash; }
    public void SetDashToMaxDash() { dash = maxDash; }
    public bool CanDash() { if (dash != 0) return true; return false; }

    //Health
    public int GetHealth() { return health; }
    public int GetMaxHealth() { return maxHealth; }
    public void AddHealth(int addHealth) { health += addHealth; }
    public void AddMaxHealth(int addMaxHealth) { maxHealth += addMaxHealth; }
    public void SetHealthToMaxHealth() { health = maxHealth; }
    public bool IsOneHP() { if (health > 1) return false; return true; }


    public Stat() { score = 0; maxHealth = health = maxDash = dash = 1; }
}
public class StatCharacterController : MonoBehaviour
{
    public static Stat player = new Stat();
}
