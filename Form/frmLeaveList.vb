Imports System.Reflection
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsJeonsoftTableAdapters

Public Class frmLeaveList
    Private connection As New clsConnection
    Private dbMethodServer As New SqlDbMethod(connection.JeonsoftConnection)
    Private dbMethodLocal As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main

    Private pageSize As Integer 'display the number of records per page
    Private pageIndex As Integer 'page sequence number
    Private totalCount As Integer 'total records
    Private pageCount As Integer 'total pages

    Private table As New DataTable
    Private isFiltered As Boolean = False
    Private indexScroll As Integer = 0
    Private indexPosition As Integer = 0

    Private dictionary As New Dictionary(Of String, Integer)
    Private employeeId As Integer = 0
    Private employmentTypeId As Integer = 0
    Private positionId As Integer = 0
    Private teamId As Integer = 0
    Private departmentId As Integer = 0

    Private dsLeaveFiling As New dsLeaveFiling

    Private isClinic As Boolean = False
    Private isSuperior As Boolean = False
    Private isManager As Boolean = False
    Private isLeader As Boolean = False

    Public Sub New(ByVal _employeeId As Integer, ByVal _positionId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _employmentTypeId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        employmentTypeId = _employmentTypeId
        positionId = _positionId
        teamId = _teamId
        departmentId = _departmentId
    End Sub

    Private Sub frmMntTrxApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ActiveControl = dgvList
        pageSize = 100
        pageIndex = 0
        SetPage(employeeId)

        SearchCriteria()

        Application.EnableVisualStyles()
        main.EnableDoubleBuffered(dgvList)

        Dim _superiorIds As New List(Of Integer) From {13, 19, 4, 17, 7, 3, 6}
        Dim _managerIds As New List(Of Integer) From {2, 21, 19, 4}
        Dim _clinicIds As New List(Of Integer) From {3}

        If _superiorIds.Contains(positionId) Then
            isSuperior = True
        ElseIf _managerIds.Contains(positionId) Then
            isManager = True
        Else
            If _clinicIds.Contains(teamId) Then
                isClinic = True
            Else
                grpStatus.Enabled = False
                btnApprove.Visible = False
                btnDisapprove.Visible = False
            End If
        End If

        rdMyFile.Checked = True

    End Sub

    Private Sub frmLeaveList_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        main.FormTrap(Me)
    End Sub

    Private Sub frmMntTrxConsole_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode.Equals(Keys.F2) Then
            btnFileLeave.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F3) Then
            btnView.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F5) Then
            btnRefresh.PerformClick()
        End If
    End Sub

    Private Sub frmMntTrxConsole_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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

    Private Sub dgvTransactionHeader_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvList.DataBindingComplete
        Try
            For _i As Integer = 0 To dgvList.Rows.Count - 1
                If dgvList.Rows(_i).Cells("ColLeaveTypeId").Value = 2 Then
                    dgvList.Rows(_i).Cells("ColClinicClearance").Value = "N/A"
                Else
                    If dgvList.Rows(_i).Cells("ColClinicIsApproved").Value = 1 Then
                        dgvList.Rows(_i).Cells("ColClinicClearance").Value = "Done"
                    End If
                End If
            Next
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

    Private Sub btnNewFiling_Click(sender As Object, e As EventArgs) Handles btnFileLeave.Click
        Try
            Using frmLeaveFiling As New frmLeaveFiling(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId)
                frmLeaveFiling.ShowDialog(Me)

                If frmLeaveFiling.DialogResult = Windows.Forms.DialogResult.OK Then
                    RefreshValues()
                End If
            End Using

            RefreshValues()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            If Me.dgvList.SelectedRows.Count > 0 Then
                Dim _leaveFilingId As Integer = dgvList.CurrentRow.Cells("ColLeaveFilingId").Value

                Using frmLeaveFiling As New frmLeaveFiling(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId, _leaveFilingId)
                    frmLeaveFiling.ShowDialog(Me)

                    If frmLeaveFiling.DialogResult = Windows.Forms.DialogResult.OK Then
                        RefreshValues()
                    End If
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub trxStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdApproved.CheckedChanged, rdPending.CheckedChanged, rdMyFile.CheckedChanged
        'If rdApproved.Checked = True Then
        '    If isClinic = True Then
        '        Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 4 AND ClinicIsApproved = 1", employeeId)
        '    ElseIf isSuperior = True Then
        '        Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 3 AND SuperiorId = {0} AND SuperiorIsApproved = 1", employeeId)
        '    ElseIf isManager = True Then
        '        Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 2 AND ManagerId = {0} AND ManagerIsApproved = 1", employeeId)
        '    End If
        'ElseIf rdPending.Checked = True Then
        '    If isClinic = True Then
        '        Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 5 AND ClinicIsApproved = 0", employeeId)
        '    ElseIf isSuperior = True Then
        '        Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 4 AND SuperiorId = {0} AND SuperiorIsApproved = 0", employeeId)
        '    ElseIf isManager = True Then
        '        Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 3 AND ManagerId = {0} AND ManagerIsApproved = 0", employeeId)
        '    End If
        'Else
        '    If isLeader = True Then
        '        Me.bsLeaveFiling.Filter = String.Format("EmployeeId = {0} OR EncoderId = {1}", employeeId, employeeId)
        '    Else
        '        Me.bsLeaveFiling.Filter = String.Format("EmployeeId = {0}", employeeId)
        '    End If
        'End If
    End Sub

    Private Sub dgvList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvList.DataError
        e.Cancel = False
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            If dgvList.Rows.Count > 0 Then
                '    Dim _leaveFilingId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFilingId")
                '    Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFilingId(_leaveFilingId)

                '    If isSuperior = True Then
                '        With _leaveFilingRow
                '            .SuperiorIsApproved = 1
                '            .SuperiorApprovalDate = DateTime.Now
                '            .SetSuperiorRemarksNull()
                '            .RoutingStatusId = 3
                '            .ModifiedId = employeeId
                '            .ModifiedDate = DateTime.Now
                '        End With

                '    ElseIf isManager = True Then
                '        With _leaveFilingRow
                '            .ManagerIsApproved = 1
                '            .ManagerApprovalDate = DateTime.Now
                '            .SetManagerRemarksNull()
                '            .RoutingStatusId = 2
                '            .ModifiedId = employeeId
                '            .ModifiedDate = DateTime.Now
                '        End With

                '    ElseIf isClinic = True Then
                '        With _leaveFilingRow
                '            .ClinicIsApproved = 1
                '            .ClinicApprovalDate = DateTime.Now
                '            .SetClinicRemarksNull()
                '            .RoutingStatusId = 4
                '            .ModifiedId = employeeId
                '            .ModifiedDate = DateTime.Now
                '        End With
                '    End If
            End If

            'Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
            'Me.dsLeaveFiling.AcceptChanges()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDisapprove_Click(sender As Object, e As EventArgs) Handles btnDisapprove.Click
        Try
            If dgvList.Rows.Count > 0 Then
                'Dim _leaveFilingId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFilingId")
                'Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFilingId(_leaveFilingId)

                'If isSuperior = True Then
                '    With _leaveFilingRow
                '        .SuperiorIsApproved = 0
                '        .SuperiorApprovalDate = DateTime.Now
                '        .SetSuperiorRemarksNull()
                '        .RoutingStatusId = 7
                '        .ModifiedId = employeeId
                '        .ModifiedDate = DateTime.Now
                '    End With

                'ElseIf isManager = True Then
                '    With _leaveFilingRow
                '        .ManagerIsApproved = 0
                '        .ManagerApprovalDate = DateTime.Now
                '        .SetManagerRemarksNull()
                '        .RoutingStatusId = 7
                '        .ModifiedId = employeeId
                '        .ModifiedDate = DateTime.Now
                '    End With

                'ElseIf isClinic = True Then
                '    With _leaveFilingRow
                '        .ClinicIsApproved = 0
                '        .ClinicApprovalDate = DateTime.Now
                '        .SetClinicRemarksNull()
                '        .RoutingStatusId = 7
                '        .ModifiedId = employeeId
                '        .ModifiedDate = DateTime.Now
                '    End With
                'End If
            End If

            'Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
            'Me.dsLeaveFiling.AcceptChanges()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Go()
    End Sub

    Private Sub BindingNavigatorMoveFirstItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveFirstItem.Click
        pageIndex = 0
        If isFiltered = True Then
            SetPage(employeeId, dtpFrom.Value, dtpTo.Value)
        Else
            SetPage(employeeId)
        End If
    End Sub

    Private Sub BindingNavigatorMovePreviousItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMovePreviousItem.Click
        pageIndex -= 1
        If pageIndex < 0 Then
            pageIndex = 0
        End If

        If isFiltered = True Then
            SetPage(employeeId, dtpFrom.Value, dtpTo.Value)
        Else
            SetPage(employeeId)
        End If
    End Sub

    Private Sub BindingNavigatorMoveNextItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveNextItem.Click
        pageIndex += 1
        If pageIndex > pageCount - 1 Then
            pageIndex = pageCount - 1
        End If

        If isFiltered = True Then
            SetPage(employeeId, dtpFrom.Value, dtpTo.Value)
        Else
            SetPage(employeeId)
        End If
    End Sub

    Private Sub BindingNavigatorMoveLastItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveLastItem.Click
        pageIndex = pageCount - 1

        If isFiltered = True Then
            SetPage(employeeId, dtpFrom.Value, dtpTo.Value)
        Else
            SetPage(employeeId)
        End If
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

    Private Sub btnSearchDate_Click(sender As Object, e As EventArgs) Handles btnSearchDate.Click
        SetPage(employeeId, dtpFrom.Value.Date, dtpTo.Value.Date)
        isFiltered = True
    End Sub

    Private Sub btnResetDate_Click(sender As Object, e As EventArgs) Handles btnResetDate.Click
        dtpFrom.Value = Date.Now
        dtpTo.Value = Date.Now
        isFiltered = False
        RefreshValues()
    End Sub

#Region "Sub"
    Public Sub RefreshValues()
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf GetScrollingIndex))
        pageSize = 100
        SetPage(employeeId)
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf SetScrollingIndex))
    End Sub

    Private Sub GetScrollingIndex()
        indexScroll = dgvList.FirstDisplayedCell.RowIndex
        indexPosition = dgvList.CurrentRow.Index
    End Sub

    Private Sub SetScrollingIndex()
        dgvList.FirstDisplayedScrollingRowIndex = indexScroll
        dgvList.Rows(indexPosition).Selected = True
    End Sub

    Private Sub SearchCriteria()
        dictionary.Add(" Date Filed", 1)
        cmbSearchCriteria.DisplayMember = "Key"
        cmbSearchCriteria.ValueMember = "Value"
        cmbSearchCriteria.DataSource = New BindingSource(dictionary, Nothing)
    End Sub

    Private Sub SetPage(Optional ByVal _employeeId As Integer = 0, Optional ByVal _startDate As Date = Nothing, Optional ByVal _endDate As Date = Nothing)
        Try
            totalCount = 0

            If Not _startDate = Nothing AndAlso Not _endDate = Nothing Then
                BindPage(pageSize, pageIndex, totalCount, employeeId, dtpFrom.Value, dtpTo.Value)
                isFiltered = True
            Else
                BindPage(pageSize, pageIndex, totalCount, employeeId)
            End If

            If totalCount Mod pageSize = 0 Then
                pageCount = totalCount / pageSize
            Else
                pageCount = Math.Truncate(totalCount / pageSize) + 1
            End If

            'current and total pages
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

    Private Sub BindPage(ByVal _pageSize As Integer, ByVal _pageIndex As Integer, ByVal _totalCount As Integer, Optional ByVal _employeeId As Integer = 0, Optional ByVal _startDate As Date = Nothing, Optional ByVal _endDate As Date = Nothing)
        Try
            totalCount = 0

            If Not _startDate = Nothing AndAlso Not _endDate = Nothing Then
                Dim _param(5) As SqlParameter
                _param(0) = New SqlParameter("@PageSize", SqlDbType.Int)
                _param(0).Value = _pageSize
                _param(1) = New SqlParameter("@PageIndex", SqlDbType.Int)
                _param(1).Value = _pageIndex
                _param(2) = New SqlParameter("@TotalCount", SqlDbType.Int)
                _param(2).Direction = ParameterDirection.Output
                _param(3) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                _param(3).Value = _employeeId
                _param(4) = New SqlParameter("@StartDate", SqlDbType.Date)
                _param(4).Value = dtpFrom.Value.Date
                _param(5) = New SqlParameter("@EndDate", SqlDbType.Date)
                _param(5).Value = dtpTo.Value.Date

                table = dbMethodLocal.FillDataTableSp("RdLeaveFilingPage", _param)
                totalCount = Convert.ToInt32(_param(2).Value)
            Else
                Dim _param(3) As SqlParameter
                _param(0) = New SqlParameter("@PageSize", SqlDbType.Int)
                _param(0).Value = pageSize
                _param(1) = New SqlParameter("@PageIndex", SqlDbType.Int)
                _param(1).Value = pageIndex
                _param(2) = New SqlParameter("@TotalCount", SqlDbType.Int)
                _param(2).Direction = ParameterDirection.Output
                _param(3) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                _param(3).Value = _employeeId

                table = dbMethodLocal.FillDataTableSp("RdLeaveFilingPage", _param)
                totalCount = Convert.ToInt32(_param(2).Value)
            End If

            dgvList.AutoGenerateColumns = False
            dgvList.DataSource = table
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
            If isFiltered = True Then
                SetPage(employeeId, dtpFrom.Value.Date, dtpTo.Value.Date)
            Else
                SetPage()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

End Class