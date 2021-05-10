using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonseManager : MonoBehaviour
{
    public static MonseManager Instance;
    public Texture2D point, doorway, attack, target, arrow;
    private void Awake() {
        if(Instance != null)
         Destroy(gameObject);
        Instance = this;
    }
    RaycastHit hitInfo;
    public event Action<Vector3> onMouseCliscked;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetCursorTexture(); 
        MouseControl();
    }
    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target,new Vector2 (16,16),CursorMode.Auto);
                    break;
            }
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
