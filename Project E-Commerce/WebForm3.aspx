<%@ Page Title="My Cart" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="WebForm3.aspx.cs"
    Inherits="Project_E_Commerce.WebForm3" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">My Cart</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
  <h2>Your Shopping Cart</h2>

  <asp:GridView ID="GridViewCart" runat="server"
                AutoGenerateColumns="False"
                CssClass="grid"
                OnRowEditing="GridViewCart_RowEditing"
                OnRowCancelingEdit="GridViewCart_RowCancelingEdit"
                OnRowUpdating="GridViewCart_RowUpdating"
                OnRowDeleting="GridViewCart_RowDeleting">
    <Columns>
      <asp:BoundField  DataField="Title"   HeaderText="Title"   ReadOnly="true" />
      <asp:BoundField  DataField="Price"   HeaderText="Price"
                            DataFormatString="SAR {0:N2}" />
      <asp:TemplateField HeaderText="Quantity">
        <ItemTemplate>
          <%# Eval("Quantity") %>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="txtQty" runat="server"
                       Text='<%# Bind("Quantity") %>' />
        </EditItemTemplate>
      </asp:TemplateField>
      <asp:BoundField  DataField="Subtotal" HeaderText="Subtotal"
                            DataFormatString="SAR {0:N2}" />
      <asp:CommandField ShowEditButton="True"
                        ShowDeleteButton="True" />
    </Columns>
  </asp:GridView>

  <asp:CustomValidator ID="cvCartNotEmpty" runat="server"
        ErrorMessage="Your cart is empty."
        Display="Dynamic"
        OnServerValidate="cvCartNotEmpty_ServerValidate" />

  <asp:Button ID="btnCheckout" runat="server"
              Text="Proceed to Checkout"
              OnClick="btnCheckout_Click" />
</asp:Content>
