#pragma checksum "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step9Text.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "338c90ccada5282035c613b2534b3e6660d1ee3f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ConditioningOrder__Step9Text), @"mvc.1.0.view", @"/Views/ConditioningOrder/_Step9Text.cshtml")]
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
#line 1 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step9Text.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"338c90ccada5282035c613b2534b3e6660d1ee3f", @"/Views/ConditioningOrder/_Step9Text.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db18a59dec23355f1f262b7f88f2a4018d585ecc", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ConditioningOrder__Step9Text : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LiberacionProductoWeb.Models.ConditioningOrderViewModels.ConditioningOrderViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <div class=""panel-group"" id=""step-9-accordionOA"">
            <div class=""panel panel-primary overflow-hidden"">
                <div class=""panel-heading"">
                    <h3 class=""panel-title"">
                        <a class=""accordion-toggle accordion-toggle-styled"" data-toggle=""collapse""
                            data-parent=""#step-9-accordionOA"" href=""#step-9-collapseOA-three"" aria-expanded=""true"">
                            <i id=""icon-9"" class=""fa fa-plus-circle pull-right""></i>
                            Liberación del producto terminado.
                        </a>
                    </h3>
                </div>
                <div id=""step-9-collapseOA-three""");
            BeginWriteAttribute("class", " class=\"", 933, "\"", 962, 1);
#nullable restore
#line 18 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step9Text.cshtml"
WriteAttributeValue("", 941, Model.ShowPanelSteps, 941, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 963, "\"", 971, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    <div class=""panel-body"">
                        <p>
                            Con la firma del responsable sanitario en la Tabla 7- Cierre de
                            expediente, se avala que:
                        </p>

                        <p>a) se ha realizado un adecuado despeje de línea,</p>

                        <p>
                            b) el producto acondicionado en cada una de las unidades de distribución
                            cumple con las especificaciones establecidas en la FEUM vigente y, las
                            buenas prácticas de fabricación,
                        </p>

                        <p>c) la documentación se encuentra completa,</p>

                        <p>
                            d) la validación del proceso de producción y acondicionamiento de
                            producto
                            medicinal en ");
#nullable restore
#line 38 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step9Text.cshtml"
                                     Write(Model.Plant);

#line default
#line hidden
#nullable disable
            WriteLiteral(@", se encuentra vigente,
                        </p>

                        <p>
                            e) los controles de cambios se administran de acuerdo a lo establecido en
                            el
                            procedimiento “CC/P-08-001 – 30” vigente y,
                        </p>

                        <p>
                            f) en caso de incumplimientos, se documentó el ""Informe de desviación,
                            CC/R-01-028 - 30"" y se condujo de acuerdo a lo establecido en el
                            procedimiento “Control de desviaciones, CC/P-01-009- 30” vigente,
                        </p>

                        <p>por lo tanto, el producto puede ser enviado a los clientes.</p>
                    </div>
                </div>


            </div>
        </div>
    </div>
</div>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LiberacionProductoWeb.Models.ConditioningOrderViewModels.ConditioningOrderViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
