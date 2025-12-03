using System;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Mono.Cecil.Cil;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialListener : MonoBehaviour
{
    public int TutorialStep;
    private bool StepDone;
    private bool ManualStep;
    private bool CameraMoved;
    public ResourceType TypeStone;
    public ResourceType TypePesos;    

    private GameObject InstructionParent;
    private GameObject CurrentInstruction;
    private bool IsCoroutineRunning;
    private Array AllInstructions;
    public GameObject HouseScreen1;
    public GameObject HouseScreen2;
    public GameObject FarmB;
    public GameObject LumberyardB;
    public GameObject StormShelterB;

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
    public GameObject Instruction10;
    public GameObject Instruction11;
    public GameObject Instruction12;

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
       StepDone = true;
    }
    private void StartNext()
    {
       ManualStep = false;
       StepDone = false;
       IsCoroutineRunning = false;
    }
    private void ShowScreen()
    {
        CurrentInstruction.SetActive(true);
    }
    public void ManualProgress()
    {
        ManualStep = true;
    }
    public void MovedCamera()
    {
        CameraMoved = true;
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
                StartNext();
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
                StartNext();
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
                StartNext();
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
                StartNext();
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
                StartNext();
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
                StartNext();
            }
        }
        if (TutorialStep == 7)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction7;
                    StartCoroutine("Disaster2");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("Disaster2");
                StartNext();
            }
        }
        if (TutorialStep == 8)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction8;
                    StartCoroutine("BuildSS");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("BuildSS");
                StartNext();
            }
        }
        if (TutorialStep == 9)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction8;
                    StartCoroutine("ActualBuildSS");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("ActualBuildSS");
                StartNext();
            }
        }
        if (TutorialStep == 10)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction8;
                    StartCoroutine("DisasterOccurs");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("DisasterOccurs");
                StartNext();
            }
        }
        if (TutorialStep == 11)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction8;
                    StartCoroutine("NotInTime");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("NotInTime");
                StartNext();
            }
        }
        if (TutorialStep == 12)
        {
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
                    CurrentInstruction = Instruction8;
                    StartCoroutine("TutorialDone");  
                    IsCoroutineRunning = true; 
                }
            }
            else if (StepDone)
            {
                StopCoroutine("TutorialDone");
                StartNext();
            }
        }
    }

    private void MoveCheck()
    {
        ShowScreen();
        while (!StepDone)
        {
            if (CameraMoved)
            {
                TutorialManager.OnTutorialProgressed();
            }
        }
    }
    private void HouseLook()
    {
        ShowScreen();
        while (!StepDone && CurrentInstruction)
        {
            if (HouseScreen1||HouseScreen2)
            {
                TutorialManager.OnTutorialProgressed();
            }
        }
    }
    private void EconomyLook()
    {
        //pause here
        ShowScreen();
        TutorialManager.OnTutorialProgressed();
    }
    private void EconomyLook2()
    {
        //pause here
        ShowScreen();
        TutorialManager.OnTutorialProgressed();
    }
    private void BuildFL()
    {
        //Need pause here
        ShowScreen();
        while (!StepDone)
        {
            if (FarmB && LumberyardB)
            {
                TutorialManager.OnTutorialProgressed();
            }
        }
    }
    private void UnPause()
    {
        ShowScreen();
        while (!StepDone)
        {
            //pause here
            TutorialManager.OnTutorialProgressed();
        }
    }
    private void Disaster1()
    {
        //Make newpaper pop up in this one
        ShowScreen();
        while (!StepDone)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
            }
        }
    }
    private void Disaster2()
    {
        ShowScreen();
        while (!StepDone)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
            }
        }
    }
    private void BuildSS()
    {
        ShowScreen();
        while (!StepDone)
        {
            //I gotta also make the divergent "Not enough stone!" and such here
            if (ResourceManager.Instance.Get(TypeStone) > 60 && ResourceManager.Instance.Get(TypePesos) > 40)
            {
                TutorialManager.OnTutorialProgressed();
            }
        }
    }
    private void ActualBuildSS()
    {
        ShowScreen();
        while (!StepDone)
        {
            if (StormShelterB)
            {
                TutorialManager.OnTutorialProgressed();
            }
        }
    }
    private void DisasterOccurs()
    {
        ShowScreen();
        //Do disaster stuff here


        TutorialManager.OnTutorialProgressed();
    }
    private void NotInTime()
    {
        //pause here
        ShowScreen();
        while (!StepDone)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
            }
        }
    }
    private void TutorialDone()
    {
        ShowScreen();
        while (!StepDone)
        {
            if (ManualStep)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
            }
        }
    }
    
}
