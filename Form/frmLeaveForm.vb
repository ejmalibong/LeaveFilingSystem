Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsJeonsoftTableAdapters

Public Class frmLeaveForm
    Private connection As New clsConnection
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main
    'server datetime
    Private serverDate As DateTime = dbLeaveFiling.GetServerDate
    'dataset
    Private dsLeaveFiling As New dsLeaveFiling
    Private dsJeonsoft As New dsJeonsoft
    'adapters
    Private adpPositions As New tblPositionsTableAdapter
    Private adpTeams As New tblTeamsTableAdapter
    Private adpRoutingStatus As New RoutingStatusTableAdapter
    Private adpLeaveFiling As New LeaveFilingTableAdapter
    Private adpScreening As New ScreeningTableAdapter
    'datatables
    Private dtRoutingStatus As New RoutingStatusDataTable
    Private dtLeaveFiling As New LeaveFilingDataTable
    'datarows
    Private rowClinicPositions As tblPositionsRow
    Private rowSuperiorPositions As tblPositionsRow
    Private rowManagerPositions As tblPositionsRow
    Private rowRoutingStatus As RoutingStatusRow
    Private rowScreening As ScreeningRow
    'bs
    Private bsLeaveFiling As New BindingSource
    'flags
    Private isDgm As Boolean = False
    Private isManager As Boolean = False
    Private isSuperior As Boolean = False
    Private isHr As Boolean = False
    Private isClinic As Boolean = False
    'custom bindings
    Private WithEvents dateCreated As Binding
    'constructors
    Private employeeId As Integer = 0
    Private positionId As Integer = 0
    Private departmentId As Integer = 0
    Private teamId As Integer = 0
    Private employmentTypeId As Integer = 0
    Private emailAddress As String = String.Empty
    Private mobileNumber As String = String.Empty
    Private nbcEmailAddress As String = String.Empty
    Private leaveFileId As Integer = 0
    Private screenId As Integer = 0
    'requestor's info
    Private requestorEmployeeId As Integer = 0
    Private requestorDepartmentId As Integer = 0
    Private requestorTeamId As Integer = 0
    'additional
    Private dictSuperior As New Dictionary(Of String, Integer)
    Private dictManager As New Dictionary(Of String, Integer)

    Public Sub New(ByVal _dataset As DataSet, ByVal _employeeId As Integer, ByVal _positionId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _employmentTypeId As Integer, Optional ByVal _leaveFileId As Integer = 0)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        positionId = _positionId
        departmentId = _departmentId
        teamId = _teamId
        employmentTypeId = _employmentTypeId
        leaveFileId = _leaveFileId

        'check position and section
        Dim _managerIds As New List(Of Integer) From {15, 21, 2, 13, 19, 4} 'dgm, sr mngr, mngr, asst mngr, sv, asv
        Dim _superiorIds As New List(Of Integer) From {19, 4, 3, 6, 17, 7, 25} 'sv, asv, sr engr, sr staff, sr line leader, line leader, sr nurse
        Dim _hrIds As New List(Of Integer) From {1} 'hr

        If _managerIds.Contains(positionId) Then
            isManager = True
            If _superiorIds.Contains(positionId) Then
                isSuperior = True
            End If
        ElseIf _superiorIds.Contains(positionId) Then
            isSuperior = True
        End If

        If positionId = 15 Then
            isDgm = True
        End If

        If _hrIds.Contains(teamId) Then
            isHr = True
        End If

        Me.adpPositions.Fill(Me.dsJeonsoft.tblPositions)
        Me.adpTeams.Fill(Me.dsJeonsoft.tblTeams)

        FillSuperiorStatus()
        FillManagerStatus()

        dbLeaveFiling.FillCmbWithCaption("RdLeaveType", CommandType.StoredProcedure, "Id", "LeaveTypeName", cmbLeaveType, "< Select Leave Type >")
        dbLeaveFiling.FillCmbWithCaption("RdClinic", CommandType.StoredProcedure, "Id", "Name", cmbClinicName, "< Select Clinic Personnel >")

        'new filing
        If leaveFileId = 0 Then
            Me.dsLeaveFiling = _dataset
            Me.dsLeaveFiling.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            Me.adpRoutingStatus.Fill(Me.dsLeaveFiling.RoutingStatus)

            ResetForm()

            txtFileId.Text = "(new)"
            txtFileId.Visible = False
            lblFileId.Visible = False
            txtDateCreated.Text = String.Format("{0:MMMM dd, yyyy HH:mm}", serverDate)
            rowRoutingStatus = Me.dsLeaveFiling.RoutingStatus.FindByRoutingStatusId(5)
            txtRoutingStatus.Text = rowRoutingStatus.RoutingStatusName.ToString.Trim
            GetEmployeeInfo(employeeId)

            'If Not teamId = 0 AndAlso (departmentId = 4 Or departmentId = 3) Then
            If Not teamId = 0 Then
                If departmentId = 2 Then 'warehouse planning
                    Dim _prmSup(0) As SqlParameter
                    _prmSup(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                    _prmSup(0).Value = departmentId
                    dbLeaveFiling.FillCmbWithCaption("RdSuperior", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName, "< Select Immediate Superior >", _prmSup)
                Else
                    Dim _prmSup(1) As SqlParameter
                    _prmSup(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                    _prmSup(0).Value = departmentId
                    _prmSup(1) = New SqlParameter("TeamId", SqlDbType.Int)
                    _prmSup(1).Value = teamId
                    dbLeaveFiling.FillCmbWithCaption("RdSuperior", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName, "< Select Immediate Superior >", _prmSup)
                End If
            Else
                Dim _prmSup(0) As SqlParameter
                _prmSup(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _prmSup(0).Value = departmentId
                dbLeaveFiling.FillCmbWithCaption("RdSuperior", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName, "< Select Immediate Superior >", _prmSup)
            End If

            If isManager = True Then
                If isSuperior = True Then
                    Dim _prmMgr(0) As SqlParameter
                    _prmMgr(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                    _prmMgr(0).Value = departmentId
                    dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >", _prmMgr)
                Else
                    dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >")
                End If
            Else
                Dim _prmMgr(0) As SqlParameter
                _prmMgr(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _prmMgr(0).Value = departmentId
                dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >", _prmMgr)
            End If

            Me.ActiveControl = cmbLeaveType

            'existing file
        Else
            Me.adpLeaveFiling.FillByLeaveFileId(Me.dsLeaveFiling.LeaveFiling, leaveFileId)
            Me.bsLeaveFiling.DataSource = Me.dsLeaveFiling
            Me.bsLeaveFiling.DataMember = dtLeaveFiling.TableName
            Me.bsLeaveFiling.Position = Me.bsLeaveFiling.Find("LeaveFileId", leaveFileId)
            requestorEmployeeId = CType(Me.bsLeaveFiling.Current, DataRowView).Item("EmployeeId")
            GetEmployeeInfo(requestorEmployeeId)

            Me.adpRoutingStatus.Fill(Me.dsLeaveFiling.RoutingStatus)
            rowRoutingStatus = Me.dsLeaveFiling.RoutingStatus.FindByRoutingStatusId(CType(Me.bsLeaveFiling.Current, DataRowView).Item("RoutingStatusId"))
            txtRoutingStatus.Text = rowRoutingStatus.RoutingStatusName.ToString.Trim

            txtFileId.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "LeaveFileId", False))
            txtFileId.Visible = True
            lblFileId.Visible = True
            dateCreated = New Binding("Text", Me.bsLeaveFiling.Current, "DateCreated")
            txtDateCreated.DataBindings.Add(dateCreated)

            cmbLeaveType.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "LeaveTypeId"))
            dtpFrom.DataBindings.Add(New Binding("Value", Me.bsLeaveFiling.Current, "StartDate", False))
            dtpTo.DataBindings.Add(New Binding("Value", Me.bsLeaveFiling.Current, "EndDate", False))
            txtTotalLeaveCredits.Text = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveCredits")
            txtBalance.Text = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveBalance")
            txtNumberOfDays.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "Quantity"))
            txtReason.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "Reason"))

            If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ScreenId") Is DBNull.Value Then
                screenId = CType(Me.bsLeaveFiling.Current, DataRowView).Item("ScreenId")
                Me.adpScreening.FillByScreenId(Me.dsLeaveFiling.Screening, screenId)
                rowScreening = Me.dsLeaveFiling.Screening.FindByScreenId(screenId)

                Dim _prmEmployeeCode(0) As SqlParameter
                _prmEmployeeCode(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
                _prmEmployeeCode(0).Value = rowScreening.ScreenBy.ToString
                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinic", CommandType.StoredProcedure, _prmEmployeeCode)

                While _reader.Read
                    cmbClinicName.SelectedValue = _reader.Item("Id")
                    txtClinicPosition.Text = _reader.Item("PositionName").ToString.Trim
                End While
                _reader.Close()

                txtClinicStatus.Text = "Fit To Work"
                txtClinicDateApproved.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ClinicApprovalDate"))
                txtClinicRemarks.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ClinicRemarks"))
            End If

            If Not requestorTeamId = 0 Then
                If requestorDepartmentId = 2 Then
                    Dim _prmSup(0) As SqlParameter
                    _prmSup(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                    _prmSup(0).Value = requestorDepartmentId
                    dbLeaveFiling.FillCmbWithCaption("RdSuperior", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName, "< Select Immediate Superior >", _prmSup)
                Else
                    Dim _prmSup(1) As SqlParameter
                    _prmSup(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                    _prmSup(0).Value = requestorDepartmentId
                    _prmSup(1) = New SqlParameter("TeamId", SqlDbType.Int)
                    _prmSup(1).Value = requestorTeamId
                    dbLeaveFiling.FillCmbWithCaption("RdSuperior", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName, "< Select Immediate Superior >", _prmSup)
                End If
            Else
                Dim _prmSup(0) As SqlParameter
                _prmSup(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _prmSup(0).Value = requestorDepartmentId
                dbLeaveFiling.FillCmbWithCaption("RdSuperior", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName, "< Select Immediate Superior >", _prmSup)
            End If

            'If Not requestorTeamId = 0 AndAlso (requestorDepartmentId = 4 Or requestorDepartmentId = 3) Then
            '    Dim _prmSup(1) As SqlParameter
            '    _prmSup(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
            '    _prmSup(0).Value = requestorDepartmentId
            '    _prmSup(1) = New SqlParameter("TeamId", SqlDbType.Int)
            '    _prmSup(1).Value = requestorTeamId
            '    dbLeaveFiling.FillCmbWithCaption("RdSuperior", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName, "< Select Immediate Superior >", _prmSup)
            'Else
            '    Dim _prmSup(0) As SqlParameter
            '    _prmSup(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
            '    _prmSup(0).Value = requestorDepartmentId
            '    dbLeaveFiling.FillCmbWithCaption("RdSuperior", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName, "< Select Immediate Superior >", _prmSup)
            'End If

            If isManager = True AndAlso isSuperior = True Then
                Dim _prmMgr(0) As SqlParameter
                _prmMgr(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _prmMgr(0).Value = requestorDepartmentId
                dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >", _prmMgr)
            ElseIf isManager = True Then
                If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId").ToString = employeeId Then
                    If isDgm = True Then
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >")
                    Else
                        Dim _prmMgr(0) As SqlParameter
                        _prmMgr(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prmMgr(0).Value = requestorDepartmentId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >", _prmMgr)
                    End If
                Else
                    If isDgm = True Then
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >")
                    Else
                        Dim _prmMgr(0) As SqlParameter
                        _prmMgr(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prmMgr(0).Value = requestorDepartmentId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >", _prmMgr)
                    End If
                    'dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >")
                End If
            Else
                Dim _prmMgr(0) As SqlParameter
                _prmMgr(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _prmMgr(0).Value = requestorDepartmentId
                dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "< Select Manager/Last Approver >", _prmMgr)
            End If

            cmbManagerName.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "ManagerId"))
            txtManagerDateApproved.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ManagerApprovalDate", False))
            Dim _managerPosId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId")
            Dim _managerPositionId As Integer = dbJeonsoft.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & _managerPosId & "", CommandType.Text)
            rowManagerPositions = Me.dsJeonsoft.tblPositions.FindById(_managerPositionId)

            txtManagerPosition.Text = rowManagerPositions.Name.ToString.Trim
            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerIsApproved") = True Then
                cmbManagerStatus.SelectedValue = 1
            ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerIsApproved") = False And CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value Then
                cmbManagerStatus.SelectedValue = 0
            Else
                cmbManagerStatus.SelectedValue = 2
            End If
            txtManagerRemarks.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ManagerRemarks"))

            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId") Is DBNull.Value Then
                cmbSuperiorName.SelectedValue = 0
                cmbSuperiorStatus.SelectedValue = 0
            Else
                cmbSuperiorName.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "SuperiorId"))
                txtSuperiorDateApproved.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "SuperiorApprovalDate", False))
                Dim _superiorPosId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId")
                Dim _superiorPositionId As Integer = dbJeonsoft.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & _superiorPosId & "", CommandType.Text)
                rowSuperiorPositions = Me.dsJeonsoft.tblPositions.FindById(_superiorPositionId)
                txtSuperiorPosition.Text = rowSuperiorPositions.Name.ToString.Trim

                If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorIsApproved") = True Then
                    cmbSuperiorStatus.SelectedValue = 1
                ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorIsApproved") = False AndAlso CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate") Is DBNull.Value Then
                    cmbSuperiorStatus.SelectedValue = 0
                Else
                    cmbSuperiorStatus.SelectedValue = 2
                End If
            End If
            txtSuperiorRemarks.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "SuperiorRemarks"))

            If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate") Is DBNull.Value Or Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value Then
                cmbLeaveType.Enabled = False
                dtpFrom.Enabled = False
                dtpTo.Enabled = False
                txtReason.ReadOnly = True
                cmbSuperiorName.Enabled = False
                cmbManagerName.Enabled = False
            End If

            If Not requestorEmployeeId = employeeId Then
                cmbLeaveType.Enabled = False
                dtpFrom.Enabled = False
                dtpTo.Enabled = False
                txtReason.ReadOnly = True
            End If

            If isManager = True AndAlso isSuperior = True Then
                If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId") Is DBNull.Value Then
                    If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId") = employeeId Then
                        If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId") = employeeId Then
                            cmbSuperiorStatus.Enabled = False
                            cmbSuperiorName.Enabled = False
                            txtSuperiorRemarks.ReadOnly = True

                            cmbManagerStatus.Enabled = True
                            cmbManagerName.Enabled = True
                            txtManagerRemarks.ReadOnly = False
                            Me.ActiveControl = txtManagerRemarks
                            txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)

                        Else
                            cmbManagerStatus.Enabled = False
                            cmbManagerName.Enabled = False
                            txtManagerRemarks.ReadOnly = True

                            cmbSuperiorStatus.Enabled = True
                            cmbSuperiorName.Enabled = True
                            txtSuperiorRemarks.ReadOnly = False
                            Me.ActiveControl = txtSuperiorRemarks
                            txtSuperiorRemarks.Select(txtSuperiorRemarks.Text.Trim.Length, 0)

                        End If
                    Else
                        cmbSuperiorStatus.Enabled = False
                        cmbSuperiorName.Enabled = False
                        txtSuperiorRemarks.ReadOnly = True
                    End If
                Else
                    cmbSuperiorStatus.Enabled = False
                    cmbSuperiorName.Enabled = False
                    txtSuperiorRemarks.ReadOnly = True

                    If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId") = employeeId Then
                        cmbManagerStatus.Enabled = True
                        cmbManagerName.Enabled = True
                        txtManagerRemarks.ReadOnly = False
                        Me.ActiveControl = txtManagerRemarks
                        txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)
                    Else
                        cmbManagerStatus.Enabled = False
                        cmbManagerName.Enabled = False
                        txtManagerRemarks.ReadOnly = True
                    End If
                End If

            ElseIf isManager = True Then
                cmbSuperiorStatus.Enabled = False
                cmbSuperiorName.Enabled = False
                txtSuperiorRemarks.ReadOnly = True

                If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId") = employeeId Then
                    cmbManagerStatus.Enabled = True
                    cmbManagerName.Enabled = True
                    txtManagerRemarks.ReadOnly = False
                    Me.ActiveControl = txtManagerRemarks
                    txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)
                Else
                    cmbManagerStatus.Enabled = False
                    cmbManagerName.Enabled = False
                    txtManagerRemarks.ReadOnly = True
                End If

            ElseIf isSuperior = True Then
                cmbManagerStatus.Enabled = False
                cmbManagerName.Enabled = False
                txtManagerRemarks.ReadOnly = True

                If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId") Is DBNull.Value Then
                    If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId") = employeeId Then
                        cmbSuperiorStatus.Enabled = True
                        cmbSuperiorName.Enabled = True
                        txtSuperiorRemarks.ReadOnly = False
                        txtSuperiorRemarks.Select()
                        txtSuperiorRemarks.Select(txtSuperiorRemarks.Text.Trim.Length, 0)
                    Else
                        cmbSuperiorStatus.Enabled = False
                        cmbSuperiorName.Enabled = False
                        txtSuperiorRemarks.ReadOnly = True
                    End If
                Else
                    cmbSuperiorStatus.Enabled = False
                    cmbSuperiorName.Enabled = False
                    txtSuperiorRemarks.ReadOnly = True
                End If
            Else
                cmbSuperiorStatus.Enabled = False
                cmbSuperiorName.Enabled = False
                txtSuperiorRemarks.ReadOnly = True

                cmbManagerStatus.Enabled = False
                cmbManagerName.Enabled = False
                txtManagerRemarks.ReadOnly = True
            End If
        End If
    End Sub

    Private Sub frmLeaveFiling_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmLeaveFiling_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode.Equals(Keys.Enter) Then
            e.Handled = True
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        ElseIf e.KeyCode.Equals(Keys.F8) Then
            e.Handled = True
            btnDelete.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F10) Then
            e.Handled = True
            btnSave.PerformClick()
        End If
    End Sub

    Private Sub cmbLeaveType_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbLeaveType.SelectionChangeCommitted
        If cmbLeaveType.SelectedValue = 1 Then
            Dim _prmScreen(1) As SqlParameter
            _prmScreen(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmScreen(0).Value = employeeId
            _prmScreen(1) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
            _prmScreen(1).Value = 1
            screenId = dbLeaveFiling.ExecuteScalar("SELECT TOP 1 (ScreenId) FROM Screening WHERE EmployeeId = @EmployeeId AND LeaveTypeId = @LeaveTypeId AND IsUsed = 0 AND IsFitToWork = 1 ORDER BY ScreenId DESC", CommandType.Text, _prmScreen)

            If screenId > 0 Then
                dtpFrom.Enabled = False
                dtpTo.Enabled = False
                txtReason.ReadOnly = True
                cmbSuperiorName.Enabled = True
                cmbManagerName.Enabled = True

                Me.adpScreening.FillByScreenId(Me.dsLeaveFiling.Screening, screenId)
                rowScreening = Me.dsLeaveFiling.Screening.FindByScreenId(screenId)
                txtClinicStatus.Text = "Fit To Work"

                Dim _prmEmpCode(0) As SqlParameter
                _prmEmpCode(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
                _prmEmpCode(0).Value = rowScreening.ScreenBy.ToString
                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinic", CommandType.StoredProcedure, _prmEmpCode)

                While _reader.Read
                    cmbClinicName.SelectedValue = _reader.Item("Id")
                    txtClinicPosition.Text = _reader.Item("PositionName").ToString.Trim
                End While
                _reader.Close()

                dtpFrom.Value = rowScreening.AbsentFrom.Date
                dtpTo.Value = rowScreening.AbsentTo.Date
                txtReason.Text = rowScreening.Reason.ToString.Trim
                txtNumberOfDays.Text = rowScreening.Quantity.ToString
                txtClinicDateApproved.Text = rowScreening.ScreenDate.ToString("MMMM dd, yyyy HH:mm")
                txtClinicRemarks.Text = rowScreening.Diagnosis.Trim
           
                GetTotalLeaveCredits(employeeId)
                GetLeaveBalance(employeeId)
            Else
                ResetForm()
                MessageBox.Show("No health screening record found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbLeaveType.Focus()
                Return
            End If

        ElseIf cmbLeaveType.SelectedValue = 2 Then
            Dim _prmScreen(1) As SqlParameter
            _prmScreen(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmScreen(0).Value = employeeId
            _prmScreen(1) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
            _prmScreen(1).Value = 2
            screenId = dbLeaveFiling.ExecuteScalar("SELECT TOP 1 (ScreenId) FROM Screening WHERE EmployeeId = @EmployeeId AND LeaveTypeId = @LeaveTypeId AND IsUsed = 0 AND IsFitToWork = 1 ORDER BY ScreenId DESC", CommandType.Text, _prmScreen)

            If screenId > 0 Then
                dtpFrom.Enabled = False
                dtpTo.Enabled = False
                txtReason.ReadOnly = True
                cmbSuperiorName.Enabled = True
                cmbManagerName.Enabled = True

                Me.adpScreening.FillByScreenId(Me.dsLeaveFiling.Screening, screenId)
                rowScreening = Me.dsLeaveFiling.Screening.FindByScreenId(screenId)
                txtClinicStatus.Text = "Fit To Work"

                Dim _prmEmployeeCode(0) As SqlParameter
                _prmEmployeeCode(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
                _prmEmployeeCode(0).Value = rowScreening.ScreenBy.ToString
                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinic", CommandType.StoredProcedure, _prmEmployeeCode)

                While _reader.Read
                    cmbClinicName.SelectedValue = _reader.Item("Id")
                    txtClinicPosition.Text = _reader.Item("PositionName").ToString.Trim
                End While
                _reader.Close()

                dtpFrom.Value = rowScreening.AbsentFrom.Date
                dtpTo.Value = rowScreening.AbsentTo.Date
                txtReason.Text = rowScreening.Reason.ToString.Trim
                txtNumberOfDays.Text = rowScreening.Quantity.ToString
                txtClinicDateApproved.Text = rowScreening.ScreenDate.ToString("MMMM dd, yyyy HH:mm")
                txtClinicRemarks.Text = rowScreening.Diagnosis.Trim

                GetTotalLeaveCredits(employeeId)
                GetLeaveBalance(employeeId)
            Else
                dtpFrom.Enabled = True
                dtpTo.Enabled = True
                dtpFrom.Value = Date.Now.Date
                dtpTo.Value = Date.Now.Date
                txtReason.Clear()
                txtReason.ReadOnly = False

                txtClinicStatus.Text = String.Empty
                cmbClinicName.SelectedValue = 0
                txtClinicPosition.Text = String.Empty
                txtClinicDateApproved.Text = String.Empty
                txtClinicRemarks.Text = String.Empty
            End If

            If Not cmbLeaveType.SelectedValue = 0 Then
                GetTotalLeaveCredits(employeeId)
                GetLeaveBalance(employeeId)
                txtNumberOfDays.Text = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
            Else
                txtTotalLeaveCredits.Text = String.Empty
                txtBalance.Text = String.Empty
                txtNumberOfDays.Text = String.Empty
            End If

        ElseIf cmbLeaveType.SelectedValue = 3 Then
            dtpFrom.Enabled = True
            dtpTo.Enabled = True
            dtpFrom.Value = Date.Now.Date
            dtpTo.Value = Date.Now.Date
            txtReason.Clear()
            txtReason.ReadOnly = False

            txtClinicStatus.Text = String.Empty
            cmbClinicName.SelectedValue = 0
            txtClinicPosition.Text = String.Empty
            txtClinicDateApproved.Text = String.Empty
            txtClinicRemarks.Text = String.Empty

            dtpTo.Enabled = False
        Else
            ResetForm()
        End If

        If isManager = True AndAlso (positionId = 2 Or positionId = 21) Then
            cmbSuperiorName.Enabled = False
        Else
            cmbSuperiorName.Enabled = True
        End If
        cmbManagerName.Enabled = True
    End Sub

    Private Sub dtpFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged
        txtNumberOfDays.Text = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
    End Sub

    Private Sub dtpFrom_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpFrom.Validating
        'If cmbLeaveType.SelectedValue = 2 Then
        '    If dtpFrom.Value.Date < Date.Now.Date And screenId = 0 Then
        '        MessageBox.Show("Cannot select previous date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        dtpFrom.Value = Date.Now.Date
        '        e.Cancel = True
        '    End If

        '    If dtpFrom.Value.Date < Date.Now.Date.AddDays(3) AndAlso leaveFileId = 0 Then
        '        MessageBox.Show("Leave must be files at least three (3) days before going on leave.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        dtpFrom.Focus()
        '        Return
        '    End If
        'End If
    End Sub

    Private Sub dtpTo_ValueChanged(sender As Object, e As EventArgs) Handles dtpTo.ValueChanged
        txtNumberOfDays.Text = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
    End Sub

    Private Sub dtpTo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpTo.Validating
        'If cmbLeaveType.SelectedValue = 2 Then
        '    If dtpTo.Value.Date < Date.Now.Date And screenId = 0 Then
        '        MessageBox.Show("Cannot select previous date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        dtpTo.Value = Date.Now.Date
        '        e.Cancel = True
        '    End If
        'End If
    End Sub

    Private Sub cmbSuperiorName_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSuperiorName.SelectedValueChanged
        If Not cmbSuperiorName.SelectedValue = 0 Then
            Dim _positionId As Integer = dbJeonsoft.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & cmbSuperiorName.SelectedValue & "", CommandType.Text)
            rowSuperiorPositions = Me.dsJeonsoft.tblPositions.FindById(_positionId)
            txtSuperiorPosition.Text = rowSuperiorPositions.Name.ToString.Trim
        Else
            txtSuperiorPosition.Text = String.Empty
        End If
    End Sub

    Private Sub cmbManagerName_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbManagerName.SelectedValueChanged
        If Not cmbManagerName.SelectedValue = 0 Then
            Dim _positionId As Integer = dbJeonsoft.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & cmbManagerName.SelectedValue & "", CommandType.Text)
            rowManagerPositions = Me.dsJeonsoft.tblPositions.FindById(_positionId)
            txtManagerPosition.Text = rowManagerPositions.Name.ToString.Trim
        Else
            txtManagerPosition.Text = String.Empty
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim _frmMain As frmMain = TryCast(Me.Owner, frmMain)
            Dim _bDay As Date

            If cmbLeaveType.SelectedValue = 0 Then
                MessageBox.Show("Please select leave type.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbLeaveType.Focus()
                Return
                'Else
                '    If leaveFileId = 0 And cmbLeaveType.SelectedValue = 1 Then
                '        If screenId = 0 Then
                '            MessageBox.Show("No health screening record found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '            Return
                '        End If
                '    End If
            End If

            If leaveFileId = 0 AndAlso cmbLeaveType.SelectedValue = 2 AndAlso cmbClinicName.SelectedValue = 0 Then
                If dtpFrom.Value.Date < Date.Now.Date.AddDays(3) Then
                    MessageBox.Show("Leave must be files at least three (3) days before going on leave.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    dtpFrom.Focus()
                    Return
                End If
            End If

            If leaveFileId = 0 AndAlso cmbLeaveType.SelectedValue = 3 Then
                Dim _prmBday(0) As SqlParameter
                _prmBday(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                _prmBday(0).Value = employeeId

                Dim _reader As IDataReader = dbJeonsoft.ExecuteReader("SELECT CAST(BirthDate AS DATE) AS BirthDate FROM dbo.tblEmployees WHERE Id = @EmployeeId", CommandType.Text, _prmBday)

                While _reader.Read
                    If Not _reader.Item("BirthDate").ToString Is DBNull.Value Then
                        _bDay = _reader.Item("BirthDate").ToString
                    Else
                        _bDay = Date.Now.Date
                    End If
                End While
                _reader.Close()
               
                If Not _bDay.Date.Date.Month.Equals(dtpFrom.Value.Date.Month) Then
                    MessageBox.Show("Birthday leave must be within your birth month.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    dtpFrom.Focus()
                    Return
                End If
            End If

            If dtpFrom.Value.Date > dtpTo.Value.Date Then
                MessageBox.Show("Start date is later than end date.", "Invalid date range", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFrom.Focus()
                Return
            End If

            If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                MessageBox.Show("Reason cannot be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtReason.Focus()
                Return
            End If

            If cmbManagerName.SelectedValue = 0 Then
                MessageBox.Show("Please select manager or last approver.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbManagerName.Focus()
                Return
            End If

            Me.Validate()

            'new transaction
            If leaveFileId = 0 Then
                Dim _newRowLeave As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.NewLeaveFilingRow

                With _newRowLeave
                    .DateCreated = DateTime.Now

                    If cmbLeaveType.SelectedValue = 1 Then
                        .LeaveTypeId = 1

                        Dim _prmScreenId(0) As SqlParameter
                        _prmScreenId(0) = New SqlParameter("@ScreenId", SqlDbType.Int)
                        _prmScreenId(0).Value = screenId
                        dbLeaveFiling.ExecuteNonQuery("UPDATE Screening SET IsUsed = 1 WHERE ScreenId = @ScreenId", CommandType.Text, _prmScreenId)
                        .ScreenId = screenId
                        .ClinicIsApproved = 1
                        .ClinicId = cmbClinicName.SelectedValue
                        .ClinicApprovalDate = rowScreening.ScreenDate
                        .ClinicRemarks = rowScreening.Diagnosis.ToString.Trim
                        .IsLateFiling = 1

                    ElseIf cmbLeaveType.SelectedValue = 2 Then
                        .LeaveTypeId = 2

                        If screenId > 0 Then
                            Dim _prmScreenId(0) As SqlParameter
                            _prmScreenId(0) = New SqlParameter("@ScreenId", SqlDbType.Int)
                            _prmScreenId(0).Value = screenId
                            dbLeaveFiling.ExecuteNonQuery("UPDATE Screening SET IsUsed = 1 WHERE ScreenId = @ScreenId", CommandType.Text, _prmScreenId)
                            .ScreenId = screenId
                            .ClinicIsApproved = 1
                            .ClinicId = cmbClinicName.SelectedValue
                            .ClinicApprovalDate = rowScreening.ScreenDate
                            .ClinicRemarks = rowScreening.Diagnosis.ToString.Trim
                            .IsLateFiling = 1
                        Else
                            .SetScreenIdNull()
                            .ClinicIsApproved = 0
                            .SetClinicIdNull()
                            .SetClinicApprovalDateNull()
                            .SetClinicRemarksNull()
                            .IsLateFiling = 0
                        End If

                    ElseIf cmbLeaveType.SelectedValue = 3 Then
                        .LeaveTypeId = 3

                        .SetScreenIdNull()
                        .ClinicIsApproved = 0
                        .SetClinicIdNull()
                        .SetClinicApprovalDateNull()
                        .SetClinicRemarksNull()
                        .IsLateFiling = 0
                    End If

                    If cmbSuperiorName.SelectedValue = 0 Then 'no immediate superior
                        .SetSuperiorIdNull()
                        .RoutingStatusId = 3

                        _frmMain.SendEmailApprovers(cmbManagerName.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)

                    Else 'with immediate superior
                        .SuperiorId = cmbSuperiorName.SelectedValue
                        .RoutingStatusId = 4

                        _frmMain.SendEmailApprovers(cmbSuperiorName.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)
                    End If

                    If teamId = 0 Then
                        .SetTeamIdNull()
                    Else
                        .TeamId = teamId
                    End If

                    .SuperiorIsApproved = 0
                    .SetSuperiorApprovalDateNull()
                    .SetSuperiorRemarksNull()

                    .ManagerIsApproved = 0
                    .ManagerId = cmbManagerName.SelectedValue
                    .SetManagerApprovalDateNull()
                    .SetManagerRemarksNull()

                    .EmployeeId = employeeId
                    .DepartmentId = departmentId
                    .StartDate = dtpFrom.Value.Date
                    .EndDate = dtpTo.Value.Date
                    .Quantity = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
                    .Reason = txtReason.Text.Trim

                    If String.IsNullOrEmpty(txtTotalLeaveCredits.Text.Trim) Then
                        .LeaveCredits = 0
                    Else
                        .LeaveCredits = txtTotalLeaveCredits.Text.Trim()
                    End If

                    If String.IsNullOrEmpty(txtBalance.Text.Trim) Then
                        .LeaveBalance = 0
                    Else
                        .LeaveBalance = txtBalance.Text.Trim
                    End If

                    .ModifiedBy = employeeId
                    .ModifiedDate = DateTime.Now
                End With

                Me.dsLeaveFiling.LeaveFiling.AddLeaveFilingRow(_newRowLeave)
                Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                Me.dsLeaveFiling.AcceptChanges()
            Else
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(leaveFileId)

                With _leaveFilingRow
                    If isManager = True AndAlso isSuperior = True Then
                        If .IsSuperiorIdNull = True AndAlso .ManagerId = employeeId Then
                            If cmbManagerStatus.SelectedValue = 0 Then
                                MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                txtManagerRemarks.Focus()
                                txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)
                                Return
                            End If

                            If cmbManagerStatus.SelectedValue = 1 Then
                                .ManagerIsApproved = 1
                                .RoutingStatusId = 2

                                _frmMain.SendEmailRequestor(True, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                'If cmbLeaveType.SelectedValue = 2 Then
                                '    SendEmail(True, txtIdNumber.Text.Trim, dtpFrom.Value.Date, dtpTo.Value.Date)
                                'End If
                            ElseIf cmbManagerStatus.SelectedValue = 2 Then
                                .ManagerIsApproved = 0
                                .RoutingStatusId = 6

                                _frmMain.SendEmailRequestor(False, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                'If cmbLeaveType.SelectedValue = 2 Then
                                '    SendEmail(False, txtIdNumber.Text.Trim, dtpFrom.Value.Date, dtpTo.Value.Date)
                                'End If
                            End If
                            .ManagerApprovalDate = DateTime.Now

                            If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) Then
                                .SetManagerRemarksNull()
                            Else
                                .ManagerRemarks = txtManagerRemarks.Text.Trim
                            End If

                        ElseIf .SuperiorId = employeeId AndAlso .ManagerId = employeeId Then
                            If cmbManagerStatus.SelectedValue = 0 Then
                                MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                txtManagerRemarks.Focus()
                                txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)
                                Return
                            End If

                            If cmbManagerStatus.SelectedValue = 1 Then
                                .ManagerIsApproved = 1
                                .RoutingStatusId = 2
                                .SuperiorIsApproved = 1

                                _frmMain.SendEmailRequestor(True, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                'If cmbLeaveType.SelectedValue = 2 Then
                                '    SendEmail(True, txtIdNumber.Text.Trim, dtpFrom.Value.Date, dtpTo.Value.Date)
                                'End If
                            ElseIf cmbManagerStatus.SelectedValue = 2 Then
                                .ManagerIsApproved = 0
                                .RoutingStatusId = 6
                                .SuperiorIsApproved = 0

                                _frmMain.SendEmailRequestor(False, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                'If cmbLeaveType.SelectedValue = 2 Then
                                '    SendEmail(False, txtIdNumber.Text.Trim, dtpFrom.Value.Date, dtpTo.Value.Date)
                                'End If
                            End If
                            .ManagerApprovalDate = DateTime.Now
                            .SuperiorApprovalDate = DateTime.Now

                            If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) Then
                                .SetManagerRemarksNull()
                            Else
                                .ManagerRemarks = txtManagerRemarks.Text.Trim
                            End If
                            .SetSuperiorRemarksNull()

                        ElseIf .SuperiorId = employeeId Then
                            If cmbSuperiorStatus.SelectedValue = 0 Then
                                MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                txtSuperiorRemarks.Focus()
                                txtSuperiorRemarks.Select(txtSuperiorRemarks.Text.Trim.Length, 0)
                                Return
                            End If

                            If cmbSuperiorStatus.SelectedValue = 1 Then
                                .SuperiorIsApproved = 1
                                .RoutingStatusId = 3

                                _frmMain.SendEmailApprovers(cmbManagerName.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)

                            ElseIf cmbSuperiorStatus.SelectedValue = 2 Then
                                .SuperiorIsApproved = 0
                                .RoutingStatusId = 6
                            End If
                            .SuperiorApprovalDate = DateTime.Now

                            If String.IsNullOrEmpty(txtSuperiorRemarks.Text.Trim) Then
                                .SetSuperiorRemarksNull()
                            Else
                                .SuperiorRemarks = txtSuperiorRemarks.Text.Trim
                            End If

                        ElseIf .ManagerId = employeeId Then
                            If cmbManagerStatus.SelectedValue = 0 Then
                                MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                txtManagerRemarks.Focus()
                                txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)
                                Return
                            End If

                            If cmbManagerStatus.SelectedValue = 1 Then
                                .ManagerIsApproved = 1
                                .RoutingStatusId = 2

                                _frmMain.SendEmailRequestor(True, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                'If cmbLeaveType.SelectedValue = 2 Then
                                '    SendEmail(True, txtIdNumber.Text.Trim, dtpFrom.Value.Date, dtpTo.Value.Date)
                                'End If
                            ElseIf cmbManagerStatus.SelectedValue = 2 Then
                                .ManagerIsApproved = 0
                                .RoutingStatusId = 6

                                _frmMain.SendEmailRequestor(False, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                'If cmbLeaveType.SelectedValue = 2 Then
                                '    SendEmail(False, txtIdNumber.Text.Trim, dtpFrom.Value.Date, dtpTo.Value.Date)
                                'End If
                            End If
                            .ManagerApprovalDate = DateTime.Now

                            If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) Then
                                .SetManagerRemarksNull()
                            Else
                                .ManagerRemarks = txtManagerRemarks.Text.Trim
                            End If
                        End If

                    ElseIf isManager = True Then
                        If .ManagerId = employeeId Then
                            If cmbManagerStatus.SelectedValue = 0 Then
                                MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                txtManagerRemarks.Focus()
                                txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)
                                Return
                            End If

                            If cmbManagerStatus.SelectedValue = 1 Then
                                .ManagerIsApproved = 1
                                .RoutingStatusId = 2

                                _frmMain.SendEmailRequestor(True, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                'If cmbLeaveType.SelectedValue = 2 Then
                                '    SendEmail(True, txtIdNumber.Text.Trim, dtpFrom.Value.Date, dtpTo.Value.Date)
                                'End If
                            ElseIf cmbManagerStatus.SelectedValue = 2 Then
                                .ManagerIsApproved = 0
                                .RoutingStatusId = 6

                                _frmMain.SendEmailRequestor(False, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                'If cmbLeaveType.SelectedValue = 2 Then
                                '    SendEmail(False, txtIdNumber.Text.Trim, dtpFrom.Value.Date, dtpTo.Value.Date)
                                'End If
                            End If
                            .ManagerApprovalDate = DateTime.Now

                            If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) Then
                                .SetManagerRemarksNull()
                            Else
                                .ManagerRemarks = txtManagerRemarks.Text.Trim
                            End If
                        End If

                    ElseIf isSuperior = True Then
                        If Not .IsSuperiorIdNull = True And .SuperiorId = employeeId Then
                            If cmbSuperiorStatus.SelectedValue = 0 Then
                                MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                txtSuperiorRemarks.Focus()
                                txtSuperiorRemarks.Select(txtSuperiorRemarks.Text.Trim.Length, 0)
                                Return
                            End If

                            If cmbSuperiorStatus.SelectedValue = 1 Then
                                .SuperiorIsApproved = 1
                                .RoutingStatusId = 3

                                _frmMain.SendEmailApprovers(cmbManagerName.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)
                            ElseIf cmbSuperiorStatus.SelectedValue = 2 Then
                                .SuperiorIsApproved = 0
                                .RoutingStatusId = 6
                            End If
                            .SuperiorApprovalDate = DateTime.Now

                            If String.IsNullOrEmpty(txtSuperiorRemarks.Text.Trim) Then
                                .SetSuperiorRemarksNull()
                            Else
                                .SuperiorRemarks = txtSuperiorRemarks.Text.Trim
                            End If
                        End If

                    Else
                        If .EmployeeId = employeeId Then
                            .Reason = txtReason.Text.Trim
                            .ModifiedDate = DateTime.Now
                        End If
                    End If
                End With

                Me.bsLeaveFiling.EndEdit()
                Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                Me.dsLeaveFiling.AcceptChanges()
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If employeeId = requestorEmployeeId And leaveFileId <> 0 And (Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate") Is DBNull.Value Or Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value) Then
            MessageBox.Show("Leave is already approved.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            If MessageBox.Show("Delete this record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Me.bsLeaveFiling.RemoveCurrent()
                Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                Me.dsLeaveFiling.AcceptChanges()
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dateCreated_Format(sender As Object, e As ConvertEventArgs) Handles dateCreated.Format
        If Not e.Value Is DBNull.Value Then
            e.Value = Format(e.Value, "MMMM dd, yyyy  HH:mm")
        Else
            e.Value = DateTime.Now.ToString("MMMM dd, yyyy  HH:mm")
        End If
    End Sub

#Region "Sub"
    Private Sub GetEmployeeInfo(ByVal _employeeId As Integer)
        Try
            Dim _prmEmpId(0) As SqlParameter
            _prmEmpId(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmEmpId(0).Value = _employeeId

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdEmployee", CommandType.StoredProcedure, _prmEmpId)

            While _reader.Read
                txtIdNumber.Text = _reader.Item("EmployeeCode").ToString.Trim
                txtName.Text = _reader.Item("EmployeeName").ToString.Trim

                If Not _reader.Item("TeamName") Is DBNull.Value Then
                    If _reader.Item("DepartmentName").ToString.Trim.Equals(_reader.Item("TeamName").ToString.Trim) Then
                        txtDepartment.Text = _reader.Item("DepartmentName").ToString.Trim
                    Else
                        txtDepartment.Text = _reader.Item("DepartmentName").ToString.Trim & " - " & _reader.Item("TeamName").ToString.Trim
                    End If
                Else
                    txtDepartment.Text = _reader.Item("DepartmentName").ToString.Trim
                End If

                txtPosition.Text = _reader.Item("PositionName").ToString.Trim
                txtEmpStatus.Text = _reader.Item("EmploymentTypeName").ToString.Trim
                txtDateHired.Text = CDate(_reader.Item("DateHired")).ToString("MMMM dd, yyyy")

                If Not _reader.Item("EmailAddress") Is DBNull.Value Then
                    emailAddress = _reader.Item("EmailAddress")
                Else
                    emailAddress = String.Empty
                End If

                If Not _reader.Item("MobileNo") Is DBNull.Value Then
                    mobileNumber = _reader.Item("MobileNo")
                Else
                    mobileNumber = 0
                End If

                If Not _reader.Item("NbcEmailAddress") Is DBNull.Value Then
                    nbcEmailAddress = _reader.Item("NbcEmailAddress")
                Else
                    nbcEmailAddress = String.Empty
                End If

                'exising file
                If leaveFileId <> 0 Then
                    If Not _reader.Item("TeamName") Is DBNull.Value Then
                        requestorTeamId = _reader.Item("TeamId").ToString
                    End If
                    requestorDepartmentId = _reader.Item("DepartmentId").ToString
                End If
            End While
            _reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetTotalLeaveCredits(ByVal _empId As Integer)
        Try
            Dim _prmCredits(1) As SqlParameter
            _prmCredits(0) = New SqlParameter("@EmployeeId", _empId)
            _prmCredits(0).Value = _empId
            _prmCredits(1) = New SqlParameter("@LeaveTypeId", _empId)
            _prmCredits(1).Value = cmbLeaveType.SelectedValue
            txtTotalLeaveCredits.Text = dbJeonsoft.ExecuteScalar("SELECT Quantity FROM dbo.tblEmployeeLeaves WHERE EmployeeId = @EmployeeId AND LeaveTypeId = @LeaveTypeId", CommandType.Text, _prmCredits)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetLeaveBalance(ByVal _empId As Integer)
        Try
            Dim _leaveBalance As Double
            Dim _paramsLeaveBalance(3) As SqlParameter
            _paramsLeaveBalance(0) = New SqlParameter("@CompanyId", SqlDbType.Int)
            _paramsLeaveBalance(0).Value = 1
            _paramsLeaveBalance(1) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _paramsLeaveBalance(1).Value = _empId
            _paramsLeaveBalance(2) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
            _paramsLeaveBalance(2).Value = cmbLeaveType.SelectedValue
            _paramsLeaveBalance(3) = New SqlParameter("@Date", SqlDbType.Date)
            _paramsLeaveBalance(3).Value = DBNull.Value
            _leaveBalance = dbJeonsoft.ExecuteFunction(Of Double)("dbo.fnGetLeaveBalance", _paramsLeaveBalance)
            txtBalance.Text = _leaveBalance
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetTotalDays(ByVal _startDate As Date, ByVal _endDate As Date) As Integer
        Dim _count As Integer = 0

        Try
            If _startDate.Date.Equals(_endDate.Date) Then
                _count = 1
            Else
                For _i As Integer = 0 To (_endDate - _startDate).Days
                    If Not IsHoliday(_startDate) Then
                        If Not IsWeekend(_startDate) Then
                            _count += 1
                        End If
                    End If
                    _startDate = _startDate.AddDays(1)
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _count
    End Function

    Private Function IsWeekend(ByVal _date As Date) As Boolean
        If _date.DayOfWeek.Equals(DayOfWeek.Sunday) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsHoliday(ByVal _date As Date) As Boolean
        Dim _count As Integer

        Try
            Dim _paramHoliday(0) As SqlParameter
            _paramHoliday(0) = New SqlParameter("@HolidayDate", SqlDbType.Date)
            _paramHoliday(0).Value = _date.ToShortDateString
            _count = 0
            _count = dbLeaveFiling.ExecuteScalar("SELECT COUNT(HolidayId) FROM Holiday WHERE HolidayDate = @HolidayDate", CommandType.Text, _paramHoliday)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        If _count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ResetForm()
        cmbLeaveType.SelectedValue = 0
        dtpFrom.Enabled = False
        dtpFrom.Value = Date.Now.Date
        dtpTo.Enabled = False
        dtpTo.Value = Date.Now.Date
        txtReason.Text = String.Empty
        txtReason.ReadOnly = True

        txtTotalLeaveCredits.Text = String.Empty
        txtBalance.Text = String.Empty
        txtNumberOfDays.Text = String.Empty

        txtClinicStatus.Text = String.Empty
        txtClinicPosition.Text = String.Empty
        txtClinicDateApproved.Text = String.Empty
        txtClinicRemarks.Text = String.Empty

        cmbSuperiorStatus.Enabled = False
        cmbSuperiorStatus.SelectedValue = 0
        cmbSuperiorName.Enabled = False
        cmbSuperiorName.SelectedValue = 0
        txtSuperiorRemarks.Text = String.Empty
        txtSuperiorRemarks.ReadOnly = True

        cmbManagerStatus.Enabled = False
        cmbManagerStatus.SelectedValue = 0
        cmbManagerName.Enabled = False
        cmbManagerName.SelectedValue = 0
        txtManagerRemarks.Text = String.Empty
        txtManagerRemarks.ReadOnly = True
    End Sub

    Private Sub FillSuperiorStatus()
        dictSuperior.Add(" < Select Status > ", 0)
        dictSuperior.Add("Approve", 1)
        dictSuperior.Add("Disapprove", 2)
        cmbSuperiorStatus.DisplayMember = "Key"
        cmbSuperiorStatus.ValueMember = "Value"
        cmbSuperiorStatus.DataSource = New BindingSource(dictSuperior, Nothing)
    End Sub

    Private Sub FillManagerStatus()
        dictManager.Add(" < Select Status > ", 0)
        dictManager.Add("Approve", 1)
        dictManager.Add("Disapprove", 2)
        cmbManagerStatus.DisplayMember = "Key"
        cmbManagerStatus.ValueMember = "Value"
        cmbManagerStatus.DataSource = New BindingSource(dictManager, Nothing)
    End Sub

    'Private Function GetEmail(ByVal _employeeCode As String) As String
    '    Dim _emailAddress As String = String.Empty

    '    Try
    '        Dim _prmEmployeeCode(0) As SqlParameter
    '        _prmEmployeeCode(0) = New SqlParameter("@EmployeeCode", SqlDbType.NVarChar)
    '        _prmEmployeeCode(0).Value = _employeeCode
    '        _emailAddress = dbJeonsoft.ExecuteScalar("SELECT EmailAddress FROM dbo.tblEmployees WHERE EmployeeCode = @EmployeeCode", CommandType.Text, _prmEmployeeCode)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    '    Return _emailAddress
    'End Function

    'Private Sub SendEmail(ByVal _isApproved As Boolean, ByVal _employeeCode As String, ByVal _startDate As Date, ByVal _endDate As Date)
    '    Dim _frmMain As frmMain = TryCast(Me.Owner, frmMain)
    '    Dim _emailAddress As String = GetEmail(_employeeCode.Trim)

    '    If _isApproved = True Then 'approved
    '        If Not String.IsNullOrEmpty(_emailAddress.Trim) Then
    '            If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
    '                _frmMain.SendEmailRequestors(False, _emailAddress, "You vacation leave dated " & _startDate.Date.ToString("MMMM dd, yyyy") & " is approved." _
    '                                   & Environment.NewLine & Environment.NewLine & "This is a system-generated email. Please do not reply. Thank you.")
    '            Else
    '                _frmMain.SendEmailRequestors(False, _emailAddress, "You vacation leave dated from " & _startDate.Date.ToString("MMMM dd, yyyy") & _
    '                                   " to " & _endDate.Date.ToString("MMMM dd, yyyy") & " is approved." & Environment.NewLine & _
    '                                   Environment.NewLine & "This is a system-generated email. Please do not reply. Thank you.")
    '            End If
    '        Else
    '            _frmMain.SendEmailRequestors(True, _emailAddress, "No email found - " & Environment.NewLine & "Leave File Id : " & txtFileId.Text.Trim _
    '                             & Environment.NewLine & "Employee Number: " & txtIdNumber.Text.Trim & Environment.NewLine _
    '                             & "Employee Name: " & txtName.Text.Trim & Environment.NewLine & "Leave Status: APPROVED")
    '        End If

    '    ElseIf _isApproved = False Then 'disapproved
    '        If Not String.IsNullOrEmpty(_emailAddress.Trim) Then
    '            If _startDate.Date.Date.Equals(_endDate.Date.Date) Then
    '                _frmMain.SendEmailRequestors(False, _emailAddress, "You vacation leave dated " & _startDate.Date.ToString("MMMM dd, yyyy") & " is disapproved." _
    '                                   & Environment.NewLine & Environment.NewLine & "This is a system-generated email. Please do not reply. Thank you.")
    '            Else
    '                _frmMain.SendEmailRequestors(False, _emailAddress, "You vacation leave dated from " & _startDate.Date.ToString("MMMM dd, yyyy") & _
    '                                   " to " & _endDate.Date.ToString("MMMM dd, yyyy") & " is disapproved." & Environment.NewLine & _
    '                                   Environment.NewLine & "This is a system-generated email. Please do not reply. Thank you.")
    '            End If
    '        Else
    '            _frmMain.SendEmailRequestors(True, _emailAddress, "No email found - " & Environment.NewLine & "Leave File Id : " & txtFileId.Text.Trim _
    '                             & Environment.NewLine & "Employee Number: " & txtIdNumber.Text.Trim & Environment.NewLine _
    '                             & "Employee Name: " & txtName.Text.Trim & Environment.NewLine & "Leave Status: DISAPPROVED")
    '        End If
    '    End If
    'End Sub
#End Region

End Class