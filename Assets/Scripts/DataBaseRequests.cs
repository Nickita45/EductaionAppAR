using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataBaseRequests : MonoBehaviour
{
    private string secretKey = "vladlox";

    private string url_authorization = "http://statys.farlep.net/getData.php";

    public  IEnumerator CheckLogin(string login, string pas, System.Action<string> callbackOnFinish)
    {

        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("password", pas);

        UnityWebRequest hs_get = UnityWebRequest.Post(url_authorization, form);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
            callbackOnFinish("ERROR");
            //FailConnect();
        }
        else
        {

        callbackOnFinish(hs_get.downloadHandler.text);
            if (hs_get.downloadHandler.text != "Wrong login" && hs_get.downloadHandler.text != "Error")
            {
              
                //КЛАСС КАСТОМНЫЙ
                //PlayerStatesList GetPlayerstates = JsonUtility.FromJson<PlayerStatesList>("{\"playerstates\":" + hs_get.downloadHandler.text + "}"); // this is a GUIText that will display the scores in game.
                //ТУТ ЕСЛИ ПОЛУЧИЛОСЬ
  

            }
           

        }
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
