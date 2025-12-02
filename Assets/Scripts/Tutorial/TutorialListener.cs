using System;
using Microsoft.Unity.VisualStudio.Editor;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialListener : MonoBehaviour
{
    public int TutorialStep;
    private bool StepDone;

    private GameObject InstructionParent;
    private GameObject CurrentInstruction;
    private bool IsCoroutineRunning;
    private Array AllInstructions;
    public GameObject HouseScreen1;
    public GameObject HouseScreen2;
    public GameObject FarmB;
    public GameObject LumberyardB;

    //dumb list, but it works
    public GameObject Instruction0;
    public GameObject Instruction1;
    public GameObject Instruction2;
    public GameObject Instruction3;
    public GameObject Instruction4;
    public GameObject Instruction5;
    public GameObject Instruction6;
    public GameObject Instruction7;
    public GameObject Instruction8;
    public GameObject Instruction9;

    private void Start()
    {
        TutorialManager.TutorialProgressed += StepComplete;
        StepDone = false;
        InstructionParent = GameObject.Find("TipScreen");
        IsCoroutineRunning = false;
        //Find way to unpause then pause automatically
    }

    private void StepComplete()
    {
       TutorialStep ++;
    }

    private void Update()
    {
        if (!StepDone)
        {
                if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction1;
                    StartCoroutine("MoveCheck");   
                    IsCoroutineRunning = true;
                }
            }
            else if (StepDone)
            {
                StopCoroutine("MoveCheck");
                IsCoroutineRunning = false;
                TutorialManager.OnTutorialProgressed();
                StepDone = false;
            }  
    }

    private void Update()
    {
        if (TutorialStep == 0)
        {
            if (!StepDone)
            {
                if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction1;
                    StartCoroutine("MoveCheck");   
                    IsCoroutineRunning = true;
                }
            }
            else if (StepDone)
            {
                StopCoroutine("MoveCheck");
                IsCoroutineRunning = false;
                TutorialManager.OnTutorialProgressed();
                StepDone = false;
            }
        }
        if (TutorialStep == 1)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction1;
                    StartCoroutine("HouseLook");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("HouseLook");
                IsCoroutineRunning = false;
                TutorialManager.OnTutorialProgressed();
                StepDone = false;
            }
        }
        if (TutorialStep == 2)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction2;
                    StartCoroutine("EconomyLook");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("EconomyLook");
                IsCoroutineRunning = false;
                TutorialManager.OnTutorialProgressed();
                StepDone = false;
            }
        }
        if (TutorialStep == 3)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction3;
                    StartCoroutine("EconomyLook2");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("EconomyLook2");
                IsCoroutineRunning = false;
                TutorialManager.OnTutorialProgressed();
                StepDone = false;
            }
        }
        if (TutorialStep == 4)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction4;
                    StartCoroutine("BuildFL");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("BuildFL");
                IsCoroutineRunning = false;
                TutorialManager.OnTutorialProgressed();
                StepDone = false;
            }
        }
        if (TutorialStep == 5)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction5;
                    StartCoroutine("UnPause");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("UnPause");
                IsCoroutineRunning = false;
                TutorialManager.OnTutorialProgressed();
                StepDone = false;
            }
        }
        if (TutorialStep == 6)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction6;
                    StartCoroutine("Disaster1");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("Disaster1");
                IsCoroutineRunning = false;
                TutorialManager.OnTutorialProgressed();
                StepDone = false;
            }
        }
    }

    private void MoveCheck()
    {
        while (!StepDone)
        {
            if (Input.GetKey("W")||Input.GetKey("A")||Input.GetKey("S")||Input.GetKey("D"))
            {
                StepDone = true;
            }
        }
    }
    private void HouseLook()
    {
        //Need pause here
        CurrentInstruction.SetActive(true);
        while (!StepDone)
        {
            if (HouseScreen1||HouseScreen2)
            {
                StepDone = true;
            }
        }
    }
    private void EconomyLook()
    {
        CurrentInstruction.SetActive(true);
        //pause here
        StepDone = true;
    }
    private void EconomyLook2()
    {
        CurrentInstruction.SetActive(true);
        //pause here
        StepDone = true;
    }
    private void BuildFL()
    {
        //Need pause here
        CurrentInstruction.SetActive(true);
        while (!StepDone)
        {
            if (FarmB && LumberyardB)
            {
                StepDone = true;
            }
        }
    }
    private void UnPause()
    {
        CurrentInstruction.SetActive(true);
        while (!StepDone)
        {
            //pause here
            StepDone = true;
        }
    }
    private void Disaster1()
    {
        //Make newpaper pop up in this one
        CurrentInstruction.SetActive(true);
        while (!StepDone)
        {
            //pause here
            StepDone = true;
        }
    }
    private void Disaster2()
    {
        //
        CurrentInstruction.SetActive(true);
        while (!StepDone)
        {
            //pause here
            StepDone = true;
        }
    }
    
}
