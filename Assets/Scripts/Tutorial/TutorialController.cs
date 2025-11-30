using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private GameObject CurrentObject;
    
    private void OnEnable()
    {
        TutorialManager.TutorialProgressed += TutorialManagerOnTutorialProgressed;
        CurrentObject = GameObject.Find("Instruction");
    }

    private void OnDisable()
    {
        TutorialManager.TutorialProgressed -= TutorialManagerOnTutorialProgressed;
    }

    private void TutorialManagerOnTutorialProgressed()
    {
        CurrentObject.SetActive(false);
    }
}
