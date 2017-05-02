using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Framework;

namespace Colorbrewer
{
    public class ColorbrewerExtension : ESRI.ArcGIS.Desktop.AddIns.Extension
    {

        private IMap map;
        private static ColorbrewerExtension s_extension;
        private static string filepath;
        private IDockableWindow dockWin;

        public ColorbrewerExtension()
        {
        }

        protected override void OnStartup()
        {
            s_extension = this;
            if (s_extension == null || this.State != ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled)
                return;
            s_extension.WireDocumentEvents();
            UID winID = new UIDClass();
            winID.Value = ThisAddIn.IDs.DockableWindow;
            dockWin = ArcMap.DockableWindowManager.GetDockableWindow(winID);
            dockWin.Show(true);
        }

        private void WireDocumentEvents()
        {
            //
            // TODO: Sample document event wiring code. Change as needed
            //
            map = ArcMap.Document.FocusMap;
            IActiveViewEvents_Event activeViewEvents = map as IActiveViewEvents_Event;
            activeViewEvents.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(PopulateCboLayers);
            activeViewEvents.ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(PopulateCboLayers);

            // Named event handler
            ArcMap.Events.NewDocument += delegate () { ArcMap_NewDocument(); };

            //Anonymous event handler
            ArcMap.Events.BeforeCloseDocument += delegate ()
            {
          // Return true to stop document from closing
          ESRI.ArcGIS.Framework.IMessageDialog msgBox = new ESRI.ArcGIS.Framework.MessageDialogClass();
                return msgBox.DoModal("BeforeCloseDocument Event", "Abort closing?", "Yes", "No", ArcMap.Application.hWnd);
            };

        }

        void ArcMap_NewDocument()
        {
            // TODO: Handle new document event
        }

        internal void PopulateCboColors()
        {
            try
            {
                List<string> colors = new List<string>();
                System.IO.StreamReader file =
                   new System.IO.StreamReader(filepath);
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string name = line.Split(',')[0];
                    if (!(name == "" || name == "ColorName" || colors.Contains(name)))
                    {
                        colors.Add(name);
                    }
                }

                foreach (string color in colors)
                {
                    CboColors.AddItem(color);
                }
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                LocateCBfile();
                PopulateCboColors();
            }
            
        }

        internal void PopulateCboLayers(object item)
        {
            map = (ArcMap.Application.Document as IMxDocument).FocusMap;
            CboFields.ClearAllItems();
            CboLayers.ClearAllItems();
            ILayer layer;
            for(int i = 0; i < map.LayerCount; i++)
            {
                layer = map.Layer[i];
                if(map.Layer[i] is IFeatureLayer)
                    CboLayers.AddItem(layer.Name);
            }
        }

        internal static void UpdateCboClasses()
        {
            try
            {
                CboClasses.ClearAllItems();
                string selectedColor = CboColors.GetSelectedColor();
                /* FILEPATH needs to be modified whenever the cb.csv file is moved aka during installation */
                System.IO.StreamReader file =
                   new System.IO.StreamReader(filepath);
                string line;
                string currentColor = "";

                List<string> values = new List<string>();
                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    string value = "";
                    if (words[0] != "")
                    {
                        currentColor = words[0];
                    }
                    if (currentColor.Equals(selectedColor))
                    {
                        switch (CboRenderers.GetSelectedRenderer())
                        {
                            case "Simple Marker Renderer":
                            case "Simple Fill Renderer":
                            case "Proportional Symbol Renderer":
                                value = words[5];
                                break;
                            case "Classbreaks Renderer":
                                value = words[1];
                                break;
                        }
                        if (!values.Contains(value) && !value.Equals(""))
                        {
                            values.Add(value);
                        }
                    }
                }
                foreach (string value in values)
                {
                    CboClasses.AddItem(value);
                }
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                LocateCBfile();
                UpdateCboClasses();
            }           
        }      

        internal static ICmykColor GetSingleCMYKColor()
        {
            try
            {
                ICmykColor color = new CmykColorClass();

                string selectedColor = CboColors.GetSelectedColor();
                string selectedClass = CboClasses.GetSelectedClass();
                /* FILEPATH needs to be modified whenever the cb.csv file is moved aka during installation */
                System.IO.StreamReader file =
                   new System.IO.StreamReader(filepath);
                string line;
                string currentColor = "";
                string currentClass = "";

                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    if (words[0] != "")
                    {
                        currentColor = words[0];
                    }
                    currentClass = words[5];
                    if (currentColor.Equals(selectedColor) && currentClass.Equals(selectedClass))
                    {
                        color.Cyan = Convert.ToInt32(words[6]);
                        color.Magenta = Convert.ToInt32(words[7]);
                        color.Yellow = Convert.ToInt32(words[8]);
                        color.Black = Convert.ToInt32(words[9]);
                        return color;
                    }
                }
                return color;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                LocateCBfile();
                return GetSingleCMYKColor();
            }
        }

        internal static ICmykColor[] GetCmykColors()
        {
            try
            {
                string selectedClass = CboClasses.GetSelectedClass();
                ICmykColor[] colors = new ICmykColor[Convert.ToInt32(selectedClass)];
                int currentIndex = 0;

                string selectedColor = CboColors.GetSelectedColor();

                System.IO.StreamReader file =
                  new System.IO.StreamReader(filepath);
                string line;
                string currentColor = "";
                string currentClass = "";

                while ((line = file.ReadLine()) != null)
                {
                    if (currentIndex == colors.Length)
                        break;

                    string[] words = line.Split(',');
                    if (words[0] != "")
                    {
                        currentColor = words[0];
                    }
                    if (words[1] != "")
                    {
                        currentClass = words[1];
                    }
                    if (currentColor.Equals(selectedColor) && currentClass.Equals(selectedClass))
                    {
                        ICmykColor color = new CmykColorClass();
                        color.Cyan = Convert.ToInt32(words[6]);
                        color.Magenta = Convert.ToInt32(words[7]);
                        color.Yellow = Convert.ToInt32(words[8]);
                        color.Black = Convert.ToInt32(words[9]);
                        colors[currentIndex] = color;
                        currentIndex++;
                    }
                }
                return colors;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                LocateCBfile();
                return GetCmykColors();
            }
            
        }

        internal static bool IsExtensionEnabled()
        {
            if (s_extension == null)
                GetTheExtension();
            if (s_extension == null)
                return false;
            if (s_extension.State == ESRI.ArcGIS.Desktop.AddIns.ExtensionState.Enabled)
                return true;
            else
                return false;
        }

        private static ColorbrewerExtension GetTheExtension()
        {
            UID extensionID = new UIDClass();
            extensionID.Value = ThisAddIn.IDs.ColorbrewerExtension;
            ArcMap.Application.FindExtensionByCLSID(extensionID);
            return s_extension;
        }

        internal static string LocateCBfile()
        {
            IGxDialog GxD = new GxDialog();
            GxD.AllowMultiSelect = false;
            GxD.ButtonCaption = "Add";
            GxD.Title = "Select the Colorbrewer File";
            GxD.RememberLocation = true;

            IGxObjectFilter textFileFilter = new GxFilterTextFiles();
            GxD.ObjectFilter = textFileFilter;

            IEnumGxObject enumObj;
            if (GxD.DoModalOpen(ArcMap.Application.hWnd, out enumObj) == false)
            {
                System.Windows.Forms.MessageBox.Show("Operation Failed");
                return "Operation Failed: Please Try Again";
            }         
            IGxObject gxObj = enumObj.Next();
            filepath = gxObj.FullName;
            System.Windows.Forms.MessageBox.Show("File Path: " + filepath);
            return filepath;
        }

        internal static void UpdateCombos()
        {
            s_extension.PopulateCboColors();
            UpdateCboClasses();
        }

    }

}
