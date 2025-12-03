using UnityEngine;
using UnityEngine.InputSystem;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            audioSource.Play();
    }
}
