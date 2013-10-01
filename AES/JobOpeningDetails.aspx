<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobOpeningDetails.aspx.cs" Inherits="AES.JobOpeningDetails" %>

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
        #form1
        {
            height: 805px;
            margin-top: 34px;
        }
        #content
        {
            margin-left: 111px;
            margin-top: 0px;
            height: 601px;
            margin-right: 112px;
        }
            
        .style1
        {
            width: 285px;
            font-size: 20px;
            margin-left: 0px;
        }
                 
        .style2
        {
            width: 204px;
        }
          
        .style3
        {
            width: 203px;
        }
        
        .style4
        {
            width: 385px;
        }
          
        
        
        
    </style>
</head>
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
                    runat="server"/></div>
                    </div>
                     

<body style="background-color: #EAF5FF">
    <form id="form1" runat="server">

     <div id="content" class="body">
     <div style="height: 44px; width: 534px">
     </div>
     <div class="style1">
         <strong>Job Details:
     </strong>
     </div>
     <div>
     </div>
             <asp:Repeater ID="RepeaterJobDetails" runat="server" >

            <ItemTemplate>
           <div>
          </div>
          <table>
          <tr >
          <td >
           <br />
     Job Name:
     <%# DataBinder.Eval(Container.DataItem, "JOBNAME") %>
     <br />
     Description:
     <%# DataBinder.Eval(Container.DataItem, "JobDescription") %>
     <br />
     Location:
     <%# DataBinder.Eval(Container.DataItem, "Location") %>
     <br />
     Wage:
     <%#DataBinder.Eval(Container.DataItem,"Wage","{0:C}")%>
     <br />
     </td>
   </tr>
     </table>
   
     
     
  
    
  </ItemTemplate>
  <SeparatorTemplate>
    <br>
  </SeparatorTemplate>

        </asp:Repeater>
   
         <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  Font-Bold="True" BorderWidth="2pt" BorderColor="Black" Height="25px"
             style="margin-right: 1px; margin-top: 22px" Text="Back" Width="70px" />
         <asp:Button ID="Button2" runat="server" style="margin-left: 14px"  Font-Bold="True" BorderWidth="2pt" BorderColor="Black" Height="25px"
             Text="Apply" onclick="Button2_Click" Width="70px" />
   
    </div>
    <div style="height: 65px; width: 708px; margin-top: 46px;">
    
       
         <table>
          <tr >
          <td class="style2" >
          <td class="style3">
          <td class="style4">
              &nbsp;</td>
          </td>
          </td>
          <tr>
          <td>
          <td class="style3">
          </td>
          <td class="style4">
          </td>
          </td>
          </tr>
        </Table>
    
    </div>
    </form>
    </body>
</html>
