using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameCTR : MonoBehaviour
{

    public Network network;
    public UICTR uiCTR;

    //for playing
    public GameObject playingCTR;
    public Text putNumText;
    public Text readyNumText;
    public Text playerNumText;
    public GameObject inPutNumBox;
    public Text inPutGuessNum;
    public GameObject sendButton;
    //public Text playerNumReady;
    public Text myGuessText;
    public Text lessOrMoreText;

    //result box
    public string backName;
    public string backID;
    public string backResult;
    public int backNum;
    public int playerCount;



    public bool isGameSetUp = false;

    public bool isPlayed = false;

    public bool isPlaying = false;

    public bool isLogin = false;

    public bool isWinner = false;

    //public int thisNum;


    // Start is called before the first frame update
    void Start()
    {
        network = GameObject.Find("Network").GetComponent<Network>();
        uiCTR = GameObject.Find("UICTR").GetComponent<UICTR>();

        isLogin = true;
        playingCTR.SetActive(false);
        uiCTR.endGameCTR.SetActive(false);

        isPlaying = false;
        isGameSetUp = false;
        isWinner = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isLogin)
        {
            LoginToGame();
            isLogin = false;
        }
        if (isPlaying)
        {
            StartGame();
            isPlaying = false;
        }

    }

    void LoginToGame()
    {
        uiCTR.logInCTR.SetActive(true);
        uiCTR.endGameCTR.SetActive(false);
        playingCTR.SetActive(false);
    }


    void StartGame()
    {
        playingCTR.SetActive(true);
    }



    public void GuessButton()
    {
        if (!isPlayed)
        {
            isPlayed = true;
            network.SendGuess(int.Parse(inPutGuessNum.text));
        }
    }

    public void ShowRound()
    {
        lessOrMoreText.text = backResult;
        Debug.Log(backResult);
        myGuessText.text = "Next Geuss!";
        isPlayed = false;


    }
}
