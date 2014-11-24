using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace HUI
{
    class KinectEngine
    {
        private KinectSensor _kinect;
        public KinectSensor kinect
        {
            get { return this._kinect; }
            set
            {
                if (this._kinect != value)
                {
                    //如果当前的传感对象不为null 
                    if (this._kinect != null)
                    {
                        //uninitailize当前对象 
                        UninitializeKinectSensor(this._kinect);
                        this._kinect = null;
                    }
                    //如果传入的对象不为空，且状态为连接状态 
                    if (value != null && value.Status == KinectStatus.Connected)
                    {
                        this._kinect = value;
                        InitializeKinectSensor(this._kinect);
                    }
                }
            }
        }

        private void InitializeKinectSensor(KinectSensor kinectSensor)
        {
            if (kinectSensor != null)
            {
                kinectSensor.DepthStream.Enable();
                //TransformSmoothParameters p = new TransformSmoothParameters();
                //p.Smoothing = 0.7f;
                //p.Correction = 0.1f;
                //p.JitterRadius = 0.02f;
                //p.MaxDeviationRadius = 0.1f;
                kinectSensor.SkeletonStream.Enable();
                kinectSensor.Start();
            }
        }

        private void UninitializeKinectSensor(KinectSensor kinectSensor)
        {
            if (kinectSensor != null)
            {
                kinectSensor.Stop();
            }
        }

        private void DiscoverKinectSensor()
        {
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
        }

        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Connected:
                    if (this.kinect == null)
                        this.kinect = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    if (this.kinect == e.Sensor)
                    {
                        this.kinect = null;
                        this.kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                        if (this.kinect == null)
                        {
                            //TODO:通知用于Kinect已拔出 
                        }
                    }
                    break;
                //TODO:处理其他情况下的状态
            }
        }


        public void start()
        {
            DiscoverKinectSensor();
        }

    }
}
