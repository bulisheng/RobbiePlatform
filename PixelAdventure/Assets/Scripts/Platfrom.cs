using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platfrom : MonoBehaviour
{
    public Vector3 movement;
    GameObject topLine;
    // Start is called before the first frame update
    void Start()
    {
        topLine = GameObject.Find("TopLine");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlaform();
    }
    void  MovePlaform()
    {
        transform.position += movement * Time.deltaTime;
        if (transform.position.y>=topLine.transform.position.y)
        {
            Destroy(gameObject
                );
        }
    }
}
