<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HandleErrorInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="content">
        <fieldset>
            <legend><i class="error"></i><%= Model.Exception.GetType().ToString() %></legend>
            <div>you clever/unlucky bastard, you found an error; if this persists, please open a new <a href="http://forganizer.codeplex.com/WorkItem/List.aspx">work item</a> </div>
            <div>
                <label>controller</label>
                <%= Html.Encode(Model.ControllerName) %>
            </div>
            <div>
                <label>action</label>
                <%= Html.Encode(Model.ActionName) %>
            </div>
            <div>
                <label>message</label>
                <%= Html.Encode(Model.Exception.Message) %>
                <% if (Model.Exception.InnerException != null) { %> - <%= Html.Encode(Model.Exception.InnerException.Message) %><% } %>
            </div>
        </fieldset>    
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    welcome to error town
</asp:Content>
