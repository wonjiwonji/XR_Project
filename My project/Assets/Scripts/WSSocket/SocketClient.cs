using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;   // C#���� �� ��Ĺ�� �����ϴ� ���̺귯��
using System.Text;
using Newtonsoft.Json;  // JSON�� ����ϱ� ���� ���̺귯��
using UnityEngine.UI;   // �Ť�fmf xhdgotj vozlt qusrud

public class MyData
{
    public string clientID; // �������� �����ؼ� Ŭ���̾�Ʈ�� ���� �� ��
    public string message;
    public int requestType; // ��û ��ȣ json�� ����
}

public class InfoData   // �������� ������ ��Ŷ ����
{
    public string type;
    public InfoParams myParams;
}

public class InfoParams // �������� ������ ��Ŷ ���� (����)
{
    public string room;
    public int loopTimeCount;
}

public class SocketClient : MonoBehaviour
{
    private WebSocket webSocket;
    private bool isConnected = false;
    private int connectionAttempt = 0;  // ���� �õ� Ƚ��
    private const int maxConnectionAttempts = 3;    // �ִ� ���� �õ� Ƚ��

    MyData sendData = new MyData { message = "�޼��� ����" };

    public Button sendButton;   // JSON ���� ��ư
    public Button ReconnectButton;  // ������ ������ �� �ٽ� �����ϴ� ��ư
    public Text typeText;   // �޼��� ���� ������ �޾ƿͼ� ��Ŷ�� ������ ���� ����
    public Text messageText;
    public Text uiLoopCountText;    // ���� ī��Ʈ Ȯ���� ���� UI

    public int loopCount;

    // Start is called before the first frame update
    void Start()
    {
        ConnectWebSocket();

        sendButton.onClick.AddListener(() =>    // SEND ��ư�� ������ ��
        {
            // JSON ������ ����
            sendData.requestType = int.Parse(typeText.text);    // 0, 10, 200, 300
            sendData.message = messageText.text;
            string jsonData = JsonConvert.SerializeObject(sendData);

            webSocket.Send(jsonData);   // webSocket���� JSON ������ ����
        });

        ReconnectButton.onClick.AddListener(() =>
        {
            ConnectWebSocket();
        });
    }

    void ConnectWebSocket()
    {
        webSocket = new WebSocket("ws://localhost:8000");   // localhost 127.0.0.1 port : 8000, ws => websocket
        webSocket.OnOpen += OnWebSocketOpen;    // �� ��Ĺ�� ����Ǿ��� �� �̺�Ʈ�� �߻����Ѽ� �Լ��� ���� ��Ų��.
        webSocket.OnMessage += OnWebSocketMessage;  // �� ��Ĺ �޼����� ���� �� �̺�Ʈ�� �߻����� Message �Լ��� ���� ��Ų��.
        webSocket.OnClose += OnWebSocketClose;  // �� ��Ĺ ������ �������� �� �̺�Ʈ�� �߻� ���� Close �Լ��� ���� ��Ų��.

        webSocket.ConnectAsync();
    }

    void OnWebSocketOpen(object sender, System.EventArgs e) // �� ��Ĺ�� ���µǰ� ���� �Ǿ��� ��
    {
        Debug.Log("WebSocket connected");
        isConnected = true;
        connectionAttempt = 0;
    }

    void OnWebSocketMessage(object sender, MessageEventArgs e)  // �� ��Ĺ�� ����� �� Message�� ���� ��
    {
        string jsonData = Encoding.Default.GetString(e.RawData);    // MessageEventArgs�� ���� RawData�� Json���� ���ڵ��Ѵ�.
        Debug.Log("Received JSON data : " + jsonData);

        MyData receivedData = JsonConvert.DeserializeObject<MyData>(jsonData);  // JSON �����͸� ��ü�� ������ȭ

        InfoData infoData = JsonConvert.DeserializeObject<InfoData>(jsonData);

        if(infoData != null)
        {
            string room = infoData.myParams.room;
            loopCount = infoData.myParams.loopTimeCount;
        }

        if(receivedData != null & !string.IsNullOrEmpty(receivedData.clientID)) // receivedData ���� ��� ���� ���� ��
        {
            sendData.clientID = receivedData.clientID;  // �������� �޾ƿ� ID ���� MyData�� �ִ´�.
        }
    }

    void OnWebSocketClose(object sender, CloseEventArgs e)  // �� ��Ĺ ������ ������ ��
    {
        Debug.Log("WebSocket connection closed");
        isConnected = false;    // ���� ���� flag

        if(connectionAttempt < maxConnectionAttempts)   // �� 3���� �õ�
        {
            connectionAttempt++;
            Debug.Log("Attempting to reconnect. Attempt : " + connectionAttempt);
            ConnectWebSocket(); // Connect �õ��� �Ѵ�.
        }
        else
        {
            Debug.Log("Failed to connect ");
        }

    }

    private void OnApplicationQuit()    // ���α׷� ���� �ÿ� ȣ��Ǵ� �Լ�
    {
        DisconnectWebSocket();
    }

    void DisconnectWebSocket()  // ����� socket�� Release ���ش�.
    {
        if(webSocket != null && isConnected)
        {
            webSocket.Close();
            isConnected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(webSocket == null || !isConnected)
        {
            return;
        }

        uiLoopCountText.text = "LoopCount : " + loopCount.ToString();

        if(Input.GetKeyDown(KeyCode.Space)) // ������ �Ǿ����� �� SpaceŰ�� ������ Json Data�� �ѱ⵵��.
        {
            sendData.requestType = 0;
            string jsonData = JsonConvert.SerializeObject(sendData);    // Mydata�� Json���� �������.
            // �� ���¿����� ���� �ۼ��� line22. '�޼��� ����' �� �� ����

            webSocket.Send(jsonData);
        }
    }
}