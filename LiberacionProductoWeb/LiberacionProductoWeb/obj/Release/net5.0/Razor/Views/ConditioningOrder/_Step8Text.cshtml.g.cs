#pragma checksum "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step8Text.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fcf597b7275533f6ff0a3348124266374ce1b9b6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ConditioningOrder__Step8Text), @"mvc.1.0.view", @"/Views/ConditioningOrder/_Step8Text.cshtml")]
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
#line 1 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step8Text.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fcf597b7275533f6ff0a3348124266374ce1b9b6", @"/Views/ConditioningOrder/_Step8Text.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db18a59dec23355f1f262b7f88f2a4018d585ecc", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ConditioningOrder__Step8Text : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LiberacionProductoWeb.Models.ConditioningOrderViewModels.ConditioningOrderViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <div class=""panel-group"" id=""step-8-accordionOA"">
            <div class=""panel panel-primary overflow-hidden"">
                <div class=""panel-heading"">
                    <h3 class=""panel-title"">
                        <a class=""accordion-toggle accordion-toggle-styled"" data-toggle=""collapse""
                            data-parent=""#step-8-accordionOA"" href=""#step-8-collapseOA-three"" aria-expanded=""true"">
                            <i id=""icon-8"" class=""fa fa-plus-circle pull-right""></i>
                            Rendimiento del proceso de acondicionamiento.
                        </a>
                    </h3>
                </div>
                <div id=""step-8-collapseOA-three""");
            BeginWriteAttribute("class", " class=\"", 944, "\"", 973, 1);
#nullable restore
#line 18 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step8Text.cshtml"
WriteAttributeValue("", 952, Model.ShowPanelSteps, 952, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 974, "\"", 982, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <div class=\"panel-body\">\r\n                        <p>\r\n");
            WriteLiteral("                        </p>\r\n");
#nullable restore
#line 27 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step8Text.cshtml"
                         if (Model.Product != "CO2")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            <p>
                                El tamaño del lote de producción en toneladas, es determinado por la
                                multiplicación del nivel del tanque reportado en porcentaje (%), el cual
                                se
                                observa en la pantalla del sistema computarizado iFix, por el factor
                                de ingeniería que aplica al tanque de almacenaminento.
                            </p>
");
#nullable restore
#line 36 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step8Text.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <p>
                        Las toneladas de producto en los tanques de almacenamiento de planta se calculan
                        capturando el nivel del tanque (In H2O) en la hoja electrónica ""Calculo de masa en
                        tanques de Morelos 2019-05-05 1000 am"" disponible en la computadora de cuarto de control;
                        de acuerdo con lo establecido en el procedimiento ""Manual de usuario de la hoja “Cálculo de
                        masa en tanques de almacenamiento de producto de planta CO2 Morelos”, OO /P-02-017 – 806"".
                    </p>
");
#nullable restore
#line 46 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Step8Text.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                        <p>
                            El producto contenido en el tanque de almacenamiento de la localidad, es
                            llenado (acondicionamiento) en unidades de distribución dedicadas a un
                            solo
                            tipo de producto, las cuales se registran en la Tabla 5 - Control de
                            llenado
                            de pipas.
                        </p>

                        <p>
                            En la Tabla 6 - Rendimiento del proceso de acondicionamiento, se reportan
                            la
                            cantidad de producto medicinal fabricado en el lote y, la cantidad de
                            producto acondicionado en las unidades de distribución.
                        </p>

                        <p>
                            El tamaño de los lotes de producto terminado varía en función de la
                            demanda
      ");
            WriteLiteral(@"                      de producto en el mercado y, el inventario existente del producto en la
                            instalación. En todos los casos existirá una diferencia del producto
                            acondicionado vs el tamaño del lote de producción, debido a que el
                            contenido
                            del tanque de almacenamiento de producto en planta, se utiliza para
                            suministrar producto medicinal y otros grados diferentes al medicinal a
                            los
                            clientes.
                        </p>

                        <p>
                            El rendimiento no influye en el cumplimiento de especificaciones por ser
                            un
                            proceso cerrado y planta dedicada.
                        </p>
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