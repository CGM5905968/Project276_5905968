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
    public GameObject sendButton;
    //public Text playerNumReady;
    public Text myGuessText;
    public Text lessOrMoreText;

    public bool isGameSetUp = false;

    public bool isPlaying = false;

    public bool isLogin = false;

    public bool isWinner = false;


    // Start is called before the first frame update
    void Start()
    {
        network = GameObject.Find("Network").GetComponent<Network>();
        uiCTR = GameObject.Find("UICTR").GetComponent<UICTR>();

        isLogin = true;

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
    }
    void StartGame()
    {
        playingCTR.SetActive(true);
    }
}
