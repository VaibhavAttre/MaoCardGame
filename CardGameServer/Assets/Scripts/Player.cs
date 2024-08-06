using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ushort Id {  get; private set; }
    public string Username { get; private set; }
    
    public void Init(ushort id, string username)
    {
        Id = id;
        Username = username;
    }

    private void OnDestroy()
    {
        PlayerManager.RemovePlayer(Id);
    }

    #region Messages

    public void LoginApprove(bool approve)
    {
        Message msg = Message.Create(MessageSendMode.Reliable, ServerToClientMessage.ApproveLogin);
        msg.AddBool(approve);
        NetworkManager.Instance.server.Send(msg, Id, approve);
    }

    #endregion
}
