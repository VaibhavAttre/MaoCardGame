using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Riptide;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;


public class ArrangeAroundBoard : MonoBehaviour
{

    //[SerializeField] private GameObject board;
    [SerializeField] private GameObject spawnPoint;
    private bool arranged = false;

    public float boardRadius;
    private Dictionary<int, string> cardDict = new Dictionary<int, string>
    {
        { 1, "Club01_00" }, { 2, "Club02_00" }, { 3, "Club03_00" }, { 4, "Club04_00" }, { 5, "Club05_00" }, { 6, "Club06_00" }, { 7, "Club07_00" }, { 8, "Club08_00" }, { 9, "Club09_00" }, { 10, "Club10_00" },
        { 11, "Club11_00" }, { 12, "Club12_00" }, { 13, "Club13_00" }, { 14, "Diamond01_00" }, { 15, "Diamond02_00" }, { 16, "Diamond03_00" }, { 17, "Diamond04_00" }, { 18, "Diamond05_00" }, { 19, "Diamond06_00" },
        { 20, "Diamond07_00" }, { 21, "Diamond08_00" }, { 22, "Diamond09_00" }, { 23, "Diamond10_00" }, { 24, "Diamond11_00" }, { 25, "Diamond12_00" }, { 26, "Diamond13_00" }, { 27, "Heart01_00" }, { 28, "Heart02_00" },
        { 29, "Heart03_00" }, { 30, "Heart04_00" }, { 31, "Heart05_00" }, { 32, "Heart06_00" }, { 33, "Heart07_00" }, { 34, "Heart08_00" }, { 35, "Heart09_00" }, { 36, "Heart10_00" }, { 37, "Heart11_00" },
        { 38, "Heart12_00" }, { 39, "Heart13_00" }, { 40, "Spade01_00" }, { 41, "Spade02_00" }, { 42, "Spade03_00" }, { 43, "Spade04_00" }, { 44, "Spade05_00" }, { 45, "Spade06_00" }, { 46, "Spade07_00" },
        { 47, "Spade08_00" }, { 48, "Spade09_00" }, { 49, "Spade10_00" }, { 50, "Spade11_00" }, { 51, "Spade12_00" }, { 52, "Spade13_00" }
    };
    void Start()
    {
       // this.transform.localScale = new Vector3(boardRadius * 2, 0.1f, boardRadius * 2);

    }

    // Update is called once per frame
    void Update()
    {
        /*if (!arranged)
        {
            Arrange();
            Deal();
        }*/
    }

   
    public void Arrange(int numPlayers, Player player, int i)
    {
        /*
        GameObject spawnParent = GameObject.Find("SpawnPoint");
        GameObject currSpawnPoint = Instantiate(spawnPoint);
        float x = boardRadius * Mathf.Cos(2 * Mathf.PI * i / numPlayers);
        float y = boardRadius * Mathf.Sin(2 * Mathf.PI * i / numPlayers);
        Vector3 pos = new Vector3(x, 0.5f, y);
        currSpawnPoint.transform.position = pos;
        //player.gameObject.transform.position = pos;

        Vector3 diff = transform.TransformDirection(currSpawnPoint.transform.position - player.transform.position);
        player.Movement.Controller.Move(diff);

        Vector3 relativePos = spawnParent.transform.position - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        SendSpawnLocation(pos, relativePos, player);
        //player.transform.rotation = rotation;

        Debug.Log($"Player {i} moved to position: {player.transform.position}");

        if (Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit hitinfo, 200f))
        {
            Debug.DrawRay(player.transform.position, player.transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red, 1000);
        } else
        {
            Debug.Log("Nun");
        }
        
        arranged = true;*/

        GameObject spawnParent = GameObject.Find("SpawnPoint");

        float x = boardRadius * Mathf.Cos(2 * Mathf.PI * i / numPlayers);
        float z = boardRadius * Mathf.Sin(2 * Mathf.PI * i / numPlayers);
        Vector3 targetPosition = new Vector3(x, 0.5f, z);

        CharacterController controller = player.GetComponent<CharacterController>();

        Vector3 motion = targetPosition - player.transform.position;
        controller.Move(motion); 
        
        Vector3 relativePos = spawnParent.transform.position - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        player.transform.rotation = rotation;

        Debug.Log($"Player {i} moved to position: {player.transform.position}, SpawnPoint at: {targetPosition}");

        // Visual Debugging: Draw a ray to see where the player is facing
        if (Physics.Raycast(player.transform.position, player.transform.forward, out RaycastHit hitinfo, 200f))
        {
            Debug.DrawRay(player.transform.position, player.transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red, 1000);
        }
        else
        {
            Debug.Log("No hit detected.");
        }

        SendSpawnLocation(targetPosition, relativePos, player);
        arranged = true;
    }

    private void SendSpawnLocation(Vector3 location, Vector3 rotation, Player player)
    {
        Message message = Message.Create(MessageSendMode.Reliable, (ushort)ServerToClientId.spawnDeck);
        message.AddUShort(player.Id);            // Send player ID
        message.AddVector3(location);            // Send position
        message.AddVector3(rotation);            // Send rotation
        NetworkManager.Singleton.Server.SendToAll(message);  // Send to all clients
    }

    /*
    public void Deal()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                System.Random random = new System.Random();
                int cardNum = random.Next(1, 53);
                string card = cardDict[cardNum];

                GameObject cardPrefab = Resources.Load<GameObject>($"BackColor_Blue/Blue_PlayingCards_{card}");
                if (cardPrefab != null)
                {
                    GameObject currCard = Instantiate(cardPrefab, players[i].transform);
                    currCa  rd.transform.position = new Vector3(players[i].transform.position.x, players[i].transform.position.y + 0.5f, players[i].transform.position.z);
                    //players[i].GetComponent<PlayerDeck>().addCard(card, currCard);
                    currCard.transform.localScale = new Vector3(.25f, 1, .25f);

                }
                else
                {
                    Debug.Log($"Blue_PlayingCards_{card}");
                }
            }
        }
    }*/
}
