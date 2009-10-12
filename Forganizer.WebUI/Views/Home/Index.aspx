<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Forganizer.DomainModel.Entities.FileObject>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div id="content">
        <fieldset class="clear">
            <legend><i class="arrow_blue"></i>files</legend>
            <ul class="list_vertical">
                <li class="title">
                    <span class="name"></span>
                    <span class="tags title">tags</span>
                </li>
                <% foreach (var fileObject in Model) { %>
                <li>
                    <a class="name" title="<%= fileObject.FilePath %>">
                        <i class="<%= fileObject.FileInfo.Extension.Replace(".","") %>"></i>
                        <%= fileObject.FileInfo.Name%>
                    </a>    
                    <span class="tags"><% foreach (var tag in fileObject.Tags)
                                          { %><%= tag%> <% } %></span>
                    <span class="actions">
                        <%= Html.ActionLink(" ", "Delete", new { fileObject.Id }, new { @class = "delete", title = "delete" })%>
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
    <div id="sidebar" class="third right">
        <fieldset class="box">
            <legend>
                <span id="tagFilter">
                    <% using (Html.BeginForm()) { %>
                        <%= Html.DropDownList("tagAndOr",
                            new List<SelectListItem>() { 
                                new SelectListItem() { Value="And", Text="And" } ,
                                new SelectListItem() { Value="Or", Text="Or", 
                                    Selected = (ViewData["tagAndOr"] == null ? false : ViewData["tagAndOr"].Equals("Or"))  }
                            })%>
                        <input type="submit" value="change" />
                    <% } %>    
                </span>
                <i class="tag_green"></i>tags
            </legend>
            <% Html.RenderAction("TagCloud", "Box"); %>
        </fieldset>
        <fieldset class="box">
            <legend><i class="tag_blue"></i>extensions</legend>
            <% Html.RenderAction("ExtensionCloud", "Box"); %>
        </fieldset>
        <fieldset class="box">
            <legend><i class="tag_red"></i>categories</legend>
            <% Html.RenderAction("Box", "Category", new { fileObjects = Model }); %>
        </fieldset>                
    </div>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    forganizer
</asp:Content>