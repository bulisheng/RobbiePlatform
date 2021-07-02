using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using  UnityEngine.UI;
public class Login : MonoBehaviour
{
    public InputField m_ID;

    public InputField m_Password;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnButton()
    {
        if (m_ID.text=="111"&&m_Password.text=="chi")
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
