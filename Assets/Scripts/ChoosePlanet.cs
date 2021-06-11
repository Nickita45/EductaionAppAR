using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoosePlanet : MonoBehaviour
{
    public bool isTouch;
    public GameObject SpriteObj;
    public Sprite ImageSet;
    public string TextSet, NameObj;

    //Cash
    ChoosePlanet[] Planets;
    GameObject Target, TargetButton;
    TextMeshProUGUI TextTarget, NameTarget;
    Image ImageTarget;
    // Start is called before the first frame update
    void Start()
    {
        Planets = FindObjectsOfType<ChoosePlanet>();
        Target = GameObject.Find("PanelTarget");
        TextTarget = GameObject.Find("TextTarget").GetComponent<TextMeshProUGUI>();
        ImageTarget = GameObject.Find("ImageTarget").GetComponent<Image>();
        NameTarget = GameObject.Find("NameTarget").GetComponent<TextMeshProUGUI>();
        TargetButton = GameObject.Find("ButtonTarget");


        TargetButton.GetComponent<Button>().onClick.AddListener(CloseArrow);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.gameObject.name == gameObject.name)
                {
                    if (isTouch == false)
                    {
                        OpenArrow();
                    }else
                    {
                        CloseArrow();
                    }
                }
            }
        }

        if(isTouch == true)
        {
            SpriteObj.SetActive(true);
          //  Camera.main.transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            //Camera.main.transform.LookAt(gameObject.transform);
        }
    }

    void CloseArrow()
    {
        foreach (ChoosePlanet obj in Planets)
        {
            obj.isTouch = false;
            obj.SpriteObj.SetActive(false);
        }
       // isTouch = false;
      //  SpriteObj.SetActive(false);
        Target.GetComponent<Animation>().Play("OpenClose");
    }

    void OpenArrow()
    {
        NameTarget.text = NameObj;
        TextTarget.text = TextSet;
        ImageTarget.sprite = ImageSet;
        isTouch = true;
        foreach (ChoosePlanet obj in Planets)
        {
            if (obj.gameObject != gameObject)
            {
                obj.isTouch = false;
                obj.SpriteObj.SetActive(false);
            }
        }
        Target.GetComponent<Animation>().Play("OpenPanel");
        if(NameObj == FindObjectOfType<Buttons>().targetnow) {
            for(int i = 0; i< FindObjectOfType<Buttons>().targets.Length; i++) {
                if(FindObjectOfType<Buttons>().targets[i] == NameObj && i+1 < FindObjectOfType<Buttons>().targets.Length) {
                    FindObjectOfType<Buttons>().targetnow = FindObjectOfType<Buttons>().targets[i+1];
                }
            }
            FindObjectOfType<ProgrammManager>().indexs++;
        }
    }

}