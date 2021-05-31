using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject PanelShow;


    public void SetSpawn(GameObject obj)
    {
        FindObjectOfType<ProgrammManager>().ObjectToSpawn = obj;
        PanelShow.SetActive(false);
    }

    public void NextScene(string name)
    {
        SceneManager.LoadScene(name);
    }

  
}
