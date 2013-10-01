<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobApplicationSubmitted.aspx.cs" Inherits="AES.JobApplicationSubmitted" %>

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
    <form id="form1" runat="server">
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
                      
                <asp:Button ID="Button1" runat="server" Font-Bold="True" 
                    onclick="Button1_Click" Text="Main Page" CssClass="style3" BorderWidth="2pt" BorderColor="Black"/>

            </div>
        </div>
         <p> 
            <font size="4.5" color="black">Thank you for applying at AES.<br>
                    Your application has been successfully submitted. A Hiring Specialist will contact you shortly.</font>
        </p>
    </form>
    <p>
        <img alt="" class="style1" src="Capture2.jpg" style="border:5px solid blue"/></p>
</body>
</html>
