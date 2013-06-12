using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels
{
    public class CamerasViewModel : NotificationEnabledObject
    {
        ObservableCollection<Camera> cameraList;
        ServiceModel serviceModel = new ServiceModel();

        public ObservableCollection<Camera> CameraList
        {
            get
            {
                if (cameraList == null)
                {
                    cameraList = new ObservableCollection<Camera>();
                }

                //Datos de ejemplo para tiempo de diseño
                if (DesignerProperties.IsInDesignTool)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        cameraList.Add(new Camera() { Name = Guid.NewGuid().ToString() });
                    }
                }
                return cameraList;
            }
            set
            {
                cameraList = value;
                OnPropertyChanged();
            }
        }

        public CamerasViewModel()
        {
            serviceModel.GetCamerasCompleted += (s, a) =>
            {
                CameraList = new ObservableCollection<Camera>(a.Results);
            };
        }

        ActionCommand getCamerasCommand;
        public ActionCommand GetCamerasCommand
        {
            get {
                if (getCamerasCommand == null)
                {
                    getCamerasCommand = new ActionCommand(() =>
                    {
                        serviceModel.GetCameras();
                    });
                }
                return getCamerasCommand; 
            }
        }
        
    }
}
