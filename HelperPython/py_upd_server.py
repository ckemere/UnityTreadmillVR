import socket
 

localIP     = "127.0.0.1"
localPort   = 20001
bufferSize  = 1024

 

msgFromServer       = "Hello UDP Client"
bytesToSend         = str.encode(msgFromServer)

 

# Create a datagram socket
UDPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)


# Bind to address and ip
UDPServerSocket.bind((localIP, localPort))

 
print("UDP server up and listening")
 

import struct

# Listen for incoming datagrams

while(True):

    bytesAddressPair = UDPServerSocket.recvfrom(bufferSize)
    message = bytesAddressPair[0]
    address = bytesAddressPair[1]

    timestamp, speed = struct.unpack('Ih',message)
    #clientMsg = "Message from Client:{}".format(message)
    clientMsg = "Message from Client:{} - {}".format(timestamp, speed)
    clientIP  = "Client IP Address:{}".format(address)
    
    print(clientMsg)
    print(clientIP)

   

    # Sending a reply to client

    UDPServerSocket.sendto(bytesToSend, address)
