using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace PSReCSDKLib
{
    public class CameraControl
    {
        // fields
        private PRApi m_api;
        private PrDeviceList m_deviceList;
        private uint m_cameraHandle;
        private int m_accepted;
        private Timer m_timer;
        private ViewDataCallback m_viewDataCallback;
        private GetFileCallback m_getFileCallback;
        private PRApi.prViewFinderCB m_prViewFinderCallback;
        private PRApi.prGetFileDataCB m_prGetFileCallback;
        private PRApi.prSetEventCB m_setEventCB;
        private uint m_ObjdectHandle;
        private byte[] m_buffer;
        private ZoomPropDesc m_zoomDesc;

        // public methods
        public CameraControl()
        {
            this.m_api = new PRApi();
        }

        public void InitCamera()
        {
            uint response = 0;
            response = this.m_api.StartSDK();
            if (!VerifyReponse(response))
            {
                throw new Exception("StartSDK");
            }
            response = this.m_api.GetDeviceList(out this.m_deviceList);
            if (!VerifyReponse(response) || this.m_deviceList.NumList == 0)
            {
                throw new Exception("GetDeviceList");
            }
            response = this.m_api.CreateCameraOject(ref this.m_deviceList.DeviceInfo[0], out this.m_cameraHandle);
            if (!VerifyReponse(response))
            {
                throw new Exception("CreateCameraObject");
            }
            response = this.m_api.ConnectCamera(this.m_cameraHandle);
            if (!VerifyReponse(response))
            {
                throw new Exception("ConnectCamera");
            }
        }

        public void SetRelease(ViewDataCallback viewDataCallback, GetFileCallback getFileCallback)
        {
            // Handle Callbacks
            if (viewDataCallback == null || getFileCallback == null)
            {
                throw new ArgumentNullException("Callbacks");
            }
            this.m_viewDataCallback = viewDataCallback;
            this.m_getFileCallback = getFileCallback;
            // Init ReleaseControl
            uint response = 0;
            response = this.m_api.InitiateReleaseControl(this.m_cameraHandle);
            if (!VerifyReponse(response))
            {
                throw new Exception("InitiateReleaseControl");
            }
            // Init ViewData Timer
            this.m_timer = new Timer(new TimerCallback((o) =>
            {
                if (Thread.VolatileRead(ref this.m_accepted) == 1)
                {
                    return;
                }
                Thread.VolatileWrite(ref this.m_accepted, 1);
            }), null, 0, 100);
            // Register ViewFinder
            this.m_prViewFinderCallback = new PRApi.prViewFinderCB(MyViewFinderCB);
            response = this.m_api.RC_StartViewFinder(this.m_cameraHandle, m_prViewFinderCallback);
            if (!VerifyReponse(response))
            {
                throw new Exception("RC_StartViewFinder");
            }
            // Register SetEvent
            this.m_setEventCB = new PRApi.prSetEventCB(MySetEventCB);
            response = this.m_api.SetEventCallback(this.m_cameraHandle, this.m_setEventCB);
            if (!VerifyReponse(response))
            {
                throw new Exception("SetEventCallback");
            }
        }

        public void Release()
        {
            uint response = 0;
            // Verify Available Shot
            uint num = 0;
            response = this.m_api.RC_GetNumAvailableShot(this.m_cameraHandle, out num);
            if (!VerifyReponse(response) && num > 0)
            {
                throw new Exception("RC_GetNumAvailableShot");
            }
            // Set Transfer Mode
            ushort value = 0x0002 | 0x0008;
            response = this.m_api.SetDevicePropValue(this.m_cameraHandle, DevicePropCode.prPTP_DEV_PROP_CAPTURE_TRANSFER_MODE, value);
            if (!VerifyReponse(response))
            {
                throw new Exception("SetDevicePropValue");
            }
            // Reister GetFileCallback
            if (this.m_prGetFileCallback == null)
            {
                this.m_prGetFileCallback = new PRApi.prGetFileDataCB(MyGetFileDataCB);
            }
            // Release
            response = this.m_api.RC_Release(this.m_cameraHandle);
            if (!VerifyReponse(response))
            {
                throw new Exception("RC_Release");
            }
            this.GetReleaseData();
        }

        public void CloseCamera()
        {
            this.m_api.TerminateReleaseControl(this.m_cameraHandle);
            this.m_api.DisconnectCamera(this.m_cameraHandle);
            this.m_api.FinishSDK();
        }
        
        // private methods
        private bool VerifyReponse(uint response)
        {
            PR_Error error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;
            return error == PR_Error.prOK;
        }

        private void GetReleaseData()
        {
            // GetFile
            uint response = 0;
            response = this.m_api.RC_GetReleasedData(this.m_cameraHandle, this.m_ObjdectHandle, PrptpEventCode.prPTP_FULL_VIEW_RELEASED, 1024000, m_prGetFileCallback);
            if (!VerifyReponse(response))
            {
                throw new Exception("RC_GetReleasedData");
            }
        }

        private uint MyViewFinderCB(uint cameraHandle, uint context, uint size, IntPtr pVFData)
        {
            if (size > 0)
            {
                byte[] buffer = new byte[size];
                Marshal.Copy(pVFData, buffer, 0, (int)size);
                MemoryStream stream = new MemoryStream(buffer);
                this.m_viewDataCallback.Invoke(stream);
            }
            return 0;
        }

        private uint MyGetFileDataCB(uint cameraHandle, uint objectHandle, uint context, ref PrProgress pProgress)
        {
            if (pProgress.lMessage == PrProgressMsg.prMSG_DATA_HEADER)
            {
                this.m_buffer = new byte[0];
            }
            if (pProgress.lMessage == PrProgressMsg.prMSG_DATA)
            {
                uint length = pProgress.lOffset + pProgress.lLength;
                Array.Resize(ref this.m_buffer, (int)length);
                IntPtr pData = new IntPtr((int)pProgress.pbData);
                Marshal.Copy(pData, this.m_buffer, (int)pProgress.lOffset, (int)pProgress.lLength);
            }
            if (pProgress.lMessage == PrProgressMsg.prMSG_TERMINATION)
            {
                this.m_getFileCallback.Invoke(this.m_buffer);
            }
            return (uint)PR_Error.prOK;
        }

        private uint MySetEventCB(uint cameraHanlde, uint context, IntPtr pEventData)
        {
            var data = (EventGenericContainer)Marshal.PtrToStructure(pEventData, typeof(EventGenericContainer));
            if (data.Code == (ushort)PrptpEventCode.prPTP_FULL_VIEW_RELEASED)
            {
                this.m_ObjdectHandle = data.Parameter[0];
            }
            return 0;
        }
    }
}