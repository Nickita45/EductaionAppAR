using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ProgrammManager : MonoBehaviour
{
    public GameObject PlaneMarkerPrefab;
    ARRaycastManager raycastManager;
    Vector2 touchPosition;
    bool IsStarted;

    TextMeshProUGUI textt;
    string[] str;
    public GameObject PanelShow;
    public DontDestroy dontdestr;
    public GameObject[] ObjectToSpawn;
    public GameObject startUI;
    public int indexs;

    // Start is called before the first frame update
    void Start()
    {
        textt = FindObjectOfType<Buttons>().textForAll.GetComponent<TextMeshProUGUI>();
        str = FindObjectOfType<Buttons>().planetSystem;
        raycastManager = FindObjectOfType<ARRaycastManager>();
        dontdestr = FindObjectOfType<DontDestroy>();
        PlaneMarkerPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsStarted == false && PanelShow.activeInHierarchy == false) 
        ShowMarket();

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && PlaneMarkerPrefab.activeInHierarchy == true
             && IsStarted == false && PanelShow.activeInHierarchy == false)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            raycastManager.Raycast(touchPosition, hits, TrackableType.Planes);
            var obj = Instantiate(ObjectToSpawn[dontdestr.Index], PlaneMarkerPrefab.transform.position, ObjectToSpawn[dontdestr.Index].transform.rotation);
            obj.SetActive(true);
            IsStarted = true;
            PlaneMarkerPrefab.SetActive(false);
            startUI.SetActive(false);

            // Instantiate(ObjectToSpawn[dontdestr.Index], hits[0].pose.position, ObjectToSpawn[dontdestr.Index].transform.rotation);
        }
        if(dontdestr.Index == 0 && IsStarted == true && indexs < str.Length) {
            Settext(indexs);
        }
    }

    void Settext(int ind) {
        textt.text = str[indexs];
    }

    
    void ShowMarket()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);


        if (hits.Count > 0)
        {
            PlaneMarkerPrefab.transform.position = hits[0].pose.position;
            PlaneMarkerPrefab.SetActive(true);
        }
    }
}
