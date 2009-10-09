<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Forganizer.DomainModel.Entities.FileObject>>" %>

    <ul class="list_vertical">
        <% foreach (var fileObject in Model) { %>
            <li><%= fileObject.FileInfo.Name %></li>
        <% } %>
    </ul>