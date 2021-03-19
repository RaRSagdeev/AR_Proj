using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideManager : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject secondButton;
    [SerializeField] private GameObject hideObject;
    [SerializeField] private bool hide = false;
    [SerializeField] private GameObject CloseBut;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HideOrShow);
    }

    // Update is called once per frame
    void HideOrShow()
    {
        CloseBut.SetActive(!CloseBut.activeSelf);
        hideObject.SetActive(hide);
        button.gameObject.SetActive(false);
        secondButton.SetActive(true);
    }
}
