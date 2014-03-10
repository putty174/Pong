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
