using System;
using System.Runtime.InteropServices;

namespace PSReCSDKLib
{
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct PrVerInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string ModuleName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Version;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class PrDllsVerInfo
    {
        public uint Entry;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public PrVerInfo[] VerInfo;
    }

    public enum PrPortType : ushort
    {
        prPORTTYPE_WIA = 0x0001,
        prPORTTYPE_STI = 0x0002
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct PrDeviceInfoTable
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string DeviceInternalName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string ModuleName;
        public ushort Generation;
        public uint Reserved1;
        public uint ModelID;
        public ushort Reserved2;
        public PrPortType PortType;
        public uint Reserved3;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class PrDeviceList
    {
        public uint NumList;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public PrDeviceInfoTable[] DeviceInfo;
    }

    public enum PrptpEventCode : ushort
    {
        prPTP_DEVICE_PROP_CHANGED = 0x4006,
        prPTP_CAPTURE_COMPLETE = 0x400D,
        prPTP_SHUTDOWN_CF_GATE_WAS_OPENED = 0xC001,
        prPTP_RESET_HW_ERROR = 0xC005,
        prPTP_ABORT_PC_EVF = 0xC006,
        prPTP_ENABLE_PC_EVF = 0xC007,
        prPTP_FULL_VIEW_RELEASED = 0xC008,
        prPTP_THUMBNAIL_RELEASED = 0xC009,
        prPTP_CHANGE_BATTERY_STATUS = 0xC00A,
        prPTP_PUSHED_RELEASE_SW = 0xC00B,
        prPTP_RC_PROP_CHANGED = 0xC00C,
        prPTP_RC_ROTATION_ANGLE_CHANGED = 0xC00D,
        prPTP_RC_CHANGED_BY_CAM_UI = 0xC00E,
        prCAL_SHUTDOWN = 0xD001
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct PrProgress
    {
        public PrProgressMsg lMessage;
        public int lStatus;
        public uint lPercentComplete;
        public uint lOffset;
        public uint lLength;
        public uint lReserved;
        public uint lResLength;
        public uint pbData;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct EventGenericContainer
    {
        public uint ContainerLength;
        public ushort ContainerType;
        public ushort Code;
        public uint TransactionID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] Parameter;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ZoomRangeForm
    {
        public byte MinValue;
        public byte MaxValue;
        public byte StepSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ZoomPropDesc
    {
        public ushort DataPropertyCode;
        public ushort DataType;
        public byte GetSet;
        public ushort FactoryDefaultValue;
        public ushort CurrentValue;
        public byte FormFlag;
        public ZoomRangeForm Form;
    }

    [Flags]
    public enum PrProgressMsg
    {
        prMSG_DATA_HEADER = 0x0001,
        prMSG_DATA = 0x0002,
        prMSG_TERMINATION = 0x0004
    }

    public enum DevicePropCode : ushort
    {
        prPTP_DEV_PROP_BUZZER = 0xD001,
        prPTP_DEV_PROP_BATTERY_KIND = 0xD002,
        prPTP_DEV_PROP_BATTERY_STATUS = 0xD003,
        prPTP_DEV_PROP_COMP_QUALITY = 0xD006,
        prPTP_DEV_PROP_FULLVIEW_FILE_FORMAT = 0xD007,
        prPTP_DEV_PROP_IMAGE_SIZE = 0xD008,
        prPTP_DEV_PROP_SELFTIMER = 0xD009,
        prPTP_DEV_PROP_STROBE_SETTING = 0xD00A,
        prPTP_DEV_PROP_BEEP = 0xD00B,
        prPTP_DEV_PROP_EXPOSURE_MODE = 0xD00C,
        prPTP_DEV_PROP_IMAGE_MODE = 0xD00D,
        prPTP_DEV_PROP_DRIVE_MODE = 0xD00E,
        prPTP_DEV_PROP_EZOOM = 0xD00F,
        prPTP_DEV_PROP_ML_WEI_MODE = 0xD010,
        prPTP_DEV_PROP_AF_DISTANCE = 0xD011,
        prPTP_DEV_PROP_FOCUS_POINT_SETTING = 0xD012,
        prPTP_DEV_PROP_WB_SETTING = 0xD013,
        prPTP_DEV_PROP_SLOW_SHUTTER_SETTING = 0xD014,
        prPTP_DEV_PROP_AF_MODE = 0xD015,
        prPTP_DEV_PROP_IMAGE_STABILIZATION = 0xD016,
        prPTP_DEV_PROP_CONTRAST = 0xD017,
        prPTP_DEV_PROP_COLOR_GAIN = 0xD018,
        prPTP_DEV_PROP_SHARPNESS = 0xD019,
        prPTP_DEV_PROP_SENSITIVITY = 0xD01A,
        prPTP_DEV_PROP_PARAMETER_SET = 0xD01B,
        prPTP_DEV_PROP_ISO = 0xD01C,
        prPTP_DEV_PROP_AV = 0xD01D,
        prPTP_DEV_PROP_TV = 0xD01E,
        prPTP_DEV_PROP_EXPOSURE_COMP = 0xD01F,
        prPTP_DEV_PROP_FLASH_COMP = 0xD020,
        prPTP_DEV_PROP_AEB_EXPOSURE_COMP = 0xD021,
        prPTP_DEV_PROP_AV_OPEN = 0xD023,
        prPTP_DEV_PROP_AV_MAX = 0xD024,
        prPTP_DEV_PROP_FOCAL_LENGTH = 0xD025,
        prPTP_DEV_PROP_FOCAL_LENGTH_TELE = 0xD026,
        prPTP_DEV_PROP_FOCAL_LENGTH_WIDE = 0xD027,
        prPTP_DEV_PROP_FOCAL_LENGTH_DENOMI = 0xD028,
        prPTP_DEV_PROP_CAPTURE_TRANSFER_MODE = 0xD029,
        prPTP_DEV_PROP_ZOOM_POS = 0xD02A,
        prPTP_DEV_PROP_SUPPORTED_SIZE = 0xD02C,
        prPTP_DEV_PROP_SUPPORTED_THUMB_SIZE = 0xD02D,
        prPTP_DEV_PROP_FIRMWARE_VERSION = 0xD031,
        prPTP_DEV_PROP_CAMERA_MODEL_NAME = 0xD032,
        prPTP_DEV_PROP_OWNER_NAME = 0xD033,
        prPTP_DEV_PROP_CAMERA_TIME = 0xD034,
        prPTP_DEV_PROP_CAMERA_OUTPUT = 0xD036,
        prPTP_DEV_PROP_DISP_AV = 0xD037,
        prPTP_DEV_PROP_AV_OPEN_APEX = 0xD038,
        prPTP_DEV_PROP_EZOOM_SIZE = 0xD039,
        prPTP_DEV_PROP_ML_SPOT_POS = 0xD03A,
        prPTP_DEV_PROP_DISP_AV_MAX = 0xD03B,
        prPTP_DEV_PROP_AV_MAX_APEX = 0xD03C,
        prPTP_DEV_PROP_EZOOM_START_POS = 0xD03D,
        prPTP_DEV_PROP_FOCAL_LENGTH_OF_TELE = 0xD03E,
        prPTP_DEV_PROP_EZOOM_SIZE_OF_TELE = 0xD03F,
        prPTP_DEV_PROP_PHOTO_EFFECT = 0xD040,
        prPTP_DEV_PROP_AF_LIGHT = 0xD041,
        prPTP_DEV_PROP_FLASH_QUANTITY = 0xD042,
        prPTP_DEV_PROP_ROTATION_ANGLE = 0xD043,
        prPTP_DEV_PROP_ROTATION_SENCE = 0xD044,
        prPTP_DEV_PROP_IMEGE_FILE_SIZE = 0xD048,
        prPTP_DEV_PROP_CAMERA_MODEL_ID = 0xD049	
    }
}