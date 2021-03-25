Imports System.Reflection
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsJeonsoftTableAdapters

Public Class frmLeaveFiling
    Private connection As New clsConnection
    Private dbMethodServer As New SqlDbMethod(connection.JeonsoftConnection)
    Private dbMethodLocal As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main

    Private employeeId As Integer = 0
    Private positionId As Integer = 0
    Private departmentId As Integer = 0
    Private teamId As Integer = 0
    Private employmentTypeId As Integer = 0
    Private leaveFilingId As Integer = 0

    Private dsLeaveFiling As New dsLeaveFiling
    Private dsJeonsoft As New dsJeonsoft

    Private adpPositions As New tblPositionsTableAdapter
    Private adpTeams As New tblTeamsTableAdapter
    Private adpRoutingStatus As New RoutingStatusTableAdapter
    Private adpLeaveFiling As New LeaveFilingTableAdapter

    Private bsLeaveFiling As New BindingSource

    Private dtRoutingStatus As New RoutingStatusDataTable
    Private dtLeaveFiling As New LeaveFilingDataTable

    Private rowRoutingStatus As RoutingStatusRow
    Private rowClinicPositions As tblPositionsRow
    Private rowSuperiorPositions As tblPositionsRow
    Private rowManagerPositions As tblPositionsRow

    Private leaveBalance As Double
    Private isClinic As Boolean = False
    Private isSuperior As Boolean = False
    Private isManager As Boolean = False

    Private screenId As Integer = 0

    Private origDepartmentId As Integer = 0
    Private origTeamId As Integer = 0

    Private WithEvents datetimeBinding As Binding

    Public Sub New(ByVal _dataset As DataSet, ByVal _employeeId As Integer, ByVal _positionId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _employmentTypeId As Integer, Optional ByVal _leaveFilingId As Integer = 0)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        positionId = _positionId
        departmentId = _departmentId
        teamId = _teamId
        employmentTypeId = _employmentTypeId
        leaveFilingId = _leaveFilingId

        Me.dsLeaveFiling = _dataset
        Me.dsLeaveFiling.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema

        Me.adpRoutingStatus.Fill(Me.dsLeaveFiling.RoutingStatus)
        Me.adpPositions.Fill(Me.dsJeonsoft.tblPositions)
        Me.adpTeams.Fill(Me.dsJeonsoft.tblTeams)

        'fill leave type
        dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM dbo.tblLeaveTypes WHERE Id IN (1,2)", "Id", "Name", cmbLeaveType, "< Select leave type >")

        'fill clinic
        Dim _paramClinic(0) As SqlParameter
        _paramClinic(0) = New SqlParameter("@TeamId", SqlDbType.Int)
        _paramClinic(0).Value = 3
        dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM tblEmployees WHERE TeamId = @TeamId AND Active = 1", "Id", "Name", cmbClinicName, "< Select Clinic Personnel >", _paramClinic)

        'new filing
        If leaveFilingId = 0 Then
            txtFileId.Text = "(new)"
            txtDateCreated.Text = DateTime.Now.ToString("MMMM dd, yyyy  HH:mm")
            rowRoutingStatus = Me.dsLeaveFiling.RoutingStatus.FindByRoutingStatusId(6)
            txtRoutingStatus.Text = rowRoutingStatus.RoutingStatusName.ToString.Trim

            dtpFrom.Enabled = False
            dtpTo.Enabled = False
            txtReason.ReadOnly = True

            cmbClinicName.Enabled = False
            cmbClinicName.Enabled = False
            txtClinicRemarks.ReadOnly = True

            cmbSuperiorStatus.Enabled = False
            cmbSuperiorName.Enabled = False
            txtSuperiorRemarks.ReadOnly = True

            cmbManagerStatus.Enabled = False
            cmbManagerName.Enabled = False
            txtManagerRemarks.ReadOnly = True

            GetEmployeeInfo(employeeId)

            'fill superiors
            'with section
            If Not teamId = 0 Then
                Dim _paramSuperior(1) As SqlParameter
                _paramSuperior(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _paramSuperior(0).Value = departmentId
                _paramSuperior(1) = New SqlParameter("@TeamId", SqlDbType.Int)
                _paramSuperior(1).Value = teamId
                dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM tblEmployees WHERE DepartmentId = @DepartmentId AND PositionId IN (13,19,4,17,7,3,6) AND Active = 1 AND TeamId = @TeamId", "Id", "Name", cmbSuperiorName, "< None >", _paramSuperior)
                'no section
            Else
                Dim _paramSuperior(0) As SqlParameter
                _paramSuperior(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _paramSuperior(0).Value = departmentId
                dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM tblEmployees WHERE DepartmentId = @DepartmentId AND PositionId IN (13,19,4,17,7,3,6) AND Active = 1", "Id", "Name", cmbSuperiorName, "< None >")
            End If

            'fill managers
            Dim _paramManager(0) As SqlParameter
            _paramManager(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
            _paramManager(0).Value = departmentId
            dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM tblEmployees WHERE DepartmentId = @DepartmentId AND PositionId IN (2, 21, 19, 4) AND Active = 1", "Id", "Name", cmbManagerName, "< Select Manager >", _paramManager)

            txtNumberOfDays.Text = 1
            Me.ActiveControl = cmbLeaveType

            'existing file
        Else
            Me.adpLeaveFiling.FillByLeaveFilingId(Me.dsLeaveFiling.LeaveFiling, leaveFilingId)
            Me.bsLeaveFiling.DataSource = Me.dsLeaveFiling
            Me.bsLeaveFiling.DataMember = dtLeaveFiling.TableName
            Me.bsLeaveFiling.Position = Me.bsLeaveFiling.Find("LeaveFilingId", leaveFilingId)
            Dim _empId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("EmployeeId")

            Dim _paramEmpId1(0) As SqlParameter
            _paramEmpId1(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _paramEmpId1(0).Value = _empId
            Dim _reader As IDataReader = dbMethodServer.ExecuteReader("SELECT DepartmentId, TeamId FROM dbo.tblEmployees WHERE Id = @EmployeeId", CommandType.Text, _paramEmpId1)
            While _reader.Read
                origDepartmentId = _reader.Item("DepartmentId").ToString
                origTeamId = _reader.Item("TeamId").ToString
            End While
            _reader.Close()

            'fill superiors
            'with section
            If Not teamId = 0 Then
                Dim _paramSuperior(1) As SqlParameter
                _paramSuperior(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _paramSuperior(0).Value = origDepartmentId
                _paramSuperior(1) = New SqlParameter("@TeamId", SqlDbType.Int)
                _paramSuperior(1).Value = origTeamId
                dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM tblEmployees WHERE DepartmentId = @DepartmentId AND PositionId IN (13,19,4,17,7,3,6) AND Active = 1 AND TeamId = @TeamId", "Id", "Name", cmbSuperiorName, "< None >", _paramSuperior)

                'no section
            Else
                Dim _paramSuperior(0) As SqlParameter
                _paramSuperior(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _paramSuperior(0).Value = origDepartmentId
                dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM tblEmployees WHERE DepartmentId = @DepartmentId AND PositionId IN (13,19,4,17,7,3,6) AND Active = 1", "Id", "Name", cmbSuperiorName, "< None >")
            End If

            'fill managers
            Dim _paramManager(0) As SqlParameter
            _paramManager(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
            _paramManager(0).Value = origDepartmentId
            dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM tblEmployees WHERE DepartmentId = @DepartmentId AND PositionId IN (2, 21, 19, 4) AND Active = 1", "Id", "Name", cmbManagerName, "< Select Manager >", _paramManager)

            Dim _routingStatusId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("RoutingStatusId")
            rowRoutingStatus = Me.dsLeaveFiling.RoutingStatus.FindByRoutingStatusId(_routingStatusId)
            txtRoutingStatus.Text = rowRoutingStatus.RoutingStatusName.ToString.Trim

            txtFileId.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "LeaveFilingId", False))
            datetimeBinding = New Binding("Text", Me.bsLeaveFiling.Current, "DateCreated")
            txtDateCreated.DataBindings.Add(datetimeBinding)

            Dim _paramEmpId2(0) As SqlParameter
            _paramEmpId2(0) = New SqlParameter("@EmployeeId", SqlDbType.VarChar)
            _paramEmpId2(0).Value = _empId
            GetEmployeeInfo(_empId)
            GetTotalLeaveCredits(_empId)
            GetLeaveBalance(_empId)

            cmbLeaveType.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "LeaveTypeId"))
            dtpFrom.DataBindings.Add(New Binding("Value", Me.bsLeaveFiling.Current, "StartDate", False))
            dtpTo.DataBindings.Add(New Binding("Value", Me.bsLeaveFiling.Current, "EndDate", False))
            txtNumberOfDays.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "Quantity"))
            txtReason.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "Reason"))

            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ClinicId") Is DBNull.Value Then
                cmbClinicName.SelectedValue = 0
            Else
                cmbClinicName.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "ClinicId"))
                txtClinicDateApproved.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ClinicApprovalDate", False))
            End If
            txtClinicRemarks.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ClinicRemarks"))

            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId") Is DBNull.Value Then
                cmbSuperiorName.SelectedValue = 0
            Else
                cmbSuperiorName.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "SuperiorId"))
                txtSuperiorDateApproved.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "SuperiorApprovalDate", False))

                Dim _superiorPosId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId")
                Dim _superiorPositionId As Integer = dbMethodServer.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & _superiorPosId & "", CommandType.Text)
                rowSuperiorPositions = Me.dsJeonsoft.tblPositions.FindById(_superiorPositionId)
                txtSuperiorPosition.Text = rowSuperiorPositions.Name.ToString.Trim
            End If
            txtSuperiorRemarks.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "SuperiorRemarks"))

            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId") Is DBNull.Value Then
                cmbManagerName.SelectedValue = 0
            Else
                cmbManagerName.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "ManagerId"))
                txtManagerDateApproved.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ManagerApprovalDate", False))

                Dim _managerPosId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId")
                Dim _managerPositionId As Integer = dbMethodServer.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & _managerPosId & "", CommandType.Text)
                rowManagerPositions = Me.dsJeonsoft.tblPositions.FindById(_managerPositionId)
                txtManagerPosition.Text = rowManagerPositions.Name.ToString.Trim
            End If
            txtManagerRemarks.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ManagerRemarks"))

            'clinic
            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ClinicApprovalDate") Is DBNull.Value Then
                cmbClinicStatus.SelectedIndex = 0
            Else
                If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ClinicIsApproved") = True Then
                    cmbClinicStatus.SelectedIndex = 1
                Else
                    cmbClinicStatus.SelectedIndex = 2
                End If
            End If

            'superior
            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate") Is DBNull.Value Then
                cmbSuperiorStatus.SelectedIndex = 0
            Else
                If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorIsApproved") = True Then
                    cmbSuperiorStatus.SelectedIndex = 1
                Else
                    cmbSuperiorStatus.SelectedIndex = 2
                End If
            End If

            'manager
            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value Then
                cmbManagerStatus.SelectedIndex = 0
            Else
                If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerIsApproved") = True Then
                    cmbManagerStatus.SelectedIndex = 1
                Else
                    cmbManagerStatus.SelectedIndex = 2
                End If
            End If

            cmbLeaveType.Enabled = False
            dtpFrom.Enabled = False
            dtpTo.Enabled = False
            If employeeId.Equals(_empId) Then
                txtReason.ReadOnly = False
            Else
                txtReason.ReadOnly = True
            End If

            Dim _superiorIds As New List(Of Integer) From {13, 19, 4, 17, 7, 3, 6}
            Dim _managerIds As New List(Of Integer) From {2, 21, 19, 4}
            Dim _clinicIds As New List(Of Integer) From {3}

            If _superiorIds.Contains(positionId) Then
                isSuperior = True

                cmbClinicStatus.Enabled = False
                cmbClinicName.Enabled = False
                txtClinicRemarks.ReadOnly = True

                cmbSuperiorName.Enabled = True
                txtSuperiorRemarks.ReadOnly = False

                cmbManagerStatus.Enabled = False
                cmbManagerName.Enabled = False
                txtManagerRemarks.ReadOnly = True

                Me.ActiveControl = txtSuperiorRemarks
            ElseIf _managerIds.Contains(positionId) Then
                isManager = True

                cmbClinicStatus.Enabled = False
                cmbClinicName.Enabled = False
                txtClinicRemarks.ReadOnly = True

                cmbSuperiorStatus.Enabled = False
                cmbSuperiorName.Enabled = False
                txtSuperiorRemarks.ReadOnly = True

                cmbManagerName.Enabled = True
                txtManagerRemarks.ReadOnly = False

                Me.ActiveControl = txtManagerRemarks
            Else
                If _clinicIds.Contains(teamId) Then
                    isClinic = True

                    cmbClinicName.Enabled = True
                    txtClinicRemarks.ReadOnly = False

                    cmbSuperiorStatus.Enabled = False
                    cmbSuperiorName.Enabled = False
                    txtSuperiorRemarks.ReadOnly = True

                    cmbManagerStatus.Enabled = False
                    cmbManagerName.Enabled = False
                    txtManagerRemarks.ReadOnly = True

                    cmbClinicName.SelectedValue = employeeId

                    Me.ActiveControl = txtClinicRemarks
                Else
                    cmbClinicStatus.Enabled = False
                    cmbClinicName.Enabled = False
                    txtClinicRemarks.ReadOnly = True

                    cmbSuperiorStatus.Enabled = False
                    cmbSuperiorName.Enabled = False
                    txtSuperiorRemarks.ReadOnly = True

                    cmbManagerStatus.Enabled = False
                    cmbManagerName.Enabled = False
                    txtManagerRemarks.ReadOnly = True

                    Me.ActiveControl = txtReason
                End If
            End If
        End If
    End Sub

    Private Sub datetimeBinding_Format(sender As Object, e As ConvertEventArgs) Handles datetimeBinding.Format
        If Not e.Value Is DBNull.Value Then
            e.Value = Format(e.Value, "MMMM dd, yyyy  HH:mm")
        Else
            e.Value = DateTime.Now.ToString("MMMM dd, yyyy  HH:mm")
        End If
    End Sub

    Private Sub frmLeaveFiling_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmLeaveFiling_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode.Equals(Keys.Enter) Then
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        ElseIf e.KeyCode.Equals(Keys.F8) Then
            btnDelete.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F10) Then
            btnSave.PerformClick()
        End If
    End Sub

    Private Sub cmbLeaveType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbLeaveType.SelectedValueChanged
        If leaveFilingId = 0 Then
            Dim _paramEmpId(0) As SqlParameter
            _paramEmpId(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _paramEmpId(0).Value = employeeId

            screenId = dbMethodLocal.ExecuteScalar("SELECT TOP 1 (ScreenId) FROM Screening WHERE EmployeeId = @EmployeeId AND IsEncoded = 0", CommandType.Text, _paramEmpId)

            If cmbLeaveType.SelectedValue = 1 Then
                If screenId > 0 Then
                    dtpFrom.Enabled = True
                    dtpTo.Enabled = True
                    txtReason.ReadOnly = False
                    cmbSuperiorName.Enabled = True
                    cmbManagerName.Enabled = True

                    GetTotalLeaveCredits(employeeId)
                    GetLeaveBalance(employeeId)
                Else
                    MessageBox.Show("No health screening record found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbLeaveType.Focus()
                    Return
                End If
            Else
                dtpFrom.Enabled = True
                dtpTo.Enabled = True
                txtReason.ReadOnly = False
                cmbSuperiorName.Enabled = True
                cmbManagerName.Enabled = True

                If Not cmbLeaveType.SelectedValue = 0 Then
                    GetTotalLeaveCredits(employeeId)
                    GetLeaveBalance(employeeId)
                Else
                    txtTotalLeaveCredits.Text = String.Empty
                    txtBalance.Text = String.Empty
                End If
            End If
        End If
    End Sub

    Private Sub dtpTo_ValueChanged(sender As Object, e As EventArgs) Handles dtpTo.ValueChanged
        GetTotalDays()
    End Sub

    Private Sub dtpFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged
        GetTotalDays()
    End Sub

    Private Sub cmbClinicName_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbClinicName.SelectedValueChanged
        If Not cmbClinicName.SelectedValue = 0 Then
            Dim _positionId As Integer = 0
            Dim _clinicId(0) As SqlParameter
            _clinicId(0) = New SqlParameter("@Id", SqlDbType.Int)
            _clinicId(0).Value = cmbClinicName.SelectedValue

            _positionId = dbMethodServer.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = @Id", CommandType.Text, _clinicId)
            rowClinicPositions = Me.dsJeonsoft.tblPositions.FindById(_positionId)
            txtClinicPosition.Text = rowClinicPositions.Name.ToString.Trim
        Else
            txtClinicPosition.Text = String.Empty
        End If
    End Sub

    Private Sub cmbSuperiorName_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSuperiorName.SelectedValueChanged
        If Not cmbSuperiorName.SelectedValue = 0 Then
            Dim _positionId As Integer = 0
            Dim _superiorId(0) As SqlParameter
            _superiorId(0) = New SqlParameter("@Id", SqlDbType.Int)
            _superiorId(0).Value = cmbSuperiorName.SelectedValue

            _positionId = dbMethodServer.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = @Id", CommandType.Text, _superiorId)
            rowSuperiorPositions = Me.dsJeonsoft.tblPositions.FindById(_positionId)
            txtSuperiorPosition.Text = rowSuperiorPositions.Name.ToString.Trim
        Else
            txtSuperiorPosition.Text = String.Empty
        End If
    End Sub

    Private Sub cmbManagerName_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbManagerName.SelectedValueChanged
        If Not cmbManagerName.SelectedValue = 0 Then
            Dim _positionId As Integer = 0
            Dim _managerId(0) As SqlParameter
            _managerId(0) = New SqlParameter("@Id", SqlDbType.Int)
            _managerId(0).Value = cmbManagerName.SelectedValue

            _positionId = dbMethodServer.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = @Id", CommandType.Text, _managerId)
            rowManagerPositions = Me.dsJeonsoft.tblPositions.FindById(_positionId)
            txtManagerPosition.Text = rowManagerPositions.Name.ToString.Trim
        Else
            txtManagerPosition.Text = String.Empty
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            GetTotalDays()

            If cmbLeaveType.SelectedValue = 0 Then
                MessageBox.Show("Please select leave type.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbLeaveType.Focus()
                Return
            Else
                If leaveFilingId = 0 And cmbLeaveType.SelectedValue = 1 Then
                    Dim _paramEmpId(0) As SqlParameter
                    _paramEmpId(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                    _paramEmpId(0).Value = employeeId
                    Dim _screenId As Integer = dbMethodLocal.ExecuteScalar("SELECT COUNT(*) FROM Screening WHERE EmployeeId = @EmployeeId AND IsEncoded = 0", CommandType.Text, _paramEmpId)

                    If _screenId = 0 Then
                        MessageBox.Show("No health screening record found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                End If
            End If

            If dtpFrom.Value.ToShortDateString > dtpTo.Value.ToShortDateString Then
                MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFrom.Focus()
                Return
            End If

            If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                MessageBox.Show("Please enter the reason for leave.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtReason.Focus()
                Return
            End If

            If cmbManagerName.SelectedValue = 0 Then
                MessageBox.Show("Please select your manager.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbManagerName.Focus()
                Return
            End If

            Me.Validate()

            'new transaction
            If leaveFilingId = 0 Then
                Dim _newRowLeave As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.NewLeaveFilingRow

                With _newRowLeave
                    .DateCreated = DateTime.Now
                    If screenId > 0 Then
                        .ScreenId = screenId
                    Else
                        .SetScreenIdNull()
                    End If

                    'no immediate superior
                    If cmbSuperiorName.SelectedValue = 0 Then
                        .SuperiorIsApproved = 0
                        .SetSuperiorIdNull()
                        'with immediate superior
                    Else
                        .SuperiorIsApproved = 0
                        .SuperiorId = cmbSuperiorName.SelectedValue
                    End If

                    .SetSuperiorApprovalDateNull()
                    .SetSuperiorRemarksNull()

                    .ManagerIsApproved = 0
                    .ManagerId = cmbManagerName.SelectedValue
                    .SetManagerApprovalDateNull()
                    .SetManagerRemarksNull()

                    'sick leave
                    If cmbLeaveType.SelectedValue = 1 Then
                        Dim _paramScreenId(0) As SqlParameter
                        _paramScreenId(0) = New SqlParameter("@ScreenId", SqlDbType.Int)
                        _paramScreenId(0).Value = screenId
                        dbMethodLocal.ExecuteNonQuery("UPDATE Screening SET IsEncoded = 1 WHERE ScreenId = @ScreenId", CommandType.Text, _paramScreenId)
                        .RoutingStatusId = 5

                        'vacation leave
                    Else
                        'no immediate superior
                        If cmbSuperiorName.SelectedValue = 0 Then
                            .RoutingStatusId = 3
                            'with immediate superior
                        Else
                            .RoutingStatusId = 4
                        End If
                    End If

                    .ClinicIsApproved = 0
                    .SetClinicIdNull()
                    .SetClinicApprovalDateNull()
                    .SetClinicRemarksNull()

                    .StartDate = dtpFrom.Value.ToShortDateString
                    .EndDate = dtpTo.Value.ToShortDateString
                    .Quantity = txtNumberOfDays.Text.Trim
                    .Reason = txtReason.Text.Trim
                    .EmployeeId = employeeId
                    .EncoderId = employeeId
                    .EncoderDate = DateTime.Now

                    .LeaveTypeId = cmbLeaveType.SelectedValue
                    .ModifiedId = employeeId
                    .ModifiedDate = DateTime.Now
                End With

                Me.dsLeaveFiling.LeaveFiling.AddLeaveFilingRow(_newRowLeave)
                Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)

            Else
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFilingId(leaveFilingId)

                If isSuperior = True Then
                    If cmbSuperiorStatus.SelectedIndex = 0 Then
                        MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        cmbSuperiorStatus.Focus()
                        Return
                    End If

                    With _leaveFilingRow
                        If cmbSuperiorStatus.SelectedIndex = 1 Then
                            .SuperiorIsApproved = 1
                        ElseIf cmbSuperiorStatus.SelectedIndex = 2 Then
                            .SuperiorIsApproved = 0
                        End If
                        .SuperiorApprovalDate = DateTime.Now

                        If String.IsNullOrEmpty(txtSuperiorRemarks.Text.Trim) Then
                            .SetSuperiorRemarksNull()
                        Else
                            .SuperiorRemarks = txtSuperiorRemarks.Text.Trim
                        End If

                        .RoutingStatusId = 3
                        .ModifiedId = employeeId
                        .ModifiedDate = DateTime.Now
                    End With

                ElseIf isManager = True Then
                    If cmbManagerStatus.SelectedIndex = 0 Then
                        MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        cmbManagerStatus.Focus()
                        Return
                    End If

                    With _leaveFilingRow
                        If cmbManagerStatus.SelectedIndex = 1 Then
                            .ManagerIsApproved = 1
                        ElseIf cmbManagerStatus.SelectedIndex = 2 Then
                            .ManagerIsApproved = 0
                        End If
                        .ManagerApprovalDate = DateTime.Now

                        If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) Then
                            .SetManagerRemarksNull()
                        Else
                            .ManagerRemarks = txtManagerRemarks.Text.Trim
                        End If

                        .RoutingStatusId = 2
                        .ModifiedId = employeeId
                        .ModifiedDate = DateTime.Now
                    End With

                Else
                    If isClinic = True Then
                        If cmbClinicStatus.SelectedIndex = 0 Then
                            MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            cmbClinicStatus.Focus()
                            Return
                        End If

                        With _leaveFilingRow
                            If cmbClinicStatus.SelectedIndex = 1 Then
                                .ClinicIsApproved = 1
                            ElseIf cmbClinicStatus.SelectedIndex = 2 Then
                                .ClinicIsApproved = 0
                            End If
                            .ClinicId = cmbClinicName.SelectedValue
                            .ClinicApprovalDate = DateTime.Now

                            If String.IsNullOrEmpty(txtClinicRemarks.Text.Trim) Then
                                .SetClinicRemarksNull()
                            Else
                                .ClinicRemarks = txtClinicRemarks.Text.Trim
                            End If

                            If cmbSuperiorName.SelectedValue = 0 Then
                                .RoutingStatusId = 3
                            Else
                                .RoutingStatusId = 4
                            End If

                            .ModifiedId = employeeId
                            .ModifiedDate = DateTime.Now
                        End With
                    Else
                        With _leaveFilingRow
                            .Reason = txtReason.Text.Trim
                            .ModifiedId = employeeId
                            .ModifiedDate = DateTime.Now
                        End With
                    End If
                End If

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
        MessageBox.Show("Not allowed.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#Region "Subs"
    Private Sub GetEmployeeInfo(ByVal _employeeId As Integer)
        Try
            Dim _paramEmpId(0) As SqlParameter
            _paramEmpId(0) = New SqlParameter("@EmployeeId", SqlDbType.VarChar)
            _paramEmpId(0).Value = _employeeId

            Dim _reader As IDataReader = dbMethodServer.ExecuteReader("SELECT RTRIM(EmployeeCode) AS EmployeeCode, RTRIM(Name) AS Name, RTRIM(Department) AS Department, RTRIM(Team) AS Team, RTRIM(Position) AS Position, RTRIM(EmploymentType) AS EmploymentType, DateHired FROM dbo.viwGroupEmployees WHERE (Id = @EmployeeId)", CommandType.Text, _paramEmpId)

            While _reader.Read
                txtIdNumber.Text = _reader.Item("EmployeeCode").ToString.Trim
                txtName.Text = _reader.Item("Name")

                If _reader.Item("Department").ToString.Trim.Equals(_reader.Item("Team").ToString.Trim) Then
                    txtDepartment.Text = _reader.Item("Department").ToString.Trim
                Else
                    txtDepartment.Text = _reader.Item("Department").ToString.Trim & " \ " & _reader.Item("Team").ToString.Trim
                End If

                txtPosition.Text = _reader.Item("Position").ToString.Trim
                txtEmpStatus.Text = _reader.Item("EmploymentType").ToString.Trim
                txtDateHired.Text = CDate(_reader.Item("DateHired")).ToString("MMMM dd, yyyy")
            End While
            _reader.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetTotalLeaveCredits(ByVal _empId As Integer)
        Try
            txtTotalLeaveCredits.Text = dbMethodServer.ExecuteScalar("SELECT Quantity FROM tblEmployeeLeaves WHERE EmployeeId = " & _empId & " AND LeaveTypeId = " & cmbLeaveType.SelectedValue & "", CommandType.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetLeaveBalance(ByVal _empId As Integer)
        Try
            Dim _paramsLeaveBalance(3) As SqlParameter
            _paramsLeaveBalance(0) = New SqlParameter("@CompanyId", SqlDbType.Int)
            _paramsLeaveBalance(0).Value = 1
            _paramsLeaveBalance(1) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _paramsLeaveBalance(1).Value = _empId
            _paramsLeaveBalance(2) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
            _paramsLeaveBalance(2).Value = cmbLeaveType.SelectedValue
            _paramsLeaveBalance(3) = New SqlParameter("@Date", SqlDbType.Date)
            _paramsLeaveBalance(3).Value = DBNull.Value

            leaveBalance = dbMethodServer.ExecuteFunction(Of Double)("dbo.fnGetLeaveBalance", _paramsLeaveBalance)
            txtBalance.Text = leaveBalance
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetTotalDays()
        Try
            Dim _datetimeStarted As New DateTime(dtpFrom.Value.Year, dtpFrom.Value.Month, dtpFrom.Value.Day, dtpFrom.Value.Hour, dtpFrom.Value.Minute, 0)
            Dim _datetimeEnded As New DateTime(dtpTo.Value.Year, dtpTo.Value.Month, dtpTo.Value.Day, dtpTo.Value.Hour, dtpTo.Value.Minute, 0)
            Dim _span As TimeSpan = Nothing
            Dim _minutes As Integer = 0
            Dim _hours As Integer = 0
            Dim _days As Integer = 0

            _span = (_datetimeStarted - _datetimeEnded).Duration()
            _days = _span.Days
            txtNumberOfDays.Text = _days.ToString("00") + 1

        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region


End Class