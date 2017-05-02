using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.Display;

namespace Colorbrewer
{
    public class CboColors : ESRI.ArcGIS.Desktop.AddIns.ComboBox
    {
        private static CboColors s_CboColors;
        private static string s_selectedColor;

        public CboColors()
        {
            s_CboColors = this;
        }

        protected override void OnUpdate()
        {
            Enabled = (CboRenderers.GetSelectedRenderer() != null);
        }

        protected override void OnSelChange(int cookie)
        {
            if (cookie < 0)
            {
                return;
            }
            s_selectedColor = this.GetItem(cookie).Caption;
            ColorbrewerExtension.UpdateCboClasses();
            CboRenderers.Render();
        }

        internal static string GetSelectedColor()
        {
            return s_selectedColor;
        }

        public static void ClearAllItems()
        {
            s_CboColors.Clear();
        }

        public static void AddItem(string name)
        {
            s_CboColors.Add(name);
        }
    }

}
