using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Close : MonoBehaviour
{
    private Button button;
    public GameObject testObj;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Exit()
    {
        Application.Quit();
    }
}
