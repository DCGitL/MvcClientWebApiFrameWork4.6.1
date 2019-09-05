<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormTest.aspx.cs" Inherits="NoneCoreMvcWebApiClient.WebFormTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width,initial-scale=1" />

    <link href="~/lib/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="~/lib/jquery/jquery.min.js"></script>
    <script src="~/lib/jquery/validation/jquery.validate.min.js"></script>
    <script src="~/lib/jquery/validation/unobtrusive/jquery.validate.unobtrusive.min.js"></script>
    <script src="~/lib/bootstrap/js/bootstrap.js"></script>

    <title>This is a aspx Test Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Pagination - Active State</h2>
            <p>Add class .active to let the user know which page he/she is on:</p>
            <ul class="pagination">
                <li><a href="#">1</a></li>
                <li class="active"><a href="#">2</a></li>
                <li><a href="#">3</a></li>
                <li><a href="#">4</a></li>
                <li><a href="#">5</a></li>
            </ul>
        </div>
    </form>
</body>
</html>
