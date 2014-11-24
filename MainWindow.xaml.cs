using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Interaction;
using Microsoft.Kinect.Toolkit.Controls;

namespace HUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KinectEngine kinectEngine = new KinectEngine();
            kinectEngine.start();
            region.KinectSensor = kinectEngine.kinect;
            //AsicInteraction asicInteraction = new AsicInteraction();
            if (kinectEngine.kinect != null)
            {
                region.kinectAdapter.asicInteraction.MoveEvent += move;
                region.kinectAdapter.asicInteraction.GripEvent += grip;
                //DepthImageProcess depthImageProcess = new DepthImageProcess();
                //kinectEngine.kinect.DepthFrameReady += depthImageProcess.depthImageProcess;

                SkeletonProcess skeletonProcess = new SkeletonProcess();
                kinectEngine.kinect.SkeletonFrameReady += skeletonProcess.skeletonProcess;
            }
        }

        public void move(HandPointer handPointer)
        {
            Skeleton[] skeletons = new Skeleton[region.skeletonImageFrame.SkeletonArrayLength];

            label1.Content = "uiX="+handPointer.X.ToString()+"\nuiY="+handPointer.Y.ToString();
            label2.Content = "X=" + (handPointer.X/region.ActualWidth).ToString() + "\nY=" + (handPointer.Y/region.ActualHeight).ToString();
            if (handPointer.skeletonFrame == null)
            {
                MessageBox.Show("noskel");
            }
            
            //region.skeletonImageFrame.CopySkeletonDataTo(skeletons);
            //int a = handPointer.depthImageFrame.MinDepth;
            //handPointer.skeletonFrame.CopySkeletonDataTo(skeletons);
            int skelsum = 0;
            foreach (var skel in region.skeletons)
            {
                if (skel.TrackingId != 0)
                {
                    skelsum++;
                    
                }
            }
            label3.Content = skelsum.ToString();
        }

        public void grip(HandPointer handPointer)
        {
            //labelY.Content = "grip";
        }
    }
}
