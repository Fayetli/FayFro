using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    private int _maxHealth;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    

    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }


    private int _maxDash;
    public int MaxDash
    {
        get { return _maxDash; }
        set { _maxDash = value; }
    }


    private int _dash;
    public int Dash
    {
        get { return _dash; }
        set { _dash = value; }
    }

    //Dash
    public void SetDashToMaxDash() { _dash = _maxDash; }
    public bool CanDash() { return _dash != 0; }

    //Health
    public void SetHealthToMaxHealth() { _health = _maxHealth; }
    public bool IsOneHP() { return _health == 1; }


    public Stat() { _score = 0; _maxHealth = _health = 1; _maxDash = _dash = 0; }
}
public class StatCharacterController : MonoBehaviour
{
    //private StatCharacterController() { }
    public static Stat player = new Stat();
}
