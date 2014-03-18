using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;


public class TSock : MonoBehaviour
{
	byte[] bytes;
	private NetworkStream ns;
	private int buffer;
	private Client sock;
	private int serverLength = 21;


	public TSock (NetworkStream nsIn, Client sIn)
	{
		ns = nsIn;
		sock = sIn;
		bytes = new byte[7];
	}

	public void process()
	{
		try
		{
			while (sock.isConnected)
			{
				buffer = ns.Read(bytes, 0, bytes.Length);
				Debug.Log(buffer);
				lock(sock.receiverBuffer)
				{
					//Debug.Log(buffer);
					sock.receiverBuffer.Enqueue(buffer);

//					if(sock.receiverBuffer.Count > serverLength)
//					{
//						for(int i = 0; i < 7; i++) {
//							sock.receiverBuffer.Dequeue();
//						}
//					}
				}
			}
		}
		catch(Exception e)
		{
			print("TSock Exception: " + e.Message);
		}
	}
}
