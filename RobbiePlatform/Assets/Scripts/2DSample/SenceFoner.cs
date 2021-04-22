using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenceFoner : MonoBehaviour
{
    Animator animator;
    int faderID;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        faderID = Animator.StringToHash("Fade");
        GameManager.RegisterSceneFader( this);
    }
    public void FadeOut()
    {
        animator.SetTrigger(faderID);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
