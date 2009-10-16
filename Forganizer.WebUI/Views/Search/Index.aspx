<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Forganizer.DomainModel.Entities.FileAndTagCollection>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="content">
        <fieldset class="clear">
            <legend><i class="arrow_blue"></i>files</legend>
            <% using (Html.BeginForm("Index", "Search", new { returnUrl = ViewContext.HttpContext.Request.Url.ToString() })) { %>
                <ul class="list_vertical">
                    <li class="title">
                        <span class="name"></span>
                        <span class="actions"> </span>
                        <span class="tags title">tags<input type="submit" value="add tags" /></span>
                    </li>
                    <% int count = 1; foreach (var fileObject in Model.PageOfFileObjects) { %>
                    <li>
                        <a class="name" title="<%= fileObject.FilePath %>">
                            <i class="<%= fileObject.FileInfo.Extension.Replace(".","") %>"></i>
                            <%= fileObject.FileInfo.Name%>
                        </a>    
                        <span class="actions">
                            <%= Html.ActionLink(" ", "Delete", 
                                new { fileObject.Id, returnUrl = ViewContext.HttpContext.Request.Url.ToString() }, 
                                new { @class = "delete", title = "delete" })%>
                            <a target="_blank" href="<%= fileObject.FileInfo.DirectoryName %>" class="folder" title="folder"> </a>
                            <%= Html.ActionLink(" ", "Download", new { filePath = fileObject.FilePath }, 
                                new { @class = "download", title = "download" })%>
                        </span>
                        <span class="tags">
                            <span class="add_tag"><%= Html.TextBox("AddTagsTo" + fileObject.Id, null, new { tabindex = count++ })%></span>
                            <% foreach (var tag in fileObject.Tags) { %>
                            <span class="tag">
                                <%= tag%><%= Html.ActionLink("[delete]", "DeleteTag", 
                                    new { fileObject.Id, tag, returnUrl = ViewContext.HttpContext.Request.Url.ToString() }, 
                                    new { title = "delete" })%></span>
                            <% } %>
                        </span>
                    </li>
                <% } %>
                </ul>
            <% } %>
            <div class="pager">
                <%= Html.PageLinks((int)ViewData["CurrentPage"], (int)ViewData["TotalPages"], x => ViewData["currentQuery"] + "/Page" + x + ViewData["queryString"])%>   
            </div> 
        </fieldset>
    </div>
    <% Html.RenderAction("Index", "SideBar",  new { fileAndTagCollection = Model }); %>
    <div class="clear"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    forganizer
</asp:Content>
