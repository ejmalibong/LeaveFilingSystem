Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsJeonsoftTableAdapters

Public Class frmLeaveRecord
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
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

    Private employeeId As Integer = 0
    Private employmentTypeId As Integer = 0
    Private positionId As Integer = 0
    Private teamId As Integer = 0
    Private departmentId As Integer = 0
    'control access
    Private isSuperior As Boolean = False
    Private isManager As Boolean = False
    'list
    Private superiorLevel As New List(Of Integer) From {19, 4, 3, 6, 17, 25, 18, 7} 'sv, asv, sr engr, sr staff, sr line leader, sr nurse, sr technician, line leader
    Private managerLevel As New List(Of Integer) From {15, 21, 2, 13} 'dgm, sr mngr, mngr, asst mngr

    Public Sub New(ByVal _employeeId As Integer, ByVal _positionId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _employmentTypeId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        positionId = _positionId
        departmentId = _departmentId
        teamId = _teamId
        employmentTypeId = _employmentTypeId

        grpStatus.Enabled = False

        If superiorLevel.Contains(positionId) Then
            isSuperior = True
            grpStatus.Enabled = True
            btnApprove.Visible = True
            btnDisapprove.Visible = True
        ElseIf managerLevel.Contains(positionId) Then
            isManager = True
            grpStatus.Enabled = True
            btnApprove.Visible = True
            btnDisapprove.Visible = True
        ElseIf IsApprover(employeeId) = True Then
            grpStatus.Enabled = True
            btnApprove.Visible = True
            btnDisapprove.Visible = True
        Else
            grpStatus.Enabled = False
            btnApprove.Visible = False
            btnDisapprove.Visible = False
        End If
    End Sub

    Private Sub frmLeaveList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pageIndex = 0
        pageSize = 100
        BindPage()

        rdMyFile.Checked = True

        main.EnableDoubleBuffered(dgvList)
        Me.ActiveControl = dgvList

        Me.dgvList.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Me.dgvList.Columns(10).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub frmLeaveRecord_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode.Equals(Keys.F2) Then
            e.Handled = True
            btnApplyLeave.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F3) Then
            e.Handled = True
            btnView.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F5) Then
            e.Handled = True
            btnRefresh.PerformClick()
        End If
    End Sub

    Private Sub frmLeaveRecord_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        dgvList.Dispose()
    End Sub

    Private Sub dgvTransactionHeader_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        btnView.PerformClick()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Reload()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim _frmMain As frmMain = TryCast(Me.MdiParent, frmMain)
            Dim _employeeName As String = String.Empty
            Dim _leaveTypeName As String = String.Empty
            Dim _departmentName As String = String.Empty
            Dim _teamName As String = String.Empty

            If Me.dgvList.SelectedRows.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(_leaveFileId)

                Dim _prmLeaveType(0) As SqlParameter
                _prmLeaveType(0) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
                _prmLeaveType(0).Value = _leaveFilingRow.LeaveTypeId
                _leaveTypeName = dbJeonsoft.ExecuteScalar("SELECT TRIM(Name) AS LeaveTypeName FROM dbo.tblLeaveTypes WHERE Id = @LeaveTypeId", CommandType.Text, _prmLeaveType)

                Dim _prmEmployee(0) As SqlParameter
                _prmEmployee(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                _prmEmployee(0).Value = _leaveFilingRow.EmployeeId

                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdEmployee", CommandType.StoredProcedure, _prmEmployee)

                While _reader.Read()
                    _employeeName = _reader.Item("EmployeeName").ToString.Trim
                    _departmentName = _reader.Item("DepartmentName").ToString.Trim

                    If _reader.Item("TeamName") Is DBNull.Value Then
                        _teamName = String.Empty
                    Else
                        _departmentName += "/" & _reader.Item("TeamName").ToString.Trim
                    End If
                End While
                _reader.Close()

                With _leaveFilingRow
                    If .IsSuperiorId1Null = False AndAlso .SuperiorId1 = employeeId AndAlso .IsSuperiorApprovalDate1Null = True Then
                        If Confirmation(1) = Windows.Forms.DialogResult.Yes Then
                            .SuperiorIsApproved1 = 1
                            .SuperiorApprovalDate1 = DateTime.Now
                            .SetSuperiorRemarks1Null()

                            If .IsSuperiorId2Null = True Then
                                .RoutingStatusId = 3

                                If _leaveFilingRow.StartDate.Date.Equals(_leaveFilingRow.EndDate.Date) Then
                                    _frmMain.SendEmailApprovers(_leaveFilingRow.LeaveFileId, _
                                                                _leaveFilingRow.ManagerId, _
                                                                _leaveTypeName, _
                                                                _employeeName, _
                                                                _departmentName, _
                                                                CDate(dgvList.CurrentRow.Cells("ColStartDate").Value).ToString("MMMM dd, yyyy"), _
                                                                dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                                Else
                                    _frmMain.SendEmailApprovers(_leaveFilingRow.LeaveFileId, _
                                                                _leaveFilingRow.ManagerId, _
                                                                _leaveTypeName, _
                                                                _employeeName, _
                                                                _departmentName, _
                                                                CDate(dgvList.CurrentRow.Cells("ColStartDate").Value).ToString("MMMM dd, yyyy") _
                                                                & " - " & CDate(dgvList.CurrentRow.Cells("ColEndDate").Value).ToString("MMMM dd, yyyy"), _
                                                                dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                                End If
                            Else
                                .RoutingStatusId = 4

                                If _leaveFilingRow.StartDate.Date.Equals(_leaveFilingRow.EndDate.Date) Then
                                    _frmMain.SendEmailApprovers(_leaveFilingRow.LeaveFileId, _
                                                                _leaveFilingRow.SuperiorId2, _
                                                                _leaveTypeName, _
                                                                _employeeName, _
                                                                _departmentName, _
                                                                CDate(dgvList.CurrentRow.Cells("ColStartDate").Value).ToString("MMMM dd, yyyy"), _
                                                                dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                                Else
                                    _frmMain.SendEmailApprovers(_leaveFilingRow.LeaveFileId, _
                                                                _leaveFilingRow.SuperiorId2, _
                                                                _leaveTypeName, _
                                                                _employeeName, _
                                                                _departmentName, _
                                                                CDate(dgvList.CurrentRow.Cells("ColStartDate").Value).ToString("MMMM dd, yyyy") _
                                                                & " - " & CDate(dgvList.CurrentRow.Cells("ColEndDate").Value).ToString("MMMM dd, yyyy"), _
                                                                dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                                End If
                            End If
                        Else
                            Return
                        End If

                    ElseIf .IsSuperiorId2Null = False AndAlso .SuperiorId2 = employeeId AndAlso .IsSuperiorApprovalDate2Null = True Then
                        If Confirmation(1) = Windows.Forms.DialogResult.Yes Then
                            .SuperiorIsApproved2 = 1
                            .SuperiorApprovalDate2 = DateTime.Now
                            .SetSuperiorRemarks2Null()
                            .RoutingStatusId = 3

                            If _leaveFilingRow.StartDate.Date.Equals(_leaveFilingRow.EndDate.Date) Then
                                _frmMain.SendEmailApprovers(_leaveFilingRow.LeaveFileId, _
                                                            _leaveFilingRow.ManagerId, _
                                                            _leaveTypeName, _
                                                            _employeeName, _
                                                            _departmentName, _
                                                            CDate(dgvList.CurrentRow.Cells("ColStartDate").Value).ToString("MMMM dd, yyyy"), _
                                                            dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                            Else
                                _frmMain.SendEmailApprovers(_leaveFilingRow.LeaveFileId, _
                                                            _leaveFilingRow.ManagerId, _
                                                            _leaveTypeName, _
                                                            _employeeName, _
                                                            _departmentName, _
                                                            CDate(dgvList.CurrentRow.Cells("ColStartDate").Value).ToString("MMMM dd, yyyy") _
                                                            & " - " & CDate(dgvList.CurrentRow.Cells("ColEndDate").Value).ToString("MMMM dd, yyyy"), _
                                                            dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                            End If
                        End If

                    ElseIf .ManagerId = employeeId AndAlso .IsManagerApprovalDateNull = True Then
                        If Confirmation(1) = Windows.Forms.DialogResult.Yes Then
                            .ManagerIsApproved = 1
                            .ManagerApprovalDate = DateTime.Now
                            .SetManagerRemarksNull()
                            .RoutingStatusId = 2

                            _frmMain.SendEmailRequestor(True, _
                                                        _leaveFilingRow.EmployeeId, _
                                                        _leaveTypeName, _
                                                        _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy"), _
                                                        _leaveFilingRow.EndDate.Date.ToString("MMMM dd, yyyy"))

                            If _leaveFilingRow.StartDate.Date.Equals(_leaveFilingRow.EndDate.Date) Then
                                _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                     True, _
                                                    _leaveTypeName, _
                                                    _employeeName, _
                                                    _departmentName, _
                                                    _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy"), _
                                                    dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                            Else
                                _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                     True, _
                                                     _leaveTypeName, _
                                                     _employeeName, _
                                                     _departmentName, _
                                                     _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy") _
                                                     & " - " & _leaveFilingRow.EndDate.Date.ToString("MMMM dd, yyyy"), _
                                                     dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                            End If
                        End If
                    End If
                End With

                Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                Reload()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDisapprove_Click(sender As Object, e As EventArgs) Handles btnDisapprove.Click
        Try
            Dim _frmMain As frmMain = TryCast(Me.MdiParent, frmMain)
            Dim _employeeName As String = String.Empty
            Dim _leaveTypeName As String = String.Empty
            Dim _departmentName As String = String.Empty
            Dim _teamName As String = String.Empty

            If Me.dgvList.SelectedRows.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(_leaveFileId)

                Dim _prmLeaveType(0) As SqlParameter
                _prmLeaveType(0) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
                _prmLeaveType(0).Value = _leaveFilingRow.LeaveTypeId
                _leaveTypeName = dbJeonsoft.ExecuteScalar("SELECT TRIM(Name) AS LeaveTypeName FROM dbo.tblLeaveTypes WHERE Id = @LeaveTypeId", CommandType.Text, _prmLeaveType)

                Dim _prmEmployee(0) As SqlParameter
                _prmEmployee(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                _prmEmployee(0).Value = _leaveFilingRow.EmployeeId

                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdEmployee", CommandType.StoredProcedure, _prmEmployee)

                While _reader.Read()
                    _employeeName = _reader.Item("EmployeeName").ToString.Trim
                    _departmentName = _reader.Item("DepartmentName").ToString.Trim

                    If _reader.Item("TeamName") Is DBNull.Value Then
                        _teamName = String.Empty
                    Else
                        _departmentName += "/" & _reader.Item("TeamName").ToString.Trim
                    End If
                End While
                _reader.Close()

                With _leaveFilingRow
                    If .IsSuperiorId1Null = False AndAlso .SuperiorId1 = employeeId AndAlso .IsSuperiorApprovalDate1Null = True Then
                        If Confirmation(2) = Windows.Forms.DialogResult.Yes Then
                            .SuperiorIsApproved1 = 0
                            .SuperiorApprovalDate1 = DateTime.Now
                            .SetSuperiorRemarks1Null()

                            If .IsSuperiorId2Null = True Then
                                .RoutingStatusId = 7
                                _frmMain.SendEmailRequestor(False, _
                                                            _leaveFilingRow.EmployeeId, _
                                                            _leaveTypeName, _
                                                            _leaveFilingRow.StartDate.Date.Date.ToString("MMMM dd, yyyy"), _
                                                            _leaveFilingRow.EndDate.Date.Date.ToString("MMMM dd, yyyy"))

                                If _leaveFilingRow.StartDate.Date.Equals(_leaveFilingRow.EndDate.Date) Then
                                    _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                         False, _
                                                         _leaveTypeName, _
                                                         _employeeName, _
                                                         _departmentName, _
                                                         _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy"),
                                                         dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                                Else
                                    _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                         False, _
                                                         _leaveTypeName, _
                                                         _employeeName, _
                                                         _departmentName, _
                                                         _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy") _
                                                         & " - " & _leaveFilingRow.EndDate.Date.ToString("MMMM dd, yyyy"), _
                                                         dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                                End If

                            Else
                                .RoutingStatusId = 7
                                _frmMain.SendEmailRequestor(False, _
                                                            _leaveFilingRow.EmployeeId, _
                                                            _leaveTypeName, _
                                                            _leaveFilingRow.StartDate.Date.Date.ToString("MMMM dd, yyyy"), _
                                                            _leaveFilingRow.EndDate.Date.Date.ToString("MMMM dd, yyyy"))

                                If _leaveFilingRow.StartDate.Date.Equals(_leaveFilingRow.EndDate.Date) Then
                                    _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                         False, _
                                                         _leaveTypeName, _
                                                         _employeeName, _
                                                         _departmentName, _
                                                         _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy"), _
                                                         dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                                Else
                                    _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                         False, _
                                                         _leaveTypeName, _
                                                         _employeeName, _
                                                         _departmentName, _
                                                         _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy") _
                                                         & " - " & _leaveFilingRow.EndDate.Date.ToString("MMMM dd, yyyy"), _
                                                         dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                                End If
                            End If
                        End If

                    ElseIf .IsSuperiorId2Null = False AndAlso .SuperiorId2 = employeeId AndAlso .IsSuperiorApprovalDate2Null = True Then
                        If Confirmation(2) = Windows.Forms.DialogResult.Yes Then
                            .SuperiorIsApproved2 = 0
                            .SuperiorApprovalDate2 = DateTime.Now
                            .SetSuperiorRemarks2Null()

                            .RoutingStatusId = 7
                            _frmMain.SendEmailRequestor(False, _
                                                        _leaveFilingRow.EmployeeId, _
                                                        _leaveTypeName, _
                                                        _leaveFilingRow.StartDate.Date.Date.ToString("MMMM dd, yyyy"), _
                                                        _leaveFilingRow.EndDate.Date.Date.ToString("MMMM dd, yyyy"))

                            If _leaveFilingRow.StartDate.Date.Equals(_leaveFilingRow.EndDate.Date) Then
                                _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                     False, _
                                                     _leaveTypeName, _
                                                     _employeeName, _
                                                     _departmentName, _
                                                     _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy"), _
                                                     dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                            Else
                                _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                     False, _
                                                     _leaveTypeName, _
                                                     _employeeName, _
                                                     _departmentName, _
                                                     _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy") _
                                                     & " - " & _leaveFilingRow.EndDate.Date.ToString("MMMM dd, yyyy"), _
                                                     dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                            End If
                        End If

                    ElseIf .ManagerId = employeeId AndAlso .IsManagerApprovalDateNull = True Then
                        If Confirmation(2) = Windows.Forms.DialogResult.Yes Then
                            .ManagerIsApproved = 0
                            .ManagerApprovalDate = DateTime.Now
                            .SetManagerRemarksNull()

                            .RoutingStatusId = 7
                            _frmMain.SendEmailRequestor(False, _
                                                        _leaveFilingRow.EmployeeId, _
                                                        _leaveTypeName, _
                                                        _leaveFilingRow.StartDate.Date.Date.ToString("MMMM dd, yyyy"), _
                                                        _leaveFilingRow.EndDate.Date.Date.ToString("MMMM dd, yyyy"))

                            If _leaveFilingRow.StartDate.Date.Equals(_leaveFilingRow.EndDate.Date) Then
                                _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                     False, _
                                                     _leaveTypeName, _
                                                     _employeeName, _
                                                     _departmentName, _
                                                     _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy"), _
                                                     dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                            Else
                                _frmMain.SendEmailHr(_leaveFilingRow.LeaveFileId, _
                                                     False, _
                                                     _leaveTypeName, _
                                                     _employeeName, _
                                                     _departmentName, _
                                                     _leaveFilingRow.StartDate.Date.ToString("MMMM dd, yyyy") _
                                                     & " - " & _leaveFilingRow.EndDate.Date.ToString("MMMM dd, yyyy"), _
                                                     dgvList.CurrentRow.Cells("ColReason").Value.ToString)
                            End If
                        End If
                    End If
                End With

                Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                Reload()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnApplyLeave_Click(sender As Object, e As EventArgs) Handles btnApplyLeave.Click
        Try
            rdMyFile.Checked = True
            Using frmLeaveFiling As New frmLeaveForm(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId)
                frmLeaveFiling.ShowDialog(Me)

                If frmLeaveFiling.DialogResult = Windows.Forms.DialogResult.OK Then
                    Reload()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            If Me.dgvList.SelectedRows.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Using frmLeaveFiling As New frmLeaveForm(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId, _leaveFileId)
                    frmLeaveFiling.ShowDialog(Me)

                    If frmLeaveFiling.DialogResult = Windows.Forms.DialogResult.OK Then
                        Reload()
                    End If
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If Environment.MachineName = "NBCP-MDT-013" Or Environment.MachineName = "NBCP-MDT-016" Then
            Application.Exit()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub trxStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdApproved.CheckedChanged, rdPending.CheckedChanged, rdMyFile.CheckedChanged, rdDisapproved.CheckedChanged
        pageSize = 100
        pageIndex = 0
        BindPage()
    End Sub

    Private Sub dgvList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvList.DataError
        e.Cancel = False
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

#Region "Sub"
    Private Sub BindPage()
        Try
            totalCount = 0

            If rdMyFile.Checked = True Then
                Me.adpLeaveFiling.FillByEmployeeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId)
            ElseIf rdPending.Checked = True Then
                Me.adpLeaveFiling.FillPending(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId)
            ElseIf rdApproved.Checked = True Then
                Me.adpLeaveFiling.FillApproved(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId)
            ElseIf rdDisapproved.Checked = True Then
                Me.adpLeaveFiling.FillDisapproved(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId)
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
        Me.bsLeaveFiling.Position = dgvList.SelectedCells(0).RowIndex
    End Sub

    Private Function Confirmation(ByVal _status As Integer) As DialogResult
        If _status = 1 Then
            If MessageBox.Show("Are you sure you want to approve this record?" & Environment.NewLine & "This cannot be modified.", "", _
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                   MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                Return Windows.Forms.DialogResult.Yes
            Else
                Return Windows.Forms.DialogResult.No
            End If
        Else
            If MessageBox.Show("Are you sure you want to disapprove this record?" & Environment.NewLine & "This cannot be modified.", "", _
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                   MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                Return Windows.Forms.DialogResult.Yes
            Else
                Return Windows.Forms.DialogResult.No
            End If
        End If
    End Function

    'check if has the right to enable the status panel
    Private Function IsApprover(ByVal _employeeId As Integer) As Boolean
        Dim _isApprover As Boolean = False
        Dim _count As Integer = 0

        Try
            Dim _prm(0) As SqlParameter
            _prm(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prm(0).Value = _employeeId
            _count = dbLeaveFiling.ExecuteScalar("SELECT COUNT(Id) FROM dbo.Employee WHERE Id = @EmployeeId", CommandType.Text, _prm)
            If _count > 0 Then
                _isApprover = True
            Else
                _isApprover = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _isApprover
    End Function
#End Region

End Class