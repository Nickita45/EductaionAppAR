using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuizAddContent : MonoBehaviour
{
     public GenerateQuiz.jsonQuizs quiz_current;
    int number_curent_page = 0;
    int max_page = 0;
    int current_click = -1; 
    public Slider slider;
    public GameObject buttonNext;
    //public GameObject buttonFinish;
    public GameObject resultObj; 
    public GameObject test_String_Strings;
    public Image question_image;
    //public LoginForm _LoginForm;
    List<Button> buttons_answers = new List<Button>();
    List<bool> answers = new List<bool>();
    public Sprite standart_block;
    public void addContent()
    {
        //ПРОВЕРКА НА ТИП ТЕСТА
        max_page = quiz_current.questions.Length-1;

        slider.maxValue = max_page;

        number_curent_page = 0;
        GetComponentsInChildren<TextMeshProUGUI>()[0].text = quiz_current.nameMain; //!!!!

        test_String_Strings.SetActive(true);//!!!
        //resultObj.SetActive(false);

        buttons_answers.Clear();
        for(int i=0;i<4;i++)
            buttons_answers.Add(test_String_Strings.GetComponentsInChildren<Button>()[i]);
        setStandard();

        answers.Clear();
        
        randomizeQuiz();

        //buttonFinish.SetActive(true);
        changePage(0);
    }
    public void changePage(int page)
    {
        /*if(number_curent_page == (max_page -1))
            buttonNext.SetActive(false);
        else
            buttonNext.SetActive(true);*/
        
        if(page != 0 )
        {
            if(current_click != -1 )//!!!!
            addAnswer();
            number_curent_page+=1;
        }
        setStandard();
        slider.value = number_curent_page;
        current_click = -1;
        if(quiz_current.questions[number_curent_page].type_question == "img")
        {

            Sprite textures = Resources.Load<Sprite>("tests/"+quiz_current.resources_src+"/"+quiz_current.questions[number_curent_page].name_question);
            //print("TEXTURES:"+textures);
            test_String_Strings.GetComponentsInChildren<TextMeshProUGUI>()[0].text ="";
            var tempColor =  question_image.color;
            tempColor.a=1f;
            question_image.color = tempColor;
            test_String_Strings.GetComponentsInChildren<Image>()[1].sprite = textures;
        }
        else  
        {    
            test_String_Strings.GetComponentsInChildren<TextMeshProUGUI>()[0].text = quiz_current.questions[number_curent_page].name_question;
            var tempColor =  question_image.color;
            tempColor.a=0f;
            question_image.color = tempColor;
            //test_String_Strings.GetComponentsInChildren<Image>()[1].sprite = null;
        }
        
        for(int i=0;i<4;i++)
        {
            if(quiz_current.questions[number_curent_page].type_answers == "img")
            {
                test_String_Strings.GetComponentsInChildren<Image>()[i+2].sprite = Resources.Load<Sprite>("tests/"+quiz_current.resources_src+"/"+quiz_current.questions[number_curent_page].variants[i]);
                test_String_Strings.GetComponentsInChildren<TextMeshProUGUI>()[i+1].text = "";
            }
            else
            {    
                test_String_Strings.GetComponentsInChildren<Image>()[i+2].sprite = standart_block;//!!!
                test_String_Strings.GetComponentsInChildren<TextMeshProUGUI>()[i+1].text = quiz_current.questions[number_curent_page].variants[i];
            }
        }
        //test_String_Strings.GetComponentsInChildren<TextMeshProUGUI>()[6].text = quiz_current.questions[number_curent_page].extra_question;
    }
    public void clickOnButton(int i)
    {
        setStandard();
        buttons_answers[i-1].image.color = Utiliity.getColorByHex("2AE73F");
        current_click = i-1;

    }
    public void addAnswer()
    {
        string enteredAnswers = "";
        if(quiz_current.questions[number_curent_page].type_answers == "img")
            enteredAnswers = buttons_answers[current_click].GetComponent<Image>().sprite.name;
        else
            enteredAnswers = buttons_answers[current_click].GetComponentsInChildren<TextMeshProUGUI>()[0].text;
        
        print(quiz_current.questions[number_curent_page].answer);
        
        if(enteredAnswers == quiz_current.questions[number_curent_page].answer) 
            answers.Add(true);
        else
            answers.Add(false);
        
        print(answers[answers.Count-1]);
    }
    void setStandard()
    {
        for(int i = 0 ; i< 4; i++)
        {
            buttons_answers[i].image.color = Color.white;
        }
    }
    public void countResults()
    {
        if(current_click != -1 )
        addAnswer();
        resultObj.SetActive(true);
        test_String_Strings.SetActive(false);

        TextMeshProUGUI[] texts = resultObj.GetComponentsInChildren<TextMeshProUGUI>();
        int true_answer = 0;
        for(int i = 0;i<answers.Count;i++)
        {
            if(answers[i] == true)
            true_answer++;
        }
        texts[1].text= true_answer+"/"+(max_page+1)+"правильных";
        float procent = (float)true_answer/(max_page+1); 
        if(procent>=0.6f)
            texts[2].text = "Сдал";
        else
            texts[2].text= "Не сдал";

        //if(string.IsNullOrEmpty(PlayerPrefs.GetString("test_first"+_LoginForm.login_id+quiz_current.nameMain)) == true)
        //    PlayerPrefs.SetString("test_first"+_LoginForm.login_id+ quiz_current.nameMain,true_answer+"/"+(max_page+1));
        //else
        //    PlayerPrefs.SetString("test"+_LoginForm.login_id+quiz_current.nameMain,true_answer+"/"+(max_page+1));
    }
    public void randomizeQuiz()
    {
        Randomizer.Randomize(quiz_current.questions);
        for(int i=0;i<quiz_current.questions.Length;i++)
            Randomizer.Randomize(quiz_current.questions[i].variants);
    }
}
