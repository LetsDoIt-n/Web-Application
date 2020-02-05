<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication2.WebForm1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .buttonStyle
        {
            color: maroon;
            background-color: cornsilk;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
         <fieldset style="background-color:dodgerblue" runat="server">
            <center><h1><u>Manage Voucher</u></h1></center> 
         </fieldset>
         <br />
            
              <center>
              <div style="border-style:ridge;">
             <br />
             <table>
             <tr>
                 <td><asp:Label ID="label_date" runat="server" Text="Date :"> </asp:Label></td>
                 
                 <td>
                 <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
                 <asp:TextBox ID="textBox_calendar" runat="server"></asp:TextBox>
                 <cc1:CalendarExtender runat="server" TargetControlID="textBox_calendar" Format="dd/MM/yyyy" />
                 </td>
                 
                 <td><asp:Label ID="label_category" runat="server" Text="Select Category:"> </asp:Label></td>
                 <td><asp:DropDownList ID="dropdown_category" runat="server"></asp:DropDownList></td>
                 <td rowspan="2"><asp:Label ID="label_description" runat="server" Text="Description:"> </asp:Label></td>
                 <td rowspan="2"><asp:TextBox id="textArea_description" TextMode="multiline" Columns="30" Rows="4" runat="server" /></td>
                 <td rowspan="2"><asp:Label ID="label_attachment" runat="server" Text="Select Bill Attachment:"> </asp:Label></td>
                 <td rowspan="2"><asp:FileUpload ID="fileupload_attachment" runat="server" /></td>
                 </tr>
                 <tr>
                 <td><asp:Label ID="label_amount" runat="server" Text="Amount:"> </asp:Label></td>
                 <td><asp:TextBox ID="textBox_amount" runat="server"></asp:TextBox></td>
                 <td><asp:Label ID="label_paidBy" runat="server" Text="Paid By:"> </asp:Label></td>
                 <td><asp:TextBox ID="textBox_paidBy" runat="server"></asp:TextBox></td>
                 <td><asp:HiddenField id="hf1" runat="server" /></td>
                 </tr>

               
                 
             
         </table>
                  <br />
                  <table>
                      <tr>
                          <td><asp:Button ID="insert_button" runat="server" CssClass="buttonStyle" Text="Insert" OnClick="insert_button_Click" /></td>
                          <%--<td><asp:Button ID="update_button" runat="server" CssClass="buttonStyle" Text="Update" OnClick="update_button_Click" /></td>--%>
                          <td><asp:Button ID="excel_button" runat="server" CssClass="buttonStyle" Text="Export Excel" OnClick="excel_button_Click" /></td>
                          <td><asp:Button ID="generatelist_button" runat="server" CssClass="buttonStyle" Text="Generate Report" OnClick="generatelist_button_Click" /></td>
                          
                       
                     </tr>
                  </table>
         </div>
         </center>

        <div>
             <asp:GridView ID="GridView1" runat="server" style="width:100%" EmptyDataText="NO RECORD" AutoGenerateColumns="false" DataKeyNames="expense_id"  OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
              <Columns>
                 
                  <asp:templatefield headertext="Date">
                <itemtemplate>
                    <asp:label id="label2" runat="server" text='<%# Eval("expense_date") %>' />
                    <asp:label id="label1" runat="server" style="display:none;" text='<%# Eval("expense_id") %>' />
                </itemtemplate>
            </asp:templatefield>
                  <asp:templatefield headertext="Category">
                <itemtemplate>
                    <asp:label id="label7" runat="server" text='<%# Eval("expense_category") %>' />
                    <asp:label id="label3" runat="server" style="display:none;" text='<%# Eval("expense_category_id") %>' />
                </itemtemplate>
            </asp:templatefield>
                  <asp:templatefield headertext="Description">
                <itemtemplate>
                    <asp:label id="label4" runat="server" text='<%# Eval("expense_description") %>' />
                </itemtemplate>
            </asp:templatefield>

                  <asp:templatefield headertext="Amount">
                <itemtemplate>
                    <asp:label id="label5" runat="server" text='<%# Eval("expense_amount") %>' />
                </itemtemplate>
            </asp:templatefield>
                  <asp:templatefield headertext="Paid By">
                <itemtemplate>
                    <asp:label id="label6" runat="server" text='<%# Eval("expense_paidby") %>' />
                </itemtemplate>
            </asp:templatefield>
                  <asp:templatefield headertext="Attachement">
                <itemtemplate>
                   <%--<asp:label id="label7" runat="server" text='<%# Eval("expense_attachement") %>' />--%>
     <%--              <a href='#' target="_blank"><asp:Image ID="Image1" runat="server" ImageUrl ='<%# Eval("expense_attachement") %>' height="120px" Width="150px" /></a>--%>
                    <a href='<%# Eval("expense_attachement") %>' target="_blank"> <img src='<%# Eval("expense_attachement") %>' height="120px" Width="150px" /></a>
                </itemtemplate>
            </asp:templatefield>
                  
                  

                  <asp:CommandField ShowEditButton="true" />  
                  <asp:CommandField ShowDeleteButton="true" />
                  
              </Columns>
             </asp:GridView>
        </div>


            

            
         

        

        
        </div>
    </form>
</body>
</html>

