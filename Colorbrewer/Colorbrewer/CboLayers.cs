using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Colorbrewer
{
    public class CboLayers : ESRI.ArcGIS.Desktop.AddIns.ComboBox
    {
        private static CboLayers s_CboLayers;
        private static string s_selectedLayer;

        public CboLayers()
        {
            s_CboLayers = this;
        }

        protected override void OnUpdate()
        {
            this.Enabled = ColorbrewerExtension.IsExtensionEnabled();
        }

        protected override void OnSelChange(int cookie)
        {
            if(cookie < 0)
            {
                return;
            }
            s_selectedLayer = this.GetItem(cookie).Caption;
            CboFields.ClearAllItems();
            IMap map = (ArcMap.Document as IMxDocument).FocusMap;

            for(int i = 0; i < map.LayerCount; i++)
            {
                ILayer layer = map.Layer[i];
                if(layer.Name == s_selectedLayer) // && layer is IFeatureLayer
                {
                    IFeatureClass fClass = (layer as IFeatureLayer).FeatureClass;
                    for(int j = 0; j< fClass.Fields.FieldCount; j++)
                    {
                        switch (fClass.Fields.Field[j].Type)
                        {
                            case esriFieldType.esriFieldTypeDouble:
                            case esriFieldType.esriFieldTypeInteger:
                            case esriFieldType.esriFieldTypeSingle:
                            case esriFieldType.esriFieldTypeSmallInteger:
                                CboFields.AddItem(fClass.Fields.Field[j].Name);
                                break;
                        }
                    }
                    break;
                }
            }
            CboRenderers.UpdateRenderers();
        }

        internal static string GetSelectedLayer()
        {
            return s_selectedLayer;
        }

        public static void ClearAllItems()
        {
            s_selectedLayer = null;
            s_CboLayers.Clear();
        }

        public static void AddItem(string name)
        {
            s_CboLayers.Add(name);
        }
    }

}
