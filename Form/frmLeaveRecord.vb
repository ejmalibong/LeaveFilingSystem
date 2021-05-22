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
    'control access
    Private isSuperior As Boolean = False
    Private isManager As Boolean = False
    'list
    Private superiorLevel As New List(Of Integer) From {19, 4, 3, 6, 17, 25, 18, 7} 'sv, asv, sr engr, sr staff, sr line leader, sr nurse, sr technician, line leader
    Private managerLevel As New List(Of Integer) From {15, 21, 2, 13} 'dgm, sr mngr, mngr, asst mngr
    'search criteria
    Private dictionary As New Dictionary(Of String, Integer)
    'flag filters
    Private isFilterByLeaveType As Boolean = False
    Private isFilterByDateCreated As Boolean = False
    Private isFilterByEmployeeName As Boolean = False
    Private isFilterBySection As Boolean = False
    Private isFilterByReason As Boolean = False
    Private isFilterByAbsentStartDate As Boolean = False
    Private isFilterByAbsentEndDate As Boolean = False

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

        SearchCriteria()

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
                _leaveTypeName = dbLeaveFiling.ExecuteScalar("SELECT TRIM(LeaveTypeName) AS LeaveTypeName FROM dbo.LeaveType WHERE LeaveTypeId = @LeaveTypeId", CommandType.Text, _prmLeaveType)

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
                _leaveTypeName = dbLeaveFiling.ExecuteScalar("SELECT TRIM(LeaveTypeName) AS LeaveTypeName FROM dbo.LeaveType WHERE LeaveTypeId = @LeaveTypeId", CommandType.Text, _prmLeaveType)

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

    Private Sub cmbSearchCriteria_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSearchCriteria.SelectedValueChanged
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                pnlLeaveType.Visible = True
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = False
                pnlSection.Visible = False
                pnlReason.Visible = False
                pnlAbsentStartDate.Visible = False
                pnlAbsentEndDate.Visible = False
                Me.ActiveControl = cmbLeaveType
            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = True
                pnlEmployeeName.Visible = False
                pnlSection.Visible = False
                pnlReason.Visible = False
                pnlAbsentStartDate.Visible = False
                pnlAbsentEndDate.Visible = False
                Me.ActiveControl = dtpDateCreatedFrom
            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = True
                pnlSection.Visible = False
                pnlReason.Visible = False
                pnlAbsentStartDate.Visible = False
                pnlAbsentEndDate.Visible = False
                Me.ActiveControl = txtEmployeeName
            ElseIf cmbSearchCriteria.SelectedValue = 4 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = False
                pnlSection.Visible = True
                pnlReason.Visible = False
                pnlAbsentStartDate.Visible = False
                pnlAbsentEndDate.Visible = False
                Me.ActiveControl = cmbSection
            ElseIf cmbSearchCriteria.SelectedValue = 5 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = False
                pnlSection.Visible = False
                pnlReason.Visible = True
                pnlAbsentStartDate.Visible = False
                pnlAbsentEndDate.Visible = False
                Me.ActiveControl = txtReason
            ElseIf cmbSearchCriteria.SelectedValue = 6 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = False
                pnlSection.Visible = False
                pnlReason.Visible = False
                pnlAbsentStartDate.Visible = True
                pnlAbsentEndDate.Visible = False
                Me.ActiveControl = dtpAbsentStartFrom
            ElseIf cmbSearchCriteria.SelectedValue = 7 Then
                pnlLeaveType.Visible = False
                pnlDateCreated.Visible = False
                pnlEmployeeName.Visible = False
                pnlSection.Visible = False
                pnlReason.Visible = False
                pnlAbsentStartDate.Visible = False
                pnlAbsentEndDate.Visible = True
                Me.ActiveControl = dtpAbsentEndFrom
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If cmbSearchCriteria.SelectedValue = 1 Then
                isFilterByLeaveType = True
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                If dtpDateCreatedFrom.Value.Date > dtpDateCreatedTo.Value.Date Then
                    MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                isFilterByLeaveType = False
                isFilterByDateCreated = True
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = True
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

            ElseIf cmbSearchCriteria.SelectedValue = 4 Then
                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = True
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

            ElseIf cmbSearchCriteria.SelectedValue = 5 Then
                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = True
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

            ElseIf cmbSearchCriteria.SelectedValue = 6 Then
                If dtpAbsentStartFrom.Value.Date > dtpAbsentStartTo.Value.Date Then
                    MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = True
                isFilterByAbsentEndDate = False

            ElseIf cmbSearchCriteria.SelectedValue = 7 Then
                If dtpAbsentEndFrom.Value.Date > dtpAbsentEndTo.Value.Date Then
                    MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = True
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
                cmbLeaveType.SelectedValue = 0

                isFilterByLeaveType = True
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 2 Then
                If dtpDateCreatedFrom.Value.Date > dtpDateCreatedTo.Value.Date Then
                    MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

                dtpDateCreatedFrom.Value = Date.Now
                dtpDateCreatedTo.Value = Date.Now
                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 3 Then
                txtEmployeeName.Clear()

                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = True
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 4 Then
                cmbSection.SelectedValue = 0

                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = True
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 5 Then
                txtReason.Clear()

                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = True
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 6 Then
                If dtpAbsentStartFrom.Value.Date > dtpAbsentStartTo.Value.Date Then
                    MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = False

                dtpAbsentStartFrom.Value = Date.Now
                dtpAbsentStartTo.Value = Date.Now
                pageIndex = 0
                BindPage()

            ElseIf cmbSearchCriteria.SelectedValue = 7 Then
                If dtpAbsentEndFrom.Value.Date > dtpAbsentEndTo.Value.Date Then
                    MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                isFilterByLeaveType = False
                isFilterByDateCreated = False
                isFilterByEmployeeName = False
                isFilterBySection = False
                isFilterByReason = False
                isFilterByAbsentStartDate = False
                isFilterByAbsentEndDate = True

                dtpAbsentEndFrom.Value = Date.Now
                dtpAbsentEndTo.Value = Date.Now
                pageIndex = 0
                BindPage()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Sub"
    Private Sub BindPage()
        Try
            totalCount = 0

            If isFilterByLeaveType = True Then
                If rdMyFile.Checked = True Then
                    If cmbLeaveType.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillByEmployeeIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByEmployeeIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, cmbLeaveType.SelectedValue)
                    End If
                ElseIf rdPending.Checked = True Then
                    If cmbLeaveType.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillPendingByEmployeeIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillPendingByEmployeeIdLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, cmbLeaveType.SelectedValue)
                    End If
                ElseIf rdApproved.Checked = True Then
                    If cmbLeaveType.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillApprovedByLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillApprovedByLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, cmbLeaveType.SelectedValue)
                    End If
                ElseIf rdDisapproved.Checked = True Then
                    If cmbLeaveType.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillDisapprovedByLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillDisapprovedByLeaveTypeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, cmbLeaveType.SelectedValue)
                    End If
                End If

            ElseIf isFilterByDateCreated = True Then
                If rdMyFile.Checked = True Then
                    Me.adpLeaveFiling.FillByEmployeeIdDateCreated(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpDateCreatedFrom.Value.Date, dtpDateCreatedTo.Value.Date)
                ElseIf rdPending.Checked = True Then
                    Me.adpLeaveFiling.FillPendingByDateCreated(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpDateCreatedFrom.Value.Date, dtpDateCreatedTo.Value.Date)
                ElseIf rdApproved.Checked = True Then
                    Me.adpLeaveFiling.FillApprovedByDateCreated(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpDateCreatedFrom.Value.Date, dtpDateCreatedTo.Value.Date)
                ElseIf rdDisapproved.Checked = True Then
                    Me.adpLeaveFiling.FillDisapprovedByDateCreated(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpDateCreatedFrom.Value.Date, dtpDateCreatedTo.Value.Date)
                End If

            ElseIf isFilterByEmployeeName = True Then
                If rdMyFile.Checked = True Then
                    If String.IsNullOrEmpty(txtEmployeeName.Text.Trim) Then
                        Me.adpLeaveFiling.FillByEmployeeIdEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByEmployeeIdEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, txtEmployeeName.Text.Trim)
                    End If
                ElseIf rdPending.Checked = True Then
                    If String.IsNullOrEmpty(txtEmployeeName.Text.Trim) Then
                        Me.adpLeaveFiling.FillPendingByEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillPendingByEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, txtEmployeeName.Text.Trim)
                    End If
                ElseIf rdApproved.Checked = True Then
                    If String.IsNullOrEmpty(txtEmployeeName.Text.Trim) Then
                        Me.adpLeaveFiling.FillApprovedByEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillApprovedByEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, txtEmployeeName.Text.Trim)
                    End If
                ElseIf rdDisapproved.Checked = True Then
                    If String.IsNullOrEmpty(txtEmployeeName.Text.Trim) Then
                        Me.adpLeaveFiling.FillDisapprovedByEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillDisapprovedByEmployeeName(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, txtEmployeeName.Text.Trim)
                    End If
                End If

            ElseIf isFilterBySection = True Then
                If rdMyFile.Checked = True Then
                    If cmbSection.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillByEmployeeIdTeamId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByEmployeeIdTeamId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, cmbSection.SelectedValue)
                    End If
                ElseIf rdPending.Checked = True Then
                    If cmbSection.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillPendingByTeamId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillPendingByTeamId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, cmbSection.SelectedValue)
                    End If
                ElseIf rdApproved.Checked = True Then
                    If cmbSection.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillApprovedByTeamId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillApprovedByTeamId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, cmbSection.SelectedValue)
                    End If
                ElseIf rdDisapproved.Checked = True Then
                    If cmbSection.SelectedValue = 0 Then
                        Me.adpLeaveFiling.FillDisapprovedByTeamId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillDisapprovedByTeamId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, cmbSection.SelectedValue)
                    End If
                End If

            ElseIf isFilterByReason = True Then
                If rdMyFile.Checked = True Then
                    If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                        Me.adpLeaveFiling.FillByEmployeeIdReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillByEmployeeIdReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, txtReason.Text.Trim)
                    End If
                ElseIf rdPending.Checked = True Then
                    If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                        Me.adpLeaveFiling.FillPendingByReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillPendingByReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, txtReason.Text.Trim)
                    End If
                ElseIf rdApproved.Checked = True Then
                    If String.IsNullOrEmpty(txtReason.Text.Trim) = True Then
                        Me.adpLeaveFiling.FillApprovedByReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillApprovedByReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, txtReason.Text.Trim)
                    End If
                ElseIf rdDisapproved.Checked = True Then
                    If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                        Me.adpLeaveFiling.FillDisapprovedByReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, Nothing)
                    Else
                        Me.adpLeaveFiling.FillDisapprovedByReason(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, txtReason.Text.Trim)
                    End If
                End If

            ElseIf isFilterByAbsentStartDate = True Then
                If rdMyFile.Checked = True Then
                    Me.adpLeaveFiling.FillByEmployeeIdAbsentDateFrom(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpAbsentStartFrom.Value.Date, dtpAbsentStartTo.Value.Date)
                ElseIf rdPending.Checked = True Then
                    Me.adpLeaveFiling.FillPendingByAbsentDateFrom(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpAbsentStartFrom.Value.Date, dtpAbsentStartTo.Value.Date)
                ElseIf rdApproved.Checked = True Then
                    Me.adpLeaveFiling.FillApprovedByAbsentDateFrom(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpAbsentStartFrom.Value.Date, dtpAbsentStartTo.Value.Date)
                ElseIf rdDisapproved.Checked = True Then
                    Me.adpLeaveFiling.FillDisapprovedByAbsentDateFrom(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpAbsentStartFrom.Value.Date, dtpAbsentStartTo.Value.Date)
                End If

            ElseIf isFilterByAbsentEndDate = True Then
                If rdMyFile.Checked = True Then
                    Me.adpLeaveFiling.FillByEmployeeIdAbsentDateTo(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpAbsentEndFrom.Value.Date, dtpAbsentEndTo.Value.Date)
                ElseIf rdPending.Checked = True Then
                    Me.adpLeaveFiling.FillPendingByAbsentDateTo(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpAbsentEndFrom.Value.Date, dtpAbsentEndTo.Value.Date)
                ElseIf rdApproved.Checked = True Then
                    Me.adpLeaveFiling.FillApprovedByAbsentDateTo(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpAbsentEndFrom.Value.Date, dtpAbsentEndTo.Value.Date)
                ElseIf rdDisapproved.Checked = True Then
                    Me.adpLeaveFiling.FillDisapprovedByAbsentDateTo(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId, dtpAbsentEndFrom.Value.Date, dtpAbsentEndFrom.Value.Date)
                End If

            Else
                If rdMyFile.Checked = True Then
                    Me.adpLeaveFiling.FillByEmployeeId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId)
                ElseIf rdPending.Checked = True Then
                    Me.adpLeaveFiling.FillPending(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId)
                ElseIf rdApproved.Checked = True Then
                    Me.adpLeaveFiling.FillApproved(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId)
                ElseIf rdDisapproved.Checked = True Then
                    Me.adpLeaveFiling.FillDisapproved(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, employeeId)
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

    Private Sub SearchCriteria()
        dictionary.Add(" Leave Type", 1)
        dictionary.Add(" Date Created", 2)
        dictionary.Add(" Employee Name", 3)
        dictionary.Add(" Section", 4)
        dictionary.Add(" Reason", 5)
        dictionary.Add(" Start Date of Absent", 6)
        dictionary.Add(" End Date of Absent", 7)
        cmbSearchCriteria.DisplayMember = "Key"
        cmbSearchCriteria.ValueMember = "Value"
        cmbSearchCriteria.DataSource = New BindingSource(dictionary, Nothing)

        dbLeaveFiling.FillCmbWithCaption("RdLeaveType", CommandType.StoredProcedure, "LeaveTypeId", "LeaveTypeName", cmbLeaveType, "< All > ")

        Dim _prmDeptId(0) As SqlParameter
        _prmDeptId(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
        _prmDeptId(0).Value = departmentId

        dbLeaveFiling.FillCmbWithCaption("RdTeam", CommandType.StoredProcedure, "TeamId", "TeamName", cmbSection, "< All > ", _prmDeptId)
    End Sub
#End Region

End Class