using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ProgrammManager : MonoBehaviour
{
    public GameObject PlaneMarkerPrefab;
    ARRaycastManager raycastManager;
    Vector2 touchPosition;
    bool IsStarted;

    public GameObject ObjectToSpawn, PanelShow;

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
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
            var obj = Instantiate(ObjectToSpawn, PlaneMarkerPrefab.transform.position, ObjectToSpawn.transform.rotation);
            obj.SetActive(true);
            IsStarted = true;
            PlaneMarkerPrefab.SetActive(false);
            // Instantiate(ObjectToSpawn, hits[0].pose.position, ObjectToSpawn.transform.rotation);
        }
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
