using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GenerateLessons : MonoBehaviour
{
     public GameObject prefab;
    public GameObject childPrefab;
    public GameObject openLesson;
    public GameObject currentPanel;
    public Image top_image;
    void Start()
    {
        onGenerate("lessons_theme");
    }
    public void setTopImage(Sprite img)
    {
        top_image.sprite = img;
    }
    public void onGenerate(string name_json)
    {

        
        //TextAsset asset = Resources.Load<TextAsset>("lessons/lesson_1");
        //print(asset);
        
        //var info = JsonUtility.FromJson<jsonLessonData>(asset.ToString());
        //print(info.child_chapters[2].name);
        List<jsonLessonData> assets = getJsonFiles(name_json);

        int count_buttons = assets.Count; 
        deleteComponents(gameObject);
        GameObject mainobj;
        GameObject childObj;
        for(int i=0;i<count_buttons;i++)
        {
            mainobj = Instantiate(prefab,transform);
            mainobj.name = "Theme_"+i;
            mainobj.GetComponentsInChildren<TextMeshProUGUI>()[0].text="Тема "+ (i+1)+" "+ assets[i].nameMain;
            mainobj.GetComponentsInChildren<TextMeshProUGUI>()[0].color = Color.black;
            for(int j=0;j<assets[i].child_chapters.Length;j++)
            {
                childObj = Instantiate(childPrefab,mainobj.GetComponentsInChildren<ContentSizeFitter>()[0].transform);
                childObj.GetComponentsInChildren<TextMeshProUGUI>()[0].text=(i+1)+"."+(j+1)+" "+assets[i].child_chapters[j].name;
                childObj.GetComponentsInChildren<Image>()[0].color = Utiliity.colors[Random.Range(0,Utiliity.colors.Length)];
                
                childObj.name = "ChildTheme_"+i+"_"+j;
                GameObject gmj = childObj;
                string name = assets[i].child_chapters[j].name + "|" + assets[i].src_child_folder;
                childObj.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => openLessonPanel(name));
                //mainobj.GetComponent<ThemeContent>().gmjs.Add(childObj);                
            }
        }
    }
    public void openLessonPanel(string name)
    {
        string[] str = name.Split('|');
        currentPanel.SetActive(false);
        openLesson.SetActive(true);
        openLesson.GetComponentsInChildren<TextMeshProUGUI>()[0].text=str[0];
        openLesson.GetComponent<GenerateLessonPages>().addContent(str[1]);
    }
    
    public List<jsonLessonData> getJsonFiles(string name_json_theme)
    {
        TextAsset[] asset = Resources.LoadAll<TextAsset>(name_json_theme);
        List<jsonLessonData> assets = new List<jsonLessonData>();
        for(int i=0;i<asset.Length;i++)
            assets.Add(JsonUtility.FromJson<jsonLessonData>(asset[i].ToString()));
        return assets;
    }

    public void deleteComponents(GameObject gmj)
    {
        
        for(int i=gmj.transform.childCount-1;i>=0;i--)
        {
            Destroy(gmj.transform.GetChild(i).gameObject);
        }
    }
    [System.Serializable]
    public class jsonLessonData{
        public string nameMain;
        public string src_child_folder;
        public childJson[] child_chapters;
    }
    [System.Serializable]
    public class childJson{
        public string name;
    }
}
