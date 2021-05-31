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
    void Start()
    {
        generateQuizThemes();
    }
    void generateQuizThemes()
    {
        List<jsonQuizs> assets = getJsonFiles();
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
    [System.Serializable]
    public class jsonQuizs{
        public string nameMain;
        public string resources_src;
        public string nameCourse;
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
