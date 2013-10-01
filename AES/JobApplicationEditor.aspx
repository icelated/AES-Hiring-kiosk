<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobApplicationEditor.aspx.cs" Inherits="AES.JobApplicationEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script type="text/javascript" src="scripts/jquery-1.4.1.js"></script>
<script type="text/javascript">
    function Start() {
        NormalizeFields();
    }

    function DeleteField(row) {
        var table = document.getElementById("Fields");
        table.deleteRow(row);
        NormalizeFields();
    }

    function DeleteThisField(me) {
        DeleteField(me.row);
    }

    function InsertField(rowNumber) {
        try {
            var table = document.getElementById("Fields");
            var row = table.insertRow(rowNumber);
            var cell = row.insertCell(0);
            cell.appendChild(document.createElement("br"));

            table = NewField();
            cell.appendChild(table);
            NormalizeFields();
        } catch (ex) {
            alert(ex);
        }
    }

    function InsertFieldHere(me) {
        InsertField(me.row);
    }

    var FieldID = 65536;

    function NewField() {
        FieldID++;
        var table = document.createElement("table");
        table.name = "FieldTable";
            var row;
            var cell1 = null;
            var cell2 = null;
            var input;
            row = table.insertRow(-1);
            cell1 = row.insertCell(-1);
            cell2 = row.insertCell(-1);
            cell1.appendChild(document.createTextNode("Field"));
            input = document.createElement("input");
            input.type = "button";
            input.name = "InsertFieldBtn";
            input.value = "Insert Field";
            cell2.appendChild(input);
            input = document.createElement("input");
            input.type = "button";
            input.name = "DeleteFieldBtn";
            input.value = "Delete Field";
            cell2.appendChild(input);
            row = table.insertRow(-1);
            cell1 = row.insertCell(0);
            cell2 = row.insertCell(1);
            cell1.innerHTML = "Text";
            input = document.createElement("input");
            input.name = "Text" + FieldID;
            input.size = "100";
            cell2.appendChild(input);

            row = table.insertRow(-1);
            cell1 = row.insertCell(0);
            cell2 = row.insertCell(1);
            cell1.innerHTML = "Correct Answer";
            input = document.createElement("input");
            input.Name = "CorrectAnswer" + FieldID;
            input.size = "100";
            cell2.appendChild(input);

            row = table.insertRow(-1);
            cell1 = row.insertCell(0);
            cell2 = row.insertCell(1);
            cell1.innerHTML = "Answer Required";
            input = document.createElement("input");
            input.type = "checkbox";
            input.name = "AnswerRequired" + FieldID;
            input.tagName = "Required";
            input.value = "Yes";
            cell2.appendChild(input);

            row = table.insertRow(-1);
            cell1 = row.insertCell(0);
            cell2 = row.insertCell(1);
            cell1.innerHTML = "Correct Answer Required";
            input = document.createElement("input");
            input.type = "checkbox";
            input.name = "CorrectAnswerRequired" + FieldID;
            input.tagName = "Required";
            input.value = "Yes";
            cell2.appendChild(input);

            return table;
        }

        function NormalizeFields() {
            var i;

            var fieldsTable = document.getElementById("Fields");
            var fieldRow;
            for (i = 0; i < fieldsTable.rows.length; i++) {
                fieldRow = fieldsTable.rows[i];
                var fieldCell = fieldRow.cells[0];
                var elem = fieldCell.firstChild;
                do {
                    if (elem.nodeName == 'TABLE') {
                        var innerTable = elem;
                        var innerRow = innerTable.rows[0];
                        var innerCell = innerRow.cells[0];
                        innerCell.innerHTML = "Field " + (i+1);
                    }
                    elem = elem.nextSibling;
                    
                }while (elem);
            }

            var buttons = document.getElementsByName("InsertFieldBtn");
            var x;
            for (i = 0; i < buttons.length; i++ ) {
                x = buttons[i];
                x.row = i;
                x.onclick = function () { InsertFieldHere(this); };
            }

            buttons = document.getElementsByName("DeleteFieldBtn");
            for (i = 0; i < buttons.length; i++) {
                x = buttons[i];
                x.row = i;
                x.onclick = function () { DeleteThisField(this); };
            }
        }
</script>
</head>
<body id="body" onload="javascript:Start()" style="background-color: #EAF5FF">
    <form id="form1" runat="server" action="JobApplicationEditor.aspx">
    <input type="submit" value="Save Application" /><br />
    <table id="Fields">
    <asp:Repeater ID="Repeater1" runat="server" >
        <ItemTemplate>
        <tr id="Field_<%#DataBinder.Eval(Container.DataItem, ("ID"))%>"><td>
        <br />
        
        <table>
        <tr><td id="FieldTitleCell<%#DataBinder.Eval(Container.DataItem, ("ID"))%>">Field <%#DataBinder.Eval(Container.DataItem, ("ID"))%></td>
        <td>
        <!-- <input type="button" name="InsertFieldBtn" value="Insert Field"/>
        <input type="button" name="DeleteFieldBtn" value="Delete Field"/> -->
        </td></tr>
        <tr><td>Text:</td><td><input type="text" size="100" name="Text<%#DataBinder.Eval(Container.DataItem, ("ID"))%>" value="<%#DataBinder.Eval(Container.DataItem, ("Name"))%>" readonly="readonly" /></td></tr>
        <tr><td>Correct Answer:</td><td><input type="text" size="100" name="CorrectAnswer<%#DataBinder.Eval(Container.DataItem, ("ID"))%>" value="<%#DataBinder.Eval(Container.DataItem,("Answer"))%>" readonly="readonly" /></td></tr>
        <tr><td>Answer Required:</td><td><input type="checkbox" size="100" name="AnswerRequired<%#DataBinder.Eval(Container.DataItem, ("ID"))%>" value="<%#DataBinder.Eval(Container.DataItem,("Required"))%>" disabled="disabled" /></td></tr>
        <tr><td>Correct Answer Required:</td><td><input type="checkbox" size="100" name="CorrectAnswerRequired<%#DataBinder.Eval(Container.DataItem, ("ID"))%>" value="<%#DataBinder.Eval(Container.DataItem,("HasAnswer"))%>" disabled="disabled" /></td></tr>
        </table>
        </td></tr>
        </ItemTemplate>
    </asp:Repeater>
    </table>
    <br />
    <input type="button" onclick="javascript:InsertField(-1)" value="Add Field"/><br />
    <input type="submit" value="Save Application" />
    
    </form>
</body>
</html>
