#pragma checksum "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "63611b4121f03138673194cdeb686a3a162c20e3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__SerchTool), @"mvc.1.0.view", @"/Views/Shared/_SerchTool.cshtml")]
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
#line 1 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
using LiberacionProductoWeb.Localize;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
using LiberacionProductoWeb.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
using Microsoft.AspNetCore.Mvc;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63611b4121f03138673194cdeb686a3a162c20e3", @"/Views/Shared/_SerchTool.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db18a59dec23355f1f262b7f88f2a4018d585ecc", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__SerchTool : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LiberacionProductoWeb.Models.SechToolViewModels.SechToolDistributionBatchVM>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ProductionOrder", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ConditioningOrder", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 11 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
 if (Model?.SechToolDistributions != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div style=""width: 250px; float: right; height:200px; overflow-y: scroll; position: absolute; background: linear-gradient(#F1F5F7, #DCE6EB); "" class=""alert alert-info"">
        <a class=""close text-right"" data-dismiss=""alert"" href=""#"">&times;</a>
        <br />
        <table id=""table-Serch"" class=""table table-striped table-bordered"">
            <thead>
                <tr>
                    <th colspan=""3"">Coincidencias</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 23 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
                 foreach (var entry in Model.SechToolDistributions)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n");
#nullable restore
#line 27 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
                             if (entry.Source == "ProductionOrder")
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "63611b4121f03138673194cdeb686a3a162c20e37712", async() => {
                WriteLiteral("\r\n                                    ");
#nullable restore
#line 30 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
                               Write(entry.DistributionBatch);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 29 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
                                                                                           WriteLiteral(entry.ProductionOrderId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 32 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "63611b4121f03138673194cdeb686a3a162c20e311024", async() => {
                WriteLiteral("\r\n                                    ");
#nullable restore
#line 36 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
                               Write(entry.DistributionBatch);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 35 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
                                                                                             WriteLiteral(entry.ConditioningOrderId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 38 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 41 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n\r\n    </div>\r\n");
#nullable restore
#line 46 "C:\Users\ImSoftware\source\repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\Shared\_SerchTool.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LiberacionProductoWeb.Models.SechToolViewModels.SechToolDistributionBatchVM> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
