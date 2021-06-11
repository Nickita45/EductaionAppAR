using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestAnim : MonoBehaviour
{
    public GameObject Partical, Sphere, SphereNew;
    ParticleSystem ps;
    
    TextMeshProUGUI textt;
    string[] str;
    float TimerToStart, k = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        textt = FindObjectOfType<Buttons>().textForAll.GetComponent<TextMeshProUGUI>();
        str = FindObjectOfType<Buttons>().elovutionStars;
         ps = Partical.GetComponent<ParticleSystem>();
         textt.text = str[0];
    }

    // Update is called once per frame
    void Update()
    { 
        TimerToStart += Time.deltaTime;

      // if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended && TimerToStart > 5) {
      //.     StartCoroutine(StartCoolEffect());
      //  }
       if(Input.GetKeyUp(KeyCode.G) && TimerToStart > 5) {
            if (Sphere.activeInHierarchy == false)
            {
                StartCoroutine(StartCoolEffect());
                         textt.text = str[1];
            }
            else
            {
                if (Sphere.transform.localScale.x < 1f)
                {
                    StartCoroutine(StarBigSphere());
                    textt.text = str[2];
                }else
                {
                    SphereNew.SetActive(true);
                    StartCoroutine(StartCoolEffect());
                    textt.text = str[3];
                }
            }

        }
    }

    IEnumerator StartCoolEffect()
    {
        if (k == 0)
        {
            while (k > -40)
            {
                CoolEffect(-0.5f);
               if(k == -30 )
                {
                    StartCoroutine(StarSphere());
                }
                yield return new WaitForSeconds(0.1f);
            }
        } else
        {
            while (k < 0)
            {
                if(k == 0)
                {
          
                    //   StartCoroutine(Explothen());
                }
                CoolEffect(0.5f);
                yield return new WaitForSeconds(0.1f);
                if(Sphere.transform.localScale.x > 0)
                {
                    Sphere.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                }
            }
            Sphere.SetActive(false);
        }
    }

    IEnumerator Explothen()
    {
        while (Sphere.transform.localScale.x < 4f)
        {
            TimerToStart = 0;
            Sphere.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        Sphere.SetActive(false);
    }



    IEnumerator StarBigSphere()
    {
        while (Sphere.transform.localScale.x < 1f)
        {
            TimerToStart = 0;
            Sphere.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
            Sphere.GetComponent<MeshRenderer>().material.color -= new Color32(0, 2, 2, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }


    IEnumerator StarSphere()
    {
        Sphere.SetActive(true);
        Sphere.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        while (Sphere.transform.localScale.x < 0.75f)
        {
            Sphere.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void CoolEffect(float kof)
    {
        TimerToStart = 0;
        k += kof;
        var test = ps.velocityOverLifetime;
        test.radialMultiplier = k;
    }
}
