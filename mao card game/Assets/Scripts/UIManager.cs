using Riptide;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager singleton;

    public static UIManager Singleton
    {

        get => singleton;
        private set
        {
            if (singleton == null)
                singleton = value;
            else if (singleton != value)
            {
                Debug.Log($"{nameof(UIManager)} instnace already exists");
                Destroy(value);
            }

        }

    }

    [SerializeField] private GameObject connect;
    [SerializeField] private TMP_InputField username;
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject startGame;

    private void Awake()
    {
        Singleton = this;
    }

    public void Connect()
    {
        NetworkManager.Singleton.Connect();
        username.interactable = false;
        connect.SetActive(false);
        image.SetActive(false);
    }

    public void Disconnect()
    {
        username.interactable = true;
        connect.SetActive(true);
        image.SetActive(true);
    }

    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerId.name);
        message.AddString(username.text);
        NetworkManager.Singleton.client.Send(message);
    }

    public void StartGame()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ClientToServerId.startGame);
        message.AddBool(true);
        NetworkManager.Singleton.client.Send(message);
    }
}
