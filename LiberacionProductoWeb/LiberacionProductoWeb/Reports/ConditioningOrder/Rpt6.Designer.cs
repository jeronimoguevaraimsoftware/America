
namespace LiberacionProductoWeb.Reports.ConditioningOrder
{
    partial class Rpt6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rpt6));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.label39 = new DevExpress.XtraReports.UI.XRLabel();
            this.label38 = new DevExpress.XtraReports.UI.XRLabel();
            this.label37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.label5 = new DevExpress.XtraReports.UI.XRLabel();
            this.label1 = new DevExpress.XtraReports.UI.XRLabel();
            this.label2 = new DevExpress.XtraReports.UI.XRLabel();
            this.label32 = new DevExpress.XtraReports.UI.XRLabel();
            this.label3 = new DevExpress.XtraReports.UI.XRLabel();
            this.label4 = new DevExpress.XtraReports.UI.XRLabel();
            this.label6 = new DevExpress.XtraReports.UI.XRLabel();
            this.label48 = new DevExpress.XtraReports.UI.XRLabel();
            this.label46 = new DevExpress.XtraReports.UI.XRLabel();
            this.label45 = new DevExpress.XtraReports.UI.XRLabel();
            this.label44 = new DevExpress.XtraReports.UI.XRLabel();
            this.label40 = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2,
            this.xrPictureBox1,
            this.xrLabel20,
            this.label39,
            this.label38,
            this.label37,
            this.xrLabel21,
            this.xrLabel22,
            this.xrLabel19,
            this.xrShape1});
            this.TopMargin.HeightF = 156.875F;
            this.TopMargin.Name = "TopMargin";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(738.8856F, 132.5834F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(85.15631F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Tanque:";
            // 
            // xrLabel2
            // 
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Tank]")});
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(824.0419F, 133.875F);
            this.xrLabel2.Multiline = true;
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(159.1975F, 23F);
            this.xrLabel2.Text = "label38";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageUrl = "wwwroot\\img\\logo.jpg";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(824.0419F, 20.41667F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(175.9581F, 60.41667F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(10.00004F, 109.5833F);
            this.xrLabel20.Multiline = true;
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(53.125F, 23F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.Text = "Planta:";
            // 
            // label39
            // 
            this.label39.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[LotProd]")});
            this.label39.LocationFloat = new DevExpress.Utils.PointFloat(157.9169F, 133.875F);
            this.label39.Multiline = true;
            this.label39.Name = "label39";
            this.label39.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label39.SizeF = new System.Drawing.SizeF(473.958F, 23F);
            this.label39.Text = "label39";
            // 
            // label38
            // 
            this.label38.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Product]")});
            this.label38.LocationFloat = new DevExpress.Utils.PointFloat(824.0419F, 104.375F);
            this.label38.Multiline = true;
            this.label38.Name = "label38";
            this.label38.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label38.SizeF = new System.Drawing.SizeF(159.1975F, 23F);
            this.label38.Text = "label38";
            // 
            // label37
            // 
            this.label37.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Plant]")});
            this.label37.LocationFloat = new DevExpress.Utils.PointFloat(157.9166F, 109.5833F);
            this.label37.Multiline = true;
            this.label37.Name = "label37";
            this.label37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label37.SizeF = new System.Drawing.SizeF(314.5833F, 23F);
            this.label37.Text = "label37";
            // 
            // xrLabel21
            // 
            this.xrLabel21.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(10.00004F, 132.5834F);
            this.xrLabel21.Multiline = true;
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(145.8334F, 22.99999F);
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.Text = "Lote de Producción:";
            // 
            // xrLabel22
            // 
            this.xrLabel22.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(738.8856F, 104.375F);
            this.xrLabel22.Multiline = true;
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(85.15631F, 23F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.Text = "Producto:";
            // 
            // xrLabel19
            // 
            this.xrLabel19.Font = new System.Drawing.Font("Constantia", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(10.00004F, 40.41667F);
            this.xrLabel19.Multiline = true;
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(387.4998F, 29.25F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.Text = "ORDEN DE ACONDICIONAMIENTO";
            // 
            // xrShape1
            // 
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(6.357829E-05F, 20.41667F);
            this.xrShape1.Name = "xrShape1";
            this.xrShape1.Shape = shapeRectangle1;
            this.xrShape1.SizeF = new System.Drawing.SizeF(1001F, 136.4583F);
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 31.16671F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.label5,
            this.label1,
            this.label2,
            this.label32,
            this.label3,
            this.label4,
            this.label6,
            this.label48,
            this.label46,
            this.label45,
            this.label44,
            this.label40});
            this.Detail.HeightF = 668.2501F;
            this.Detail.Name = "Detail";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 321.75F);
            this.label5.Multiline = true;
            this.label5.Name = "label5";
            this.label5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label5.SizeF = new System.Drawing.SizeF(999.9999F, 254.0834F);
            this.label5.StylePriority.UseFont = false;
            this.label5.StylePriority.UseTextAlignment = false;
            this.label5.Text = resources.GetString("label5.Text");
            this.label5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 199.1667F);
            this.label1.Multiline = true;
            this.label1.Name = "label1";
            this.label1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label1.SizeF = new System.Drawing.SizeF(999.9999F, 23F);
            this.label1.StylePriority.UseFont = false;
            this.label1.Text = "Conciliación de materiales";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 288.3333F);
            this.label2.Multiline = true;
            this.label2.Name = "label2";
            this.label2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label2.SizeF = new System.Drawing.SizeF(999.9999F, 23F);
            this.label2.StylePriority.UseFont = false;
            this.label2.Text = "Rendimiento del proceso de acondicionamiento";
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23.00002F);
            this.label32.Multiline = true;
            this.label32.Name = "label32";
            this.label32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label32.SizeF = new System.Drawing.SizeF(999.9999F, 164.2501F);
            this.label32.StylePriority.UseFont = false;
            this.label32.StylePriority.UseTextAlignment = false;
            this.label32.Text = resources.GetString("label32.Text");
            this.label32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 236.75F);
            this.label3.Multiline = true;
            this.label3.Name = "label3";
            this.label3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label3.SizeF = new System.Drawing.SizeF(999.9999F, 37.16669F);
            this.label3.StylePriority.UseFont = false;
            this.label3.StylePriority.UseTextAlignment = false;
            this.label3.Text = resources.GetString("label3.Text");
            this.label3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.label4.Multiline = true;
            this.label4.Name = "label4";
            this.label4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label4.SizeF = new System.Drawing.SizeF(999.9999F, 23F);
            this.label4.StylePriority.UseFont = false;
            this.label4.Text = "Certificado de análisis";
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
            this.label6.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 587.8751F);
            this.label6.Multiline = true;
            this.label6.Name = "label6";
            this.label6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label6.SizeF = new System.Drawing.SizeF(998.9582F, 23F);
            this.label6.StylePriority.UseBackColor = false;
            this.label6.StylePriority.UseBorderColor = false;
            this.label6.StylePriority.UseBorders = false;
            this.label6.StylePriority.UseFont = false;
            this.label6.StylePriority.UseForeColor = false;
            this.label6.StylePriority.UseTextAlignment = false;
            this.label6.Text = "Tabla 6: Rendimiento del proceso de acondicionamiento";
            this.label6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label48.BorderColor = System.Drawing.Color.Black;
            this.label48.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label48.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.White;
            this.label48.LocationFloat = new DevExpress.Utils.PointFloat(691.9064F, 610.8752F);
            this.label48.Multiline = true;
            this.label48.Name = "label48";
            this.label48.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label48.SizeF = new System.Drawing.SizeF(308.0936F, 57.37494F);
            this.label48.StylePriority.UseBackColor = false;
            this.label48.StylePriority.UseBorderColor = false;
            this.label48.StylePriority.UseBorders = false;
            this.label48.StylePriority.UseFont = false;
            this.label48.StylePriority.UseForeColor = false;
            this.label48.StylePriority.UseTextAlignment = false;
            this.label48.Text = "Observaciones";
            this.label48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label46
            // 
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label46.BorderColor = System.Drawing.Color.Black;
            this.label46.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label46.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.White;
            this.label46.LocationFloat = new DevExpress.Utils.PointFloat(488.5734F, 610.8752F);
            this.label46.Multiline = true;
            this.label46.Name = "label46";
            this.label46.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label46.SizeF = new System.Drawing.SizeF(203.333F, 57.37494F);
            this.label46.StylePriority.UseBackColor = false;
            this.label46.StylePriority.UseBorderColor = false;
            this.label46.StylePriority.UseBorders = false;
            this.label46.StylePriority.UseFont = false;
            this.label46.StylePriority.UseForeColor = false;
            this.label46.StylePriority.UseTextAlignment = false;
            this.label46.Text = "Revisado por";
            this.label46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label45.BorderColor = System.Drawing.Color.Black;
            this.label45.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label45.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.White;
            this.label45.LocationFloat = new DevExpress.Utils.PointFloat(311.709F, 610.8752F);
            this.label45.Multiline = true;
            this.label45.Name = "label45";
            this.label45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label45.SizeF = new System.Drawing.SizeF(176.8644F, 57.37494F);
            this.label45.StylePriority.UseBackColor = false;
            this.label45.StylePriority.UseBorderColor = false;
            this.label45.StylePriority.UseBorders = false;
            this.label45.StylePriority.UseFont = false;
            this.label45.StylePriority.UseForeColor = false;
            this.label45.StylePriority.UseTextAlignment = false;
            this.label45.Text = "Diferencia contra el tamaño del lote (Kg)";
            this.label45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label44.BorderColor = System.Drawing.Color.Black;
            this.label44.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label44.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.White;
            this.label44.LocationFloat = new DevExpress.Utils.PointFloat(97.91705F, 610.8752F);
            this.label44.Multiline = true;
            this.label44.Name = "label44";
            this.label44.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label44.SizeF = new System.Drawing.SizeF(213.7919F, 57.37494F);
            this.label44.StylePriority.UseBackColor = false;
            this.label44.StylePriority.UseBorderColor = false;
            this.label44.StylePriority.UseBorders = false;
            this.label44.StylePriority.UseFont = false;
            this.label44.StylePriority.UseForeColor = false;
            this.label44.StylePriority.UseTextAlignment = false;
            this.label44.Text = "Cantidad total del producto de grado medicinal llenado en unidades de distribució" +
    "n (Kg)";
            this.label44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(98)))));
            this.label40.BorderColor = System.Drawing.Color.Black;
            this.label40.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.label40.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.White;
            this.label40.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 610.8752F);
            this.label40.Multiline = true;
            this.label40.Name = "label40";
            this.label40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.label40.SizeF = new System.Drawing.SizeF(96.87538F, 57.37494F);
            this.label40.StylePriority.UseBackColor = false;
            this.label40.StylePriority.UseBorderColor = false;
            this.label40.StylePriority.UseBorders = false;
            this.label40.StylePriority.UseFont = false;
            this.label40.StylePriority.UseForeColor = false;
            this.label40.StylePriority.UseTextAlignment = false;
            this.label40.Text = "Tamaño del lote (Kg)";
            this.label40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(LiberacionProductoWeb.Models.ConditioningOrderViewModels.ConditioningOrderViewModel);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.DataMember = "PerformanceList";
            this.DetailReport.DataSource = this.objectDataSource1;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail1.HeightF = 23.00002F;
            this.Detail1.Name = "Detail1";
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(998.9582F, 23.00002F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1339847.111111111D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SizeLote]")});
            this.xrTableCell3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.Text = "label9";
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell3.Weight = 95.416702270507812D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TotalTons]")});
            this.xrTableCell4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.Text = "label10";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell4.Weight = 213.79171752929688D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DifTons]")});
            this.xrTableCell5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell5.Multiline = true;
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.Text = "label11";
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell5.Weight = 176.86447143554688D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ReviewedBy] + \' \' + FormatString(\'{0:yyyy-MM-dd HH:mm}\',[ReviewedDate])")});
            this.xrTableCell6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell6.Multiline = true;
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "label12";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell6.Weight = 204.79190192735311D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Notes]")});
            this.xrTableCell7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell7.Multiline = true;
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "label14";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell7.Weight = 308.09341585667579D;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
            this.PageFooter.HeightF = 74.04175F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1.041667F, 29.33337F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(981F, 34.7084F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // Rpt6
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.DetailReport,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(40, 59, 157, 31);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Version = "21.2";
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel xrLabel20;
        private DevExpress.XtraReports.UI.XRLabel label39;
        private DevExpress.XtraReports.UI.XRLabel label38;
        private DevExpress.XtraReports.UI.XRLabel label37;
        private DevExpress.XtraReports.UI.XRLabel xrLabel21;
        private DevExpress.XtraReports.UI.XRLabel xrLabel22;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRShape xrShape1;
        private DevExpress.XtraReports.UI.XRLabel label5;
        private DevExpress.XtraReports.UI.XRLabel label1;
        private DevExpress.XtraReports.UI.XRLabel label2;
        private DevExpress.XtraReports.UI.XRLabel label32;
        private DevExpress.XtraReports.UI.XRLabel label3;
        private DevExpress.XtraReports.UI.XRLabel label4;
        private DevExpress.XtraReports.UI.XRLabel label6;
        private DevExpress.XtraReports.UI.XRLabel label48;
        private DevExpress.XtraReports.UI.XRLabel label46;
        private DevExpress.XtraReports.UI.XRLabel label45;
        private DevExpress.XtraReports.UI.XRLabel label44;
        private DevExpress.XtraReports.UI.XRLabel label40;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
    }
}
