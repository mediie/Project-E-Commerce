<%@ Page Title="Browse Books" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="WebForm1.aspx.cs"
    Inherits="Project_E_Commerce.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Browse Books
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Browse Books</h2>

    <asp:TextBox ID="txtSearch" runat="server"
                 Width="250px"
                 Placeholder="Search by title or author" />
    <asp:Button ID="btnSearch" runat="server"
                Text="Search"
                OnClick="btnSearch_Click"
                CssClass="btn" />

    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />

    <asp:GridView ID="GridViewBooks"
              runat="server"
              AutoGenerateColumns="False"
              DataKeyNames="BookID"
              AllowPaging="True"
              PageSize="10"
              OnPageIndexChanging="GridViewBooks_PageIndexChanging"
              OnRowCommand="GridViewBooks_RowCommand"
              CssClass="grid"
              EmptyDataText="No books found.">
    <Columns>
        <asp:BoundField  DataField="Title"        HeaderText="Title"    />
        <asp:BoundField  DataField="AuthorName"   HeaderText="Author"   />
        <asp:BoundField  DataField="CategoryName" HeaderText="Category" />
        <asp:BoundField  DataField="Price"        HeaderText="Price"
                        DataFormatString="SAR {0:N2}"
                        HtmlEncode="false"              />

       
        <asp:TemplateField HeaderText="Details">
          <ItemTemplate>
            <asp:Button ID="btnView"
                        runat="server"
                        Text="View"
                        CssClass="btn"
                        PostBackUrl='<%# Eval("BookID", "WebForm2.aspx?BookID={0}") %>' />
          </ItemTemplate>
        </asp:TemplateField>

       
        <asp:TemplateField HeaderText="Add to Cart">
          <ItemTemplate>
            <asp:Button ID="btnAddToCart"
                        runat="server"
                        Text="Add to Cart"
                        CssClass="btn"
                        CommandName="AddToCart"
                        CommandArgument='<%# Eval("BookID") %>' />
          </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</asp:Content>
