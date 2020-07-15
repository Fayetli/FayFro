using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Score : MonoBehaviour
{
    static private int _value = 0;

    static public int get_value()
    {
        return _value;
    }

    static public void add_value(int adder)
    {
        _value += adder;
    }

    static public void set_value(int value)
    {
        _value = value;
    }

}
