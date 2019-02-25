import socket

msgFromClient       = "Hello UDP Server"
bytesToSend         = str.encode(msgFromClient)
serverAddressPort   = ("127.0.0.1", 20001)

bufferSize          = 1024

 

# Create a UDP socket at client side
UDPClientSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)

# Send to server using created UDP socket
for idx in range(10):
    bytesToSend         = str.encode("Hello UDP Server {}".format(idx))
    UDPClientSocket.sendto(bytesToSend, serverAddressPort)

for idx in range(10):
    msgFromServer = UDPClientSocket.recvfrom(bufferSize)
    msg = "Message from Server {}".format(msgFromServer[0])
    print(msg)
