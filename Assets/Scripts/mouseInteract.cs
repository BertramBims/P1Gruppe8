using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class mouseInteract : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void ApplyRedToChildren(){
        SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer r in childRenderers)
        {
            r.color = new Color (255,0,0,255);
        }
    }
    void ApplyBlackToChildren()
    {
        SpriteRenderer[] childRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer i in childRenderers)
        {
            i.color = new Color(0, 0, 0, 255);
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
