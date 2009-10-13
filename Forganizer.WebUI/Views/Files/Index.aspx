<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Forganizer.DomainModel.Entities.Folder>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <div id="content">
        <% using (Html.BeginForm("Edit", "Category", FormMethod.Post)) {%>
        <fieldset>
            <legend>
                <i class="arrow_blue"></i>add files
            </legend>
            <div>
                <label for="Folder">
                    <%= Html.ValidationMessage("Folder") %>from folder
                </label>
                <%= Html.TextBox("Folder") %>
            </div>
            <div>
                <label for="Exclude">exclude extensions</label>
                <%= Html.TextBox("Exclude") %>
            </div>
            <div>
                <label for="Include">include only these extensions</label>
                <%= Html.TextBox("Include") %>
            </div>
            <div>
                <label for="Recursive">recursive? <%= Html.CheckBox("Recursive") %></label>
                
            </div>
            <div class="align_center">
                <input type="submit" value="run it" />
            </div>
        </fieldset>
        <% } %>
    </div>
    <div id="sidebar">
        <fieldset class="box">
            <legend>actions</legend>
            <ul class="list_vertical">
                <li><%=Html.ActionLink("add single file", "SingleFile")%></li>
                <li><%=Html.ActionLink("folder maintenance", "Folders")%></li>
            </ul>
        </fieldset>
        <fieldset class="box">
            <legend>info</legend>
            <%= Html.DelimiterInfo() %>
            <div>filling in the 'include only these extensions' field, it will override anthing in the 'exclude extensions' field</div>
            <div>if 'recursive' is checked, forganizer will scan down into all folders contained within the one specified, otherwise it will simply search that exact folder</div>
        </fieldset>
    </div>
    <div class="clear"> </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadTitle" runat="server">
    add files
</asp:Content>