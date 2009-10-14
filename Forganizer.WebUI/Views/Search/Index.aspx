<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Forganizer.DomainModel.Entities.FileAndTagCollection>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="content">
        <fieldset class="clear">
            <legend><i class="arrow_blue"></i>files</legend>
            <ul class="list_vertical">
                <li class="title">
                    <span class="name"></span>
                    <span class="tags title">tags</span>
                </li>
                <% foreach (var fileObject in Model.PageOfFileObjects) { %>
                <li>
                    <a class="name" title="<%= fileObject.FilePath %>">
                        <i class="<%= fileObject.FileInfo.Extension.Replace(".","") %>"></i>
                        <%= fileObject.FileInfo.Name%>
                    </a>    
                    <span class="tags">
                        <% using(Html.BeginForm()) { %>
                            <span class="add_tag">
                                <%= Html.Hidden("Id", fileObject.Id) %>
                                <%= Html.TextBox("AddTag") %>
                                <input type="submit" value="add" />
                            </span>
                        <% } %>
                        <% foreach (var tag in fileObject.Tags) { %><span class="tag"><%= tag%> <a href="#">delete</a></span><% } %>
                    </span>
                    <span class="actions">
                        <%= Html.ActionLink(" ", "Delete", 
                            new { fileObject.Id, returnUrl = ViewContext.HttpContext.Request.Url.ToString() }, 
                            new { @class = "delete", title = "delete" })%>
                        <a target="_blank" href="<%= fileObject.FileInfo.DirectoryName %>" class="folder" title="folder"> </a>
                        <a href="<%= fileObject.FilePath %>" class="download" title="download"> </a>
                    </span>
                </li>
                <% } %>
            </ul>
            <div class="pager">
                <%= Html.PageLinks((int)ViewData["CurrentPage"], (int)ViewData["TotalPages"], x => ViewData["currentQuery"] + "/page" + x + ViewData["queryString"])%>   
            </div> 
        </fieldset>
    </div>
    <% Html.RenderAction("Index", "SideBar",  new { fileAndTagCollection = Model }); %>
    <div class="clear"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    forganizer
</asp:Content>
