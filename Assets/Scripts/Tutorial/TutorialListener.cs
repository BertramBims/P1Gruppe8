using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Mono.Cecil.Cil;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TutorialListener : MonoBehaviour
{
    public int TutorialStep;
    private bool StepDone;
    private bool ManualStep;
    private bool CameraMoved;
    private bool PauseAction;
    public ResourceType TypeStone;
    public ResourceType TypePesos;    
    private TimeManager PauseScript;
    private string MergedInstruction = "Instruction";


    private GameObject CurrentInstruction;
    private bool IsCoroutineRunning;
    public GameObject HouseScreen1;
    public GameObject HouseScreen2;
    public GameObject FarmB;
    public GameObject LumberyardB;
    public GameObject StormShelterB;
    public GameObject House_BuildingPrefab1;
    public GameObject House_BuildingPrefab2;

    Dictionary<string, GameObject> Instructions = new Dictionary<string, GameObject>();
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
        ManualStep = false;
        StepDone = false;
        IsCoroutineRunning = false;
        PauseScript = GameObject.Find("GameManager").GetComponent<TimeManager>();
        CameraMoved = false;
        PauseAction = false;
        House_BuildingPrefab1.SetActive(true);
        House_BuildingPrefab2.SetActive(true);

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
        if (!IsCoroutineRunning && StepDone == false)
        {
            MergedInstruction = "Instruction" + TutorialStep;
            //Debug.Log(MergedInstruction);
            CurrentInstruction = Instructions[MergedInstruction];
            //Debug.Log(CurrentInstruction);
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
       TutorialStep++;
       StepDone = true;
    }
    private void StartNext()
    {
       ManualStep = false;
       StepDone = false;
       StopAllCoroutines();
       IsCoroutineRunning = false;
       i = 0;
    }
    private void ShowScreen()
    {
        CurrentInstruction.SetActive(true);
    }
    public void ManualProgress()
    {
        ManualStep = true;
    }
    public void MovedCamera(InputAction.CallbackContext ctx)
    {
        CameraMoved = true;
    }
    public void PauseTouch(InputAction.CallbackContext ctx)
    {
        PauseAction = true;
        Debug.Log("TouchPause");
    }
    private IEnumerator MoveCheck()
    {
        Debug.Log("Started");
        PauseScript.PauseTime();
        yield return new WaitForSeconds(2.5f);
        PauseScript.PauseTime();
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone)
        {
            if (CameraMoved)
            {
                TutorialManager.OnTutorialProgressed();
                Debug.Log("HalfProgress");
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator HouseLook()
    {
        Debug.Log("Progress");
        yield return new WaitForSeconds(1.5f);
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            if (HouseScreen1.activeSelf||HouseScreen2.activeSelf)
            {
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator EconomyLook()
    {
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator EconomyLook2()
    {
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator BuildFL()
    {
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            if (FarmB.activeSelf && LumberyardB.activeSelf)
            {
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator UnPause()
    {
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            Debug.Log("SearchingForPause");
            if (PauseAction)
            {
                Debug.Log("PauseFound");
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator Disaster1()
    {
        //Waits for construction to be done(If they don't pause again)
        yield return new WaitForSeconds(32.5f);
        //First newspaper pops up in this one
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator Disaster2()
    {
        //Pauses game, and instructs player
        i = 0;
        if (i == 0)
        {
            PauseScript.PauseTime();
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator BuildSS()
    {
        //Tells player to build
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            //I gotta also make the divergent "Not enough stone!" and such here
            if (ResourceManager.Instance.Get(TypeStone) > 60 && ResourceManager.Instance.Get(TypePesos) > 40)
            {
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator ActualBuildSS()
    {
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            if (StormShelterB)
            {
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator DisasterOccurs()
    {
        yield return new WaitForSeconds(22.5f);
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        //Do disaster stuff here
        //DisasterManager.TriggerDisaster(disaster);
        TutorialManager.OnTutorialProgressed();
        yield return null;
    }
    private IEnumerator NotInTime()
    {
        //pause here
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            if (ManualStep)
            {
                TutorialManager.OnTutorialProgressed();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator TutorialDone()
    {
        i = 0;
        if (i == 0)
        {
            ShowScreen();
            i = 1;
        }
        while (!StepDone && CurrentInstruction)
        {
            if (ManualStep)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    
}
