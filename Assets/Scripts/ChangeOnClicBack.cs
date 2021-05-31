using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChangeOnClicBack : MonoBehaviour
{
    public GameObject[] buttons_clicked;
    public GameObject[] panels;
    public bool isHaveTest = true;
    public int status = 0; //1,2,3
    void Start()
    {
        setObject(0);
        if(panels.Length != 0 )
            changeUI();
    }
    public void setObject(int index)
    {
        status = index;
        for(int i = 0; i<buttons_clicked.Length;i++)
        {   
            var tempColor =  buttons_clicked[i].GetComponent<Image>().color;
            if(status == i)
            {    
                if(isHaveTest)
                buttons_clicked[i].GetComponentsInChildren<TextMeshProUGUI>()[0].color = Color.black;
                tempColor.a = 1f;
            }
            else
            {    
                if(isHaveTest)
                buttons_clicked[i].GetComponentsInChildren<TextMeshProUGUI>()[0].color = Color.gray;
                tempColor.a=0f;
            }
            buttons_clicked[i].GetComponent<Image>().color = tempColor; 
        }
    }
    public void changeUI()
    {
        for(int i=0;i<buttons_clicked.Length;i++)
        {
            panels[i].SetActive(false);
        }
        panels[status].SetActive(true);
    }
}
