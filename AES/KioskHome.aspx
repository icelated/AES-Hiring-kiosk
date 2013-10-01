<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KioskHome.aspx.cs" Inherits="AES.KioskHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
    </title>
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
     .GridviewDiv 
     {
         font-size: 100%; 
         font-family: 'Lucida Grande', 'Lucida Sans Unicode', Verdana, Arial, Helevetica, sans-serif; 
         color: #303933;
         width: 896px;
         margin-top: 31px;
     }
Table.Gridview{border:solid 1px #df5015;}
.GridviewTable{border:solid 2 px}
.GridviewTable td{margin-top:0;padding: 0; vertical-align:middle }
.GridviewTable tr{color: White; background-color: #5E767E; height: 30px; text-align:center}
.Gridview th{color:#FFFFFF;border-right-color:#abb079;border-bottom-color:#abb079;padding:0.5em 0.5em 0.5em 0.5em;text-align:center} 
.Gridview td{border-bottom-color:#f0f2da;border-right-color:#f0f2da;padding:0.5em 0.5em 0.5em 0.5em;}
.Gridview tr{color: Black; background-color: White; text-align:left}
:link,:visited { color: #DF4F13; text-decoration:none }
     #form1
     {
         margin-top: 93px;
     }
     .style1
     {
         width: 154px;
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
                    runat="server"/> 
            </div>

<body  style="background-color: #EAF5FF" style="height: 489px; margin-top: 6px;">
    <form id="form1" runat="server">

   
    
    <div class="GridviewDiv">
        <table>
        <tr>
        <th class="style1">
        </th>
        <th style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: normal; font-style: normal">
        Available jobs
        </th>
        </tr>
        </table>

    
</div>

    <div style="height: 258px">
        <asp:GridView ID="GridView2" runat="server" Width="563px" 
            AutoGenerateColumns="False" style="margin-left: 162px" CellPadding="4" 
            ForeColor="#333333" GridLines="None">

            

            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

            

        <Columns>
<asp:TemplateField HeaderText="View Details">
<ItemTemplate>
    <asp:HyperLink ID="lnkSelect" runat='server' NavigateUrl='<%# String.Format("~/JobOpeningDetails.aspx?JobApplicationID={0}", Eval("JobApplicationID")) %>'>Select</asp:HyperLink>
    
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="JobApplicationId" HeaderText="JobId" ItemStyle-Width="40px" >
<ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>
<asp:BoundField DataField="JobName" HeaderText="Name" ItemStyle-Width="120px" >
<ItemStyle Width="120px"></ItemStyle>
            </asp:BoundField>
<asp:BoundField DataField="JobDescription" HeaderText="Description" ItemStyle-Width="130px">
<ItemStyle Width="130px"></ItemStyle>
            </asp:BoundField>
<asp:BoundField DataField="Location" HeaderText="Location" ItemStyle-Width="130px">



<ItemStyle Width="130px"></ItemStyle>
            </asp:BoundField>



</Columns>


            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />


        </asp:GridView>
    </div>

    </form>
</body>
</html>
