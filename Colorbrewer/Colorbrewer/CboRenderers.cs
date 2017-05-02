using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.CartoUI;
using ESRI.ArcGIS.esriSystem;

namespace Colorbrewer
{
    public class CboRenderers : ESRI.ArcGIS.Desktop.AddIns.ComboBox
    {
        private static CboRenderers s_CboRenderers;
        private static string s_selectedRenderer;

        public CboRenderers()
        {
            s_CboRenderers = this;
        }

        private static ILayer GetLayerByName(string name)
        {
            IMap map = (ArcMap.Document as IMxDocument).FocusMap;
            for(int i = 0; i < map.LayerCount; i++)
            {
                ILayer layer = map.Layer[i];
                if(layer.Name == name)
                {
                    return layer;
                }
            }
            return null;
        }

        public static void UpdateRenderers()
        {
            if (s_CboRenderers == null)
                return;
            s_CboRenderers.Clear();
            string name = CboLayers.GetSelectedLayer();
            if (name == null)
                return;
            ILayer layer = GetLayerByName(name);
            IFeatureLayer fLayer = layer as IFeatureLayer;
            IFeatureClass fClass = fLayer.FeatureClass;
            if (fClass.ShapeType == esriGeometryType.esriGeometryPoint 
                || fClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
            {
                s_CboRenderers.Add("Simple Marker Renderer");
                s_CboRenderers.Add("Proportional Symbol Renderer");
            }
            if(fClass.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                s_CboRenderers.Add("Simple Fill Renderer");
                s_CboRenderers.Add("Classbreaks Renderer");
            }
            Render();
        }

        protected override void OnSelChange(int cookie)
        {
            if (cookie < 0)
                return;
            s_selectedRenderer = this.GetItem(cookie).Caption;

            switch (s_selectedRenderer)
            {
                case "Simple Marker Renderer":
                case "Proportional Symbol Renderer":
                case "Simple Fill Renderer":
                    ColorbrewerExtension.UpdateCboClasses();
                    break;
                case "Classbreaks Renderer":
                    ColorbrewerExtension.UpdateCboClasses();
                    break;
            }
            Render();
        }

        internal static void Render()
        {
            if(CboLayers.GetSelectedLayer() == null 
                || CboFields.GetSelectedField() == null
                || s_selectedRenderer == null
                || CboColors.GetSelectedColor() == null
                || CboClasses.GetSelectedClass() == null)
            {
                return;
            }
                
            switch (s_selectedRenderer)
            {
                case "Simple Marker Renderer":
                    MapUsingSimpleMarkerRenderer();
                    break;
                case "Proportional Symbol Renderer":
                    MapUsingProportionalSymbolRenderer();
                    break;
                case "Simple Fill Renderer":
                    MapUsingSimpleFillRenderer();
                    break;
                case "Classbreaks Renderer":
                    MapUsingClassbreaksRenderer();
                    break;
            }
        }

        internal static string GetSelectedRenderer()
        {
            return s_selectedRenderer;
        }

        private static void MapUsingSimpleMarkerRenderer()
        {
            string layerName = CboLayers.GetSelectedLayer();
            ILayer layer = GetLayerByName(layerName);

            string colorName = CboColors.GetSelectedColor();
            ICmykColor markerColor = ColorbrewerExtension.GetSingleCMYKColor();

            ISimpleMarkerSymbol marker = new SimpleMarkerSymbol();
            marker.Style = esriSimpleMarkerStyle.esriSMSCircle;
            marker.Color = markerColor;
            marker.Size = 5;

            ISimpleRenderer renderer = new SimpleRenderer();
            renderer.Symbol = marker as ISymbol;
            renderer.Label = layer.Name;

            IGeoFeatureLayer gFLayer = layer as IGeoFeatureLayer;
            gFLayer.Renderer = renderer as IFeatureRenderer;
            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            IMap map = mxDoc.FocusMap;
            mxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography,
                gFLayer, mxDoc.ActiveView.Extent);
            mxDoc.UpdateContents();    
        }

        private static void MapUsingSimpleFillRenderer()
        {
            string layerName = CboLayers.GetSelectedLayer();
            ILayer layer = GetLayerByName(layerName);

            string colorName = CboColors.GetSelectedColor();
            ICmykColor fillColor = ColorbrewerExtension.GetSingleCMYKColor();

            ISimpleFillSymbol fill = new SimpleFillSymbol();
            fill.Style = esriSimpleFillStyle.esriSFSSolid;
            fill.Color = fillColor;

            ISimpleRenderer  simpleRenderer = new SimpleRenderer();
            simpleRenderer.Symbol = fill as ISymbol;
            simpleRenderer.Label = layer.Name;

            IGeoFeatureLayer gFLayer = layer as IGeoFeatureLayer;
            gFLayer.Renderer = simpleRenderer as IFeatureRenderer;
            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            IMap map = mxDoc.FocusMap;
            mxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography
                , gFLayer, mxDoc.ActiveView.Extent);

        }

        private static void MapUsingProportionalSymbolRenderer()
        {
            ISimpleMarkerSymbol marker = new SimpleMarkerSymbol();
            marker.Style = esriSimpleMarkerStyle.esriSMSCircle;

            ICmykColor markerColor = ColorbrewerExtension.GetSingleCMYKColor();
            marker.Size = 10;
            marker.Color = markerColor;

            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            IMap map = mxDoc.FocusMap;

            string layerName = CboLayers.GetSelectedLayer();
            ILayer layer = GetLayerByName(layerName);
            IFeatureLayer fLayer = layer as IFeatureLayer;
            IFeatureClass fClass = fLayer.FeatureClass as IFeatureClass;
            IFeatureCursor cursor = fClass.Search(null, true);
            IDataStatistics dataStats = new DataStatisticsClass();
            dataStats.Cursor = cursor as ICursor;

            string fieldName = CboFields.GetSelectedField();
            dataStats.Field = fieldName;
            IStatisticsResults statResult = dataStats.Statistics;

            IProportionalSymbolRenderer propSymRenderer = new ProportionalSymbolRendererClass();
            propSymRenderer.Field = fieldName;
            propSymRenderer.MinDataValue = statResult.Minimum == 0.0 ? 1 : statResult.Minimum;
            propSymRenderer.MaxDataValue = statResult.Maximum;
            propSymRenderer.FlanneryCompensation = true;
            propSymRenderer.ValueUnit = esriUnits.esriUnknownUnits;
            propSymRenderer.MinSymbol = marker as ISymbol;
            propSymRenderer.LegendSymbolCount = 3;
            propSymRenderer.CreateLegendSymbols();

            IGeoFeatureLayer gFLayer = layer as IGeoFeatureLayer;
            gFLayer.Renderer = propSymRenderer as IFeatureRenderer;
            mxDoc.ActiveView.Refresh();
            mxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, gFLayer
                , mxDoc.ActiveView.Extent);
            mxDoc.UpdateContents();
        }


        private static IEnumColors GetColorRamp(int size)
        {
            return new AlgorithmicColorRampClass().Colors;
        }

        private static void MapUsingClassbreaksRenderer()
        {
            string layerName = CboLayers.GetSelectedLayer();
            ILayer layer = GetLayerByName(layerName);

            IFeatureLayer2 fLayer = layer as IFeatureLayer2;
            string fieldName = CboFields.GetSelectedField();

            // Get the number of classes
            string selectedClass = CboClasses.GetSelectedClass();
            int numberOfClasses = Convert.ToInt32(selectedClass);

            ITableHistogram tableHistogram = new TableHistogramClass();
            tableHistogram.Table = fLayer.FeatureClass as ITable;
            tableHistogram.Field = fieldName;
            IHistogram histo = tableHistogram as IHistogram;
            object datavalues, datafrequencies;
            histo.GetHistogram(out datavalues, out datafrequencies);

            IClassify classify = new QuantileClass();
            classify.SetHistogramData(datavalues, datafrequencies);
            classify.Classify(ref numberOfClasses);

            if(numberOfClasses <= 1)
            {
                return;
            }

            double[] classBreaks = (double[])classify.ClassBreaks;

            IClassBreaksRenderer render = new ClassBreaksRenderer();
            render.Field = fieldName;
            render.BreakCount = numberOfClasses;
            render.MinimumBreak = classBreaks[0];

            // Get the colors
            ICmykColor[] colors = ColorbrewerExtension.GetCmykColors();
            IFillSymbol fill = null;
            // Iterate through the colors
            for(int i = 0; i < numberOfClasses; i++)
            {
                fill = new SimpleFillSymbol();
                fill.Color = colors[i];
                fill.Outline.Width = 0.5;
                render.Symbol[i] = fill as ISymbol;
                render.Break[i] = classBreaks[i + 1];
                render.Label[i] = string.Format("{0} to {1}", classBreaks[i]
                    , classBreaks[i + 1]);
            }

            IGeoFeatureLayer gFLayer = layer as IGeoFeatureLayer;
            gFLayer.Renderer = render as IFeatureRenderer;
            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            mxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, gFLayer
                , mxDoc.ActiveView.Extent);
            mxDoc.UpdateContents();
        }

        protected override void OnUpdate()
        {
            Enabled = CboFields.GetSelectedField() != null;
        }
    }

}
