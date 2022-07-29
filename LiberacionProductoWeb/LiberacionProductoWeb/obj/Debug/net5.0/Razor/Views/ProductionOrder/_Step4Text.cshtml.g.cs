#pragma checksum "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step4Text.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6939c04a2dc29cdc408810180eb71b964a2677de"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ProductionOrder__Step4Text), @"mvc.1.0.view", @"/Views/ProductionOrder/_Step4Text.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\_ViewImports.cshtml"
using LiberacionProductoWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\_ViewImports.cshtml"
using LiberacionProductoWeb.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step4Text.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6939c04a2dc29cdc408810180eb71b964a2677de", @"/Views/ProductionOrder/_Step4Text.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db18a59dec23355f1f262b7f88f2a4018d585ecc", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ProductionOrder__Step4Text : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LiberacionProductoWeb.Models.ProductionOrderViewModels.ProductionOrderViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""panel-group"" id=""step-4-accordion"">
    <div class=""panel panel-primary overflow-hidden"">
        <div class=""panel-heading"">
            <h3 class=""panel-title"">
                <a class=""accordion-toggle accordion-toggle-styled"" data-toggle=""collapse""
                   data-parent=""#step-4-accordion"" href=""#step-4-collapse-three"" aria-expanded=""true"">
                    <i id=""icon-4"" class=""fa fa-plus-circle pull-right""></i>
                    4. Monitoreo de los atributos críticos de calidad del proceso de producción
                </a>
            </h3>
        </div>
        <div id=""step-4-collapse-three""");
            BeginWriteAttribute("class", " class=\"", 817, "\"", 846, 1);
#nullable restore
#line 16 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step4Text.cshtml"
WriteAttributeValue("", 825, Model.ShowPanelSteps, 825, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 847, "\"", 855, 0);
            EndWriteAttribute();
            WriteLiteral(@">
            <div class=""panel-body"">
                <p>
                    Los atributos críticos de calidad se monitorean directamente en el
                    sistema computarizado de control de planta iFIX, durante la fabricación del lote de
                    ");
#nullable restore
#line 21 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step4Text.cshtml"
                Write(Model.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" medicinal, verificando que los parámetros se
                    encuentren dentro del rango especificado. En caso de incumplimiento, se
                    notifica al Superintendente de planta y/o al Gerente regional de
                    producción, se documenta el ""Informe de desviación, CC/R-01-028 - 30"" y
                    se aplica lo establecido en el procedimiento ""Control de desviaciones,
                    CC/P-01-009-30"".
                </p>

                <p>
                    En la Tabla 7- Atributos críticos de calidad, se registra el resultado
                    del monitoreo de los atributos críticos de calidad durante la fabricación del lote. Cuando alguno
                    de los atributos críticos de calidad, está fuera de los rangos
                    establecidos, se documentará el número de folio del ""Reporte de producto
                    no conformeO/R-02-011 - 10"" correspondiente y se aplicará lo establecido
                    en el procedimiento ""Contro");
            WriteLiteral(@"l de producto no conforme, OO/P-02-001 - 10"".
                </p>

                <p>
                    Los valores históricos promedio, máximo y mínimo del monitoreo de los
                    atributos críticos de calidad de los lotes previos, se muestran en la
                    Tabla 7- Atributos críticos de calidad como referencia para identificar
                    los cambios en la variabilidad del proceso, a fin de poder corregirlos y
                    llevarlos nuevamente a sus condiciones validadas de operación.
                </p>

            </div>
        </div>
    </div>
</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer Localizer { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LiberacionProductoWeb.Models.ProductionOrderViewModels.ProductionOrderViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
