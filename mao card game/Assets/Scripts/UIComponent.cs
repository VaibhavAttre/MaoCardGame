using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIComponent 
{
    public string key;
}

[Serializable]
public class ButtonComponent : UIComponent {

    public Button button;
}

[Serializable]
public class InputComponent : UIComponent {

    public TMP_InputField inp;
}
