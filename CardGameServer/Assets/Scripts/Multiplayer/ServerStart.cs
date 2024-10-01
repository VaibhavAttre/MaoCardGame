using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerStart : MonoBehaviour
{
    void StartServer()
    {
        NetworkManager.Singleton.changeServerState(true);
    }
}
