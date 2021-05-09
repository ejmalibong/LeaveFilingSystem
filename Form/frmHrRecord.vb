Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsJeonsoftTableAdapters

Public Class frmHrRecord
    Private connection As New clsConnection
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main
    'server datetime
    Private serverDate As DateTime = dbLeaveFiling.GetServerDate
    'dataset
    Private dsLeaveFiling As New dsLeaveFiling
    Private adpLeaveFiling As New LeaveFilingTableAdapter
    Private dtLeaveFiling As New LeaveFilingDataTable
    Private bsLeaveFiling As New BindingSource
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
    Private isFilterByLeaveType As Boolean = False
    Private isFilterByDateCreated As Boolean = False
    Private isFilterByEmployeeName As Boolean = False
    Private isFilterByDepartment As Boolean = False
    Private isFilterByReason As Boolean = False

    Private employeeId As Integer = 0
    Private employmentTypeId As Integer = 0
    Private positionId As Integer = 0
    Private teamId As Integer = 0
    Private departmentId As Integer = 0

    Public Sub New(ByVal _employeeId As Integer, ByVal _positionId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _employmentTypeId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        positionId = _positionId
        departmentId = _departmentId
        teamId = _teamId
        employmentTypeId = _employmentTypeId
    End Sub

    Private Sub frmHrList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Application.EnableVisualStyles()

        pageIndex = 0
        pageSize = 200
        BindPage()

        rdPending.Checked = True

        main.EnableDoubleBuffered(dgvList)
        Me.ActiveControl = dgvList

        SearchCriteria()

        Me.dgvList.Columns(10).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Me.dgvList.Columns(12).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub frmHrList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode.Equals(Keys.F3) Then
            e.Handled = True
            btnView.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F5) Then
            e.Handled = True
            btnRefresh.PerformClick()
        End If
    End Sub

    Private Sub frmHrList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        dgvList.Dispose()
    End Sub

    Private Sub dgvTransactionHeader_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        btnView.PerformClick()
    End Sub

    Private Sub dgvList_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvList.CellFormatting
        Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvList.DataBindingComplete
        Try
            
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            RefreshValues()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            If Me.dgvList.SelectedCells.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Using frmLeaveFiling As New frmLeaveForm(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId, _leaveFileId)
                    frmLeaveFiling.ShowDialog(Me)
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvList.DataError
        e.Cancel = False
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            If Me.dgvList.SelectedCells.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Me.adpLeaveFiling.FillByLeaveFileId(Me.dsLeaveFiling.LeaveFiling, _leaveFileId)
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(_leaveFileId)

                With _leaveFilingRow
                    If .RoutingStatusId = 2 Then
                        .RoutingStatusId = 1
                    End If
                    .IsEncoded = True
                End With

                Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                RefreshValues()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDisapprove_Click(sender As Object, e As EventArgs) Handles btnDisapprove.Click
        Try
            If Me.dgvList.SelectedCells.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Me.adpLeaveFiling.FillByLeaveFileId(Me.dsLeaveFiling.LeaveFiling, _leaveFileId)
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(_leaveFileId)

                With _leaveFilingRow
                    If .RoutingStatusId = 1 Then
                        .RoutingStatusId = 2
                    End If
                    .IsEncoded = False
                End With

                Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                RefreshValues()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trxStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdApproved.CheckedChanged, rdPending.CheckedChanged, rdDisapproved.CheckedChanged
        pageSize = 200
        pageIndex = 0
        BindPage()
    End Sub

    Private Sub BindingNavigatorMoveFirstItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveFirstItem.Click
        pageIndex = 0

        BindPage()
    End Sub

    Private Sub BindingNavigatorMovePreviousItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMovePreviousItem.Click
         pageIndex -= 1
        If pageIndex < 0 Then
            pageIndex = 0
        End If

        BindPage()
    End Sub

    Private Sub BindingNavigatorMoveNextItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveNextItem.Click
        pageIndex += 1
        If pageIndex > pageCount - 1 Then
            pageIndex = pageCount - 1
        End If

        BindPage()
    End Sub

    Private Sub BindingNavigatorMoveLastItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveLastItem.Click
        pageIndex = pageCount - 1

        BindPage()
    End Sub

    Private Sub txtPageNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPageNumber.KeyPress
        If ((Asc(e.KeyChar) >= 48 AndAlso Asc(e.KeyChar) <= 57) OrElse Asc(e.KeyChar) = 8 OrElse Asc(e.KeyChar) = 13 OrElse Asc(e.KeyChar) = 127) Then
            e.Handled = False
            If Asc(e.KeyChar) = 13 Then
                Go()
            End If
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Go()
    End Sub

    Private Sub cmbSearchCriteria_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSearchCriteria.SelectedValueChanged
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                pnlLeaveType.Visible = True
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = False
                pnlDepartment.Visible = False
                pnlReason.Visible = False
                Me.ActiveControl = cmbLeaveType
            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = True
                pnlEmployeeName.Visible = False
                pnlDepartment.Visible = False
                pnlReason.Visible = False
                Me.ActiveControl = dtpDateCreatedFrom
            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = True
                pnlDepartment.Visible = False
                pnlReason.Visible = False
                Me.ActiveControl = txtEmployeeName
            ElseIf cmbSearchCriteria.SelectedValue = 4 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = False
                pnlDepartment.Visible = True
                pnlReason.Visible = False
                Me.ActiveControl = cmbDepartment
            ElseIf cmbSearchCriteria.SelectedValue = 5 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = False
                pnlDepartment.Visible = False
                pnlReason.Visible = True
                Me.ActiveControl = txtReason
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                isFilterByLeaveType = True
            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                If dtpDateCreatedFrom.Value.Date > dtpDateCreatedTo.Value.Date Then
                    MessageBox.Show("Start date is later than end date.", "Invalid date range", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
                isFilterByDateCreated = True
            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                If String.IsNullOrEmpty(txtEmployeeName.Text.Trim) Then
                    Return
                End If
                isFilterByEmployeeName = True
            ElseIf cmbSearchCriteria.SelectedValue = 4 Then
                isFilterByDepartment = True
            ElseIf cmbSearchCriteria.SelectedValue = 5 Then
                If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                    Return
                End If
                isFilterByReason = True
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
                isFilterByLeaveType = False
                cmbLeaveType.SelectedValue = 0
                pageIndex = 0
                BindPage()
            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                isFilterByDateCreated = False
                dtpDateCreatedFrom.Value = Date.Now.Date
                dtpDateCreatedTo.Value = Date.Now.Date
                pageIndex = 0
                BindPage()
            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                isFilterByEmployeeName = False
                txtEmployeeName.Clear()
                pageIndex = 0
                BindPage()
            ElseIf cmbSearchCriteria.SelectedValue = 4 Then
                isFilterByDepartment = False
                cmbDepartment.SelectedValue = 0
                pageIndex = 0
                BindPage()
            ElseIf cmbSearchCriteria.SelectedValue = 5 Then
                isFilterByReason = False
                txtReason.Clear()
                pageIndex = 0
                BindPage()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Sub"
    Private Sub BindPage(Optional ByVal _routingStatusId As Integer = 0)
        Try
            totalCount = 0

            If isFilterByLeaveType = True Then
                If rdPending.Checked = True Then
                    If cmbLeaveType.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillByRoutingStatusIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByRoutingStatusIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, cmbLeaveType.SelectedValue)
                    End If
                ElseIf rdApproved.Checked = True Then
                    If cmbLeaveType.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillByRoutingStatusIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 1, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByRoutingStatusIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 1, cmbLeaveType.SelectedValue)
                    End If
                ElseIf rdDisapproved.Checked = True Then
                    If cmbLeaveType.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillByRoutingStatusIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 7, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByRoutingStatusIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 7, cmbLeaveType.SelectedValue)
                    End If
                End If
            ElseIf isFilterByDateCreated = True Then
                If rdPending.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusIdDateCreated(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, dtpDateCreatedFrom.Value.Date, dtpDateCreatedTo.Value.Date)
                ElseIf rdApproved.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusIdDateCreated(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 1, dtpDateCreatedFrom.Value.Date, dtpDateCreatedTo.Value.Date)
                ElseIf rdDisapproved.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusIdDateCreated(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 7, dtpDateCreatedFrom.Value.Date, dtpDateCreatedTo.Value.Date)
                End If
            ElseIf isFilterByEmployeeName = True Then
                If rdPending.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusIdEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, txtEmployeeName.Text.Trim)
                ElseIf rdApproved.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusIdEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 1, txtEmployeeName.Text.Trim)
                ElseIf rdDisapproved.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusIdEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 7, txtEmployeeName.Text.Trim)
                End If
            ElseIf isFilterByDepartment = True Then
                If rdPending.Checked = True Then
                    If cmbDepartment.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillByRoutingStatusIdDepartmentId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByRoutingStatusIdDepartmentId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, cmbDepartment.SelectedValue)
                    End If
                ElseIf rdApproved.Checked = True Then
                    If cmbDepartment.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillByRoutingStatusIdDepartmentId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 1, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByRoutingStatusIdDepartmentId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 1, cmbDepartment.SelectedValue)
                    End If
                ElseIf rdDisapproved.Checked = True Then
                    If cmbDepartment.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillByRoutingStatusIdDepartmentId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 7, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByRoutingStatusIdDepartmentId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 7, cmbDepartment.SelectedValue)
                    End If
                End If
            ElseIf isFilterByReason = True Then
                If rdPending.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusIdReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, txtReason.Text.Trim)
                ElseIf rdApproved.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusIdReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 1, txtReason.Text.Trim)
                ElseIf rdDisapproved.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusIdReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 7, txtReason.Text.Trim)
                End If
            Else
                If rdPending.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing)
                ElseIf rdApproved.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 1)
                ElseIf rdDisapproved.Checked = True Then
                    Me.adpLeaveFiling.FillByRoutingStatusId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, 7)
                End If
            End If

            Me.bsLeaveFiling.DataSource = Me.dsLeaveFiling
            Me.bsLeaveFiling.DataMember = dtLeaveFiling.TableName
            Me.bsLeaveFiling.ResetBindings(True)
            dgvList.AutoGenerateColumns = False
            Me.dgvList.DataSource = Me.bsLeaveFiling

            If totalCount Mod pageSize = 0 Then
                If totalCount = 0 Then
                    pageCount = (totalCount / pageSize) + 1
                Else
                    pageCount = totalCount / pageSize
                End If
            Else
                pageCount = Math.Truncate(totalCount / pageSize) + 1
            End If

            'current page index and total pages
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

    Public Sub RefreshValues()
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
        Me.bsLeaveFiling.Position = dgvList.SelectedCells(0).RowIndex
    End Sub

    Private Sub SearchCriteria()
        dictionary.Add(" Leave Type", 1)
        dictionary.Add(" Date Created", 2)
        dictionary.Add(" Employee Name", 3)
        dictionary.Add(" Department", 4)
        dictionary.Add(" Reason", 5)
        cmbSearchCriteria.DisplayMember = "Key"
        cmbSearchCriteria.ValueMember = "Value"
        cmbSearchCriteria.DataSource = New BindingSource(dictionary, Nothing)

        dbLeaveFiling.FillCmbWithCaption("RdLeaveType", CommandType.StoredProcedure, "Id", "LeaveTypeName", cmbLeaveType, "< All > ")
        dbLeaveFiling.FillCmbWithCaption("RdDepartment", CommandType.StoredProcedure, "Id", "DepartmentName", cmbDepartment, "< All > ")

    End Sub
#End Region

End Class