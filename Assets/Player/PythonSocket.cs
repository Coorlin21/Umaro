using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class PoseReceiver : MonoBehaviour
{
    UdpClient udpClient;
    Thread receiveThread;
    Player player;
    public int port = 5005;
    public int currentPoseId = 0, currentPositionId = 0, currentVerticalId = 0;

    void Start()
    {
        udpClient = new UdpClient(port);
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        player = GetComponent<Player>();
    }

    void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        while (true)
        {
            try
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                string json = Encoding.UTF8.GetString(data);
                PoseData pose = JsonUtility.FromJson<PoseData>(json);
                currentPoseId = pose.pose_id;
                currentPositionId = pose.position_id;
                currentVerticalId = pose.vertical_id;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }

    [System.Serializable]
    public class PoseData
    {
        public int pose_id, position_id, vertical_id;
    }

    void Update()
    {
        if (currentVerticalId == 1)
        {
            player.Jump();
        }
        else if(currentVerticalId == -1)
        {
            player.Crouch();
        }
        else if(currentVerticalId == 0)
        {
            player.Stand();
        }
        player.position = currentPositionId;
        player.pose = currentPoseId;
    }

    void OnApplicationQuit()
    {
        receiveThread.Abort();
        udpClient.Close();
    }
}
