#pragma checksum "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "29b3ea6aafde4d865a2166e000b4b923cbc69e0d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ProductionOrder__Step2Text), @"mvc.1.0.view", @"/Views/ProductionOrder/_Step2Text.cshtml")]
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
#line 1 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"29b3ea6aafde4d865a2166e000b4b923cbc69e0d", @"/Views/ProductionOrder/_Step2Text.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db18a59dec23355f1f262b7f88f2a4018d585ecc", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ProductionOrder__Step2Text : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LiberacionProductoWeb.Models.ProductionOrderViewModels.ProductionOrderViewModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""panel-group"" id=""step-2-accordion"">
    <div class=""panel panel-primary overflow-hidden"">
        <div class=""panel-heading"">
            <h3 class=""panel-title"">
                <a class=""accordion-toggle accordion-toggle-styled"" data-toggle=""collapse""
                   data-parent=""#step-2-accordion"" href=""#step-2-collapse-three"" aria-expanded=""true"">
                    <i id=""icon-2"" class=""fa fa-plus-circle pull-right""></i>
                    2. Despeje de línea
                </a>
            </h3>
        </div>
        <div id=""step-2-collapse-three""");
            BeginWriteAttribute("class", " class=\"", 761, "\"", 790, 1);
#nullable restore
#line 16 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
WriteAttributeValue("", 769, Model.ShowPanelSteps, 769, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("style", " style=\"", 791, "\"", 799, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <div class=\"panel-body\">\r\n");
#nullable restore
#line 18 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                 if (Model.ProductName != "CO2")
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <p>
                        La producción de gases medicinales a partir del aire ambiental, se
                        realiza en una unidad de separación de aire (ASU, por sus siglas en
                        inglés), mediante el proceso de destilación fraccionada, el cual se
                        fundamenta en la separación de los principales componentes del aire (O2,
                        N2 y Ar), debido a las diferencias entre sus volatilidades relativas.
                    </p>
");
            WriteLiteral(@"                    <p>
                        La unidad de separación de aire, es el conjunto de equipos (caseta de
                        filtros, compresores, separadores de humedad, malla molecular, sistemas
                        de enfriamiento y lubricación, columna de destilación, condensador
                        principal, recipientes criogénicos, entre otros) que permite la
                        disociación de los componentes del aire. Verificar el diagrama de flujo
                        ""Producción de ");
#nullable restore
#line 34 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                                   Write(Model.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" medicinal\", en\r\n                        el SCD.\r\n                    </p>\r\n");
#nullable restore
#line 37 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <p>
                La producción de CO2 medicinal, se realiza mediante la purificación del CO2 crudo (raw gas) 
                a través de un sistema específico para la remoción de los contaminantes esperados en la fuente 
                del raw gas (integrado por filtros, compresores, separadores de humedad, intercambiadores de calor, etc), 
                así como de un secado mediante alúmina (selexsorb) y un sistema de eliminación de olor y sabor a través 
                de carbón activado. La corriente de gas ya purificada es sometida a un proceso de destilación y licuefacción 
                en el cual se eliminan las impurezas incondensables para finalmente almacenar el producto final en su fase 
                líquida, en los tanques de planta.

            </p>
");
            WriteLiteral(@"            <p>
                El sistema de purificación de la planta Morelos está integrado principalmente por un hidrolizador para remover 
                las posibles trazas de azufre y, de un equipo catalítico que elimina el óxido de etileno y otras trazas de impurezas.
            </p>
");
            WriteLiteral("            <p>\r\n                Verificar el procedimiento \"Fabricación de Dióxido de Carbono (CO2) grado Medicinal de planta Morelos CO2\".\r\n            </p>\r\n");
#nullable restore
#line 59 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                <p>Por lo que, para el despeje de línea se considera lo siguiente:</p>

                <p>
                    a) Existe trazabilidad al lote de producto anterior. Esta información
                    está disponible en la ""bitácora de operación establecida en la ");
#nullable restore
#line 66 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                                                                               Write(Model.Location);

#line default
#line hidden
#nullable disable
            WriteLiteral("\".\r\n                    La planta es dedicada esto significa que parte de la misma materia prima ");
#nullable restore
#line 67 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                                                                                         Write(Model.ProductName != "CO2" ? "(aire)":"(raw gas)");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    y en la obtencion del producto no hay fuentes de contaminación de otros productos.


                </p>



                <p>
                    b) El área se encuentra limpia e identificada. Considerando que el
                    producto es un gas, se tiene que manejar en un sistema cerrado, es
                    decir, no entra en contacto directo con el medio ambiente y no es
                    susceptible de alguna fuente de contaminación externa. Por lo anterior,
                    la limpieza del área de fabricación no se lleva a cabo al inicio de cada
                    lote, sino de acuerdo a lo indicado en el ""Programa de orden y limpieza
                    externa a líneas y equipos de proceso, MM/R-07-001-10"" y, en el
                    ""Programa de limpieza a instalaciones, OO/R-07-001-30"", en donde están
                    establecidas las actividades a realizar, enfocadas a una limpieza
                    externa con el propósito de imagen.
 ");
            WriteLiteral(@"               </p>

                <p>
                    c) El personal utiliza el uniforme limpio y adecuado. El
                    personal de la operación utiliza el uniforme de algodón y el equipo de
                    protección personal apropiado a la actividad que desempeña (por ejemplo:
                    pantalón, camisa manga larga, tapones auditivos, lentes, casco y zapatos
                    de seguridad, mandil criogénico, protector facial), de acuerdo a lo
                    establecido en la sección de ""Higiene del personal e instalaciones de
                    los empleados, CC/M-01-013-30"" del ""Manual de BPM y prerrequisitos"".
                    Esto considerando que el personal no entra en contacto directo con el
                    producto al ser un sistema cerrado y por lo cual no representa una
                    posible fuente de contaminación del proceso y/o producto.
                </p>

                <p>
                    d) La documentación correspon");
            WriteLiteral(@"de al lote indicado. La documentación de
                    inicio de lote está disponible en tiempo real a través de las gráficas
                    de las variables de control en proceso, de los parámetros críticos de
                    proceso y de los atributos críticos de calidad, obtenidas del sistema
                    computarizado iFix, el cual está validado.
                </p>

                <p>
                    e) Se cuenta con los insumos de acuerdo a la lista de materiales para ser
                    pesados y están identificados correctamente. La materia prima utilizada
                    es el ");
#nullable restore
#line 112 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                      Write(Model.ProductName != "CO2" ? "aire ambiental" : "CO2 crudo(raw gas)");

#line default
#line hidden
#nullable disable
            WriteLiteral(@" y las lineas de proceso están identificadas de
                    acuerdo a la normatividad aplicable a la instalación y el producto. No
                    hay alguna otra materia prima o insumo que sea necesario adicionar
                    durante todo el proceso y, que requiera ser pesado e identificado.
                </p>

                <p>
                    f) El área y equipos de proceso están libres de documentos, insumos y/o
                    cualquier otra entidad que no corresponden a la lista de materiales y el
                    lote en cuestión. El único insumo utilizado en el proceso de
                    fabricación es el ");
#nullable restore
#line 122 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                                  Write(Model.ProductName != "CO2" ? "aire ambiental" : "CO2 crudo(raw gas)");

#line default
#line hidden
#nullable disable
            WriteLiteral(@" y, la documentación se maneja en forma
                    electrónica, por lo que no existe la posibilidad de la presencia de
                    cualquier otra fuente de contaminación.
                </p>

                <p>
                    g) Los equipos e instrumentos presentes en el área están limpios e
                    identificados: Considerando que el producto es un gas se tiene que
                    manejar en sistema cerrado, es decir, no entra en contacto con el
                    medio ambiente y no es susceptible a alguna fuente de contaminación
                    externa; la limpieza de cada equipo no se lleva a cabo al inicio de
                    cada lote, si no de acuerdo a lo indicado en el “Programa de Limpieza
                    a Instalaciones, OO/R-07-001 -30” y en el “Programa de Orden y
                    Limpieza externa a líneas y equipos de proceso, MM/R-07-001–10”, donde
                    están establecidas las actividades a realizar que se enfocan");
            WriteLiteral(@" a una
                    limpieza externa con propósito de imagen y, en el procedimiento
                    “Limpieza de equipos de proceso, MM/P-08-001 - 30” cuando se realiza
                    la limpieza interna, dado que al ser equipos dedicados y cerrados,
                    no habría posibilidad de que algún contaminante visualmente detectable
                    entre en contacto directo con el producto, ni contaminación cruzada
                    entre productos.
");
            WriteLiteral("                </p>\r\n\r\n                <p>\r\n");
            WriteLiteral("                </p>\r\n\r\n                <p>\r\n                    h) Los instrumentos están calibrados. Los equipos de monitoreo se indican\r\n                    en el \"Plan de calidad de ");
#nullable restore
#line 168 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                                          Write(Model.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 168 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                                                                Write(Model.ProductCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" de\r\n                    planta \"");
#nullable restore
#line 169 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ProductionOrder\_Step2Text.cshtml"
                        Write(Model.Location);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""" y su estado de calibración puede verificarse
                    de la etiqueta de calibración adherida al equipo. Esta actividad se
                    evidencía en la Tabla 2 - Equipos de monitoreo.
                </p>

                <p>
                    i) No existe alguna fuente potencial de contaminación. Considerando que
                    la producción es en un sistema cerrado y por consiguiente, el producto
                    no entra en contacto con el medio ambiente, no hay fuentes de
                    contaminación externa que afecten al producto, y al emplearse equipos
                    dedicados en la producción, no hay fuente de contaminación interna.
                </p>

                <p>
                    Considerando todos los puntos anteriores, se considera que se lleva a
                    cabo un adecuado despeje de línea cuando todos los puntos se cumplen. La
                    realización de esta actividad se documenta en la Tabla 3 - Despeje de
    ");
            WriteLiteral("                línea.\r\n                </p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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