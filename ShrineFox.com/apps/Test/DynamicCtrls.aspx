<%@ Page Title="DynamicCtrls" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DynamicCtrls.aspx.cs" Inherits="ShrineFoxCom.DynamicCtrls" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table width="35%" cellpadding="3" cellspacing="1" border="1">
            <tr>
                <td colspan="2" align="center" style="background-color: #46B525;">
                    <b>Manage Dynamic Controls</b>
                </td>
            </tr>
            <tr>
                <td>
                    Select Dynamic Controls :
                </td>
                <td>
                    <asp:DropDownList ID="ddlDynamicControlList" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="-- Select --" Value="-- Select --"></asp:ListItem>
                        <asp:ListItem Text="TextBox" Value="TextBox"></asp:ListItem>
                        <asp:ListItem Text="DropDownList" Value="DropDownList"></asp:ListItem>
                        <asp:ListItem Text="RadioButtonList" Value="RadioButtonList"></asp:ListItem>
                        <asp:ListItem Text="CheckBoxList" Value="CheckBoxList"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #EEEEEE;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblValue" runat="server" Text="" Style="color: Red;"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnRead" runat="server" Text="Read" OnClick="btnRead_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>