#pragma checksum "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1cc57627f0bc09dd3c01d06275f6465e9985fbef"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LayoutCertificate__Footer), @"mvc.1.0.view", @"/Views/LayoutCertificate/_Footer.cshtml")]
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
#line 1 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
using LiberacionProductoWeb.Localize;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
using Microsoft.Extensions.Localization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1cc57627f0bc09dd3c01d06275f6465e9985fbef", @"/Views/LayoutCertificate/_Footer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db18a59dec23355f1f262b7f88f2a4018d585ecc", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_LayoutCertificate__Footer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LiberacionProductoWeb.Models.LayoutCertificateViewModels.LeyendsCertificateVM>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("height: 130px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.TextAreaTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 7 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
 if (Model.leyendsFooterCertificateVM != null)
{
    int Index = 0;
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
     foreach (var item in Model.leyendsFooterCertificateVM)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <br />\r\n        <label>");
#nullable restore
#line 13 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
          Write(item.PlantName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n        <br />\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("textarea", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1cc57627f0bc09dd3c01d06275f6465e9985fbef5499", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.TextAreaTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 530, "FooterTxt-", 530, 10, true);
#nullable restore
#line 15 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
AddHtmlAttributeValue("", 540, Index, 540, 6, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
#nullable restore
#line 15 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item.Footer);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        <script>\r\n            $(document).ready(function () {\r\n                tinymce.init({\r\n                    selector: \'#FooterTxt-");
#nullable restore
#line 19 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
                                     Write(Index);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                    height: ""180"",
                    menubar: false,
                    resize: false,
                    elementpath: false,
                    language: 'es_MX',
                    mode: ""readonly""
                });
                tinymce.activeEditor.mode.set(""readonly"")
            });
        </script>
");
#nullable restore
#line 30 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
        Index++;
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\Users\ImSoftware\Source\Repos\liberacion-producto-web\LiberacionProductoWeb\LiberacionProductoWeb\Views\LayoutCertificate\_Footer.cshtml"
     
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LiberacionProductoWeb.Models.LayoutCertificateViewModels.LeyendsCertificateVM> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591