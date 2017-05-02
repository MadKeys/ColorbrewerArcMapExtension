using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Colorbrewer
{
    public class CboClasses : ESRI.ArcGIS.Desktop.AddIns.ComboBox
    {
        private static CboClasses s_CboClasses;
        private static string s_selectedClass;

        public CboClasses()
        {
            s_CboClasses = this;
        }

        protected override void OnSelChange(int cookie)
        {
            if (cookie < 0)
                return;
            s_selectedClass = this.GetItem(cookie).Caption;
            CboRenderers.Render();
        }

        internal static string GetSelectedClass()
        {
            return s_selectedClass;
        }

        internal static void ClearAllItems()
        {
            s_selectedClass = null;
            s_CboClasses.Clear();
        }

        internal static void AddItem(string name)
        {
            s_CboClasses.Add(name);
        }

        protected override void OnUpdate()
        {
            Enabled = CboRenderers.GetSelectedRenderer() != null;
        }
    }

}
