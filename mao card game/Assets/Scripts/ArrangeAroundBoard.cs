using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrangeAroundBoard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ReadInput readInput;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject spawnPoint;
    private int numPlayers;
    private bool arranged = false;

    public List<GameObject> players = new List<GameObject>();
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
        this.transform.localScale = new Vector3(boardRadius * 2, 0.1f, boardRadius * 2);

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

    public void setPlayerCount(int numPlayers)
    {
        this.numPlayers = numPlayers;
    }

    public void Arrange()
    {
       
        for (int i = 0; i < numPlayers; i++)
        {
            GameObject spawnParent = GameObject.Find("SpawnPoint");
            GameObject currSpawnPoint = Instantiate(spawnPoint, spawnParent.transform);
            float x = boardRadius * Mathf.Cos(2 * Mathf.PI * i / numPlayers);
            float y = boardRadius * Mathf.Sin(2 * Mathf.PI * i / numPlayers);
            currSpawnPoint.transform.position = new Vector3(x, .5f, y);

            Vector3 directionToCenter = (spawnParent.transform.position - currSpawnPoint.transform.position).normalized;
            currSpawnPoint.transform.rotation = Quaternion.LookRotation(directionToCenter);

            players.Add(currSpawnPoint);
        }
        arranged = true;

    }

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
                    currCard.transform.position = new Vector3(players[i].transform.position.x, players[i].transform.position.y + 0.5f, players[i].transform.position.z);
                    players[i].GetComponent<PlayerDeck>().addCard(card, currCard);
                    currCard.transform.localScale = new Vector3(.25f, 1, .25f);

                } else
                {
                    Debug.Log($"Blue_PlayingCards_{card}");
                }
            }
        }
    }
}


//Blue_PlayingCards_Heart03_00
//Blue_PlayingCards_Heart03_00
