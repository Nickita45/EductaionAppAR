using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GenerateQuiz : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public GameObject theme_quizs;
    public GameObject quizOpen;
    public DataBaseRequests Database;
    
    void Start()
    {
        generateQuizThemes();
    }
    public void generateQuizThemes()
    {
        //StartCoroutine(Database.getTests(Database.CallbackGetTests));
        
        jsonList asset = JsonUtility.FromJson<jsonList>(Database.questions_json);
        
        List<jsonQuizs> assets = new List<jsonQuizs>();
        for(int i=0;i<asset.lists.Length;i++)
            assets.Add(asset.lists[i]);
        //List<jsonQuizs> assets = asset.lists;//getJsonFiles();
        deleteComponents(this.gameObject);
        GameObject mainobj;
        for(int i=0;i<assets.Count;i++)
        {
            mainobj = Instantiate(prefab,transform);
            mainobj.GetComponentsInChildren<TextMeshProUGUI>()[0].text =assets[i].nameCourse;
            mainobj.GetComponentsInChildren<TextMeshProUGUI>()[1].text =assets[i].nameMain;
            mainobj.GetComponentsInChildren<Image>()[0].color = Utiliity.colors[Random.Range(0,Utiliity.colors.Length)];
            mainobj.name = "QuizTheme"+(i+1);
            jsonQuizs quiz = assets[i];
            mainobj.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => addLesson(quiz));
            
        }
    }
    public void addLesson(jsonQuizs quiz)
    {
        quizOpen.SetActive(true);
        theme_quizs.SetActive(false);
        quizOpen.GetComponent<QuizAddContent>().quiz_current = quiz; 
        quizOpen.GetComponent<QuizAddContent>().ID_test = quiz.ID_tests;
        quizOpen.GetComponent<QuizAddContent>().addContent();
    }
    public List<jsonQuizs> getJsonFiles()
    {
        TextAsset[] asset = Resources.LoadAll<TextAsset>("tests");
        List<jsonQuizs> assets = new List<jsonQuizs>();
        for(int i=0;i<asset.Length;i++)
            assets.Add(JsonUtility.FromJson<jsonQuizs>(asset[i].ToString()));
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
    public class jsonList
    {
        public jsonQuizs[] lists;
    }
    [System.Serializable]
    public class jsonQuizs{
        public string nameMain;
        public string resources_src;
        public string nameCourse;
        public int ID_tests;
        public jsonQuestions[] questions;
    }
    [System.Serializable]
    public class jsonQuestions{
        public string name_question;
        public string[] variants;
        public string answer;
        public string type_question; 
        public string type_answers;
        public string extra_question;

    }
}
