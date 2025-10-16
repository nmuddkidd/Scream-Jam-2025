using System;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class ControlChangeMixup : MonoBehaviour
{
    public TMP_Text control_info;
    public static ControlChangeMixup Instance;
    string[] controls = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator DoMixUp()
    {
        UnityEngine.Debug.Log("changing controls");
        int ran = UnityEngine.Random.Range(0, 25);
        String Newkey = controls[ran];
        switch (Newkey)
        {
            case "a":
                P1.keyboardUpKey = Key.A;
                break;
            case "b":
                P1.keyboardUpKey = Key.B;
                break;
            case "c":
                P1.keyboardUpKey = Key.C;
                break;
            case "d":
                P1.keyboardUpKey = Key.D;
                break;
            case "e":
                P1.keyboardUpKey = Key.E;
                break;
            case "f":
                P1.keyboardUpKey = Key.F;
                break;
            case "g":
                P1.keyboardUpKey = Key.G;
                break;
            case "h":
                P1.keyboardUpKey = Key.H;
                break;
            case "i":
                P1.keyboardUpKey = Key.I;
                break;
            case "j":
                P1.keyboardUpKey = Key.J;
                break;
            case "k":
                P1.keyboardUpKey = Key.K;
                break;
            case "l":
                P1.keyboardUpKey = Key.L;
                break;
            case "m":
                P1.keyboardUpKey = Key.M;
                break;
            case "n":
                P1.keyboardUpKey = Key.N;
                break;
            case "o":
                P1.keyboardUpKey = Key.O;
                break;
            case "p":
                P1.keyboardUpKey = Key.P;
                break;
            case "q":
                P1.keyboardUpKey = Key.Q;
                break;
            case "r":
                P1.keyboardUpKey = Key.R;
                break;
            case "s":
                P1.keyboardUpKey = Key.S;
                break;
            case "t":
                P1.keyboardUpKey = Key.T;
                break;
            case "u":
                P1.keyboardUpKey = Key.U;
                break;
            case "v":
                P1.keyboardUpKey = Key.V;
                break;
            case "w":
                P1.keyboardUpKey = Key.W;
                break;
            case "x":
                P1.keyboardUpKey = Key.X;
                break;
            case "y":
                P1.keyboardUpKey = Key.Y;
                break;
            case "z":
                P1.keyboardUpKey = Key.Z;
                break;
        }
        int ran2 = UnityEngine.Random.Range(0, 25);
        String Newkeydown = controls[ran2];
        switch (Newkeydown)
        {
            case "a":
                P1.keyboardDownKey = Key.A;
                break;
            case "b":
                P1.keyboardDownKey = Key.B;
                break;
            case "c":
                P1.keyboardDownKey = Key.C;
                break;
            case "d":
                P1.keyboardDownKey = Key.D;
                break;
            case "e":
                P1.keyboardDownKey = Key.E;
                break;
            case "f":
                P1.keyboardDownKey = Key.F;
                break;
            case "g":
                P1.keyboardDownKey = Key.G;
                break;
            case "h":
                P1.keyboardDownKey = Key.H;
                break;
            case "i":
                P1.keyboardDownKey = Key.I;
                break;
            case "j":
                P1.keyboardDownKey = Key.J;
                break;
            case "k":
                P1.keyboardDownKey = Key.K;
                break;
            case "l":
                P1.keyboardDownKey = Key.L;
                break;
            case "m":
                P1.keyboardDownKey = Key.M;
                break;
            case "n":
                P1.keyboardDownKey = Key.N;
                break;
            case "o":
                P1.keyboardDownKey = Key.O;
                break;
            case "p":
                P1.keyboardDownKey = Key.P;
                break;
            case "q":
                P1.keyboardDownKey = Key.Q;
                break;
            case "r":
                P1.keyboardDownKey = Key.R;
                break;
            case "s":
                P1.keyboardDownKey = Key.S;
                break;
            case "t":
                P1.keyboardDownKey = Key.T;
                break;
            case "u":
                P1.keyboardDownKey = Key.U;
                break;
            case "v":
                P1.keyboardDownKey = Key.V;
                break;
            case "w":
                P1.keyboardDownKey = Key.W;
                break;
            case "x":
                P1.keyboardDownKey = Key.X;
                break;
            case "y":
                P1.keyboardDownKey = Key.Y;
                break;
            case "z":
                P1.keyboardDownKey = Key.Z;
                break;
        }
        control_info.text = "Up-" + controls[ran] + "/nDown-" + controls[ran2];
        control_info.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        control_info.gameObject.SetActive(false);


    }
}
