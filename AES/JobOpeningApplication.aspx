<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobOpeningApplication.aspx.cs" Inherits="AES.JobOpeningApplication" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">


        var sessionTimeout = "<%= Session.Timeout %>";

        function DisplaySessionTimeout() {

            sessionTimeout = sessionTimeout - 1;

            if (sessionTimeout >= 0)

                window.setTimeout("DisplaySessionTimeout()", 60000);

            else {
                alert("Your current Session is over due to inactivity.");
                setTimeout('window.location = "KioskHome.aspx"', 0000);

            }
        }


</script>



    <style type="text/css">
        .title
        {
            height: 388px;
            margin-left: 1px;
            width: 1330px;
        }
        #form1
        {
            height: 825px;
            margin-left: 4px;
            width: 763px;
        }
        .style1
        {
            width: 623px;
        }                
        </style>
</head>
<body style="background-color: #EAF5FF">
    <form id="form1" runat="server">
    <div>
    
    </div>
        <div class="header">
            <div class="title" >
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
                    
    <div style="height: 20px; margin-top: 122px; margin-left: 47px;">
        <strong>Job Questionnaire:</strong>
    </div>
    <div>
    </div>
        <table width="60%" border="0" cellspacing="0" cellpadding="5">
<tr>
<td width="50%">        
        <asp:Table ID="myTable" runat="server" Width="68%" 
            style="margin-left: 39px; margin-top: 1px;" Height="47px"> 
    <asp:TableRow>
        <asp:TableCell></asp:TableCell>
        
    </asp:TableRow>
</asp:Table>       
   </td>

</tr>
</table>
 <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Font-Bold="True" BorderWidth="2pt" BorderColor="Black"
        style="margin-left: 42px; margin-top: 21px;" Text="Back" Width="70px" Height="25px" />
    <asp:Button ID="Button2" runat="server" style="margin-left: 14px" Text="Send" Font-Bold="True" BorderWidth="2pt" BorderColor="Black"
                    Width="70px" onclick="Button2_Click" Height="25px"/>
<table id="myTable2" runat="server" >
<tr>
<td class="style1">
</td>
</tr>
</table>
            <div style="height: 26px; width: 585px; margin-left: 52px; margin-top: 0px; font-size: 20px; font-weight: bold; color: #FF0000;">
            
                <asp:Label ID="lblError" runat="server"></asp:Label>
            
            </div>

            </div>
            </div>

    </form>

    </body>
</html>
