using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Score : MonoBehaviour
{
    static public int _value
    {
        get
        {
            return _value;
        }
        set
        {
            if (value >= 0)
            {
                _value = value;
            }
        }
    }
}
