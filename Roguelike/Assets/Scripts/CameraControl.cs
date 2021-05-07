using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    public Transform target;
    public float speed;
    public GameObject player;
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), speed * Time.deltaTime);
        }

    }
    public void ChangerTarget(Transform newTarget)
    {
        target = newTarget;
        player.transform.position = new Vector3(target.position.x, target.position.y, player.transform.position.z);
    }
}
