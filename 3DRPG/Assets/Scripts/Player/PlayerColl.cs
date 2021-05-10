using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerColl : MonoBehaviour
{
    NavMeshAgent nav;
    Animator animator;
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MonseManager.Instance.onMouseCliscked += MoveToTarget;
    }
    // Update is called once per frame
    void Update()
    {
        SetAnimator();
    }
    public void MoveToTarget(Vector3 vector)
    {
        nav.destination = vector;
    }
    public void SetAnimator()
    {
        animator.SetFloat("Speed", nav.velocity.sqrMagnitude);
    }
}
