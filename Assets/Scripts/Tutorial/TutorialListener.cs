using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class TutorialListener : MonoBehaviour
{
    public int TutorialStep;
    private bool StepDone;
  //public GameObject GameManager;
  //public TutorialManager ScriptSource;
    private GameObject InstructionParent;
    private GameObject CurrentInstruction;
    private bool IsCoroutineRunning;
    private Array AllInstructions;

    private void Start()
    {
        TutorialManager.TutorialProgressed += StepComplete;
        StepDone = false;
      //GameManager = GameObject.Find("GameManager");
      //ScriptSource = GameManager.GetComponent<TutorialManager>();
        InstructionParent = GameObject.Find("TipScreen");
        IsCoroutineRunning = false;
        AllInstructions = Transform.GetComponentsInChildren<"Image">(true);
    }

    private void StepComplete()
    {
       TutorialStep ++;
    }

    private void Update()
    {
        if (TutorialStep == 0)
        {
            if (!StepDone)
            {
                if (!IsCoroutineRunning)
                {
                    StartCoroutine("MoveCheck");   
                }
            }
            else if (StepDone)
            {
                StopCoroutine("MoveCheck");
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
                    CurrentInstruction = InstructionParent.FindChild("Instruction", TutorialStep);
                    StartCoroutine("EconomyLook");   
                }
            }
            else if (StepDone)
            {
                StopCoroutine("EconomyLook");
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
    private void EconomyLook()
    {
        CurrentInstruction.SetActive();
        while (!StepDone)
        {
            if (Input.GetKey(" W")||Input.GetKey("A")||Input.GetKey("S")||Input.GetKey("D"))
            {
                StepDone = true;
            }
        }
    }
}
