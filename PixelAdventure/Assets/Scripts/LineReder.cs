using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineReder : MonoBehaviour
{
    LineRenderer line;
    public Transform start;
    public Transform end;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0,start.position);
        line.SetPosition(1, end.position);
    }
}
