<%@ Page Title="Checkout" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="WebForm4.aspx.cs"
    Inherits="Project_E_Commerce.WebForm4" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="TitleContent" runat="server">
  Checkout
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2>Checkout</h2>

  <h3>Your Cart</h3>
  <asp:GridView ID="GridViewCartPreview"
                runat="server"
                AutoGenerateColumns="False"
                CssClass="grid"
                EmptyDataText="Your cart is empty.">
    <Columns>
      <asp:BoundField DataField="Title"    HeaderText="Title" />
      <asp:BoundField DataField="Price"
                      HeaderText="Price"
                      DataFormatString="SAR {0:N2}"
                      HtmlEncode="false" />
      <asp:BoundField DataField="Quantity" HeaderText="Qty" />
      <asp:BoundField DataField="Subtotal"
                      HeaderText="Subtotal"
                      DataFormatString="SAR {0:N2}"
                      HtmlEncode="false" />
    </Columns>
  </asp:GridView>
  <asp:Label ID="lblCartTotal"
             runat="server"
             Font-Bold="true"
             Style="display:block; margin-bottom:20px;" />

  <hr />

  <asp:ValidationSummary ID="ValidationSummary1"
                         runat="server"
                         CssClass="validation-summary"
                         HeaderText="Please fix the following errors:" />

  <div>
    <asp:Label AssociatedControlID="txtName" runat="server" Text="Name:" />
    <asp:TextBox ID="txtName" runat="server" />
    <asp:RequiredFieldValidator ControlToValidate="txtName"
                                ErrorMessage="Name is required."
                                Display="Dynamic"
                                runat="server" />
  </div>

  <div>
    <asp:Label AssociatedControlID="txtEmail" runat="server" Text="Email:" />
    <asp:TextBox ID="txtEmail" runat="server" />
    <asp:RequiredFieldValidator ControlToValidate="txtEmail"
                                ErrorMessage="Email is required."
                                Display="Dynamic"
                                runat="server" />
    <asp:RegularExpressionValidator ControlToValidate="txtEmail"
                                    ErrorMessage="Invalid email."
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic"
                                    runat="server" />
  </div>

  <div>
    <asp:Label AssociatedControlID="txtPhone" runat="server" Text="Phone:" />
    <asp:TextBox ID="txtPhone" runat="server" />
    <asp:RequiredFieldValidator 
      ControlToValidate="txtPhone"
      ErrorMessage="Phone is required."
      Display="Dynamic"
      runat="server" />
      <asp:RegularExpressionValidator
      ID="revPhone"
      ControlToValidate="txtPhone"
      ValidationExpression="^\d+$"
      ErrorMessage="Phone must contain numbers only."
      Display="Dynamic"
      runat="server" />
  </div>

  <div>
    <asp:Label AssociatedControlID="txtAddress" runat="server" Text="Address:" />
    <asp:TextBox ID="txtAddress"
                 runat="server"
                 TextMode="MultiLine"
                 Rows="3"
                 Columns="50" />
    <asp:RequiredFieldValidator ControlToValidate="txtAddress"
                                ErrorMessage="Address is required."
                                Display="Dynamic"
                                runat="server" />
  </div>

  <asp:Button ID="btnPlaceOrder"
              runat="server"
              Text="Place Order"
              OnClick="btnPlaceOrder_Click"
              Style="margin-top:20px;" />
</asp:Content>
