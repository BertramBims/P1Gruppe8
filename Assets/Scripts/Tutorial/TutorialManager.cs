using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public static event UnityAction TutorialProgressed;
    public static void OnTutorialProgressed() => TutorialProgressed?.Invoke();    
}
