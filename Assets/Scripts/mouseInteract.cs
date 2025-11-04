using System;
using System.Linq;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class mouseInteract : MonoBehaviour
{

    void ApplyRedToChildren(){
        SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < childRenderers.Length; i++)
        {
            if (i == 0)
                continue;

                 childRenderers[i].color = new Color (255,0,0,255);
        }
    }
    void ApplyBlackToChildren()
    {
        SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < childRenderers.Length; i++)
        {
            if (i == 0)
                continue;

                 childRenderers[i].color = new Color (0, 0, 0, 255);
        }
    }
    private void OnMouseEnter(){
        ApplyRedToChildren();
    }

    private void OnMouseExit(){
        ApplyBlackToChildren();
    }
    private void OnMouseDown(){
        Debug.Log("woof");
    }
}
