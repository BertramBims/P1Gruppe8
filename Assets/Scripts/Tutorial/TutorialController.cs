using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject MeNow;

    private void OnEnable()
    {
        TutorialManager.TutorialProgressed += TutorialManagerOnTutorialProgressed;
    }

    private void OnDisable()
    {
        TutorialManager.TutorialProgressed -= TutorialManagerOnTutorialProgressed;
    }

    private void TutorialManagerOnTutorialProgressed()
    {
        MeNow.SetActive(false);
    }
}
