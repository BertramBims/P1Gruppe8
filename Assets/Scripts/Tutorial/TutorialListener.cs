using UnityEngine;

public class TutorialListener : MonoBehaviour
{
    public int TutorialStep;
    private bool StepDone;
  //public GameObject GameManager;
  //public TutorialManager ScriptSource;

    private void Start()
    {
        TutorialManager.TutorialProgressed += StepComplete;
        StepDone = false;
      //GameManager = GameObject.Find("GameManager");
      //ScriptSource = GameManager.GetComponent<TutorialManager>();
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
                StartCoroutine("MoveCheck");
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
                StartCoroutine("");
            }
            else if (StepDone)
            {
                StopCoroutine("");
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
}
