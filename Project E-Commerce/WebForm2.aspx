<%@ Page Title="Book Details" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="WebForm2.aspx.cs"
    Inherits="Project_E_Commerce.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Book Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Book Details</h2>

    <asp:DetailsView ID="DetailsView1"
                     runat="server"
                     AutoGenerateRows="false"
                     DataKeyNames="BookID">
      <Fields>
        <asp:BoundField  DataField="Title"        HeaderText="Title"  />
        <asp:BoundField  DataField="AuthorName"   HeaderText="Author" />
        <asp:BoundField  DataField="CategoryName" HeaderText="Category"/>
        <asp:BoundField  DataField="Price"
                        HeaderText="Price"
                        DataFormatString="SAR {0:N2}"
                        HtmlEncode="false" />
      </Fields>
    </asp:DetailsView>

    <asp:Button ID="btnAddToCart"
                runat="server"
                Text="Add to Cart"
                OnClick="btnAddToCart_Click" />
    <asp:Button ID="btnBackToBrowse"
            runat="server"
            Text="Back to Browse"
            CssClass="btn"
            PostBackUrl="~/WebForm1.aspx"
            Style="margin-left:10px;" />
</asp:Content>
