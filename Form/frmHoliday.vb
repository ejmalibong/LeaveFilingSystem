Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters

Public Class frmHoliday
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main
    'dataset
    Private dsLeaveFiling As New dsLeaveFiling
    Private adpHoliday As New HolidayTableAdapter
    Private dtHoliday As New HolidayDataTable
    Private bsHoliday As New BindingSource
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
    Private isFilterByDate As Boolean = False
    Private isFilterByName As Boolean = False
    'flags
    Private isExists As Boolean = True
    Private isValidate As Boolean = True

    Private Sub frmHoliday_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pageIndex = 0
        pageSize = 100
        BindPage()

        SearchCriteria()

        main.EnableDoubleBuffered(dgvList)

        Me.ActiveControl = dgvList
        Me.dgvList.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub frmHoliday_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode.Equals(Keys.F2) Then
            e.Handled = True
            btnAdd.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F4) Then
            e.Handled = True
            btnDelete.PerformClick()
        End If
    End Sub

    Private Sub frmHoliday_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dgvList.Dispose()
    End Sub

    Private Sub dgvList_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dgvList.CellValidating
        Try
            If isValidate = True Then
                If e.ColumnIndex = 1 Then
                    If String.IsNullOrEmpty(e.FormattedValue.ToString.Trim) Then
                        MessageBox.Show("Holiday date is required.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        e.Cancel = True
                    End If

                    If dgvList.IsCurrentCellDirty Then
                        dgvList.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    End If

                    isExists = False

                    For x As Integer = 0 To dgvList.Rows.Count - 1
                        For y As Integer = 0 To dgvList.Rows.Count - 1
                            If y <> x AndAlso dgvList.Rows(x).Cells(1).Value.ToString.ToLower = dgvList.Rows(y).Cells(1).Value.ToString.ToLower Then
                                isExists = True
                            End If
                        Next
                    Next

                    If isExists = True Then
                        MessageBox.Show("Holiday date already exists.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        e.Cancel = True
                    End If

                    Dim _date As DateTime
                    If e.FormattedValue.ToString <> String.Empty AndAlso Not DateTime.TryParse(e.FormattedValue.ToString, _date) Then
                        MessageBox.Show("Inputted date is in incorrect format.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        e.Cancel = True
                    End If

                ElseIf e.ColumnIndex = 2 Then
                    If String.IsNullOrEmpty(e.FormattedValue.ToString.Trim) Then
                        MessageBox.Show("Holiday name is required.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        e.Cancel = True
                    End If

                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvList.DataError
        e.Cancel = False
    End Sub

    Private Sub btnAddSave_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Me.bsHoliday.AddNew()
            Me.bsHoliday.MoveLast()
            dgvList.CurrentCell = dgvList.CurrentRow.Cells(1)
            dgvList.BeginEdit(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.Validate()
            Me.bsHoliday.EndEdit()
            If Me.dsLeaveFiling.HasChanges Then
                Me.adpHoliday.Update(dsLeaveFiling.Holiday)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Validate()

            If dgvList.NewRowIndex Then
                dgvList.CancelEdit()
                Me.bsHoliday.CancelEdit()
            End If

            If dsLeaveFiling.HasChanges Then
                Dim _result As DialogResult = MessageBox.Show("Discard changes?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

                If _result = Windows.Forms.DialogResult.Yes Then
                    dgvList.CancelEdit()
                    Me.bsHoliday.CancelEdit()
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
            If Me.bsHoliday.Current Is Nothing Then
                Exit Sub
            End If

            Dim currentRow = CType(Me.bsHoliday.Current, DataRowView).Row
            Dim state = currentRow.RowState

            Select Case state
                Case DataRowState.Added
                    Me.bsHoliday.RemoveCurrent()
                Case DataRowState.Deleted
                    MessageBox.Show("Item is already deleted.", "")
                Case DataRowState.Detached
                    Me.bsHoliday.CancelEdit()
                Case DataRowState.Modified, DataRowState.Unchanged
                    If dgvList.SelectedCells.Count > 0 AndAlso dgvList.SelectedCells(0).RowIndex = dgvList.NewRowIndex Then
                        Me.bsHoliday.CancelEdit()
                        Exit Sub
                    End If

                    Dim message = String.Format("Delete {0}?", bsHoliday.Current("HolidayName"))
                    If MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                        Me.bsHoliday.RemoveCurrent()
                        Me.adpHoliday.Update(Me.dsLeaveFiling.Holiday)
                    End If
                Case Else
            End Select
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

    Private Sub cmbSearchCriteria_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSearchCriteria.SelectedValueChanged
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                pnlDate.Visible = True
                pnlName.Visible = False
                Me.ActiveControl = dtpDateFrom

            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                pnlDate.Visible = False
                pnlName.Visible = True
                Me.ActiveControl = txtName
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                isFilterByDate = True
                isFilterByName = False

            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                isFilterByDate = False
                isFilterByName = True
            End If

            pageIndex = 0
            BindPage()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                isFilterByDate = False
                isFilterByName = False

                dtpDateFrom.Value = Date.Now
                dtpDateTo.Value = Date.Now

                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                txtName.Clear()

                isFilterByDate = False
                isFilterByName = True

                pageIndex = 0
                BindPage()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            If isFilterByDate = True Then
                Me.adpHoliday.FillByHolidayDate(Me.dsLeaveFiling.Holiday, pageIndex, pageSize, totalCount, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            ElseIf isFilterByName = True Then
                If String.IsNullOrEmpty(txtName.Text.Trim) Then
                    Me.adpHoliday.FillByHolidayName(Me.dsLeaveFiling.Holiday, pageIndex, pageSize, totalCount, Nothing)
                Else
                    Me.adpHoliday.FillByHolidayName(Me.dsLeaveFiling.Holiday, pageIndex, pageSize, totalCount, txtName.Text.Trim)
                End If
            Else
                Me.adpHoliday.FillHoliday(Me.dsLeaveFiling.Holiday, pageIndex, pageSize, totalCount)
            End If

            Me.bsHoliday.DataSource = Me.dsLeaveFiling
            Me.bsHoliday.DataMember = dtHoliday.TableName
            Me.bsHoliday.ResetBindings(True)
            dgvList.AutoGenerateColumns = False
            dgvList.DataSource = Me.bsHoliday

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
        Me.bsHoliday.Position = dgvList.SelectedCells(0).RowIndex
    End Sub

    Private Sub SearchCriteria()
        dictionary.Add(" Date", 1)
        dictionary.Add(" Holiday Name", 2)
        cmbSearchCriteria.DisplayMember = "Key"
        cmbSearchCriteria.ValueMember = "Value"
        cmbSearchCriteria.DataSource = New BindingSource(dictionary, Nothing)
    End Sub
#End Region

End Class