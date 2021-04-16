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

    'paging
    Private pageSize As Integer

    Private pageIndex As Integer
    Private totalCount As Integer
    Private pageCount As Integer
    Private indexScroll As Integer = 0
    Private indexPosition As Integer = 0

    'constructor
    Private employeeId As Integer = 0

    Private employmentTypeId As Integer = 0
    Private positionId As Integer = 0
    Private teamId As Integer = 0
    Private departmentId As Integer = 0

    'access control
    Private isDgm As Boolean = False
    Private isManager As Boolean = False
    Private isSuperior As Boolean = False

    Public Sub New(ByVal _employeeId As Integer, ByVal _positionId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _employmentTypeId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        positionId = _positionId
        departmentId = _departmentId
        teamId = _teamId
        employmentTypeId = _employmentTypeId

        'check position and section
        Dim _managerIds As New List(Of Integer) From {15, 21, 2, 13, 19, 4} 'dgm, sr mngr, mngr, asst mngr, sv, asv
        Dim _superiorIds As New List(Of Integer) From {19, 4, 3, 6, 17, 7, 25} 'sv, asv, sr engr, sr staff, sr line leader, line leader, sr nurse

        If _managerIds.Contains(positionId) Then
            isManager = True
            If _superiorIds.Contains(positionId) Then
                isSuperior = True
            End If
        ElseIf _superiorIds.Contains(positionId) Then
            isSuperior = True
        Else
            grpStatus.Enabled = False
            btnApprove.Visible = False
            btnDisapprove.Visible = False
        End If

        If positionId = 15 Then
            isDgm = True
        End If
    End Sub

    Private Sub frmLeaveList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Application.EnableVisualStyles()

        pageIndex = 0
        pageSize = 100
        BindPage()

        rdMyFile.Checked = True

        main.EnableDoubleBuffered(dgvList)
        Me.ActiveControl = dgvList
    End Sub

    Private Sub frmLeaveRecord_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        'main.FormTrap(Me)
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

    Private Sub dgvList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvList.DataBindingComplete
        Try
            'For _i As Integer = 0 To dgvList.Rows.Count - 1
            '    If dgvList.Rows(_i).Cells("ColLeaveTypeId").Value = 2 Then
            '        dgvList.Rows(_i).Cells("ColClinicClearance").Value = "N/A"
            '    Else
            '        If dgvList.Rows(_i).Cells("ColClinicIsApproved").Value = True Then
            '            dgvList.Rows(_i).Cells("ColClinicClearance").Value = "FTW"
            '        Else
            '            dgvList.Rows(_i).Cells("ColClinicClearance").Value = "Pending"
            '        End If
            '    End If
            'Next
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

    Private Sub btnApplyLeave_Click(sender As Object, e As EventArgs) Handles btnApplyLeave.Click
        Try
            rdMyFile.Checked = True
            Using frmLeaveFiling As New frmLeaveForm(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId)
                frmLeaveFiling.ShowDialog(Me)

                If frmLeaveFiling.DialogResult = Windows.Forms.DialogResult.OK Then
                    RefreshValues()
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
                        RefreshValues()
                    End If
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If Environment.MachineName = "NBCP-MDT-013" Or Environment.MachineName = "NBCP-MDT-016" Or Not teamId = 1 Then
            Application.Exit()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim _frmMain As frmMain = TryCast(Me.MdiParent, frmMain)

            If Me.dgvList.SelectedRows.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Me.adpLeaveFiling.FillByLeaveFileId(Me.dsLeaveFiling.LeaveFiling, _leaveFileId)
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(_leaveFileId)

                Dim _leaveTypeName As String = String.Empty

                If _leaveFilingRow.LeaveTypeId = 1 Then
                    _leaveTypeName = "Sick Leave"
                ElseIf _leaveFilingRow.LeaveTypeId = 2 Then
                    _leaveTypeName = "Vacation Leave"
                Else
                    _leaveTypeName = "Birthday Leave"
                End If

                'Dim _employeeCode As String = String.Empty
                'Dim _prmEmpCode(0) As SqlParameter
                '_prmEmpCode(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                '_prmEmpCode(0).Value = _leaveFilingRow.EmployeeId
                '_employeeCode = dbJeonsoft.ExecuteScalar("SELECT EmployeeCode FROM dbo.tblEmployees WHERE Id = @EmployeeId", CommandType.Text, _prmEmpCode)

                'Dim _employeeName As String = String.Empty
                'Dim _prmEmpName(0) As SqlParameter
                '_prmEmpName(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                '_prmEmpName(0).Value = _leaveFilingRow.EmployeeId
                '_employeeName = dbJeonsoft.ExecuteScalar("SELECT Name FROM dbo.tblEmployees WHERE Id = @EmployeeId", CommandType.Text, _prmEmpName)

                Dim _employeeName As String = String.Empty
                Dim _departmentName As String = String.Empty
                Dim _teamName As String = String.Empty
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
                        '_teamName = _reader.Item("TeamName").ToString.Trim
                        _departmentName += "/" & _reader.Item("TeamName").ToString.Trim
                    End If
                End While
                _reader.Close()

                With _leaveFilingRow
                    If isManager = True AndAlso isSuperior = True Then
                        If .IsSuperiorIdNull = True AndAlso .ManagerId = employeeId Then
                            .ManagerIsApproved = 1
                            .RoutingStatusId = 2
                            .ManagerApprovalDate = DateTime.Now
                            .SetManagerRemarksNull()

                            _frmMain.SendEmailRequestor(True, _leaveFilingRow.EmployeeId, _leaveTypeName, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date)

                            'If .LeaveTypeId = 2 Then
                            '    SendEmail(True, _employeeName, _employeeCode, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date, _leaveFileId)
                            'End If
                        ElseIf .SuperiorId = employeeId AndAlso .ManagerId = employeeId Then
                            .ManagerIsApproved = 1
                            .RoutingStatusId = 2
                            .ManagerApprovalDate = DateTime.Now
                            .SetManagerRemarksNull()

                            .SuperiorIsApproved = 1
                            .SuperiorApprovalDate = DateTime.Now
                            .SetSuperiorRemarksNull()

                            _frmMain.SendEmailRequestor(True, _leaveFilingRow.EmployeeId, _leaveTypeName, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date)

                            'If .LeaveTypeId = 2 Then
                            '    SendEmail(True, _employeeName, _employeeCode, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date, _leaveFileId)
                            'End If

                        ElseIf .SuperiorId = employeeId Then
                            .SuperiorIsApproved = 1
                            .SuperiorApprovalDate = DateTime.Now
                            .RoutingStatusId = 3
                            .SetSuperiorRemarksNull()

                            _frmMain.SendEmailApprovers(_leaveFilingRow.ManagerId, _leaveTypeName, _employeeName, _departmentName, dgvList.CurrentRow.Cells("ColStartDate").Value.ToString("MMMM dd, yyyy") & " - " & dgvList.CurrentRow.Cells("ColEndDate").Value, dgvList.CurrentRow.Cells("ColReason").Value)

                            '_frmMain.SendEmailRequestor(True, _leaveFilingRow.EmployeeId, _leaveTypeName, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date)

                        ElseIf .ManagerId = employeeId Then
                            .ManagerIsApproved = 1
                            .ManagerApprovalDate = DateTime.Now
                            .RoutingStatusId = 2
                            .SetManagerRemarksNull()

                            _frmMain.SendEmailRequestor(True, _leaveFilingRow.EmployeeId, _leaveTypeName, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date)

                        End If
                    ElseIf isManager = True Then
                        If .ManagerId = employeeId Then
                            .ManagerIsApproved = 1
                            .ManagerApprovalDate = DateTime.Now
                            .RoutingStatusId = 2
                            .SetManagerRemarksNull()

                            _frmMain.SendEmailRequestor(True, _leaveFilingRow.EmployeeId, _leaveTypeName, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date)

                            'If .LeaveTypeId = 2 Then
                            '    SendEmail(True, _employeeName, _employeeCode, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date, _leaveFileId)
                            'End If
                        End If
                    ElseIf isSuperior = True Then
                        If .SuperiorId = employeeId Then
                            .SuperiorIsApproved = 1
                            .SuperiorApprovalDate = DateTime.Now
                            .RoutingStatusId = 3
                            .SetSuperiorRemarksNull()
                        End If
                    End If
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
            Dim _frmMain As frmMain = TryCast(Me.MdiParent, frmMain)

            If Me.dgvList.SelectedRows.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Me.adpLeaveFiling.FillByLeaveFileId(Me.dsLeaveFiling.LeaveFiling, _leaveFileId)
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(_leaveFileId)

                Dim _leaveTypeName As String = String.Empty

                If _leaveFilingRow.LeaveTypeId = 1 Then
                    _leaveTypeName = "Sick Leave"
                ElseIf _leaveFilingRow.LeaveTypeId = 2 Then
                    _leaveTypeName = "Vacation Leave"
                Else
                    _leaveTypeName = "Birthday Leave"
                End If

                'Dim _employeeCode As String = String.Empty
                'Dim _prmEmpCode(0) As SqlParameter
                '_prmEmpCode(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                '_prmEmpCode(0).Value = _leaveFilingRow.EmployeeId
                '_employeeCode = dbJeonsoft.ExecuteScalar("SELECT EmployeeCode FROM dbo.tblEmployees WHERE Id = @EmployeeId", CommandType.Text, _prmEmpCode)

                'Dim _employeeName As String = String.Empty
                'Dim _prmEmpName(0) As SqlParameter
                '_prmEmpName(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                '_prmEmpName(0).Value = _leaveFilingRow.EmployeeId
                '_employeeName = dbJeonsoft.ExecuteScalar("SELECT Name FROM dbo.tblEmployees WHERE Id = @EmployeeId", CommandType.Text, _prmEmpName)

                Dim _employeeName As String = String.Empty
                Dim _departmentName As String = String.Empty
                Dim _teamName As String = String.Empty
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
                        '_teamName = _reader.Item("TeamName").ToString.Trim
                        _departmentName += "/" & _reader.Item("TeamName").ToString.Trim
                    End If
                End While
                _reader.Close()

                With _leaveFilingRow
                    If isManager = True AndAlso isSuperior = True Then
                        If .IsSuperiorIdNull = True AndAlso .ManagerId = employeeId Then
                            .ManagerIsApproved = 0
                            .RoutingStatusId = 6
                            .ManagerApprovalDate = DateTime.Now
                            .SetManagerRemarksNull()

                            _frmMain.SendEmailRequestor(False, _leaveFilingRow.EmployeeId, _leaveTypeName, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date)

                            'If .LeaveTypeId = 2 Then
                            '    SendEmail(False, _employeeName, _employeeCode, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date, _leaveFileId)
                            'End If
                        ElseIf .SuperiorId = employeeId AndAlso .ManagerId = employeeId Then
                            .ManagerIsApproved = 0
                            .RoutingStatusId = 6
                            .ManagerApprovalDate = DateTime.Now
                            .SetManagerRemarksNull()

                            .SuperiorIsApproved = 0
                            .SuperiorApprovalDate = DateTime.Now
                            .SetSuperiorRemarksNull()

                            _frmMain.SendEmailRequestor(False, _leaveFilingRow.EmployeeId, _leaveTypeName, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date)

                            'If .LeaveTypeId = 2 Then
                            '    SendEmail(False, _employeeName, _employeeCode, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date, _leaveFileId)
                            'End If
                        ElseIf .SuperiorId = employeeId Then
                            .SuperiorIsApproved = 0
                            .SuperiorApprovalDate = DateTime.Now
                            .RoutingStatusId = 6
                            .SetSuperiorRemarksNull()

                            _frmMain.SendEmailApprovers(_leaveFilingRow.ManagerId, _leaveTypeName, _employeeName, _departmentName, dgvList.CurrentRow.Cells("ColStartDate").Value.ToString("MMMM dd, yyyy") & " - " & dgvList.CurrentRow.Cells("ColEndDate").Value, dgvList.CurrentRow.Cells("ColReason").Value)

                        ElseIf .ManagerId = employeeId Then
                            .ManagerIsApproved = 0
                            .ManagerApprovalDate = DateTime.Now
                            .RoutingStatusId = 6
                            .SetManagerRemarksNull()

                            _frmMain.SendEmailRequestor(False, _leaveFilingRow.EmployeeId, _leaveTypeName, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date)

                            'If .LeaveTypeId = 2 Then
                            '    SendEmail(False, _employeeName, _employeeCode, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date, _leaveFileId)
                            'End If
                        End If
                    ElseIf isManager = True Then
                        If .ManagerId = employeeId Then
                            .ManagerIsApproved = 0
                            .ManagerApprovalDate = DateTime.Now
                            .RoutingStatusId = 6
                            .SetManagerRemarksNull()

                            _frmMain.SendEmailRequestor(False, _leaveFilingRow.EmployeeId, _leaveTypeName, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date)

                            'If .LeaveTypeId = 2 Then
                            '    SendEmail(False, _employeeName, _employeeCode, _leaveFilingRow.StartDate.Date.Date, _leaveFilingRow.EndDate.Date.Date, _leaveFileId)
                            'End If
                        End If
                    ElseIf isSuperior = True Then
                        If .SuperiorId = employeeId Then
                            .SuperiorIsApproved = 0
                            .SuperiorApprovalDate = DateTime.Now
                            .RoutingStatusId = 6
                            .SetSuperiorRemarksNull()

                            _frmMain.SendEmailApprovers(_leaveFilingRow.ManagerId, _leaveTypeName, _employeeName, _departmentName, dgvList.CurrentRow.Cells("ColStartDate").Value.ToString("MMMM dd, yyyy") & " - " & dgvList.CurrentRow.Cells("ColEndDate").Value, dgvList.CurrentRow.Cells("ColReason").Value)
                        End If
                    End If
                End With

                Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                RefreshValues()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
                If isManager = True AndAlso isSuperior = True Then
                    'Me.adpLeaveFiling.FillByManagerSuperior(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, teamId, employeeId, 2)
                    If positionId = 19 Or positionId = 4 Then
                        Me.adpLeaveFiling.FillByManagerSuperior(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, Nothing, employeeId, 2)
                    Else
                        Me.adpLeaveFiling.FillByManagerSuperior(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, teamId, employeeId, 2)
                    End If
                ElseIf isManager = True Then
                    If isDgm = True Then
                        Me.adpLeaveFiling.FillByManagerId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, employeeId, 3)
                    Else
                        Me.adpLeaveFiling.FillByManagerId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, employeeId, 3)
                    End If
                ElseIf isSuperior = True Then
                    Me.adpLeaveFiling.FillBySuperiorId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, teamId, employeeId, 4)
                End If
            ElseIf rdApproved.Checked = True Then
                If isManager = True AndAlso isSuperior = True Then
                    Me.adpLeaveFiling.FillByManagerSuperior(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, Nothing, employeeId, 1)
                ElseIf isManager = True Then
                    If isDgm = True Then
                        Me.adpLeaveFiling.FillByManagerId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, employeeId, 2)
                    Else
                        Me.adpLeaveFiling.FillByManagerId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, Nothing, Nothing)
                    End If
                ElseIf isSuperior = True Then
                    Me.adpLeaveFiling.FillBySuperiorId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, teamId, employeeId, Nothing)
                End If
            ElseIf rdDisapproved.Checked = True Then
                If isManager = True AndAlso isSuperior = True Then
                    Me.adpLeaveFiling.FillDisapproved(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, Nothing, Nothing, Nothing, Nothing)
                ElseIf isManager = True Then
                    Me.adpLeaveFiling.FillDisapproved(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, Nothing, Nothing, Nothing, Nothing)
                ElseIf isSuperior = True Then
                    Me.adpLeaveFiling.FillBySuperiorId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, departmentId, teamId, employeeId, 6)
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
        dgvList.Rows(indexPosition).Selected = True
        Me.bsLeaveFiling.Position = dgvList.SelectedCells(0).RowIndex
    End Sub

    Private Function GetEmail(ByVal _employeeCode As String) As String
        Dim _emailAddress As String = String.Empty

        Try
            Dim _prmEmployeeCode(0) As SqlParameter
            _prmEmployeeCode(0) = New SqlParameter("@EmployeeCode", SqlDbType.NVarChar)
            _prmEmployeeCode(0).Value = _employeeCode.Trim
            _emailAddress = dbJeonsoft.ExecuteScalar("SELECT EmailAddress FROM dbo.tblEmployees WHERE EmployeeCode = @EmployeeCode", CommandType.Text, _prmEmployeeCode)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _emailAddress
    End Function

    Private Sub SendEmail(ByVal _isApproved As Boolean, ByVal _employeeName As String, ByVal _employeeCode As String, ByVal _startDate As Date, ByVal _endDate As Date, ByVal _leaveFileId As Integer)
        'Dim _frmMain As frmMain = TryCast(Me.MdiParent, frmMain)
        'Dim _emailAddress As String = GetEmail(_employeeCode.Trim)

        'If _isApproved = True Then 'approved
        '    If Not String.IsNullOrEmpty(_emailAddress.Trim) Then
        '        If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
        '            _frmMain.SendEmailRequestors(False, _emailAddress, "You vacation leave dated " & _startDate.Date.ToString("MMMM dd, yyyy") & " is approved." _
        '                               & Environment.NewLine & Environment.NewLine & "This is a system-generated email. Please do not reply. Thank you.")
        '        Else
        '            _frmMain.SendEmailRequestors(False, _emailAddress, "You vacation leave dated from " & _startDate.Date.ToString("MMMM dd, yyyy") & _
        '                               " to " & _endDate.Date.ToString("MMMM dd, yyyy") & " is approved." & Environment.NewLine & _
        '                               Environment.NewLine & "This is a system-generated email. Please do not reply. Thank you.")
        '        End If
        '    Else
        '        _frmMain.SendEmailRequestors(True, _emailAddress, "No email found - " & Environment.NewLine & "Leave File Id : " & _leaveFileId _
        '                         & Environment.NewLine & "Employee Number: " & _employeeCode & Environment.NewLine _
        '                         & "Employee Name: " & _employeeName & Environment.NewLine & "Leave Status: APPROVED")
        '    End If

        'ElseIf _isApproved = False Then 'disapproved
        '    If Not String.IsNullOrEmpty(_emailAddress.Trim) Then
        '        If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
        '            _frmMain.SendEmailRequestors(False, _emailAddress, "You vacation leave dated " & _startDate.Date.ToString("MMMM dd, yyyy") & " is disapproved." _
        '                               & Environment.NewLine & Environment.NewLine & "This is a system-generated email. Please do not reply. Thank you.")
        '        Else
        '            _frmMain.SendEmailRequestors(False, _emailAddress, "You vacation leave dated from " & _startDate.Date.ToString("MMMM dd, yyyy") & _
        '                               " to " & _endDate.Date.ToString("MMMM dd, yyyy") & " is disapproved." & Environment.NewLine & _
        '                               Environment.NewLine & "This is a system-generated email. Please do not reply. Thank you.")
        '        End If
        '    Else
        '        _frmMain.SendEmailRequestors(True, _emailAddress, "No email found - " & Environment.NewLine & "Leave File Id : " & _leaveFileId _
        '                         & Environment.NewLine & "Employee Number: " & _employeeCode & Environment.NewLine _
        '                         & "Employee Name: " & _employeeName & Environment.NewLine & "Leave Status: DISAPPROVED")
        '    End If
        'End If
    End Sub

#End Region

End Class