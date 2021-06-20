using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GenerateStatistics : MonoBehaviour
{
    public GameObject prefab;
    public DataBaseRequests dataBase;
    void Start()
    {
        generateContent();
    }
    public void generateContent()
    {
        deleteComponents(this.gameObject);
        if(dataBase.statistics_json != "none")
        {
        jsonList asset = JsonUtility.FromJson<jsonList>(dataBase.statistics_json);
        List<jsonStatistics> assets = new List<jsonStatistics>();
        for(int i=0;i<asset.lists.Length;i++)
            assets.Add(asset.lists[i]);
        
        GameObject mainobj;
        
        for(int i=0;i<assets.Count;i++)
        {
            mainobj = Instantiate(prefab,transform);
            mainobj.GetComponentsInChildren<TextMeshProUGUI>()[0].text =assets[i].nameCourse+"."+assets[i].nameMain;
            mainobj.GetComponentsInChildren<TextMeshProUGUI>()[1].text =assets[i].first_mark;
            if(assets[i].another_mark == null)
                mainobj.GetComponentsInChildren<TextMeshProUGUI>()[2].text ="-";
            else
                mainobj.GetComponentsInChildren<TextMeshProUGUI>()[2].text =assets[i].another_mark;
            
            string[] marks = assets[i].first_mark.Split('/');
            float procent = (float)float.Parse(marks[0])/float.Parse(marks[1]); 
            print(assets[i].another_mark);
            if(assets[i].another_mark != null && assets[i].another_mark !="")
            {
                marks = assets[i].another_mark.Split('/');
                //print(marks[0]+marks[1]);
                if(procent < (float)float.Parse(marks[0])/float.Parse(marks[1]))
                    procent = (float)float.Parse(marks[0])/float.Parse(marks[1]);
            }
            if(procent>=0.6f) {
            mainobj.GetComponentsInChildren<Image>()[1].GetComponent<Image>().color = Color.green;
            mainobj.GetComponentsInChildren<Image>()[1].GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
            //texts[1].text = "Сдал";
            mainobj.GetComponentsInChildren<Image>()[1].GetComponentInChildren<TextMeshProUGUI>().text = "Склав";
            }else{
            mainobj.GetComponentsInChildren<Image>()[1].GetComponent<Image>().color = Color.red;
            mainobj.GetComponentsInChildren<Image>()[1].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            //texts[1].text= "Не сдал";
            mainobj.GetComponentsInChildren<Image>()[1].GetComponentInChildren<TextMeshProUGUI>().text = "Не склав";
            }

            //mainobj.GetComponentsInChildren<Image>()[0].color = Utiliity.colors[Random.Range(0,Utiliity.colors.Length)];
            mainobj.name = "StatItem"+(i+1);
            //jsonQuizs quiz = assets[i];
            //mainobj.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => addLesson(quiz));
        }
        }
    }
    public static void deleteComponents(GameObject gmj)
    {
        
        for(int i=gmj.transform.childCount-1;i>=0;i--)
        {
            Destroy(gmj.transform.GetChild(i).gameObject);
        }
    }
    [System.Serializable]
    public class jsonList
    {
        public jsonStatistics[] lists;
    }
    [System.Serializable]
    public class jsonStatistics{
        public string nameMain;
        public string nameCourse;
        public string first_mark;
        public string another_mark;
    }
}
