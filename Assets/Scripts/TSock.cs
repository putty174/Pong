using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class TSock : MonoBehaviour
{
	private NetworkStream ns;
	private int buffer;
	private Client sock;
	private int serverLength = 21;

	public TSock (NetworkStream nsIn, Client sIn)
	{
		ns = nsIn;
		sock = sIn;
	}

	public void process()
	{
		try
		{
			while (sock.isConnected)
			{
				buffer = ns.ReadByte();
				lock(sock.receiverBuffer)
				{
					Debug.Log(buffer);
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
