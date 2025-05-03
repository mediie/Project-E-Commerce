
<%@ Page Title="Confirmation" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="WebForm5.aspx.cs"
    Inherits="Project_E_Commerce.WebForm5" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">Confirmation</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
  <h2>Order Confirmation</h2>
  <asp:Label ID="lblMsg" runat="server" CssClass="confirmation" />
  <asp:GridView ID="GridViewSummary" runat="server"
                AutoGenerateColumns="False" CssClass="grid">
    <Columns>
      <asp:BoundField  DataField="Title"    HeaderText="Title" />
      <asp:BoundField  DataField="Quantity" HeaderText="Qty"   />
      <asp:BoundField  DataField="Price"    HeaderText="Price"
                            DataFormatString="SAR {0:N2}" />
      <asp:BoundField  DataField="Subtotal" HeaderText="Subtotal"
                            DataFormatString="SAR {0:N2}" />
    </Columns>
  </asp:GridView>
  <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
     <asp:Button ID="btnContinueShopping"
              runat="server"
              Text="Continue Shopping"
              CssClass="btn"
              OnClick="btnContinueShopping_Click"
              Style="margin-top:20px;" />
</asp:Content>
