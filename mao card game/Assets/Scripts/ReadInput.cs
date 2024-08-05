using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ReadInput : MonoBehaviour
{

    private string input;
    // Start is called before the first frame update
    void Start()
    {
        input = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClicked()
    {
        GameObject.Find("Board").GetComponent<ArrangeAroundBoard>().setPlayerCount(GameObject.Find("NetworkManagerUI").GetComponent<NetworkManagerUI>().playerCount);
        GameObject.Find("Board").GetComponent<ArrangeAroundBoard>().Arrange();
        GameObject.Find("Board").GetComponent<ArrangeAroundBoard>().Deal();
    }

    public void ReadStringInput(string input)
    {
        this.input = input;
        GameObject.Find("PlayerNumInputField").SetActive(false);
    }

    public string getInput()
    {
        return input;
    }
}
