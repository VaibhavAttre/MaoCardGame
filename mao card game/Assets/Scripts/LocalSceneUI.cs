using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LocalSceneUI : MonoBehaviour
{

    [SerializeField] private List<ButtonComponent> buttons = new List<ButtonComponent>();
    [SerializeField] private List<InputComponent> inputs = new List<InputComponent>();
    private Dictionary<string, UIComponent> components = new Dictionary<string, UIComponent>();
    public Dictionary<string, UIComponent> Components { get { return components; } }

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        UIManager.Instance.LocalUI = this;
        foreach(var button in buttons)
        {
            components.Add(button.key, button);
        }

        foreach(var input in inputs)
        {
            components.Add(input.key, input);
        }
    }
}
