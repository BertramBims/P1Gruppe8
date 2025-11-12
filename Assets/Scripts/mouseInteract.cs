using System;
using System.Linq;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class mouseInteract : MonoBehaviour
{
    void ApplyRedToChildren(){
        Transform tagRenderers = transform.Find("outline");
        SpriteRenderer spriteRenderer = tagRenderers.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
    }
    void ApplyBlackToChildren()
    {
        Transform tagRenderers = transform.Find("outline");
        SpriteRenderer spriteRenderer = tagRenderers.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.black;
    }
    private void OnMouseEnter(){
        ApplyRedToChildren();

    }
    private void OnMouseExit(){
        ApplyBlackToChildren();
    }
}
