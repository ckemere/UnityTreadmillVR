import socket

serverAddressPort   = ("127.0.0.1", 20001)

# Create a UDP socket at client side
UDPClientSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)

#%%
import numpy as np
snippet = np.load('/mnt/d/Development/Python/Networking/wheelsnippet.npy')
ANGULAR_RESOLUTION = 4096
speed = np.diff(np.round(snippet/360*ANGULAR_RESOLUTION)).astype('int16')


#%%
from multiprocessing import Queue
import threading
import time
import signal
import struct


# INTERVAL = 0.01
INTERVAL = 0.02
SLACK = 0.005

timestamp = 0
idx = 0

try:
    target = time.time() + INTERVAL
    while True:
        time.sleep(INTERVAL-SLACK)
        while (time.time() < target):
            pass
        t = time.time()
        target = target + INTERVAL

        timestamp = timestamp + int(INTERVAL * 1000)
        spd = speed[idx]
        bytesToSend = struct.pack('Ih', timestamp, spd)
        # bytesToSend = str.encode("Hello UDP Server {}".format(t))
        UDPClientSocket.sendto(bytesToSend, serverAddressPort)
        idx = idx + 1;
        if (idx >= len(speed)):
            idx = 0


except (KeyboardInterrupt, SystemExit):
    print('Quitting')
