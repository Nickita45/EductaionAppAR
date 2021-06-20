using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GenerateLessonPages : MonoBehaviour
{
    int number_curent_page = 0;
    int max_page = 0;
    jsonLessonPages currentPage;
    public TextMeshProUGUI text_decription;
    public TextMeshProUGUI text_descr_bottom;
    public Image descr_image;
    public GameObject buttonback,buttonnext;
    public Slider slider;
    public void addContent(string name_src)
    {
        List<jsonLessonPages> assets = getJsonFiles(name_src);
        
        TextMeshProUGUI name = GetComponentsInChildren<TextMeshProUGUI>()[0];
        number_curent_page=0;
        for(int i=0;i<assets.Count;i++)
            if(assets[i].nameMain == name.text)
            {
                max_page = assets[i].pages.Length;
                currentPage = assets[i];
            }
        slider.maxValue = max_page-1;
        changePage(0);
                //print("Файл урока не найден");

    }
    //-1 - back, 1 - next, 0 - nothing
    public void changePage(int page)
    {
        if(page == -1)
            number_curent_page-=1;
        else if(page == 1)
            number_curent_page+=1;

        if(number_curent_page == 0)
            buttonback.SetActive(false);
        else 
            buttonback.SetActive(true);

        if(number_curent_page == (max_page -1) )
            buttonnext.SetActive(false);
        else 
            buttonnext.SetActive(true);
        
        slider.value = number_curent_page;
        //!!! CONTENT
        if(currentPage.pages[number_curent_page].src_image == "null")
        {    
            text_decription.text = currentPage.pages[number_curent_page].value;
            text_descr_bottom.text = "";
            descr_image.gameObject.SetActive(false);
        }
        else
        {
            text_descr_bottom.text = currentPage.pages[number_curent_page].value;
            Sprite textures = Resources.Load<Sprite>(currentPage.src_for_images+"/"+currentPage.pages[number_curent_page].src_image);
            print(currentPage.src_for_images+"/"+currentPage.pages[number_curent_page].src_image);
            descr_image.gameObject.SetActive(true);
            descr_image.sprite = textures;
            text_decription.text="";
        }

    }
    public List<jsonLessonPages> getJsonFiles(string name_src)
    {
        TextAsset[] asset = Resources.LoadAll<TextAsset>(name_src);
        List<jsonLessonPages> assets = new List<jsonLessonPages>();
        for(int i=0;i<asset.Length;i++)
            assets.Add(JsonUtility.FromJson<jsonLessonPages>(asset[i].ToString()));
        return assets;
    }
    [System.Serializable]
    public class jsonLessonPages{
        public string nameMain;
        public string src_for_images;
        public jsonPage[] pages;
    }
    [System.Serializable]
    public class jsonPage{
        public string type;
        public string value;
        public string src_image;
        //public string bottom_text;
    }
}
