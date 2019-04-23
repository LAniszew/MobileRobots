using System.Net.Sockets;
using Driver.Annotations;

namespace Driver
{
    public interface IController
    {
        ControllerInfo process([CanBeNull] TcpClient socket);
    }
}