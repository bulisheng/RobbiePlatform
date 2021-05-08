using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

public class MonseManager : MonoBehaviour
{
    RaycastHit hitInfo;
    public EventVector3 onMouseCliscked;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetCursorTexture();
    }
    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            MouseControl();
        }
    }
    void MouseControl()
    {
        if (Input.GetMouseButton(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
                onMouseCliscked?.Invoke(hitInfo.point);
        }
    }
}
