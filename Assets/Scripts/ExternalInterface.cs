// https://forum.unity.com/threads/simple-udp-implementation-send-read-via-mono-c.15900/ - Matthjis Kneppers

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct UDPMovementData
{
    public UInt32 timestamp;
    public Int16 speed;

    // public byte control;

}

public class UdpReceiver
{
    private UdpClient udpClient;

    Thread receiveThread;
    private bool threadRunning = false;

    // Queues for decoded message content
    private readonly Queue<Int16> incomingMovementQueue = new Queue<Int16>();
    //private readonly Queue<string> incomingEventQueue = new Queue<string>();

    public void StartReceiver(int receivePort)
    {
        try { udpClient = new UdpClient(receivePort); }
        catch (Exception e)
        {
            Debug.Log("Failed to listen for UDP at port " + receivePort + ": " + e.Message);
            return;
        }
        Debug.Log("Created receiving client at ip  and port " + receivePort);
 
        StartReceiveThread();
    }
 
    private void StartReceiveThread()
    {
        receiveThread = new Thread(() => ListenForMessages(udpClient));
        receiveThread.IsBackground = true;
        threadRunning = true;
        receiveThread.Start();
    }
 
    private void ListenForMessages(UdpClient client)
    {
        IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
 
        while (threadRunning)
        {
            try
            {
                Byte[] receiveBytes = client.Receive(ref remoteIpEndPoint); // Blocks until a message returns on this socket from a remote host.
                //string returnData = Encoding.UTF8.GetString(receiveBytes);

                // Parse receive bytes from movement data structure
                UDPMovementData[] receivedData = FromByteArray<UDPMovementData>(receiveBytes);

                lock (incomingMovementQueue)
                {
                    for (int i = 0; i < receivedData.Length; i++)
                        incomingMovementQueue.Enqueue(receivedData[i].speed);
                    //incomingQueue.Enqueue(returnData);
                }
            }
            catch (SocketException e)
            {
                // 10004 thrown when socket is closed
                if (e.ErrorCode != 10004) Debug.Log("Socket exception while receiving data from udp client: " + e.Message);
            }
            catch (Exception e)
            {
                Debug.Log("Error receiving data from udp client: " + e.Message);
            }
            Thread.Sleep(1);
        }
    }
 
    public Int16 getMovement()
    {
        Int16 pendingMovement = 0;
        lock (incomingMovementQueue)
        {
            //pendingMessages = new Int16[incomingMovementQueue.Count];
            if (incomingMovementQueue.Count > 1)
            {
                Debug.Log("Dequeuing multiple: " + incomingMovementQueue.Count);
            }
            int i = 0;
            while (incomingMovementQueue.Count != 0)
            {
                pendingMovement += incomingMovementQueue.Dequeue();
                i++;
            }
        }
 
        return pendingMovement;
    }

    public void Stop()
    {
        threadRunning = false;
        receiveThread.Abort();
        udpClient.Close();
    }


    private static T[] FromByteArray<T>(byte[] source) where T : struct
    {
        T[] destination = new T[source.Length / Marshal.SizeOf(typeof(T))];
        GCHandle handle = GCHandle.Alloc(destination, GCHandleType.Pinned);
        try
        {
            IntPtr pointer = handle.AddrOfPinnedObject();
            Marshal.Copy(source, 0, pointer, source.Length);
            return destination;
        }
        finally
        {
            if (handle.IsAllocated)
                handle.Free();
        }
    }
}