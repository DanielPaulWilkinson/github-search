<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error404.aspx.cs" Inherits="github_search.Error404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Page not found</title>
    <link href="~/Content/css/site.min.css" rel="stylesheet" />
</head>
<body class="bg-blue">
    <form id="ErrorPage" runat="server">
        <div>
            <h1 class="text-center color-white">404 - Page not found</h1>
            <p class="text-center color-white">Hmm, it appears the page you are looking for was moved, removed, renamed or doesn't exist anymore.</p>
            <p class="text-center color-white"><a class="color-white"  href="/">Back Home</a></p>
        </div>
    </form>
</body>
</html>
