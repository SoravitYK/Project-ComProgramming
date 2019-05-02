using UnityEngine;
using UnityEngine.UI;
using System;
//using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using UnityEngine.SceneManagement;


public class client_1 : MonoBehaviour {

    static public float[] axis_got = { 0, 0, 0};
    private string[] axisss = {"", "", ""};
    [SerializeField] private string server_ip = "192.168.43.172";
    #region private members 	
    private TcpClient socketConnection; 	
	private Thread clientReceiveThread;
    public InputField ipp;
    #endregion
    // Use this for initialization 	
    void Start () {
    }  	
	// Update is called once per frame
	void Update () {         
		if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Space down");   
		}    
	}  	
	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void ConnectToTcpServer () { 		
		try {
            Debug.Log("Starting...controller event");
            GameObject player1 = GameObject.Find("Sphere");
            Control_a control_a = player1.GetComponent<Control_a>();

            clientReceiveThread = new Thread (new ThreadStart(() => ListenForData(control_a))); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();  		
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e); 		
		} 	
	}  	
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData(Control_a control_a) { 		
		try {
            // This class makes it super easy to do network stuff
            var client = new TcpClient();
            //if (SceneManager.GetActiveScene().name != "start_level") {
            //   client.Connect(PlayerPrefs.GetString("IP"), 26);
            //}
            //else
            //{
            //    client.Connect(server_ip, 26);
            //}
            client.Connect(server_ip, 26);
            Debug.Log("Connecting to" + server_ip);
            var stream = new StreamReader(client.GetStream());
            var buffer = new List<byte>();
            while (client.Connected) {
                // Read the next byte
                var read = stream.Read();
                if (read == 13) {
                    // Once we have a reading, convert our buffer to a string, since the values are coming as strings
                    var str = Encoding.ASCII.GetString(buffer.ToArray());

                    //Debug.Log(str);
                    //Debug.Log("end");
                    string[] str2 = str.Split(',');
                    //Debug.Log(str);
                    //Debug.Log(str2[0]+" "+str2[1]+" "+str2[2]+" "+str2[3]);
                    control_a.get_input[0, 0] = (float.Parse(str2[0]));
                    control_a.get_input[0, 1] = (float.Parse(str2[1]));
                    control_a.get_input[0, 2] = (float.Parse(str2[2]));
                    control_a.get_input[1, 0] = (float.Parse(str2[3]));
                    control_a.get_input[1, 1] = (float.Parse(str2[4]));
                    control_a.get_input[1, 2] = (float.Parse(str2[5]));
                    control_a.get_input[2, 0] = (float.Parse(str2[6]));
                    control_a.get_input[2, 1] = (float.Parse(str2[7]));
                    control_a.get_input[2, 2] = (float.Parse(str2[8]));
                    control_a.temp_c = float.Parse(str2[9]);
                    control_a.get_butt = !Convert.ToBoolean(int.Parse(str2[10]));

                    //PlayerPrefs.SetFloat("get_input[0, 0]", float.Parse(str2[0]));
                    //PlayerPrefs.SetFloat("get_input[0, 1]", float.Parse(str2[1]));

                    Debug.Log(str2[10]);
                    //Debug.Log(control_a.get_input[0, 0].ToString()+" | "+ control_a.get_input[0, 1].ToString() + " | " + control_a.get_input[0, 2].ToString());
                    // Clear the buffer ready for another reading
                    buffer.Clear();
                }
                else {
                    buffer.Add((byte)read);
                }
            }     
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}  	
 
    public void string_textfield(string text)
    {
        server_ip = text;
        PlayerPrefs.SetString("IP", text);
        Debug.Log(text);
    }

    public void butt_pressed()
    {
        server_ip = ipp.text;
        PlayerPrefs.SetString("IP", ipp.text);
        Debug.Log(ipp.text);
        ConnectToTcpServer();
    }
}