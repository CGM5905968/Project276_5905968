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

        socket.On("Connected", OnConnected);
        socket.On("Other Player Connected", OtherConnected);

        socket.On("I Login", OnLogIn);


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

        uiCTR.playerID.text = JSONobject["id"].str;
        uiCTR.playerName.text = JSONobject["name"].str;

        uiCTR.playerNumConnected.text = "Current Playing: " + JSONobject["playerConnected"].n; 
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
}
