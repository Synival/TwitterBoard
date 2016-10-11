<%@ Page Title="Session Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SessionInfo.aspx.cs" Inherits="TwitterBoard.SessionInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>Below is a list of session variables used for debugging this app.  If you're not allowed to see this, please don't look at the information below.</p>
    <div id="sessionDiv" runat="server"></div>
</asp:Content>
