using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public Vector3 startScale;
    public GameObject Paricles;
    float pr = 0;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;

        transform.localScale = new Vector3(0, 0, 0);
        var main = Paricles.GetComponent<ParticleSystem>().main;
        main.startSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var main = Paricles.GetComponent<ParticleSystem>().main;
    
        if (transform.localScale.x < startScale.x)
        {
            if (pr > 0.5)
            {
                transform.localScale += new Vector3(0.0001f, 0.0001f, 0.0001f);
                transform.Rotate(new Vector3(0, 15, 0));
            }
          
        }
        pr += 0.01f;
        main.startSpeed = pr;

        if(pr > 3)
        {
            Paricles.SetActive(false);
        }
    }
}
