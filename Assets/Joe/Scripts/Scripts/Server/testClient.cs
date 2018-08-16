using System;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

public class testClient{

	
    Socket SckSPort; // 先行宣告Socket

    string RmIp = "127.0.0.1";  // 其中 xxx.xxx.xxx.xxx 為Server端的IP

    int SPort = 6101;

    int RDataLen = 256;  // 此文Server端和Client端都是用固定長度5在傳送資料~ 可以針對自己的需要改長度 

    StartServer ss;
    Thread SckSReceiveTd;
    // 連線

    public void ConnectServer(string IP ,StartServer st)

    {
        RmIp = IP;
        ss = st;
        try

        {

            SckSPort = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SckSPort.Connect(new IPEndPoint(IPAddress.Parse(RmIp), SPort));

            // RmIp和SPort分別為string和int型態, 前者為Server端的IP, 後者為Server端的Port



            // 同 Server 端一樣要另外開一個執行緒用來等待接收來自 Server 端傳來的資料, 與Server概念同

            SckSReceiveTd = new Thread(SckSReceiveProc);
            //kSReceiveTd.IsBackground = true;
            SckSReceiveTd.Start();

        }
        catch
        {
            UnityEngine.Debug.Log("unable to connect to server");
            UnityEngine.Debug.Log("Create Local Server");
            ss.CreateServer();
            ConnectServer("127.0.0.1", ss);

        }

    }

    // SckSReceiveProc 是接受來自 Server 端的資料其函式內容幾乎同 Server 端的 SckSAcceptProc 接收資料的Code~  ,  只差在 Client 只有一個Socket. 



    private void SckSReceiveProc()
    {

        try
        {

            long IntAcceptData;

            byte[] clientData = new byte[RDataLen];

            while (true)
            {

                // 程式會被 hand 在此, 等待接收來自 Server 端傳來的資料

                IntAcceptData = SckSPort.Receive(clientData);

                // 往下就自己寫接收到來自Server端的資料後要做什麼事唄~^^”

                string S = Encoding.Default.GetString(clientData);

                ss.jSONOs.Add(S);
                
            }

        }

        catch
        {



        }
    }

    void oncc(string s)
    {
        ss.p(s);
    }

    // 當然 Client 端也可以傳送資料給Server端~ 和 Server 端的SckSSend一樣, 只差在Client端只有一個Socket

    public void SckSSend(string Data)

    {



        try

        {

           
            SckSPort.Send(Encoding.ASCII.GetBytes(Data));

        }

        catch

        {



        }





    }

    public void Quit()
    {
        SckSReceiveTd.Abort();
        //SckSReceiveTd.DisableComObjectEagerCleanup();
        SckSPort.Close();

    }
    // Use this for initialization
    /*void Start()
    {
        ConnectServer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("D");
            SckSSend();
        }
    }*/
}
