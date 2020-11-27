using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //Game objects
    public LSystem Tree;

    public GameObject text;

    public Text angleDisplay;
    public Text lengthDisplay;
    public Text randomDisplay;
    public Text widthDisplay;

    //Check of options have been selected in the GUI
    public bool hasGeneratePressed = false;
    public bool hasRulesPressed = false;
    public bool hasDefaultPressed = false;

    //Variables for user input of their chosen ruleset 
    public string pAxiom;
    public char fKey = 'F';
    public char xKey;
    private string fRules = "F[+F]F[-F]F";
    private string xRules;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Steps through iterations of current tree with a limit
    public void IterationUp()
    {
        if (Tree.iterations < 15)
        {
            Tree.iterations++;
        }
    }

    public void IterationDown()
    {
        if(Tree.iterations > 0)
        {
            Tree.iterations--;
        }
    }

    //Changes paremeters of current tree based on user input
    public void AngleChange(float angle)
    {
        Tree.angle = angle;
    }

    public void Length(float length)
    {
        Tree.length = length;
    }

    public void RandomChange(float random)
    {
        Tree.randomLevel = random;
    }

    public void WidthChange(float width)
    {
        Tree.width = width;
    }

    //Takes L-System ruleset defined by user
    public void NewAxiom(string axiom)
    {
        pAxiom = axiom;
    }

    public void PrimaryRules(string pRules)
    {
        fRules = pRules;
    }

    public void SecondaryRules(string sRules)
    {
        xRules = sRules;
    }

    public void FKeyRule(string fKeys)
    {
        string f = fKeys;
        fKey = Convert.ToChar(f);
    }
    public void XKeyRule(string xKeys)
    {
        string x = xKeys;
        xKey = Convert.ToChar(x);
    }

    //Checks for if the preset trees have been selected
    public void DefaultTrees(int option)
    {
        if (option == 0)
        {
            Debug.Log("Tree A");
            Tree.DefaultTree = 0;
            hasDefaultPressed = true;
        }
        if (option == 1)
        {
            Debug.Log("Tree B");
            Tree.DefaultTree = 1;
            hasDefaultPressed = true;
        }
        if (option == 2)
        {
            Debug.Log("Tree C");
            Tree.DefaultTree = 2;
            hasDefaultPressed = true;
        }
        if (option == 3)
        {
            Debug.Log("Tree D");
            Tree.DefaultTree = 3;
            hasDefaultPressed = true;
        }
        if (option == 4)
        {
            Debug.Log("Tree E");
            Tree.DefaultTree = 4;
            hasDefaultPressed = true;
        }
        if (option == 5)
        {
            Debug.Log("Tree F");
            Tree.DefaultTree = 5;
            hasDefaultPressed = true;
        }
        if (option == 6)
        {
            Debug.Log("Tree G");
            Tree.DefaultTree = 6;
            hasDefaultPressed = true;
        }
        if (option == 7)
        {
            Debug.Log("Tree H");
            Tree.DefaultTree = 7;
            hasDefaultPressed = true;
        }
    }

    //Checks for if the buttons have been pressed
    public void GeneratePressed()
    {
        hasGeneratePressed = true;
    }

    public void GenerateRulesPressed()
    {
        hasRulesPressed = true;

        Tree.currentString = pAxiom;

        Tree.rules = new Dictionary<char, string>
        {
            { fKey, fRules },
            { xKey, xRules }
        };
    }

    //UI Elements for user information
    void OnGUI()
    {
        angleDisplay.text = Convert.ToString(Tree.angle);
        lengthDisplay.text = Convert.ToString(Tree.length);
        randomDisplay.text = Convert.ToString(Tree.randomLevel);
        widthDisplay.text = Convert.ToString(Tree.width);

        if (fKey == xKey)
        {
            text.SetActive(true);
        }
    }
}
