using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class DataBaseRequests : MonoBehaviour
{
    private string secretKey = "vladlox";

    private string url_authorization = "http://ecilop.farlep.net:8080/api/login";
    private string url_getTests = "http://ecilop.farlep.net:8080/api/gettests";
    private string url_setStat = "http://ecilop.farlep.net:8080/api/setstatistics";
    private string url_getStat = "http://ecilop.farlep.net:8080/api/getstatistics";
   
    public GameObject panel_authorization,panel_menu,panel_personal_data;
    public TMP_InputField login,password;
    public jsonAuthorizationInfo person_information;
    public string questions_json;
    public string statistics_json;
    void Start()
    {
        //StartCoroutine(getTests());
    }

    public void button_checklogin()
    {
        StartCoroutine(checkLogin(login.text,password.text));
    }
    public void CallbackGetTests(string call)
    {
        questions_json=call;
    }
    public IEnumerator checkLogin(string login, string pas)
    {

        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("password", pas);

        UnityWebRequest hs_get = UnityWebRequest.Post(url_authorization, form);
        yield return hs_get.SendWebRequest();
        print(hs_get.downloadHandler.text);
        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            person_information = JsonUtility.FromJson<jsonAuthorizationInfo>(hs_get.downloadHandler.text);
            if (person_information.status != "401")
            {
                panel_authorization.SetActive(false);
                panel_menu.SetActive(true);
                setPersonalInformationPanel();
                StartCoroutine(getTests(CallbackGetTests));
                StartCoroutine(getStatistics());
            }
           

        }
    }
    public IEnumerator getTests(System.Action<string> callbackOnFinish)
    {

        WWWForm form = new WWWForm();
        //form.AddField("login", login);
        //form.AddField("password", pas);

        UnityWebRequest hs_get = UnityWebRequest.Get(url_getTests);
        yield return hs_get.SendWebRequest();
        print(hs_get.downloadHandler.text);
        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            callbackOnFinish("{\"lists\":"+hs_get.downloadHandler.text+ "}");


        }
    }
    public IEnumerator setStatistics(string mark, int id_test)
    {

        WWWForm form = new WWWForm();
        form.AddField("mark", mark);
        form.AddField("id_user", person_information.id);
        form.AddField("id_test", id_test);

        UnityWebRequest hs_get = UnityWebRequest.Post(url_setStat, form);
        yield return hs_get.SendWebRequest();
        print(hs_get.downloadHandler.text);
        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            print("WORK");
            StartCoroutine(getStatistics());
            //Наверное токо проверка на статус 
            /*person_information = JsonUtility.FromJson<jsonAuthorizationInfo>(hs_get.downloadHandler.text);
            if (person_information.status != "401")
            {
                panel_authorization.SetActive(false);
                panel_menu.SetActive(true);
                setPersonalInformationPanel();
                StartCoroutine(getTests(CallbackGetTests));
            }*/
           

        }
    }
    public IEnumerator getStatistics()
    {

        WWWForm form = new WWWForm();
        form.AddField("id_user", person_information.id);
        //form.AddField("password", pas);

        UnityWebRequest hs_get = UnityWebRequest.Post(url_getStat,form);
        yield return hs_get.SendWebRequest();
        print(hs_get.downloadHandler.text);
        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            //callbackOnFinish("{\"lists\":"+hs_get.downloadHandler.text+ "}");
            if(hs_get.downloadHandler.text == "[\"nothing\"]")
            statistics_json = "none";
            else
            statistics_json = "{\"lists\":"+hs_get.downloadHandler.text+ "}";



        }
    }
    public void setPersonalInformationPanel()
    {
        
        panel_personal_data.GetComponentsInChildren<TextMeshProUGUI>()[0].text = person_information.name +" "+person_information.last_name;
        panel_personal_data.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Школа"+person_information.school_name+"\n"+"ID:"+person_information.login;
    }
    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
