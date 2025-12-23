<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="BookManagement.Views.Books" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Books</title>

    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    
    <script src="../Scripts/jquery-3.7.0.min.js"></script>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <script type="text/javascript">
            function showModal() {
                var modal = new bootstrap.Modal(
                    document.getElementById('bookModal'));
                modal.show();
            }
        </script>
        <!-- ADD BUTTON -->
    <asp:Button ID="btnAdd" runat="server" Text="➕ Add Book"
    CssClass="btn btn-primary mb-3"
    OnClick="btnAdd_Click" />

    <!-- GRID -->
    <asp:GridView ID="gvBooks" runat="server"
        CssClass="table table-bordered"
        AutoGenerateColumns="False"
        DataKeyNames="BookId"
        OnRowCommand="gvBooks_RowCommand">

        <Columns>
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Author" HeaderText="Author" />
            <asp:BoundField DataField="Price" HeaderText="Price" />

            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <!-- EDIT -->
                    <asp:LinkButton runat="server"
                        CommandName="EditBook"
                        CommandArgument='<%# Eval("BookId") %>'
                        CssClass="btn btn-sm btn-warning me-1">
                        ✏️
                    </asp:LinkButton>

                    <!-- DELETE -->
                    <asp:LinkButton runat="server"
                        CommandName="DeleteBook"
                        CommandArgument='<%# Eval("BookId") %>'
                        CssClass="btn btn-sm btn-danger"
                        OnClientClick="return confirm('Delete this book?');">
                        🗑️
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <asp:Label ID="lblMessage" runat="server" />
    <!-- MODAL -->
    <div class="modal fade" id="bookModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">
                        <asp:Label ID="lblModalTitle" runat="server" />
                    </h5>
                </div>

                <div class="modal-body">
                    <asp:HiddenField ID="hfBookId" runat="server" />
                    <div class="mb-2">
                        <asp:TextBox ID="TxtBookName" runat="server" CssClass="form-control" Placeholder="Enter Book Name" />
                    </div>
                    <div class="mb-2">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Placeholder="Enter Title" />
                    </div>
                    <div class="mb-2">
                        <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" Placeholder="Emter Author Name" />
                    </div>
                    <div class="mb-2">
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Placeholder="Enter Price" />
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSave" runat="server"
                        Text="Save"
                        CssClass="btn btn-success"
                        OnClick="btnSave_Click" />

                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        Close
                    </button>
                </div>

            </div>
        </div>
    </div>
    </form>
   
</body>
</html>
