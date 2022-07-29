
namespace LiberacionProductoWeb.Reports.ConditioningOrder
{
    partial class Rpt7
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
            DevExpress.XtraPrinting.Shape.ShapeRectangle shapeRectangle1 = new DevExpress.XtraPrinting.Shape.ShapeRectangle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rpt7));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.label39 = new DevExpress.XtraReports.UI.XRLabel();
            this.label38 = new DevExpress.XtraReports.UI.XRLabel();
            this.label37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.label32 = new DevExpress.XtraReports.UI.XRLabel();
            this.label1 = new DevExpress.XtraReports.UI.XRLabel();
            this.label10 = new DevExpress.XtraReports.UI.XRLabel();
            this.label7 = new DevExpress.XtraReports.UI.XRLabel();
            this.label6 = new DevExpress.XtraReports.UI.XRLabel();
            this.label4 = new DevExpress.XtraReports.UI.XRLabel();
            this.label3 = new DevExpress.XtraReports.UI.XRLabel();
            this.label2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2,
            this.label39,
            this.label38,
            this.label37,
            this.xrLabel19,
            this.xrLabel20,
            this.xrLabel21,
            this.xrLabel22,
            this.xrPictureBox1,
            this.xrShape1});
            this.TopMargin.HeightF = 157.2917F;
            this.TopMargin.Name = "TopMargin";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(465.2915F, 121.1666F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(69.53125F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Tanque:";
            // 
            // xrLabel2
            // 
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Tank]")});
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(534.0836F, 121.1666F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(247.9164F, 23F);
            this.xrLabel2.Text = "label38";
            // 
            // label39
            // 
            this.label39.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LotProd]")});
            this.label39.LocationFloat = new DevExpress.Utils.PointFloat(157.9164F, 121.1666F);
            this.label39.Multiline = true;
            this.label39.Name = "label39";
            this.label39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label39.SizeF = new System.Drawing.SizeF(307.3751F, 22.99998F);
            this.label39.Text = "label39";
            // 
            // label38
            // 
            this.label38.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Product]")});
            this.label38.LocationFloat = new DevExpress.Utils.PointFloat(534.0837F, 96.875F);
            this.label38.Multiline = true;
            this.label38.Name = "label38";
            this.label38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label38.SizeF = new System.Drawing.SizeF(247.9164F, 23F);
            this.label38.Text = "label38";
            // 
            // label37
            // 
            this.label37.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Plant]")});
            this.label37.LocationFloat = new DevExpress.Utils.PointFloat(157.9165F, 96.875F);
            this.label37.Multiline = true;
            this.label37.Name = "label37";
            this.label37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label37.SizeF = new System.Drawing.SizeF(269.7916F, 23F);
            this.label37.Text = "label37";
            // 
            // xrLabel19
            // 
            this.xrLabel19.Font = new System.Drawing.Font("Constantia", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 27.70834F);
            this.xrLabel19.Multiline = true;
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(367.7083F, 29.25F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.Text = "ORDEN DE ACONDICIONAMIENTO";
            // 
            // xrLabel20
            // 
            this.xrLabel20.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 96.875F);
            this.xrLabel20.Multiline = true;
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(55.20833F, 23F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.Text = "Planta:";
            // 
            // xrLabel21
            // 
            this.xrLabel21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 119.875F);
            this.xrLabel21.Multiline = true;
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(147.9167F, 23F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.Text = "Lote de Producción:";
            // 
            // xrLabel22
            // 
            this.xrLabel22.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(465.2915F, 96.875F);
            this.xrLabel22.Multiline = true;
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(69.53125F, 23F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.Text = "Producto:";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageUrl = "wwwroot\\img\\logo.jpg";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(649.4478F, 27.70837F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(132.5521F, 63.95833F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrShape1
            // 
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 17.70833F);
            this.xrShape1.Name = "xrShape1";
            this.xrShape1.Shape = shapeRectangle1;
            this.xrShape1.SizeF = new System.Drawing.SizeF(792.0001F, 136.4583F);
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.BottomMargin.HeightF = 48.95833F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 0F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(800F, 23F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.label32,
            this.label1,
            this.label10,
            this.label7,
            this.label6,
            this.label4,
            this.label3,
            this.label2,
            this.xrTable1});
            this.Detail.HeightF = 449.0835F;
            this.Detail.Name = "Detail";
            // 
            // label32
            // 
            this.label32.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label32.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 27.16672F);
            this.label32.Multiline = true;
            this.label32.Name = "label32";
            this.label32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label32.SizeF = new System.Drawing.SizeF(793F, 251.7501F);
            this.label32.StylePriority.UseBorders = false;
            this.label32.StylePriority.UseFont = false;
            this.label32.StylePriority.UseTextAlignment = false;
            this.label32.Text = resources.GetString("label32.Text");
            this.label32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 4.166698F);
            this.label1.Multiline = true;
            this.label1.Name = "label1";
            this.label1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label1.SizeF = new System.Drawing.SizeF(793F, 23F);
            this.label1.StylePriority.UseFont = false;
            this.label1.Text = "Liberación del producto terminado.";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label10.BorderColor = System.Drawing.Color.Black;
            this.label10.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.LocationFloat = new DevExpress.Utils.PointFloat(358.9584F, 314.0001F);
            this.label10.Multiline = true;
            this.label10.Name = "label10";
            this.label10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label10.SizeF = new System.Drawing.SizeF(433.0416F, 23F);
            this.label10.StylePriority.UseBackColor = false;
            this.label10.StylePriority.UseBorderColor = false;
            this.label10.StylePriority.UseBorders = false;
            this.label10.StylePriority.UseFont = false;
            this.label10.StylePriority.UseForeColor = false;
            this.label10.StylePriority.UseTextAlignment = false;
            this.label10.Text = "Observaciones";
            this.label10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label7.BorderColor = System.Drawing.Color.Black;
            this.label7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.LocationFloat = new DevExpress.Utils.PointFloat(358.9584F, 421.9167F);
            this.label7.Multiline = true;
            this.label7.Name = "label7";
            this.label7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label7.SizeF = new System.Drawing.SizeF(433.0416F, 27.16681F);
            this.label7.StylePriority.UseBackColor = false;
            this.label7.StylePriority.UseBorderColor = false;
            this.label7.StylePriority.UseBorders = false;
            this.label7.StylePriority.UseFont = false;
            this.label7.StylePriority.UseForeColor = false;
            this.label7.StylePriority.UseTextAlignment = false;
            this.label7.Text = "Firma del Responsable Sanitario";
            this.label7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label6.BorderColor = System.Drawing.Color.Black;
            this.label6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 291.0001F);
            this.label6.Multiline = true;
            this.label6.Name = "label6";
            this.label6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label6.SizeF = new System.Drawing.SizeF(792.0001F, 23F);
            this.label6.StylePriority.UseBackColor = false;
            this.label6.StylePriority.UseBorderColor = false;
            this.label6.StylePriority.UseBorders = false;
            this.label6.StylePriority.UseFont = false;
            this.label6.StylePriority.UseForeColor = false;
            this.label6.StylePriority.UseTextAlignment = false;
            this.label6.Text = "Tabla 7: Cierre del expediente de Lote";
            this.label6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label4.BorderColor = System.Drawing.Color.Black;
            this.label4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 398.9167F);
            this.label4.Multiline = true;
            this.label4.Name = "label4";
            this.label4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label4.SizeF = new System.Drawing.SizeF(792.0001F, 23F);
            this.label4.StylePriority.UseBackColor = false;
            this.label4.StylePriority.UseBorderColor = false;
            this.label4.StylePriority.UseBorders = false;
            this.label4.StylePriority.UseFont = false;
            this.label4.StylePriority.UseForeColor = false;
            this.label4.StylePriority.UseTextAlignment = false;
            this.label4.Text = "Historial de cierre / abierto de la orden de acondicionamiento";
            this.label4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label3.BorderColor = System.Drawing.Color.Black;
            this.label3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.LocationFloat = new DevExpress.Utils.PointFloat(0.0001956255F, 421.9167F);
            this.label3.Multiline = true;
            this.label3.Name = "label3";
            this.label3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label3.SizeF = new System.Drawing.SizeF(358.9583F, 27.16681F);
            this.label3.StylePriority.UseBackColor = false;
            this.label3.StylePriority.UseBorderColor = false;
            this.label3.StylePriority.UseBorders = false;
            this.label3.StylePriority.UseFont = false;
            this.label3.StylePriority.UseForeColor = false;
            this.label3.StylePriority.UseTextAlignment = false;
            this.label3.Text = "Estatus";
            this.label3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label2.BorderColor = System.Drawing.Color.Black;
            this.label2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 314.0001F);
            this.label2.Multiline = true;
            this.label2.Name = "label2";
            this.label2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label2.SizeF = new System.Drawing.SizeF(358.9583F, 23F);
            this.label2.StylePriority.UseBackColor = false;
            this.label2.StylePriority.UseBorderColor = false;
            this.label2.StylePriority.UseBorders = false;
            this.label2.StylePriority.UseFont = false;
            this.label2.StylePriority.UseForeColor = false;
            this.label2.StylePriority.UseTextAlignment = false;
            this.label2.Text = "Firma del Responsable Sanitario";
            this.label2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 337.0001F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(791.9999F, 23.00015F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 150732.8D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ReleasedBy] + \' \' + FormatString(\'{0:yyyy-MM-dd HH:mm}\',[ReleasedDate])")});
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "label11";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell1.Weight = 358.95827738444495D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ReleasedNotes]")});
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "label13";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell2.Weight = 433.04159545898438D;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSourceType = null;
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.ReportFooter});
            this.DetailReport.DataMember = "StatesList";
            this.DetailReport.DataSource = this.objectDataSource1;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail1.HeightF = 24.66621F;
            this.Detail1.Name = "Detail1";
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.0001271566F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(791.9999F, 23F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[State]")});
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "label5";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell3.Weight = 358.95827738441585D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[User] + \' \' + FormatString(\'{0:yyyy-MM-dd HH:mm}\',[Date])")});
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "label8";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell4.Weight = 433.04159545898438D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 24.95842F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // Rpt7
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.DetailReport});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(33, 17, 157, 49);
            this.Version = "21.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRShape xrShape1;
        private DevExpress.XtraReports.UI.XRLabel label39;
        private DevExpress.XtraReports.UI.XRLabel label38;
        private DevExpress.XtraReports.UI.XRLabel label37;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRLabel xrLabel20;
        private DevExpress.XtraReports.UI.XRLabel xrLabel21;
        private DevExpress.XtraReports.UI.XRLabel xrLabel22;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRLabel label32;
        private DevExpress.XtraReports.UI.XRLabel label1;
        private DevExpress.XtraReports.UI.XRLabel label10;
        private DevExpress.XtraReports.UI.XRLabel label7;
        private DevExpress.XtraReports.UI.XRLabel label6;
        private DevExpress.XtraReports.UI.XRLabel label4;
        private DevExpress.XtraReports.UI.XRLabel label3;
        private DevExpress.XtraReports.UI.XRLabel label2;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
    }
}
