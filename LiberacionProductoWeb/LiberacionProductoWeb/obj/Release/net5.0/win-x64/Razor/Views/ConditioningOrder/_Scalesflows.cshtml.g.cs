#pragma checksum "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d63f57ea3c24dce3e8db3eb2ea84eb08bd18f3aa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ConditioningOrder__Scalesflows), @"mvc.1.0.view", @"/Views/ConditioningOrder/_Scalesflows.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
#line 1 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
using LiberacionProductoWeb.Localize;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
using LiberacionProductoWeb.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
using Microsoft.AspNetCore.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d63f57ea3c24dce3e8db3eb2ea84eb08bd18f3aa", @"/Views/ConditioningOrder/_Scalesflows.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db18a59dec23355f1f262b7f88f2a4018d585ecc", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ConditioningOrder__Scalesflows : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LiberacionProductoWeb.Models.ConditioningOrderViewModels.ConditioningOrderViewModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("hidden", new global::Microsoft.AspNetCore.Html.HtmlString("hidden"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"

<div class=""form-group-sm"" style=""margin-bottom: 20px;"">
    <div class=""table-responsive"">
        <table id=""table-2"" class=""table table-striped table-bordered""
               style=""width:60%;"">
            <thead>
                <tr>
                    <th colspan=""3"">Tabla 2: Básculas y flujometros</th>
                </tr>
                <tr>
                    <th scope=""col"">Calibración de la báscula / flujometro</th>
                    <th scope=""col"">Estado de calibración</th>
                    <th scope=""col"">Revisado por</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 27 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
                  
                    int scalesflowsIndex = 0;
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
                 foreach (var item in Model.ScalesflowsList)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td hidden=\"hidden\"");
            BeginWriteAttribute("id", " id=\"", 1285, "\"", 1322, 2);
            WriteAttributeValue("", 1290, "ScalesflowsId", 1290, 13, true);
#nullable restore
#line 33 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
WriteAttributeValue("", 1303, scalesflowsIndex, 1303, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 33 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
                                                                             Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d63f57ea3c24dce3e8db3eb2ea84eb08bd18f3aa7640", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 35 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item.State);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1430, "ScalesflowsState", 1430, 16, true);
#nullable restore
#line 35 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
AddHtmlAttributeValue("", 1446, scalesflowsIndex, 1446, 19, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d63f57ea3c24dce3e8db3eb2ea84eb08bd18f3aa10077", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 36 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item.Description);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1545, "ScalesflowsDescription", 1545, 22, true);
#nullable restore
#line 36 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
AddHtmlAttributeValue("", 1567, scalesflowsIndex, 1567, 19, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
#nullable restore
#line 37 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
                       Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            <input type=\"checkbox\" class=\"yes-no-scalesflows\"");
            BeginWriteAttribute("id", " id=\"", 1784, "\"", 1828, 2);
            WriteAttributeValue("", 1789, "ScalesflowsCheckTrue", 1789, 20, true);
#nullable restore
#line 40 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
WriteAttributeValue("", 1809, scalesflowsIndex, 1809, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("\r\n                                   form-control-lg\r\n                                   data-linde-control-value=\"true\"\r\n                                   data-linde-control-group=\"Scalesflows\"\r\n                                   data-linde-control-id=\"");
#nullable restore
#line 44 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
                                                      Write(scalesflowsIndex);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" />\r\n                            <label>Calibrado</label>\r\n                            <input type=\"checkbox\" class=\"yes-no-scalesflows\"");
            BeginWriteAttribute("id", " id=\"", 2240, "\"", 2285, 2);
            WriteAttributeValue("", 2245, "ScalesflowsCheckFalse", 2245, 21, true);
#nullable restore
#line 46 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
WriteAttributeValue("", 2266, scalesflowsIndex, 2266, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"
                                   form-control-lg
                                   data-linde-control-value=""false""
                                   data-linde-control-group=""Scalesflows""
                                   data-linde-control-id=""");
#nullable restore
#line 50 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
                                                      Write(scalesflowsIndex);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" onclick=\"BlockPipelineClearance()\" />\r\n                            <label>No calibrado</label>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d63f57ea3c24dce3e8db3eb2ea84eb08bd18f3aa15493", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 52 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item.IsCalibrated);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2727, "ScalesflowsIsCalibrated", 2727, 23, true);
#nullable restore
#line 52 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
AddHtmlAttributeValue("", 2750, scalesflowsIndex, 2750, 19, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d63f57ea3c24dce3e8db3eb2ea84eb08bd18f3aa18010", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 55 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item.ReviewedBy);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2909, "ScalesflowsReviewedBy", 2909, 21, true);
#nullable restore
#line 55 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
AddHtmlAttributeValue("", 2930, scalesflowsIndex, 2930, 19, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d63f57ea3c24dce3e8db3eb2ea84eb08bd18f3aa20232", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 56 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item.ReviewedDate);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 3039, "ScalesflowsReviewedDate", 3039, 23, true);
#nullable restore
#line 56 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
AddHtmlAttributeValue("", 3062, scalesflowsIndex, 3062, 19, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            <div");
            BeginWriteAttribute("id", " id=\"", 3135, "\"", 3187, 2);
            WriteAttributeValue("", 3140, "ScalesflowsReviewedSignature", 3140, 28, true);
#nullable restore
#line 57 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
WriteAttributeValue("", 3168, scalesflowsIndex, 3168, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></div>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 60 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\ConditioningOrder\_Scalesflows.cshtml"
                    scalesflowsIndex++;
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer Localizer { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IStringLocalizer<Resource> resource { get; private set; } = default!;
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
