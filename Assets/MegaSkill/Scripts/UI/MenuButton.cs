using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public TMP_Text UIText;
    public Graphic graphic;
    public Button button;
    public Button additional_button;
    public Toggle toggle;
    [FormerlySerializedAs("Name")] public string data;
    public int categoryID;
    public int index;

    private void Reset() {
        UIText = GetComponentInChildren<TMP_Text>();
        button = GetComponentInChildren<Button>();
        toggle = GetComponentInChildren<Toggle>();
    }
}
