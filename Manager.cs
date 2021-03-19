using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject DeleteBut;
    [SerializeField] private GameObject RotateBut;
    [SerializeField] private GameObject MarkerPrefab;
    //Необходим вертеть объект или нет
    public bool Rotate;
    private Quaternion YRotatation;

    private ARRaycastManager ManagerScript;
    [SerializeField] private Camera ARCamera;
    //Создаваемый объект
    public GameObject SpawnPrefab;
    public bool objAvailable = false;
    //Выбранный объект с tag'ом "selected" 
    private GameObject selectedObj;
    List<ARRaycastHit> container = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        ManagerScript = FindObjectOfType<ARRaycastManager>();
        MarkerPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (objAvailable)
        {
            MarkSpawn();
        }
            MoveOrRotateObject();
    }

    private void MarkSpawn()
    {
        ManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), container, TrackableType.Planes);
        if (container.Count > 0)
        {
            MarkerPrefab.SetActive(true);
            MarkerPrefab.transform.position = container[0].pose.position;
        }
        else return;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Instantiate(SpawnPrefab, container[0].pose.position, SpawnPrefab.transform.rotation);
            objAvailable = false;
            MarkerPrefab.SetActive(false);
        }
    }

    private void MoveOrRotateObject()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = ARCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject)
                        && hitObject.collider.CompareTag("unselected"))
                    {
                        ShowHideSelect(true, "selected", hitObject);
                    } else if (Physics.Raycast(ray, out hitObject) && hitObject.collider.CompareTag("selected"))
                    {
                        ShowHideSelect(false,"unselected",hitObject);
                    }
                    break;
                case TouchPhase.Moved:
                    selectedObj = GameObject.FindGameObjectWithTag("selected");
                    if (!Rotate)
                    {
                        ManagerScript.Raycast(touch.position, container, TrackableType.Planes);
                        selectedObj.transform.position = container[0].pose.position;
                    }
                    else
                    {
                        YRotatation = Quaternion.Euler(0f, -touch.deltaPosition.x * 0.1f, 0f);
                        selectedObj.transform.rotation = YRotatation * selectedObj.transform.rotation;
                    }
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    selectedObj.tag = "unselected";
                    break;
                default:
                    break;
            }
        }
    }
    private void ShowHideSelect(bool select, string tag, RaycastHit hitObject)
    {
        hitObject.collider.gameObject.tag = tag;
        RotateBut.SetActive(select);
        DeleteBut.SetActive(select);
    }
}
