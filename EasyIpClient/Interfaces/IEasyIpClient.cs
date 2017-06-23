using System.Net.EasyIp.Enums;

namespace System.Net.EasyIp.Interfaces
{
    public interface IEasyIpClient : IDisposable
    {
        T[] BatchReadWord<T>(short point, DataTypeEnum dataType, byte length);
        void BatchWriteWord<T>(short point, T[] val, DataTypeEnum dataType);
    }
}
