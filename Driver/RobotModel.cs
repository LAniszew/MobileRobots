﻿using System;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using System.Diagnostics;
 using System.Linq;
 using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
 using System.Windows;
 using System.Windows.Forms.DataVisualization.Charting;
 using System.Windows.Media.Animation;
 using Driver;
 using Driver.AutomaticControl;
 using Driver.Helpers;
 using MahApps.Metro.Controls;

namespace Driver
{
    public class RobotModel
    {
        private readonly string IP;
        private readonly int port;
        public readonly TcpClient socket;
        private readonly ServerService ServerService;


        public RobotModel(string ip, int port)
        {
            socket = new TcpClient();
            IP = ip;
            this.port = port;
            //Regulator = new Regulator();
            ServerService = new ServerService();
        }

        public async void connectAsync()
        {
            if(!socket.Connected) {
                try
                {
                    await socket.ConnectAsync(IP, port);
                }
                catch (Exception ex)
                {
                  Debug.WriteLine(ex);
                }
            }
        }

        public void disconnect()
        {
            if(socket.Connected) {
             socket.Client.Disconnect(true);
            }
        }

        public void release()
        {
            if (socket.Connected)
            {
                ServerService.releaseRobots(socket);
            }
        }

        public async Task Drive(int rightMotor, int leftMotor)
        {
            if (!socket.Connected)
            {
                return;
            }
            ServerService.drivePercent(socket, rightMotor, leftMotor);
            ServerService.getCoordiantes(socket);
        }

        public async Task connectRobot()
        {
            if (!socket.Connected)
            {
                Debug.Write("socket disconnected /n");
                return;
            }
           // ServerService.releaseRobots(socket);
            ServerService.getRobot(socket, Constants.CurrentId);
        } 

        public void reidentify()
        {
            if (!socket.Connected)
            {
                Debug.Write("socket disconnected /n");
                return;
            }
            ServerService.reidentify(socket);
        }

        public bool status()
        {
            return socket.Connected;
        }
    }
}