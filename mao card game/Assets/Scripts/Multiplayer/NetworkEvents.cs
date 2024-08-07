using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class NetworkEvents 
{

    public static event UnityAction<string> ConnectRequest;
    public static void OnConnectRequest(string username) => ConnectRequest?.Invoke(username);

    public static event UnityAction<Message> SendMessage;
    public static void OnSendMessage(Message message) => SendMessage?.Invoke(message);

    public static event UnityAction<ushort, string> ConnectSuccess;
    public static void OnConnectSuccess(ushort id, string username) => ConnectSuccess?.Invoke(id, username);
}
