using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;


public class TSock : MonoBehaviour
{
	int[] buffer;
	byte[] bytes;
	private NetworkStream ns;
	private Client sock;
	bool trash;


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
				trash = false;
				ns.Read(bytes, 0, bytes.Length);
				byte[] milliHold = new byte[2];
				milliHold[0] = bytes[5];
				milliHold[1] = bytes[6];
				int milli = BitConverter.ToInt16(milliHold,0);
				//Debug.Log(bytes[0] + ", " + bytes[1] + ", " + bytes[2] + ", " + bytes[3] + ", " + bytes[4] + ", " + milli);
				for(int i = 0; i < 7; i++)
				{

					if(bytes[i] == 0)
					{
						trash = true;
					}
				}

				if(!trash)
				{
					lock(sock.receiverBuffer)
					{
						sock.receiverBuffer.Enqueue((int)bytes[0]);
						sock.receiverBuffer.Enqueue((int)bytes[1]);
						sock.receiverBuffer.Enqueue((int)bytes[2]);
						sock.receiverBuffer.Enqueue((int)bytes[3]);
						sock.receiverBuffer.Enqueue((int)bytes[4]);
						sock.receiverBuffer.Enqueue(milli);
					}
				}

			}
		}
		catch(Exception e)
		{
			print("TSock Exception: " + e.Message);
		}
	}
}
