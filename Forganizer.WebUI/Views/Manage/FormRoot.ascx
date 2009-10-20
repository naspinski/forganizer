<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
                <legend>
                    <i class="<% if(Model == "delete files") { %>delete<% } else { %>edit<% } %>"></i><%= Model %>
                </legend>
                <div>
                    <label for="Folder"><%= Html.ValidationMessage("Path") %>from folder</label>
                    <%= Html.TextBox("Path") %>
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