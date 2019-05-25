using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SocketIO;

public class Network : MonoBehaviour
{

    static SocketIOComponent socket;
    public UICTR uiCTR;
    public GameCTR gameCTR;

    JSONObject JSONobject;



    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        uiCTR = GameObject.Find("UICTR").GetComponent<UICTR>();

        socket.On("Connect To Server", OnConnected);
        socket.On("Other Player Connected", OtherConnected);

        socket.On("I Login", OnLogIn);

        socket.On("getValue", BackAnswer);
        socket.On("SomeOneGuess", OtherGuess);

        socket.On("show", ShowRound);

        socket.On("I Am Winner", IWin);
        socket.On("I Lose", ILose);


    }

    private void OnConnected(SocketIOEvent obj) // Connection To Server
    {
        uiCTR.serverStatus.text = "Server Online";
        //uiCTR.playerNumCon++;
    }
    public void LogInToServer(string name)
    {
        if(gameCTR.isPlaying == false)
        {
            JSONobject = new JSONObject(JSONObject.Type.OBJECT);
            JSONobject.AddField("playerName", name);

            socket.Emit("login", JSONobject);
        }
    }
    void OnLogIn(SocketIOEvent obj) 
    {
        uiCTR.logInCTR.SetActive(false);
        gameCTR.isPlaying = true;
        //gameCTR.isLogin = false;

        JSONobject = obj.data;

        uiCTR.playerID.text = ""+JSONobject["id"].str;
        uiCTR.playerName.text = ""+JSONobject["name"].str;
        Debug.Log(JSONobject["name"]);

        uiCTR.playerNumConnected.text = "Current Playing: " + JSONobject["playerConnected"].n; 
    }
    public void LogOut(bool data)
    {
        JSONobject = new JSONObject(JSONObject.Type.OBJECT);
        JSONobject.AddField("played", data);

        uiCTR.playerID.text = "---";
        uiCTR.playerName.text = "---";




        socket.Emit("LogOut", JSONobject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OtherConnected(SocketIOEvent obj)
    {
        //uiCTR.playerNumCon++;
        JSONobject = obj.data;

        uiCTR.playerNumConnected.text = "Current Playing: " + JSONobject["playerConnected"].n;


    }
    public void SendGuess(int data)
    {
        gameCTR.myGuessText.text = "My Guess " + data.ToString();
        gameCTR.lessOrMoreText.text = "Please Wait...";

        JSONobject = new JSONObject(JSONObject.Type.OBJECT);
        JSONobject.AddField("GuessNum", data);
        JSONobject.AddField("name", uiCTR.playerName.text);
        JSONobject.AddField("id", uiCTR.playerID.text);


        socket.Emit("Guess", JSONobject);
        //gameCTR.isPlayed = true;

        print("send value");
        Debug.Log(uiCTR.playerName.text);

    }

    void BackAnswer(SocketIOEvent obj)
    {
        JSONobject = obj.data;

        gameCTR.backID = JSONobject["id"].str;
        gameCTR.backName = JSONobject["name"].str;
        gameCTR.backResult = JSONobject["result1"].str;
        gameCTR.backNum = (int)JSONobject["myGuess"].n;
        gameCTR.readyNumText.text = JSONobject["count"].str + "Player Is Ready";

        Debug.Log(gameCTR.backName + gameCTR.readyNumText + gameCTR.backID);
        //Debug.Log(gameCTR.backResult);
        Debug.Log(JSONobject["result1"].str);


    }
    void OtherGuess(SocketIOEvent obj)
    {
        JSONobject = obj.data;

        gameCTR.readyNumText.text = JSONobject["count"].str + "Player Is Ready";

    }
    void ShowRound(SocketIOEvent obj)
    {
        gameCTR.readyNumText.text = "Next Round";

        gameCTR.ShowRound();

    }

    void IWin(SocketIOEvent obj)
    {

        JSONobject = obj.data;

        gameCTR.playingCTR.SetActive(false);
        uiCTR.endGameCTR.SetActive(true);
        uiCTR.winName.text = "I AM THE WINNER";
        uiCTR.answer.text = JSONobject["answer"].str;


    }
    void ILose(SocketIOEvent obj)
    {
        JSONobject = obj.data;

        gameCTR.playingCTR.SetActive(false);
        uiCTR.endGameCTR.SetActive(true);



    }
}
