using System.IO;

namespace PSReCSDKLib
{
    public delegate void ViewDataCallback(Stream viewDataStream);
    public delegate void GetFileCallback(byte[] bytes);
}