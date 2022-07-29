
namespace LiberacionProductoWeb.Reports.OrderProduction
{
    partial class VariablesChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraPrinting.Shape.ShapeRectangle shapeRectangle1 = new DevExpress.XtraPrinting.Shape.ShapeRectangle();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.xrLabel59 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel55 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel56 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel57 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 14.125F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.BottomMargin.HeightF = 46.4166F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 9.708341F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(1041F, 23F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart1});
            this.Detail.HeightF = 487.5838F;
            this.Detail.Name = "Detail";
            // 
            // xrChart1
            // 
            this.xrChart1.BorderColor = System.Drawing.Color.Black;
            this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.xrChart1.Diagram = xyDiagram1;
            this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(8.98997F, 76.91669F);
            this.xrChart1.Name = "xrChart1";
            series1.Name = "Serie1";
            series1.View = lineSeriesView1;
            this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.xrChart1.SizeF = new System.Drawing.SizeF(1023.02F, 390.3753F);
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(LiberacionProductoWeb.Models.ProductionOrderViewModels.ProductionOrderViewModel);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // xrLabel59
            // 
            this.xrLabel59.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BatchDetails].[Number]")});
            this.xrLabel59.LocationFloat = new DevExpress.Utils.PointFloat(159.0831F, 103.4583F);
            this.xrLabel59.Multiline = true;
            this.xrLabel59.Name = "xrLabel59";
            this.xrLabel59.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel59.SizeF = new System.Drawing.SizeF(872.9265F, 23F);
            this.xrLabel59.Text = "xrLabel2";
            // 
            // xrLabel55
            // 
            this.xrLabel55.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel55.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 103.4583F);
            this.xrLabel55.Multiline = true;
            this.xrLabel55.Name = "xrLabel55";
            this.xrLabel55.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel55.SizeF = new System.Drawing.SizeF(149.0831F, 23F);
            this.xrLabel55.StylePriority.UseFont = false;
            this.xrLabel55.Text = "Lote de Producción:";
            // 
            // xrShape1
            // 
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrShape1.Name = "xrShape1";
            this.xrShape1.Shape = shapeRectangle1;
            this.xrShape1.SizeF = new System.Drawing.SizeF(1039F, 136.4583F);
            // 
            // xrLabel53
            // 
            this.xrLabel53.Font = new System.Drawing.Font("Constantia", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel53.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 10.00001F);
            this.xrLabel53.Multiline = true;
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel53.SizeF = new System.Drawing.SizeF(527.208F, 21.95834F);
            this.xrLabel53.StylePriority.UseFont = false;
            this.xrLabel53.Text = "ORDEN DE PRODUCCIÓN";
            // 
            // xrLabel54
            // 
            this.xrLabel54.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel54.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 72.58326F);
            this.xrLabel54.Multiline = true;
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel54.SizeF = new System.Drawing.SizeF(149.0831F, 23F);
            this.xrLabel54.StylePriority.UseFont = false;
            this.xrLabel54.Text = "Planta:";
            // 
            // xrLabel56
            // 
            this.xrLabel56.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel56.LocationFloat = new DevExpress.Utils.PointFloat(653.0739F, 73.95834F);
            this.xrLabel56.Multiline = true;
            this.xrLabel56.Name = "xrLabel56";
            this.xrLabel56.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel56.SizeF = new System.Drawing.SizeF(71.91623F, 23F);
            this.xrLabel56.StylePriority.UseFont = false;
            this.xrLabel56.Text = "Producto:";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageUrl = "wwwroot\\img\\logo.jpg";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(870.7085F, 10.00001F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(160.2916F, 63.95833F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel58,
            this.xrLabel57,
            this.xrLabel59,
            this.xrLabel55,
            this.xrLabel56,
            this.xrPictureBox1,
            this.xrLabel54,
            this.xrLabel53,
            this.xrShape1});
            this.ReportHeader.HeightF = 136.4583F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel58
            // 
            this.xrLabel58.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProductName]")});
            this.xrLabel58.LocationFloat = new DevExpress.Utils.PointFloat(724.9901F, 73.95834F);
            this.xrLabel58.Multiline = true;
            this.xrLabel58.Name = "xrLabel58";
            this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel58.SizeF = new System.Drawing.SizeF(306.0099F, 23F);
            this.xrLabel58.Text = "xrLabel1";
            // 
            // xrLabel57
            // 
            this.xrLabel57.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Location]"),
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Location]"),
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Location]"),
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Location]")});
            this.xrLabel57.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel57.LocationFloat = new DevExpress.Utils.PointFloat(159.0831F, 72.58326F);
            this.xrLabel57.Multiline = true;
            this.xrLabel57.Name = "xrLabel57";
            this.xrLabel57.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel57.SizeF = new System.Drawing.SizeF(344.7916F, 23F);
            this.xrLabel57.StylePriority.UseFont = false;
            this.xrLabel57.Text = "xrLabel9";
            // 
            // VariablesChart
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(20, 39, 14, 46);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "21.2";
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel59;
        private DevExpress.XtraReports.UI.XRLabel xrLabel55;
        private DevExpress.XtraReports.UI.XRShape xrShape1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel53;
        private DevExpress.XtraReports.UI.XRLabel xrLabel54;
        private DevExpress.XtraReports.UI.XRLabel xrLabel56;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel58;
        private DevExpress.XtraReports.UI.XRLabel xrLabel57;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRChart xrChart1;
    }
}
