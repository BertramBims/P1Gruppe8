using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class EscapeMenu : MonoBehaviour
{
    public GameObject pause;
    public void PressReturn()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        Debug.Log("grrrr");
    }
    public void PressEscape(InputAction.CallbackContext ctx)
    {

        GameObject[] menus = GameObject.FindGameObjectsWithTag("BuildMenu");
        
        bool anyActive = false;
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].activeInHierarchy)
            {
                anyActive = true;
                break;
            }
        }
        if(ctx.performed && anyActive)
        {
             var allMenues = GameObject.FindGameObjectsWithTag("BuildMenu");

        for (int i = 0; i < allMenues.Length; i++)
        {
            allMenues[i].gameObject.SetActive(false);
        }
            return;
      
        }
        else if(ctx.performed && !pause.activeInHierarchy)
        {
            Time.timeScale = 0;
            pause.SetActive(true);
            Debug.Log("Meow");
        }
        else if (ctx.performed && pause.activeInHierarchy)
        {
            Time.timeScale = 1;
            pause.SetActive(false);
            Debug.Log("grrrr");
        }
    }
}
