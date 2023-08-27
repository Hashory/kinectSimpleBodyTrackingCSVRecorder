# Kinect Simple Body Tracking CSV Recorder

This is a tool that uses Azure kinect to record body tracking data to a CSV file.

## Usage

1. Install [Azure kinect Sensor SDK](https://learn.microsoft.com/en-us/azure/kinect-dk/sensor-sdk-download) and [Body Tracking SDK](https://learn.microsoft.com/en-us/azure/Kinect-dk/body-sdk-download).
1. Download this tool. (→ [Release page](https://github.com/Hashory/kinectSimpleBodyTrackingCSVRecorder/releases))
1. Run `kinectSimpleBodyTrackingCSVRecorder.exe`.

## CSV file format

The CSV file contains the following information.  
There are 32 joints in total, each with three coordinates (x, y, z). Therefore, there are 96 rows in total.  
See [here](https://learn.microsoft.com/en-us/azure/kinect-dk/body-joints#joint-hierarchy) for joint order and more information.  

**CSV format:**
```csv
PELVIS.x, PELVIS.y, PELVIS.z, SPINE_NAVAL.x, ･･･  EAR_RIGHT.y, EAR_RIGHT.z
```

Also, be careful with the coordinate system. Azure kinect uses [this](https://learn.microsoft.com/en-us/azure/kinect-dk/coordinate-systems#3d-coordinate-systems).

## License

[MIT License](LICENSE.txt)

## Acknowledgements

This project uses some open source code from the following sources:

- [Azure Kinect Sensor SDK](https://github.com/microsoft/Azure-Kinect-Samples/)   
	([MIT License](https://github.com/microsoft/Azure-Kinect-Samples/blob/d87e80a2775413ee65f40943bbb65057e4c41976/LICENSE). Copyright: Microsoft Corporation)
- [AKRecorder](https://github.com/shoda888/AKRecorder)   
  ([MIT License](https://github.com/shoda888/AKRecorder/blob/d5cbe673474b2559640fe4f9cfec40a2eac9693e/LICENSE.txt). Copyright: KoheiShoda)
