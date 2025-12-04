using System;
using System.Collections;
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
    //private string CurrentRoutine;
    private string MergedInstruction = "Instruction";


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

    Dictionary<string, Func<IEnumerator>> AllRoutines = new Dictionary<string, Func<IEnumerator>>();
    int i;

    private void Start()
    {
        i = 0;
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

        AllRoutines.Add("Instruction0", () => MoveCheck());
        AllRoutines.Add("Instruction1", () => HouseLook());
        AllRoutines.Add("Instruction2", () => EconomyLook());
        AllRoutines.Add("Instruction3", () => EconomyLook2());
        AllRoutines.Add("Instruction4", () => BuildFL());
        AllRoutines.Add("Instruction5", () => UnPause());
        AllRoutines.Add("Instruction6", () => Disaster1());
        AllRoutines.Add("Instruction7", () => Disaster2());
        AllRoutines.Add("Instruction8", () => BuildSS());
        AllRoutines.Add("Instruction9", () => ActualBuildSS());
        AllRoutines.Add("Instruction10", () => DisasterOccurs());
        AllRoutines.Add("Instruction11", () => NotInTime());
        AllRoutines.Add("Instruction12", () => TutorialDone());
    }

    private void Update()
    {
        if (!IsCoroutineRunning && !StepDone)
        {
            MergedInstruction = "Instruction" + TutorialStep;
            Debug.Log(MergedInstruction);
            CurrentInstruction = Instructions[MergedInstruction];
            Debug.Log(CurrentInstruction);
            //CurrentRoutine = AllRoutines[MergedInstruction]();
            StartCoroutine(AllRoutines[MergedInstruction]());
            IsCoroutineRunning = true;
        }
        else if (StepDone)
        {
            StartNext();
        }
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
       StopAllCoroutines();
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

    private IEnumerator MoveCheck()
    {
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (CameraMoved)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
    }
    private IEnumerator HouseLook()
    {
        //pause needed here
        //this.Invoke("ShowScreen", 3f);
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (HouseScreen1||HouseScreen2)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator EconomyLook()
    {
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator EconomyLook2()
    {
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator BuildFL()
    {
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (FarmB && LumberyardB)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator UnPause()
    {
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (PauseScript.isTimePaused == false)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator Disaster1()
    {
        //Make newpaper pop up in this one
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator Disaster2()
    {
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator BuildSS()
    {
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            //I gotta also make the divergent "Not enough stone!" and such here
            if (ResourceManager.Instance.Get(TypeStone) > 60 && ResourceManager.Instance.Get(TypePesos) > 40)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator ActualBuildSS()
    {
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (StormShelterB)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator DisasterOccurs()
    {
        ShowScreen();
        //Do disaster stuff here
        //DisasterManager.TriggerDisaster(disaster);
        TutorialManager.OnTutorialProgressed();
        yield return null;
    }
    private IEnumerator NotInTime()
    {
        //pause here
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator TutorialDone()
    {
        ShowScreen();
        i = 0;
        while (!StepDone && CurrentInstruction && i < 1)
        {
            if (ManualStep)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
                i++;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    
}
