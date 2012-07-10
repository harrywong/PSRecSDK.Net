using System;
using PSReCSDKLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PSReCSDKLib.Test
{
    [TestClass]
    public class PRApiTest
    {
        PRApi m_api;
        PrDllsVerInfo m_dllsVerInfo;
        PrDeviceList m_deviceList;
        uint m_cameraHandle;

        [TestMethod]
        public void StartSDKTest()
        {
            this.m_api = new PRApi();
            uint response;
            PR_Error error;

            response = this.m_api.StartSDK();
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
        }

        [TestMethod]
        public void FinishSDKTest()
        {
            StartSDKTest();
            uint response;
            PR_Error error;

            response = this.m_api.FinishSDK();
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
        }

        [TestMethod()]
        public void GetDllsVersionTest()
        {
            StartSDKTest();
            uint response;
            PR_Error error;

            response = this.m_api.GetDllsVersion(out m_dllsVerInfo);
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
            Assert.IsTrue(this.m_dllsVerInfo.Entry > 0);
        }

        [TestMethod()]
        public void GetDeviceListTest()
        {
            StartSDKTest();
            uint response;
            PR_Error error;

            response = this.m_api.GetDeviceList(out m_deviceList);
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
            Assert.IsTrue(this.m_deviceList.NumList > 0);
        }

        [TestMethod()]
        public void CreateCameraObjectTest()
        {
            StartSDKTest();
            GetDeviceListTest();
            uint response;
            PR_Error error;

            response = this.m_api.CreateCameraOject(ref this.m_deviceList.DeviceInfo[0], out this.m_cameraHandle);
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
            Assert.IsTrue(this.m_cameraHandle != 0);
        }

        [TestMethod()]
        public void DestroyCameraObjectTest()
        {
            StartSDKTest();
            GetDeviceListTest();
            CreateCameraObjectTest();
            uint response;
            PR_Error error;

            response = this.m_api.DestoryCameraObject(this.m_cameraHandle);
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
        }

        [TestMethod()]
        public void ConnectCameraTest()
        {
            StartSDKTest();
            GetDeviceListTest();
            CreateCameraObjectTest();
            uint response;
            PR_Error error;

            response = this.m_api.ConnectCamera(this.m_cameraHandle);
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
        }

        [TestMethod()]
        public void DisconnectCameraTest()
        {
            StartSDKTest();
            GetDeviceListTest();
            CreateCameraObjectTest();
            ConnectCameraTest();
            uint response;
            PR_Error error;

            response = this.m_api.DisconnectCamera(this.m_cameraHandle);
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
        }

        [TestMethod()]
        public void InitiateReleaseControlTest()
        {
            StartSDKTest();
            GetDeviceListTest();
            CreateCameraObjectTest();
            ConnectCameraTest();
            uint response;
            PR_Error error;

            response = this.m_api.InitiateReleaseControl(this.m_cameraHandle);
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
        }

        [TestMethod()]
        public void TerminateReleaseControlTest()
        {
            StartSDKTest();
            GetDeviceListTest();
            CreateCameraObjectTest();
            ConnectCameraTest();
            InitiateReleaseControlTest();
            uint response;
            PR_Error error;

            response = this.m_api.TerminateReleaseControl(this.m_cameraHandle);
            error = (PR_Error)response & PR_Error.prERROR_ERRORID_MASK;

            Assert.AreEqual(error, PR_Error.prOK, error.ToString());
        }
    }
}