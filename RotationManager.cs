using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationManager : MonoBehaviour
{
    [SerializeField] private Button ShowButton;
    private Button button;
    private Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Rotate);
    }

    void Rotate()
    {
        manager.Rotate = !manager.Rotate;
        ShowButton.gameObject.SetActive(true);
        button.gameObject.SetActive(false);
    }
}
