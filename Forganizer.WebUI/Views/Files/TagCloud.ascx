<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Forganizer.DomainModel.Entities.Tag>>" %>


    <ul class="list_horizontal">
        <% foreach(var Tag in Model) { %>
        <li><%= Html.ActionLink(Tag.Name, "List", new { tags = Tag.Name }, 
                new { style = "font-size:" + Tag.Size.ToString("0.0") + "em;" })%></li>
        <% } %>
    </ul>