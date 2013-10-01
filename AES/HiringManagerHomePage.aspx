<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HiringManagerHomePage.aspx.cs" Inherits="AES.HiringManagerHomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        .style1
        {            
            width: 1019px;
            height: 661px;
        }
        .style3
        {
            margin-top: 10px;
            margin-right: 30px;
        }        
        </style>
</head>
<body style="background-color: #EAF5FF">
    <form id="form2" runat="server">
        <div class="header">
            <div class="title">
                <asp:label 
                    id="lblTitle" 
                    backcolor="Lightblue"
                    borderwidth="6px"
                    borderstyle="Solid"
                    width="99%"
                    Font-Bold="true"
                    font-italic="True"
                    font-size="30pt"
                    font-name="Agency FB"
                    text="<CENTER>AES Employment Systems</CENTER>"
                    runat="server"/>
            </div>
        </div>
         <p> 
            <font size="4.5" color="black">blah blah blah.<br>
                    Hiring Manager blah blah blah.</font>
        </p>
    <p>

        <asp:Button ID="Button" runat="server" Text="Button" />
        <asp:Button ID="JobOpeningButton" runat="server" Font-Bold="True" 
            Text="JobOpening"  style="margin-left: 14px" 
            onclick="JobOpeningButton_Click"/>
        <asp:Button ID="HomeButton" runat="server" Font-Bold="True" Text="Home" 
            Width="68px"  style="margin-left: 14px" onclick="HomeButton_Click"/>
    </form>
    
</body>
</html>
