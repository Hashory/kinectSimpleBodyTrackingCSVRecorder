using Microsoft.Azure.Kinect.BodyTracking;
using Microsoft.Azure.Kinect.Sensor;

//get current directry
string currentDirectory = Directory.GetCurrentDirectory();
// create a pos data file in the current directry.
string fileName = $@"{currentDirectory}\{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}_posdata.csv";

// capture and write pos data
using (StreamWriter sw = new StreamWriter(fileName, true))
{
    // Open device.
    using (Device device = Device.Open())
    {
        device.StartCameras(new DeviceConfiguration()
        {
            ColorFormat = ImageFormat.ColorBGRA32,
            ColorResolution = ColorResolution.R720p,
            DepthMode = DepthMode.NFOV_Unbinned,
            SynchronizedImagesOnly = true,
            WiredSyncMode = WiredSyncMode.Standalone,
            CameraFPS = FPS.FPS30
        });



        // Camera calibration.
        var deviceCalibration = device.GetCalibration();
        var transformation = deviceCalibration.CreateTransformation();

        using (Tracker tracker = Tracker.Create(deviceCalibration, new TrackerConfiguration() { ProcessingMode = TrackerProcessingMode.Gpu, SensorOrientation = SensorOrientation.Default }))
        {
            var isActive = true;
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                isActive = false;
            };
            Console.WriteLine("Start Recording. Press Ctrl+C to stop.");

            while (isActive)
            {
                using (Capture sensorCapture = device.GetCapture())
                {
                    // Queue latest frame from the sensor.
                    tracker.EnqueueCapture(sensorCapture);
                }

                // Try getting latest tracker frame.
                using (Frame frame = tracker.PopResult(TimeSpan.Zero, throwOnTimeout: false))
                {
                    if (frame != null)
                    {
                        // is Human?
                        if (frame.NumberOfBodies > 0)
                        {
                            Console.Write("\r" + new string(' ', Console.WindowWidth));
                            Console.Write("\rIs there person: ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Yes");

                            // get body
                            var skeleton = frame.GetBodySkeleton(0);

                            float[] posData = new float[(int)JointId.Count * 3];
                            for (int i = 0; i < (int)JointId.Count; i++)
                            {
                                var joint = skeleton.GetJoint((JointId)i);
                                posData[i * 3] = joint.Position.X;
                                posData[i * 3 + 1] = joint.Position.Y;
                                posData[i * 3 + 2] = joint.Position.Z;
                            }

                            // write pos data to file
                            sw.WriteLine(string.Join(",", posData));
                        } else
                        {
                            Console.Write("\r" + new string(' ', Console.WindowWidth));
                            Console.Write("\rIs there person: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("No Person");
                        }

                        Console.ResetColor();
                    }
                }
            }
        }
        Console.Write("\r" + new string(' ', Console.WindowWidth));
        Console.WriteLine("\rStop Recording.");
    }
}