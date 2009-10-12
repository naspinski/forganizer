<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Forganizer.DomainModel.Entities.Tag>>" %>

        <div class="active">
            <% Html.RenderAction("BoxPart", "Category", new { Model, ActivePart = true }); %>
        </div>
        <div class="inactive">
            <% Html.RenderAction("BoxPart", "Category", new { Model, ActivePart = false }); %>
        </div>