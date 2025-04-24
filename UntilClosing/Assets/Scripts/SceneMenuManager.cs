using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenuManager : MonoBehaviour
{
    //really not much to comment on here.
    public void LoadSceneOne()
    {
        SceneManager.LoadScene("SceneOne"); 
    }

    
    public void LoadSceneTwo()
    {
        SceneManager.LoadScene("SceneTwo"); 
    }

    
    public void LoadSceneThree()
    {
        SceneManager.LoadScene("SceneThree"); 
    }
}
