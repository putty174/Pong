using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;


public class TSock : MonoBehaviour
{
	int[] buffer;
	byte[] bytes;
	byte[] milliHold;
	private NetworkStream ns;
	private Client sock;


	public TSock (NetworkStream nsIn, Client sIn)
	{
		ns = nsIn;
		sock = sIn;
		bytes = new byte[7];
		milliHold = new byte[2];
	}

	public void process()
	{
		try
		{
			while (sock.isConnected)
			{
				if(ns.DataAvailable)
				{




					ns.Read(bytes,0,bytes.Length);
					milliHold[0] = bytes[5];
					milliHold[1] = bytes[6];
					int milli = BitConverter.ToInt16(milliHold,0);

						GameProcess.opPosY = (int)bytes[0];
						GameProcess.ballPosX = (int)bytes[1];
						GameProcess.ballPosY = (int)bytes[2];
						GameProcess.min = (int)bytes[3];
						GameProcess.sec = (int)bytes[4];
						GameProcess.milli = milli;
						Debug.Log(GameProcess.opPosY + ", " + GameProcess.ballPosX + ", " + GameProcess.ballPosY + ", " + GameProcess.min + ", " + GameProcess.sec + ", " + GameProcess.milli);

				}


//				if(!trash)
//				{
//					lock(sock.receiverBuffer)
//					{
//						sock.receiverBuffer.Enqueue((int)bytes[0]);
//						sock.receiverBuffer.Enqueue((int)bytes[1]);
//						sock.receiverBuffer.Enqueue((int)bytes[2]);
//						sock.receiverBuffer.Enqueue((int)bytes[3]);
//						sock.receiverBuffer.Enqueue((int)bytes[4]);
//						sock.receiverBuffer.Enqueue(milli);
//					}
//				}


//				ns.Read(bytes, 0, bytes.Length);
//				byte[] milliHold = new byte[2];
//				milliHold[0] = bytes[5];
//				milliHold[1] = bytes[6];
//				int milli = BitConverter.ToInt16(milliHold,0);
//				//Debug.Log(bytes[0] + ", " + bytes[1] + ", " + bytes[2] + ", " + bytes[3] + ", " + bytes[4] + ", " + milli);
//				for(int i = 0; i < 7; i++)
//				{
//					if(bytes[i] == 0)
//					{
//						trash = true;
//					}
//				}
//				if(sock.receiverBuffer.Count > Client.maxLimit - 6)
//				{
//					ns.Read (bytes,0,bytes.Length);
//				}
//				trash = false;
//				ns.Read(bytes, 0, bytes.Length);
//
//				milliHold[0] = bytes[5];
//				milliHold[1] = bytes[6];
//				int milli = BitConverter.ToInt16(milliHold,0);
//				//Debug.Log(bytes[0] + ", " + bytes[1] + ", " + bytes[2] + ", " + bytes[3] + ", " + bytes[4] + ", " + milli);
//				for(int i = 0; i < 7; i++)
//				{
//					if(bytes[i] == 0)
//					{
//						trash = true;
//					}
//				}
//				if(!trash)
//				{
//					lock(sock.receiverBuffer)
//					{
//						GameProcess.opPosY = (int)bytes[0];
//						GameProcess.ballPosX = (int)
//						sock.receiverBuffer.Enqueue((int)bytes[0]);
//						sock.receiverBuffer.Enqueue((int)bytes[1]);
//						sock.receiverBuffer.Enqueue((int)bytes[2]);
//						sock.receiverBuffer.Enqueue((int)bytes[3]);
//						sock.receiverBuffer.Enqueue((int)bytes[4]);
//						sock.receiverBuffer.Enqueue(milli);
//					}
//				}

			}
		}
		catch(Exception e)
		{
			print("TSock Exception: " + e.Message);
		}
	}
}
