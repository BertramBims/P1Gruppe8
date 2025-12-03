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
    private TimeManager PauseScript;
    private string CurrentRoutine;
    private string MergedInstruction;


    private GameObject CurrentInstruction;
    private bool IsCoroutineRunning;
    public GameObject HouseScreen1;
    public GameObject HouseScreen2;
    public GameObject FarmB;
    public GameObject LumberyardB;
    public GameObject StormShelterB;

    Dictionary<string, GameObject> Instructions = new Dictionary<string, GameObject>();
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

    Dictionary<string, string> AllRoutines = new Dictionary<string, string>();

    private void Start()
    {
        TutorialManager.TutorialProgressed += StepComplete;
        StepDone = false;
        IsCoroutineRunning = false;
        PauseScript = GetComponent<TimeManager>();

        Instructions.Add("Instruction0", Instruction0);
        Instructions.Add("Instruction1", Instruction1);
        Instructions.Add("Instruction2", Instruction2);
        Instructions.Add("Instruction3", Instruction3);
        Instructions.Add("Instruction4", Instruction4);
        Instructions.Add("Instruction5", Instruction5);
        Instructions.Add("Instruction6", Instruction6);
        Instructions.Add("Instruction7", Instruction7);
        Instructions.Add("Instruction8", Instruction8);
        Instructions.Add("Instruction9", Instruction9);
        Instructions.Add("Instruction10", Instruction10);
        Instructions.Add("Instruction11", Instruction11);
        Instructions.Add("Instruction12", Instruction12);

        AllRoutines.Add("Instruction0", "MoveCheck");
        AllRoutines.Add("Instruction1", "HouseLook");
        AllRoutines.Add("Instruction2", "EconomyLook");
        AllRoutines.Add("Instruction3", "EconomyLook2");
        AllRoutines.Add("Instruction4", "BuildFL");
        AllRoutines.Add("Instruction5", "UnPause");
        AllRoutines.Add("Instruction6", "Disaster1");
        AllRoutines.Add("Instruction7", "Disaster2");
        AllRoutines.Add("Instruction8", "BuildSS");
        AllRoutines.Add("Instruction9", "ActualBuildSS");
        AllRoutines.Add("Instruction10", "DisasterOcurs");
        AllRoutines.Add("Instruction11", "NotInTime");
        AllRoutines.Add("Instruction12", "TutorialDone");
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
        if (!IsCoroutineRunning && !StepDone)
        {
            MergedInstruction = "Instruction" + TutorialStep;
            CurrentInstruction = Instructions[MergedInstruction];
            CurrentRoutine = AllRoutines[MergedInstruction];
        }
        if (TutorialStep == 0)
        {
            CurrentInstruction = Instruction0;
            CurrentRoutine = "MoveCheck";
            if (!StepDone && !IsCoroutineRunning)
            {
                StartCoroutine("MoveCheck");   
                IsCoroutineRunning = true;
            }
            else if (StepDone)
            {
                StopCoroutine("MoveCheck");
                StartNext();
            }
        }
        if (TutorialStep == 1)
        {
            CurrentInstruction = Instruction1;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction2;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction3;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction4;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction5;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction6;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction7;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction8;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction9;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction10;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction11;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
            CurrentInstruction = Instruction12;
            if (!StepDone)
            {
                 if (!IsCoroutineRunning)
                {
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
        this.Invoke("ShowScreen", 3f);
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
        ShowScreen();
        while (!StepDone)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
            }
        }
    }
    private void EconomyLook2()
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
    private void BuildFL()
    {
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
            if (PauseScript.isTimePaused == false)
            {
                TutorialManager.OnTutorialProgressed();
            }
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
        //DisasterManager.TriggerDisaster(disaster);
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
