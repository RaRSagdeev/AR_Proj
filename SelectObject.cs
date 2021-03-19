using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour
{
    private Manager ManagerScript;
    private Button Button;
    public GameObject Prefab;
    // Start is called before the first frame update
    void Start()
    {
        ManagerScript = FindObjectOfType<Manager>();
        Button = GetComponent<Button>();
        Button.onClick.AddListener(ChangeObject);
    }

    // Update is called once per frame
    void ChangeObject()
    {
        ManagerScript.SpawnPrefab = Prefab;
        ManagerScript.objAvailable = true;
    }
}
