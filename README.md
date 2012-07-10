#PSRecSDK.Net
##What is PSRecSDK.Net
PSRecSDK.Net is a .Net wrapper for PS-Rec SDK(Power Shot Remote Capture SDK).  
The current PS-Rec SDK version is 1.1.0e.
##What is PS-ReCSDK 1.1.0e
>The PS-ReC SDK(Power Shot Remote Capture SDK) is a software develpment kit.  
The PS-ReC SDK provides an interface for controlling Canon digital cameras from a computer to capture images.  
>The PS-ReC SDK is provided as a library that can be linked to application software and programs.

###Supported Camera Models
>  PowerShot A620,  
>  PowerShot S80,  
>  PowerShot S3 IS  
>  PowerShot G7  
>  PowerShot A640  
>  PowerShot S5 IS  
>  PowerShot G9  
>  PowerShot SX100 IS  
>  PowerShot G10  
>  PowerShot SX110 IS


If you want to know more about PS-ReCSDK, you can find that in **/PS-ReCSDK 1.1.0e**, which is original Canon SDK files including documents, sdk and C samples. 

##Brief Introduction
In **PSReCSDKLib** project, the *PRApi* class is the wrapper for the PRSDK.dll, and CameraControl class is one implementation class for the api.

**REMEMBER** When you debug or release your application which has a reference to PSRecSDKLib project, you need to add both the *PRLIB.dll* and *PRSDK.dll* to the output folder together with the *PSRecSDK.dll*.  
 You can find those two files in **/PS-ReCSDK 1.1.0e/PSReCSDK/redist/**.