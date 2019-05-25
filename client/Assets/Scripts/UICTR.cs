﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UICTR : MonoBehaviour
{
    public Network network;

    //Status
    public Text playerNumConnected;
    public Text serverStatus;
    public Text playerID;
    public Text playerName;

    public int playerNumCon;

    //for login
    public GameObject logInCTR;
    public Text signIn;
    public GameObject inPutBox;
    public Text playerInputName;
    public GameObject signInButton;

    //for playing
    /*public GameObject playingCTR;
    public Text putNumText;
    public Text readyNumText;
    public Text playerNumText;
    public GameObject inPutNumBox;
    public GameObject sendButton;
    public Text playerNumReady;
    public Text myGuessText;
    public Text lessOrMoreText;*/


    //for EndGame
    public GameObject endGameCTR;
    public Text winName;
    public Text answer;




    // Start is called before the first frame update
    void Start()
    {
        network = GameObject.Find("Network").GetComponent<Network>();
        serverStatus = GameObject.Find("ServerStatus").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogInButton()
    {
        network.LogInToServer(playerInputName.text);
    }
    public void LogOutButton()
    {
        network.LogOut();
    }
}
