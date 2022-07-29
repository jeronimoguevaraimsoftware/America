#pragma checksum "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step2Text.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5c2645ba584c9bf0a1610a16ad5b46eebef731e8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ConditioningOrder__Step2Text), @"mvc.1.0.view", @"/Views/ConditioningOrder/_Step2Text.cshtml")]
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
#line 1 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\_ViewImports.cshtml"
using LiberacionProductoWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\_ViewImports.cshtml"
using LiberacionProductoWeb.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step2Text.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5c2645ba584c9bf0a1610a16ad5b46eebef731e8", @"/Views/ConditioningOrder/_Step2Text.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db18a59dec23355f1f262b7f88f2a4018d585ecc", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ConditioningOrder__Step2Text : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LiberacionProductoWeb.Models.ConditioningOrderViewModels.ConditioningOrderViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <div class=""panel-group"" id=""step-2-accordionOA"">
            <div class=""panel panel-primary overflow-hidden"">
                <div class=""panel-heading"">
                    <h3 class=""panel-title"">
                        <a class=""accordion-toggle accordion-toggle-styled"" data-toggle=""collapse""
                            data-parent=""#step-2-accordionOA"" href=""#step-2-collapseOA-three"" aria-expanded=""true"">
                            <i id=""icon-2"" class=""fa fa-plus-circle pull-right""></i>
                            Equipos analíticos
                        </a>
                    </h3>
                </div>
                <div id=""step-2-collapseOA-three""");
            BeginWriteAttribute("class", " class=\"", 917, "\"", 946, 1);
#nullable restore
#line 18 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step2Text.cshtml"
WriteAttributeValue("", 925, Model.ShowPanelSteps, 925, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 947, "\"", 955, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    <div class=""panel-body"">
                        <p>
                            Los equipos de monitoreo utilizados durante el acondicionamiento de
                            producto
                            en las unidades de distribución, se indican en el ""Plan de calidad de
                            ");
#nullable restore
#line 24 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step2Text.cshtml"
                        Write(Model.Product);

#line default
#line hidden
#nullable disable
            WriteLiteral(", que aplique en ");
#nullable restore
#line 24 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step2Text.cshtml"
                                                         Write(Model.Plant);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" y su estado de calibración
                            puede
                            obtenerse de la etiqueta de calibración adherida al equipo, o del
                            registro
                            de calibración correspondiente y que se encuentra disponible en forma
                            impresa o en el sistema INFOR EAM.
                        </p>

                        <p>
                            Verificar y registrar el estado de calibración de los equipos de
                            monitoreo
                            indicados en la Tabla 1 - Equipos analíticos.
                        </p>
                    </div>
                </div>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LiberacionProductoWeb.Models.ConditioningOrderViewModels.ConditioningOrderViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
