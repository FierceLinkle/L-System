using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

public class LSystem : MonoBehaviour
{
    //Game objects
    public GameObject TreeBase;
    public GameObject branch;
    public GameObject leaf;
    public HUD HUD;

    public GameObject NewTree = null;

    //Axiom variables for different rulesets
    private const string axiomE = "F"; //axiom for edge-rewriting L-systems
    private const string axiomN = "X"; //axiom for Node-rewriting L-systems
    private const string axiomA = "a"; //axiom for L-Systems with new rules
    private const string axiomaF = "aF"; //axiom for 3D L-System
    //private string currentAxiom;

    //L-System information
    private Stack<TransformInfo> transformStack;
    public Dictionary<char, string> rules;
    private Dictionary<char, string> currentRules;

    public string currentString;
    private Vector3 initialPosition;

    //L-System Paremeters
    public float length = 0.5f;
    public float lengthScaleFactor = 1.36f;
    public float angle = 25.7f;
    public int iterations = 1;
    public float width = 0.5f;

    public float randomLevel = 10;
    private float[] randomValues = new float[50];

    //Input L-System variables
    public string PrimaryAxiom = "F";
    public string PrimaryRules = "F[+F]F[-F]F" ;
    public string SecondaryRules;

    public int DefaultTree = 0;
     
    // Start is called before the first frame update
    void Start()
    {
        //Tree A is called at the start as the default tree

        transformStack = new Stack<TransformInfo>();

        //Tree A ruleset
        rules = new Dictionary<char, string>
        {
            {'F', "F[+F]F[-F]F" }
        };

        HUD.fKey = 'F';
        currentString = axiomE;
        //FAxiom = true;

        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        //Changes when user selects a different tree from the presets
        switch (DefaultTree)
        {
            case 0:
                if (HUD.hasDefaultPressed)
                {
                    TreeOne();
                    HUD.hasDefaultPressed = false;
                }               
                break;
            case 1:
                if (HUD.hasDefaultPressed)
                {
                    TreeTwo();
                    HUD.hasDefaultPressed = false;
                }
                break;
            case 2:
                if(HUD.hasDefaultPressed)
                {
                    TreeThree();
                    HUD.hasDefaultPressed = false;
                }
                break;
            case 3:
                if (HUD.hasDefaultPressed)
                {
                    TreeFour();
                    HUD.hasDefaultPressed = false;
                }
                break;
            case 4:
                if (HUD.hasDefaultPressed)
                {
                    TreeFive();
                    HUD.hasDefaultPressed = false;
                }
                break;
            case 5:
                if (HUD.hasDefaultPressed)
                {
                    TreeSix();
                    HUD.hasDefaultPressed = false;
                }
                break;
            case 6:
                if (HUD.hasDefaultPressed)
                {
                    TreeSeven();
                    HUD.hasDefaultPressed = false;
                }
                break;
            case 7:
                if (HUD.hasDefaultPressed)
                {
                    TreeEight();
                    HUD.hasDefaultPressed = false;
                }
                break;
        }

        //Will change the paramters of the current tree based on user input
        if(HUD.hasGeneratePressed)
        {
            HUD.hasGeneratePressed = false;
            GenerateTree();
            
        }
        //Will spawn a new tree based on user input
        if(HUD.hasRulesPressed)
        {
            HUD.hasRulesPressed = false;
            GenerateNewRules();
        }
    }


    private void Generate()
    {
        //Each generation of a tree will differ slightly
        CreateRandomValueSet();

        //Reset position and orientation
        transform.position = new Vector3(50, 10, 0);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //Destroys previous tree
        Destroy(NewTree);

        //Creates a new tree
        NewTree = Instantiate(TreeBase);

        StringBuilder sb = new StringBuilder();

        //Create the string that will be used to build the tree
        for (int i = 0; i < iterations; i++)
        {
            foreach (char c in currentString)
            {
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }

            currentString = sb.ToString();
            sb = new StringBuilder();
        }


        //Builds the tree
        for (int i = 0; i < currentString.Length; i++)
        {
                switch (currentString[i])
                {
                    case 'F':
                        initialPosition = transform.position;
                        transform.Translate(Vector3.up * 2 * length);

                        // Code from: https://www.youtube.com/watch?v=tUbTGWl-qus
                        GameObject fLine = currentString[(i + 1) % currentString.Length] == 'X' || currentString[(i + 3) % currentString.Length] == 'F' && currentString[(i + 4) % currentString.Length] == 'X' ? Instantiate(leaf) : Instantiate(branch);
                        
                        fLine.transform.SetParent(NewTree.transform);
                        fLine.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                        fLine.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                        branch.GetComponent<LineRenderer>().startWidth = width;
                        branch.GetComponent<LineRenderer>().endWidth = width;
                        leaf.GetComponent<LineRenderer>().startWidth = width;
                        //leaf end width will always stay at 0 to keep shape
                    break;

                    case 'X':

                        break;

                    case '+':

                        transform.Rotate(Vector3.forward * angle * (1 + randomLevel / 100 * randomValues[i % randomValues.Length]));

                        break;

                    case '-':

                        transform.Rotate(Vector3.back * angle * (1 + randomLevel / 100 * randomValues[i % randomValues.Length]));

                        break;                 

                    case '>':

                        length = length * lengthScaleFactor;

                        break;

                    case '<':

                        length = length / lengthScaleFactor;

                        break;

                    case 'a':

                        break;

                    case 'b':

                        break;

                    case 'x':

                        break;

                    case 'y':

                        break;            

                    case '[':

                        transformStack.Push(new TransformInfo()
                        {
                            position = transform.position,
                            rotation = transform.rotation
                        });

                        break;

                    case ']':
                        TransformInfo ti = transformStack.Pop();
                        transform.position = ti.position;
                        transform.rotation = ti.rotation;
                        break;
                }
        }
    }

    //Tree A
    private void TreeOne()
    {
        angle = 25.7f;
        iterations = 5;   
        length = 0.5f;
        width = 0.5f;

        
        rules = new Dictionary<char, string>
        {
            { 'F', "F[+F]F[-F]F" }
        };
        

        HUD.pAxiom = "F";
        currentString = axiomE;
        Generate();
    }

    //Tree B
    private void TreeTwo()
    {
        angle = 20f;
        iterations = 5;
        length = 0.5f;
        width = 0.5f;

        rules = new Dictionary<char, string>
        {
            { 'F', "F[+F]F[-F][F]" }
        };

        HUD.pAxiom = "F";
        currentString = axiomE;
        Generate();      
    }

    //Tree C
    private void TreeThree()
    {
        angle = 22.5f;
        iterations = 4;
        length = 0.5f;
        width = 0.5f;

        rules = new Dictionary<char, string>
        {
            { 'F', "FF-[-F+F+F]+[+F-F-F]" }
        };

        HUD.pAxiom = "F";
        currentString = axiomE;
        Generate();
    }

    //Tree D
    private void TreeFour()
    {
        angle = 20f;
        iterations = 7;
        length = 0.5f;
        width = 0.5f;

        rules = new Dictionary<char, string>
        {
            { 'X', "F[+X]F[-X]+X" },
            { 'F', "FF" }
        };

        HUD.pAxiom = "X";
        currentString = axiomN;
        Generate();
    }

    //Tree E
    private void TreeFive()
    {
        angle = 25.7f;
        iterations = 5;
        length = 0.5f;
        width = 0.5f;

        rules = new Dictionary<char, string>
        {
            { 'X', "F[+X][-X]FX" },
            { 'F', "FF" }
        };

        HUD.pAxiom = "X";
        currentString = axiomN;
        Generate();
    }

    //Tree F
    private void TreeSix()
    {
        angle = 22.5f;
        iterations = 5;
        length = 0.5f;
        width = 0.5f;

        rules = new Dictionary<char, string>
        {
            { 'X', "F-[[X]+X]+F[+FX]-X" },
            { 'F', "FF" }
        };

        HUD.pAxiom = "X";
        currentString = axiomN;
        Generate();
    }

    //Tree G (from: http://paulbourke.net/fractals/lsys/)
    private void TreeSeven()
    {
        angle = 45;
        iterations = 15;
        length = 0.2f;
        width = 0.5f;

        rules = new Dictionary<char, string>
        {
            { 'F', ">F<" },
            { 'a', "F[+x]Fb" },
            { 'b', "F[-y]Fa" },
            { 'x', "a" },
            { 'y', "b" }
        };

        HUD.pAxiom = "a";
        currentString = axiomA;
        Generate();
    }

    //Tree H (from: http://paulbourke.net/fractals/lsys/)
    private void TreeEight()
    {
        angle = 12;
        iterations = 7;
        length = 1f;
        width = 0.5f;

        rules = new Dictionary<char, string>
        {
            { 'a', "FFFFFv[+++h][---q]fb" },
            { 'b', "FFFFFv[+++h][---q]fc" },
            { 'c', "FFFFFv[+++fa]fd" },
            { 'd', "FFFFFv[+++h][---q]fe" },
            { 'e', "FFFFFv[+++h][---q]fg" },
            { 'g', "FFFFFv[---fa]fa" },
            { 'h', "ifFF" },
            { 'i', "fFFF[--m]j" },
            { 'j', "ffFFF[--n]k" },
            { 'k', "fFFF[--o]l" },
            { 'l', "fFFF[--p]" },
            { 'm', "fFn" },
            { 'n', "fFo" },
            { 'o', "fFp" },
            { 'p', "fF" },
            { 'q', "rfF" },
            { 'r', "fFFF[++m]s" },
            { 's', "fFFF[++n]t" },
            { 't', "fFFF[++o]u" },
            { 'u', "fFFF[++p]" },
            { 'v', "Fv" }
        };

        HUD.pAxiom = "aF";
        currentString = axiomaF;
        Generate();

        //Generate();
    }

    //Changes the parameters of the current tree
    private void GenerateTree()
    {
        
        //First two conditions are for the presets
        if(HUD.pAxiom == "F")
        {
            currentString = axiomE;
        }
        else if(HUD.pAxiom == "X")
        {
            currentString = axiomN;
        }
        else
        {
            currentString = HUD.pAxiom;
        }  

        print(currentString);

        Generate();
    }

    //Generates the user inputed tree
    private void GenerateNewRules()
    {
        iterations = 5;

        print(currentString);

        Generate();
    }

    //Create the random values for each tree generation
    private void CreateRandomValueSet()
    {
        for (int i = 0; i < randomValues.Length; i++)
        {
            randomValues[i] = UnityEngine.Random.Range(-1f, 1f);
        }
    }
  
}

