<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreManagerHomePage.aspx.cs"
    Inherits="AES.StoreManagerHomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 28px;
        }
    </style>
</head>
<body style="background-color: #EAF5FF">
    <form id="form2" runat="server">
    <div class="header">
        <div class="title">
            <asp:Label ID="lblTitle" BackColor="Lightblue" BorderWidth="6px" BorderStyle="Solid"
                Width="99%" Font-Bold="true" Font-Italic="True" Font-Size="30pt" font-name="Agency FB"
                Text="<CENTER>AES Employment Systems</CENTER>" runat="server" />
        </div>
    </div>
    <p>
        <div style="height: 88px">
        </div>
        <div>
            <div style="height: 65px; font-size: medium; font-weight: bold; margin-left: 56px;">
                Add new Job</div>
        </div>
        <div style="height: 142px">
            <div>
                <table border="1" style="margin-left: 52px">
                    <tr>
                        <td>
                            Job Description:
                        </td>
                        <td>
                            <asp:TextBox ID="TxtDescription" runat="server" CausesValidation="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtDescription"
                                ErrorMessage="Required Field" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Wage
                        </td>
                        <td>
                            <asp:TextBox ID="TxtWage" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Department ID
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TxtDepartmentID" runat="server"></asp:TextBox>
                        </td>
                        <tr>
                            <td>
                                Has Been Filled
                            </td>
                            <td>
                                <asp:TextBox ID="TxtBeenFilled" runat="server" Height="24px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Location
                            </td>
                            <td>
                                <asp:TextBox ID="TxtLocation" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Job Name
                            </td>
                            <td>
                                <asp:TextBox ID="TxtJobName" runat="server"></asp:TextBox>
                            </td>
                            <tr>
                                <td>
                                    <asp:Button ID="Buttonjob" runat="server" Text="Add New Job" OnClick="Buttonjob_Click"
                                        Font-Bold="True" BorderWidth="2pt" BorderColor="Black" Height="25px" Style="margin-right: 1px;
                                        margin-top: 22px" Width="124px" />
                                </td>
                            </tr>
                        </tr>
                    </tr>
                </table>
            </div>
            <div style="height: 51px">
                <div style="height: 32px; margin-left: 52px; margin-top: 40px;">
                    <div style="font-size: medium; font-weight: bold">
                        View Jobs
                    </div>
                </div>
            </div>
            <div style="margin-left: 54px">
                <asp:Repeater ID="Repeater1" runat="server" EnableViewState="true" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                        <div>
                        </div>
                        <table class="vertaligntop">
                            <tr>
                                <td>
                                    <br />
                                    Job ID
                                    <%# DataBinder.Eval(Container.DataItem, "JobApplicationID")%>
                                    <br />
                                    Job Description:
                                    <%# DataBinder.Eval(Container.DataItem, "JobDescription") %>
                                    <br />
                                    Wage:
                                    <%# DataBinder.Eval(Container.DataItem, "Wage") %>
                                    <br />
                                    Department ID:
                                    <%# DataBinder.Eval(Container.DataItem, "DepartmentID") %>
                                    <br />
                                    Has been filled:
                                    <%# DataBinder.Eval(Container.DataItem, "HasBeenFilled") %>
                                    <br />
                                    Location:
                                    <%# DataBinder.Eval(Container.DataItem, "Location") %>
                                    <br />
                                    Job Name:
                                    <%# DataBinder.Eval(Container.DataItem, "JobName") %>
                                    <br />
                                    <asp:Button ID="Button1" runat="server" OnClientClick="return confirm('Are you sure you want to delete?');"
                                        Text="Delete" CommandName="delete" CommandArgument='<%# Eval("JobApplicationID") %>'
                                        Font-Bold="True" BorderWidth="2pt" BorderColor="Black" Height="25px" Style="margin-right: 1px;
                                        margin-top: 22px" Width="124px" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <SeparatorTemplate>
                        <br>
                    </SeparatorTemplate>
                </asp:Repeater>
            </div>
    </form>
</body>
</html>
