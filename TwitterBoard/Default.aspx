<% @Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TwitterBoard._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <div class="jumbotron">
        <h1>Twitter Board</h1>
        <p class="lead">Twitter Board is a simple, straight-forward app for posting messages for all or individual users to see.  It employs the Azure SQL framework and is very, very neat.</p>
        <asp:LoginView runat="server">
            <AnonymousTemplate>
                <div class="navbar-form">
                    <a href="Account/Login"    class="btn btn-primary btn-lg">Log in</a>
                    <a href="Account/Register" class="btn btn-primary btn-lg">Register</a>
                </div>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <div class="navbar-form">
                <input type="text" class="form-control tweet-post" id="tweet-text" />
                <button id="tweet-button" class="btn btn-primary btn-lg" onclick="return postToSql();"
                    value="post tweet">Post!</button>
                </div>
            </LoggedInTemplate>
        </asp:LoginView>
        <div id="messages"></div>
        <div id="client-test-div">Awaiting messages.</div>
        <script src="TwitterBoard.js"></script>
    </div>

<%--
    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>
--%>
</asp:Content>
