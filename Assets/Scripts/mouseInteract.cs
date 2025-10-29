using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class mouseInteract : MonoBehaviour
{
   SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
 void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
   
    private void OnMouseEnter(){

        sprite.color = new Color (1, 0, 0, 1); 
    }

    private void OnMouseExit(){
        sprite.color = new Color (255,255,255,1); 
    }
    private void OnMouseDown(){
        Debug.Log("woof");
    }
}
