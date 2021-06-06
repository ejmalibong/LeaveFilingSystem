Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters

Public Class frmEmployee
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private main As New Main
    'dataset
    Private dsLeaveFiling As New dsLeaveFiling
    Private adpEmployee As New EmployeeTableAdapter
    Private dtEmployee As New EmployeeDataTable
    Private WithEvents bsEmployee As New BindingSource
    'pagination
    Private pageSize As Integer
    Private pageIndex As Integer
    Private totalCount As Integer
    Private pageCount As Integer
    Private indexScroll As Integer = 0
    Private indexPosition As Integer = 0
    'search criteria
    Private dictionary As New Dictionary(Of String, Integer)
    'flag filters
    Private isFilterByEmployeeName As Boolean = False
    Private isFilterByEmployeeCode As Boolean = False
    Private isFilterByEmailAddress As Boolean = False
    'flags
    Private isExists As Boolean = True
    Private isValidate As Boolean = True

    Private Sub frmEmployee_Load(sender As Object, e As EventArgs) Handles Me.Load
        pageIndex = 0
        pageSize = 100
        BindPage()

        SearchCriteria()

        main.EnableDoubleBuffered(dgvList)

        Me.ActiveControl = dgvList
        Me.dgvList.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Me.dgvList.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub frmEmployee_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode.Equals(Keys.F2) Then
            e.Handled = True
            btnAdd.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F4) Then
            e.Handled = True
            btnDelete.PerformClick()
        End If
    End Sub

    Private Sub frmEmployee_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dgvList.Dispose()
    End Sub

    Private Sub frmEmployee_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        main.FormTrap(Me)
    End Sub

    Private Sub dgvList_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dgvList.CellValidating

    End Sub

    Private Sub dgvList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvList.DataError
        e.Cancel = False
    End Sub

    Private Sub BindingNavigatorMoveNextItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveNextItem.Click
        pageIndex += 1
        If pageIndex > pageCount - 1 Then
            pageIndex = pageCount - 1
        End If
        BindPage()
    End Sub

    Private Sub BindingNavigatorMovePreviousItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMovePreviousItem.Click
        pageIndex -= 1
        If pageIndex < 0 Then
            pageIndex = 0
        End If
        BindPage()
    End Sub

    Private Sub BindingNavigatorMoveLastItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveLastItem.Click
        pageIndex = pageCount - 1
        BindPage()
    End Sub

    Private Sub BindingNavigatorMoveFirstItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveFirstItem.Click
        pageIndex = 0
        BindPage()
    End Sub

    Private Sub btnSync_Click(sender As Object, e As EventArgs) Handles btnSync.Click
        Try
            Dim _count As Integer = 0
            Dim _totalCount As Integer = 0
            Dim _rowsAffected As Integer = 0

            Dim _prm1(0) As SqlParameter
            _prm1(0) = New SqlParameter("@TotalCount", SqlDbType.Int)
            _prm1(0).Direction = ParameterDirection.Output

            dbLeaveFiling.ExecuteScalar("CntEmployee", CommandType.StoredProcedure, _prm1)
            _count = _prm1(0).Value

            If _count > 0 Then
                _rowsAffected = dbLeaveFiling.ExecuteNonQuery("InsEmployee", CommandType.StoredProcedure)

                If _rowsAffected > 0 Then
                    MessageBox.Show(_rowsAffected & " rows affected.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("No rows affected.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("No items need to be imported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAddSave_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            'dgvList.ClearSelection()
            'Me.bsEmployee.AddNew()
            'Me.bsEmployee.MoveLast()
            'dgvList.CurrentCell = dgvList.CurrentRow.Cells(1)
            'dgvList.BeginEdit(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Me.Validate()
        Me.bsEmployee.EndEdit()
        If Me.dsLeaveFiling.HasChanges Then
            Me.adpEmployee.Update(dsLeaveFiling.Employee)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Validate()

            If dgvList.NewRowIndex Then
                dgvList.CancelEdit()
                Me.bsEmployee.CancelEdit()
            End If

            If dsLeaveFiling.HasChanges Then
                Dim _result As DialogResult = MessageBox.Show("Discard changes?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If _result = Windows.Forms.DialogResult.Yes Then
                    dgvList.CancelEdit()
                    Me.bsEmployee.CancelEdit()
                    Me.dsLeaveFiling.RejectChanges()

                ElseIf _result = Windows.Forms.DialogResult.No Then
                    dgvList.CurrentRow.Cells(1).Selected = True
                    dgvList.BeginEdit(True)
                    Return
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            'If Me.bsEmployee.Current Is Nothing Then
            '    Exit Sub
            'End If

            'Dim currentRow = CType(Me.bsEmployee.Current, DataRowView).Row
            'Dim state = currentRow.RowState

            'Select Case state
            '    Case DataRowState.Added
            '        Me.bsEmployee.RemoveCurrent()
            '    Case DataRowState.Deleted
            '        MessageBox.Show("Item is already deleted.", "")
            '    Case DataRowState.Detached
            '        Me.bsEmployee.CancelEdit()
            '    Case DataRowState.Modified, DataRowState.Unchanged
            '        If dgvList.SelectedCells.Count > 0 AndAlso dgvList.SelectedCells(0).RowIndex = dgvList.NewRowIndex Then
            '            Me.bsEmployee.CancelEdit()
            '            Exit Sub
            '        End If

            '        Dim message = String.Format("Delete {0}?", bsEmployee.Current("EmployeeName"))
            '        If MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            '            Me.bsEmployee.RemoveCurrent()
            '            Me.adpEmployee.Update(Me.dsLeaveFiling.Employee)
            '        End If
            '    Case Else
            'End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Go()
    End Sub

    Private Sub cmbSearchCriteria_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSearchCriteria.SelectedValueChanged
        Try
            txtName.Clear()
            Me.ActiveControl = txtName
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                isFilterByEmployeeName = True
                isFilterByEmployeeCode = False
                isFilterByEmailAddress = False

            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                isFilterByEmployeeName = False
                isFilterByEmployeeCode = True
                isFilterByEmailAddress = False

            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                isFilterByEmployeeName = False
                isFilterByEmployeeCode = False
                isFilterByEmailAddress = True

            End If

            pageIndex = 0
            BindPage()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtName.Clear()

            isFilterByEmployeeName = False
            isFilterByEmployeeCode = False
            isFilterByEmailAddress = False

            pageIndex = 0
            BindPage()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub bsEmployee_AddingNew(sender As Object, e As System.ComponentModel.AddingNewEventArgs) Handles bsEmployee.AddingNew
        Try
          
        Catch ex As Exception
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_MouseEnter(sender As Object, e As EventArgs) Handles btnCancel.MouseEnter
        isValidate = False
    End Sub

    Private Sub btnCancel_MouseLeave(sender As Object, e As EventArgs) Handles btnCancel.MouseLeave
        isValidate = True
    End Sub

    Private Sub btnClose_MouseEnter(sender As Object, e As EventArgs) Handles btnClose.MouseEnter
        isValidate = False
    End Sub

    Private Sub btnClose_MouseLeave(sender As Object, e As EventArgs) Handles btnClose.MouseLeave
        isValidate = True
    End Sub

#Region "Sub"
    Private Sub BindPage()
        Try
            totalCount = 0

            If isFilterByEmployeeName = True Then
                If String.IsNullOrEmpty(txtName.Text.Trim) Then
                    Me.adpEmployee.FillEmployeeMasterlistByEmployeeName(Me.dsLeaveFiling.Employee, pageIndex, pageSize, totalCount, Nothing)
                Else
                    Me.adpEmployee.FillEmployeeMasterlistByEmployeeName(Me.dsLeaveFiling.Employee, pageIndex, pageSize, totalCount, txtName.Text.Trim)
                End If
            ElseIf isFilterByEmployeeCode = True Then
                If String.IsNullOrEmpty(txtName.Text.Trim) Then
                    Me.adpEmployee.FillEmployeeMasterlistByEmployeeCode(Me.dsLeaveFiling.Employee, pageIndex, pageSize, totalCount, Nothing)
                Else
                    Me.adpEmployee.FillEmployeeMasterlistByEmployeeCode(Me.dsLeaveFiling.Employee, pageIndex, pageSize, totalCount, txtName.Text.Trim)
                End If
            ElseIf isFilterByEmailAddress = True Then
                If String.IsNullOrEmpty(txtName.Text.Trim) Then
                    Me.adpEmployee.FillEmployeeMasterlistByEmailAddress(Me.dsLeaveFiling.Employee, pageIndex, pageSize, totalCount, Nothing)
                Else
                    Me.adpEmployee.FillEmployeeMasterlistByEmailAddress(Me.dsLeaveFiling.Employee, pageIndex, pageSize, totalCount, txtName.Text.Trim)
                End If
            Else
                Me.adpEmployee.FillEmployeeMasterlist(Me.dsLeaveFiling.Employee, pageIndex, pageSize, totalCount)
            End If

            Me.bsEmployee.DataSource = Me.dsLeaveFiling
            Me.bsEmployee.DataMember = dtEmployee.TableName
            Me.bsEmployee.ResetBindings(True)
            dgvList.AutoGenerateColumns = False
            dgvList.DataSource = Me.bsEmployee

            If totalCount Mod pageSize = 0 Then
                If totalCount = 0 Then
                    pageCount = (totalCount / pageSize) + 1
                Else
                    pageCount = totalCount / pageSize
                End If
            Else
                pageCount = Math.Truncate(totalCount / pageSize) + 1
            End If

            'current page index and total number of pages
            txtPageNumber.Text = pageIndex + 1
            txtTotalPageNumber.Text = "of " & CInt(pageCount) & " Page(s)"

            'enables pager
            txtPageNumber.Enabled = True
            txtTotalPageNumber.Enabled = True
            BindingNavigatorMoveFirstItem.Enabled = True
            BindingNavigatorMovePreviousItem.Enabled = True
            BindingNavigatorMoveNextItem.Enabled = True
            BindingNavigatorMoveLastItem.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Go()
        Try
            If String.IsNullOrEmpty(txtPageNumber.Text) Then
                MessageBox.Show("Page not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPageNumber.Focus()
                Return
            End If

            If CInt(txtPageNumber.Text) > pageCount Then
                MessageBox.Show("Page not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPageNumber.Focus()
                Return
            End If

            If CInt(txtPageNumber.Text) = 0 Then
                MessageBox.Show("Page not found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPageNumber.Focus()
                Return
            End If

            pageIndex = CInt(txtPageNumber.Text) - 1
            BindPage()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Reload()
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf GetScrollingIndex))
        pageIndex = 0
        BindPage()
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf SetScrollingIndex))
    End Sub

    Private Sub GetScrollingIndex()
        indexScroll = dgvList.FirstDisplayedCell.RowIndex
        indexPosition = dgvList.CurrentRow.Index
    End Sub

    Private Sub SetScrollingIndex()
        dgvList.FirstDisplayedScrollingRowIndex = indexScroll
        If dgvList.Rows.Count > indexPosition Then
            dgvList.Rows(indexPosition).Selected = True
        Else
            dgvList.Rows(indexPosition - 1).Selected = True
        End If
        Me.bsEmployee.Position = dgvList.SelectedCells(0).RowIndex
    End Sub

    Private Sub SearchCriteria()
        dictionary.Add(" Employee Name", 1)
        dictionary.Add(" Employee Code", 2)
        dictionary.Add(" NBC Email Address", 3)
        cmbSearchCriteria.DisplayMember = "Key"
        cmbSearchCriteria.ValueMember = "Value"
        cmbSearchCriteria.DataSource = New BindingSource(dictionary, Nothing)
    End Sub
#End Region


End Class