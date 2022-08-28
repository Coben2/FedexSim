using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Buttons", menuName = "Button")]
public class Buttons : ScriptableObject
{
    public int buttonOrder;

    public bool activated = true;

    public ButtonScreen screen;
}
