using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Colorbrewer
{
    public class CboFields : ESRI.ArcGIS.Desktop.AddIns.ComboBox
    {
        private static CboFields s_CboFields;
        private static string s_selectedField;

        public CboFields()
        {
            s_CboFields = this;
        }

        protected override void OnUpdate()
        {
            Enabled = (CboLayers.GetSelectedLayer() != null);
        }

        protected override void OnSelChange(int cookie)
        {
            if (cookie < 0)
                return;
            s_selectedField = this.GetItem(cookie).Caption;
            CboRenderers.Render();
        }

        internal static string GetSelectedField()
        {
            return s_selectedField;
        }

        internal static void ClearAllItems()
        {
            s_selectedField = null;
            s_CboFields.Clear();
        }

        internal static void AddItem(string name)
        {
            s_CboFields.Add(name);
        }


    }

}
