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

    void ApplyredOnTag()
    {
        GameObject tagRenderers = GameObject.FindWithTag("OutlineBlack");
        SpriteRenderer spriteRenderer = tagRenderers.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }
    private void OnMouseEnter(){
        //ApplyRedToChildren();
        ApplyredOnTag();
    }

    private void OnMouseExit(){
        ApplyBlackToChildren();
    }
}
