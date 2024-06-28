using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    
    public void onClick_StartBtn()
    {
        SceneManager.LoadScene("SelectCharScene");
    }
}
