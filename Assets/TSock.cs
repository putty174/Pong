using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class TSock : MonoBehaviour
{
	private NetworkStream ns;
	private byte[] buffer;
	private Client sock;

	public TSock (NetworkStream nsIn, Client sIn)
	{
		ns = nsIn;
		sock = sIn;
		buffer = new byte[1024];
	}

	public void process()
	{
		try
		{
			while (sock.isConnected)
			{
				ns.Read(buffer,0,buffer.Length);
				lock(sock.receiverBuffer)
				{
					sock.receiverBuffer.Enqueue(buffer);
				}
			}
		}
		catch(Exception e)
		{
			print("TSock Exception: " + e.Message);
		}
	}
}
