using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SocketIO;

using UnityEngine.UI;

public class Network : MonoBehaviour
{
    static SocketIOComponent socket;

    public Text getText;
    //public Text playText;

    //public GameObject currentPlayer;
    //public Spawner spawner;

    void Start()
    {
        socket = GetComponent<SocketIOComponent>();

        socket.On("open", OnConnected);

        socket.On("getValue", GetMessage);

        //socket.On("register", OnRegister);


    }

    private void OnConnected(SocketIOEvent obj)
    {
        Debug.Log("conected");
    }

    /*private void OnRegister(SocketIOEvent obj)
    {
        Debug.Log("registered id = " + obj.data);
        spawner.AddPlayer(obj.data["id"].str, currentPlayer);
        currentPlayer.GetComponent<NetWorkEntity>().id = obj.data["id"].str;
    }*/

    private void GetMessage(SocketIOEvent obj)
    {
        //string input = obj.data.ToString();
        //Debug.Log(input);

        JSONObject JSONobject = obj.data;

        getText.text = JSONobject["text"].ToString();

        Debug.Log(JSONobject["text"].ToString());
    }


    public void SendValue(int num)
    {
        JSONObject JSONobject = new JSONObject(JSONObject.Type.OBJECT);
        JSONobject.AddField("mynum",num);

        socket.Emit("Check", JSONobject);

        print("send value");
        
        //string data = JsonUtility.ToJson(num);
        //socket.Emit("Check", new JSONObject(data));

        
    }
}
