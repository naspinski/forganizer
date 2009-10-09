<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Forganizer.DomainModel.Entities.FileObject>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>recently modified</h3>
    <ul>
        <% foreach (var fileObject in Model) { %>
            <li><%= fileObject.FileInfo.Name %></li>
        <% } %>
    </ul>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    forganizer
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Title" runat="server">
    dashboard
</asp:Content>
