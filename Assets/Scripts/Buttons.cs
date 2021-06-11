using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Buttons : MonoBehaviour
{

    public GameObject PanelShow, button, textForAll, panel;
    public bool stop;
    public string[] elovutionStars, planetSystem, targets;
    public string targetnow;
    public Sprite spriteZam,spriteneZam;

    public void SetSpawn(GameObject obj)
    {
        //FindObjectOfType<ProgrammManager>().ObjectToSpawn = obj;
        PanelShow.SetActive(false);
    }

    public void NextScene(string name)
    {
        SceneManager.LoadScene(name);
    }

public void SetForm(GameObject text) {
    if(stop == false) {
       // Debug.Log(gameObject.name); 
    if(button.GetComponent<Image>().sprite == spriteZam) {
        Mood(text, "Вы сменили на режим обучения", spriteneZam);
        panel.SetActive(true);
    } else {
        Mood(text, "Вы сменили на свободный режим", spriteZam);
        panel.SetActive(false);
    }
    }
}
void Mood(GameObject text, string mood, Sprite sp) {
    text.GetComponent<TextMeshProUGUI>().text = mood;
    button.GetComponent<Image>().sprite = sp;
}
  public void SetScene(int index) {
      FindObjectOfType<DontDestroy>().Index = index;
      SceneManager.LoadScene("SampleScene");
  }

  public void AnimationButton(GameObject gm) {
      if(!gm.GetComponent<Animation>().isPlaying && stop == false) {
          StartCoroutine(Anime(gm));
      }
  }
  IEnumerator Anime(GameObject gm) {
      stop = true;
      var anim = gm.GetComponent<Animation>();
      anim.Play("ButtonR");
      yield return new WaitForSeconds(2);
      anim.Play("ButtonRBack");
      stop = false;
  }
}

