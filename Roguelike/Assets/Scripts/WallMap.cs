using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{
    GameObject mapSpite;
    private void OnEnable()
    {
        mapSpite = transform.parent.GetChild(0).gameObject;
        mapSpite.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mapSpite.SetActive(true);
        }
    }
   
}
