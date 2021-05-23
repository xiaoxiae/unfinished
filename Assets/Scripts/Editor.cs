using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MonoBehaviour
{
    InputField InputField;

    void Start()
    {
        InputField = GetComponent<InputField>();
    }

    void Update()
    {
        if (InputField.isFocused) {
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;
    }
}