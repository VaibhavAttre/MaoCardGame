using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    public LocalSceneUI LocalUI;
    public void Connect()
    {
        string inputStr = "ConnectInput";
        if (!LocalUI.Components.TryGetValue(inputStr, out UIComponent component))
        {
            Debug.Log("no");
        }
        InputComponent input = (InputComponent)component;
        string username = input.inp.text;
        NetworkEvents.OnConnectRequest(username);
    }
}
