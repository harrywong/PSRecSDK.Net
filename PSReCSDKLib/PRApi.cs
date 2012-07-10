using System;
using System.Runtime.InteropServices;

namespace PSReCSDKLib
{
    public class PRApi
    {
        readonly int BUFFER_SIZE = 10240;

        [DllImport("PRSDK.dll")]
        public static extern uint PR_StartSDK();

        public uint StartSDK()
        {
            return PR_StartSDK();
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_FinishSDK();

        public uint FinishSDK()
        {
            return PR_FinishSDK();
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_GetDllsVersion(ref uint pBufferSize, IntPtr pDllVersion);

        public uint GetDllsVersion(out PrDllsVerInfo pDllVersion)
        {
            uint size = (uint)BUFFER_SIZE;
            uint response = 0;

            IntPtr pSpace = Marshal.AllocHGlobal((int)size);
            response = PR_GetDllsVersion(ref size, pSpace);

            pDllVersion = (PrDllsVerInfo)Marshal.PtrToStructure(pSpace, typeof(PrDllsVerInfo));
            Marshal.FreeHGlobal(pSpace);
            return response;
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_GetDeviceList(ref uint pBufferSize, IntPtr pDeviceList);

        public uint GetDeviceList(out PrDeviceList pDeviceList)
        {
            uint size = (uint)BUFFER_SIZE;
            uint response = 0;

            IntPtr pSpace = Marshal.AllocHGlobal((int)size);
            response = PR_GetDeviceList(ref size, pSpace);

            pDeviceList = (PrDeviceList)Marshal.PtrToStructure(pSpace, typeof(PrDeviceList));
            Marshal.FreeHGlobal(pSpace);
            return response;
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_CreateCameraObject(ref PrDeviceInfoTable pDeviceInfo, out uint pCameraHandle);

        public uint CreateCameraOject(ref PrDeviceInfoTable pDeviceInfo, out uint pCameraHandle)
        {
            return PR_CreateCameraObject(ref pDeviceInfo, out pCameraHandle);
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_DestroyCameraObject([In]
                                                         uint CameraHandle);

        public uint DestoryCameraObject(uint cameraHandle)
        {
            return PR_DestroyCameraObject(cameraHandle);
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_ConnectCamera([In]
                                                   uint CameraHandle);

        public uint ConnectCamera(uint cameraHandle)
        {
            return PR_ConnectCamera(cameraHandle);
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_DisconnectCamera([In]
                                                      uint CameraHandle);

        public uint DisconnectCamera(uint cameraHandle)
        {
            return PR_DisconnectCamera(cameraHandle);
        }

        public delegate uint prSetEventCB(uint CameraHandle, uint Context, IntPtr pEventData);
        [DllImport("PRSDK.dll")]
        public static extern uint PR_SetEventCallBack(uint CameraHandle, uint Context, IntPtr pSetEventCB);

        public uint SetEventCallback(uint cameraHandle, prSetEventCB prSetEventCB)
        {
            return PR_SetEventCallBack(cameraHandle, 0, Marshal.GetFunctionPointerForDelegate(prSetEventCB));
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_ClearEventCallBack(uint CameraHandle);

        public uint ClearEventCallback(uint cameraHanlde)
        {
            return PR_ClearEventCallBack(cameraHanlde);
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_InitiateReleaseControl([In]
                                                            uint CameraHandle);

        public uint InitiateReleaseControl(uint cameraHandle)
        {
            return PR_InitiateReleaseControl(cameraHandle);
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_TerminateReleaseControl([In]
                                                             uint CameraHandle);

        public uint TerminateReleaseControl(uint cameraHandle)
        {
            return PR_TerminateReleaseControl(cameraHandle);
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_RC_Release([In]
                                                uint CameraHandle);

        public uint RC_Release(uint cameraHandle)
        {
            return PR_RC_Release(cameraHandle);
        }

        public delegate uint prGetFileDataCB(uint CameraHandle, uint ObjectHandle, uint Context, ref PrProgress pProgress);
        [DllImport("PRSDK.dll")]
        public static extern uint PR_RC_GetReleasedData([In]
                                                        uint CameraHandle, [In]
                                                        uint ObjectHandle,
            [In]
            ushort EventCode, [In]
            uint TransSize, [In]
            uint Context, [In]
            IntPtr pGetFileDataCB);

        public uint RC_GetReleasedData(uint cameraHandle, uint objectHandle, PrptpEventCode eventCode, uint transSize, prGetFileDataCB pGetFileDataCB)
        {
            uint response = 0;
            IntPtr pFunc = Marshal.GetFunctionPointerForDelegate(pGetFileDataCB);
            response = PR_RC_GetReleasedData(cameraHandle, objectHandle, (ushort)eventCode, transSize, 0, pFunc);
            return response;
        }

        public delegate uint prViewFinderCB(uint CameraHandle, uint Context, uint Size, IntPtr pVFData);
        [DllImport("PRSDK.dll")]
        public static extern uint PR_RC_StartViewFinder([In]
                                                        uint CameraHandle, [In]
                                                        uint Context, IntPtr pViewFinderCB);

        public uint RC_StartViewFinder(uint cameraHandle, prViewFinderCB pViewFinderCB)
        {
            uint response = 0;
            IntPtr pFunc = Marshal.GetFunctionPointerForDelegate(pViewFinderCB);
            response = PR_RC_StartViewFinder(cameraHandle, 0, pFunc);
            return response;
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_RC_TermViewFinder([In]
                                                       uint CameraHandle);

        public uint RC_TermViewFinder(uint cameraHandle)
        {
            return PR_RC_TermViewFinder(cameraHandle);
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_RC_GetNumAvailableShot([In]
                                                            uint CameraHandle, out uint pNum);

        public uint RC_GetNumAvailableShot(uint cameraHandle, out uint num)
        {
            return PR_RC_GetNumAvailableShot(cameraHandle, out num);
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_GetDevicePropDesc([In]
                                                       uint CameraHandle, [In]
                                                       ushort DevicePropCode, ref uint pBufferSize, IntPtr pDevicePropDesc);

        public uint GetZoomPropDesc(uint cameraHandle, out ZoomPropDesc propDesc)
        {
            uint response = 0;
            uint size = (uint)this.BUFFER_SIZE;
            IntPtr pSpace = Marshal.AllocHGlobal(this.BUFFER_SIZE);

            response = PR_GetDevicePropDesc(cameraHandle, (ushort)DevicePropCode.prPTP_DEV_PROP_ZOOM_POS, ref size, pSpace);
            if (response == 0)
            {
                propDesc = (ZoomPropDesc)Marshal.PtrToStructure(pSpace, typeof(ZoomPropDesc));
            }
            else
            {
                propDesc = null;
            }
            return response;
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_GetDevicePropValue([In]
                                                        uint CameraHandle, [In]
                                                        ushort DevicePropCode, ref uint pBufferSize, IntPtr pDevicePropDesc);

        public uint GetZoomPropValue(uint cameraHandle, out ZoomPropDesc propDesc)
        {
            uint response = 0;
            uint size = (uint)this.BUFFER_SIZE;
            IntPtr pPropDesc = Marshal.AllocHGlobal(this.BUFFER_SIZE);
            response = PR_GetDevicePropValue(cameraHandle, (ushort)DevicePropCode.prPTP_DEV_PROP_ZOOM_POS, ref size, pPropDesc);
            if (response == 0)
            {
                var upropDesc = (ushort)Marshal.PtrToStructure(pPropDesc, typeof(ushort));
                propDesc = null;
            }
            else
            {
                propDesc = null;
            }
            return response;
        }

        [DllImport("PRSDK.dll")]
        public static extern uint PR_SetDevicePropValue([In]
                                                        uint CameraHandle, [In]
                                                        ushort DevicePropCode, uint DataSize, IntPtr pDeviceProperty);

        public uint SetDevicePropValue(uint cameraHandle, DevicePropCode propCode, object value)
        {
            int size = Marshal.SizeOf(value);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value, ptr, true);
            return PR_SetDevicePropValue(cameraHandle, (ushort)propCode, (uint)size, ptr);
        }
    }
}