#pragma checksum "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3fe218a2e2574312577a51965f589cb37ae88559"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Borrowing_Index), @"mvc.1.0.view", @"/Views/Borrowing/Index.cshtml")]
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
#line 1 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\_ViewImports.cshtml"
using Library_Management;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3fe218a2e2574312577a51965f589cb37ae88559", @"/Views/Borrowing/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"92fa90b3e862576c09f6fc569215e0164fe898c2", @"/Views/_ViewImports.cshtml")]
    public class Views_Borrowing_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DataAccessLayer.Borrowings>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_ApplicationLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Index</h1>\r\n\r\n");
            WriteLiteral("<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 17 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.RetriveDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 20 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.DueDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 23 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.SubmitDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 26 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Student));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 29 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Book));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 35 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
         foreach (var item in Model)
        {
            if (item.SubmitDate == DateTime.MinValue)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n        <td>\r\n            ");
#nullable restore
#line 41 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.RetriveDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
#nullable restore
#line 44 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.DueDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            Yet Not Submitted Book\r\n        </td>\r\n        <td>\r\n            ");
#nullable restore
#line 50 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.Student.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
#nullable restore
#line 53 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.Book.BookName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n");
            WriteLiteral("        </td>\r\n    </tr> ");
#nullable restore
#line 60 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
          }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>\r\n                                ");
#nullable restore
#line 65 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.RetriveDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 68 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.DueDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 71 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.SubmitDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 74 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Student.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 77 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Book.BookName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n");
            WriteLiteral("                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 85 "C:\Users\Sai\Desktop\clone\maro-project\Library Management\Views\Borrowing\Index.cshtml"
                    }
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DataAccessLayer.Borrowings>> Html { get; private set; }
    }
}
#pragma warning restore 1591