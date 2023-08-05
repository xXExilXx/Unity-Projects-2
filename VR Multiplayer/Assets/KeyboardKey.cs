using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Keyboard;
public class KeyboardKey : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(wasPressed);
        textMeshPro = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    void wasPressed()
    {
        string keyPressed = textMeshPro.text;
        KeyboardManager.Instance.RegisterKeyPress(keyPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}