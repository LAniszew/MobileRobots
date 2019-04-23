using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using Driver.AutomaticControl;
using Driver.Helpers;
using OpenTK.Input;

namespace Driver
{
    public class KeyboardController : IController
    {
       
        public ControllerInfo process(TcpClient socket)
        {
            var state = Keyboard.GetState();
            int motorL = 0, motorR = 0;
            Debug.Write("");
            try
            {
                if (state.IsKeyDown(Key.W))
                {    
                    motorL = motorR = Constants.max_speed;

                    if (state.IsKeyDown(Key.A))
                    {
                        motorL = (int) (Constants.max_speed * Constants.turning_multipler);
                    }
                    else if (state.IsKeyDown(Key.D))
                    {
                        motorR = (int) (Constants.max_speed * Constants.turning_multipler);
                    }
                }

                if (state.IsKeyDown(Key.S))
                {
                    motorL = motorR = -Constants.max_speed;

                    if (state.IsKeyDown(Key.A))
                    {
                        motorR = (int) (Constants.max_speed * Constants.turning_multipler);
                    }
                    else if (state.IsKeyDown(Key.D))
                    {
                        motorL = (int) (Constants.max_speed * Constants.turning_multipler);
                    }
                }

                if (state.IsKeyDown(Key.A) && !IsDirectionKeyPressed(state))
                {
                    motorR = (int) (Constants.max_speed * Constants.turning_multipler);
                }

                if (state.IsKeyDown(Key.D) && !IsDirectionKeyPressed(state))
                {
                    motorL = (int) (Constants.max_speed * Constants.turning_multipler);
                }

                if (state.IsKeyDown(Key.Space))
                {
                    motorL = motorR = 0;
                }

                if (state.IsKeyDown((Key.Escape)))
                {
                    throw new Exception();
                    
                }

            }
            catch (Exception)
            {
                Environment.Exit(-1);
             
            }
     /*       var _motorL = motorL.ToString();
            var _motorR = motorR.ToString();
            
            if (_motorL.Length <= 1)
            {
              _motorL = string.Concat(motorL.ToString(), "0");
            }
            if (_motorR.Length <= 1)
            {
                _motorR = string.Concat(motorR.ToString(), "0");
            }*/
            
            return new ControllerInfo(motorL, motorR);
        }

        private static bool IsDirectionKeyPressed(KeyboardState state)
        {
            return state.IsKeyDown(Key.W) || state.IsKeyDown(Key.S);
        }
    }
}
