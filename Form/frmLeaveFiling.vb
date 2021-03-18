Imports System.Reflection
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports LeaveFilingSystem.dsJeonsoftTableAdapters

Public Class frmLeaveFiling
    Private clsConnection As New clsConnection
    Private clsMain As New Main
    Private dbMethodServer As New SqlDbMethod(clsConnection.JeonsoftConnection)
    Private dbMethodLocal As New SqlDbMethod(clsConnection.LocalConnection)

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
    'Private adpEmployees As New tblEmployeesTableAdapter
    Private adpRoutingStatus As New RoutingStatusTableAdapter
    Private adpLeaveFiling As New LeaveFilingTableAdapter

    'Private bsRoutingStatus As New BindingSource
    'Private bsLeaveType As New BindingSource
    Private bsLeaveFiling As New BindingSource

    'Private dtLeaveType As New tblLeaveTypesDataTable
    Private dtRoutingStatus As New RoutingStatusDataTable
    Private dtLeaveFiling As New LeaveFilingDataTable

    'Private rowEmployees As tblEmployeesRow
    'Private rowPositions As tblPositionsRow
    'Private rowDepartments As tblDepartmentsRow
    Private rowTeams As tblTeamsRow
    'Private rowEmploymentTypes As tblEmploymentTypesRow
    Private rowRoutingStatus As RoutingStatusRow

    Private rowSuperiorPositions As tblPositionsRow
    Private rowManagerPositions As tblPositionsRow

    Private leaveBalance As Double
    Private isLeader As Boolean = False
    Private isClinic As Boolean = False
    Private isSuperior As Boolean = False
    Private isManager As Boolean = False

    Private _superiorIds As New List(Of Integer) From {13, 19, 4, 17, 7, 3, 6, 27, 28} 'position
    Private _managerIds As New List(Of Integer) From {2, 21} 'position
    Private _clinicIds As New List(Of Integer) From {3} 'team

    'custom binding
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

        rowTeams = Me.dsJeonsoft.tblTeams.FindById(teamId)

        'leave type
        dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM dbo.tblLeaveTypes WHERE Id IN (1,2)", "Id", "Name", cmbLeaveType, "< Select leave type >")

        'fill clinic
        Dim _paramClinic(0) As SqlParameter
        _paramClinic(0) = New SqlParameter("@TeamId", SqlDbType.Int)
        _paramClinic(0).Value = 3
        dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM tblEmployees WHERE TeamId = @TeamId AND Active = 1", "Id", "Name", cmbClinicName, "< Select Clinic Personnel >", _paramClinic)

        'fill superior
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
        dbMethodServer.FillCmbWithCaptionText("SELECT Id, Name FROM tblEmployees WHERE DepartmentId = @DepartmentId AND PositionId IN (2,21) AND Active = 1", "Id", "Name", cmbManagerName, "< Select Manager >", _paramManager)

        'new filing
        If leaveFilingId = 0 Then
            txtFileId.Text = "(new)"
            txtDateFiled.Text = DateTime.Now.ToString("MMMM dd, yyyy  HH:mm")
            rowRoutingStatus = Me.dsLeaveFiling.RoutingStatus.FindByRoutingStatusId(6)
            txtRoutingStatus.Text = rowRoutingStatus.RoutingStatusName.ToString.Trim

            'prod - line leader, senior line leader, senior leader, leader
            Dim _leadersIds As New List(Of Integer) From {7, 17, 27, 28}
            If _leadersIds.Contains(positionId) Then
                isLeader = True
                txtName.Visible = False

                Dim _paramFillCmb(1) As SqlParameter
                _paramFillCmb(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                _paramFillCmb(0).Value = departmentId
                _paramFillCmb(1) = New SqlParameter("@Team", SqlDbType.NVarChar)
                _paramFillCmb(1).Value = rowTeams.Name.ToString.Trim

                dbMethodServer.FillCmbWithCaptionText("SELECT Id, RTRIM(Name) AS Name FROM dbo.viwGroupEmployees WHERE Active = 1 AND DepartmentId = @DepartmentId AND TRIM(Team) = @Team", "Id", "Name", cmbName, "< Select Employee >", _paramFillCmb)
                cmbName.SelectedValue = employeeId
                cmbName.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                cmbName.AutoCompleteSource = AutoCompleteSource.ListItems
            End If

            GetEmployeeInfo(employeeId)

            cmbLeaveType.SelectedValue = 0
            txtNumberOfDays.Text = 1

            cmbClinicStatus.SelectedIndex = 0
            cmbClinicStatus.Enabled = False
            cmbClinicName.Enabled = False
            txtClinicRemarks.ReadOnly = True

            cmbSuperiorStatus.SelectedIndex = 0
            cmbSuperiorStatus.Enabled = False
            If isLeader = True Then
                cmbSuperiorName.SelectedValue = employeeId
                Dim _superiorPositionId As Integer = dbMethodServer.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & cmbSuperiorName.SelectedValue & "", CommandType.Text)
                rowSuperiorPositions = Me.dsJeonsoft.tblPositions.FindById(_superiorPositionId)
                txtSuperiorPosition.Text = rowSuperiorPositions.Name.ToString.Trim
            Else
                cmbSuperiorName.SelectedValue = 0
            End If

            txtSuperiorRemarks.ReadOnly = True

            cmbManagerStatus.SelectedIndex = 0
            cmbManagerStatus.Enabled = False
            cmbManagerName.SelectedValue = 0
            txtManagerRemarks.ReadOnly = True

            Me.ActiveControl = cmbLeaveType

            'existing file
        Else
            Me.bsLeaveFiling.DataSource = Me.dsLeaveFiling
            Me.bsLeaveFiling.DataMember = dtLeaveFiling.TableName

            Me.bsLeaveFiling.Position = Me.bsLeaveFiling.Find("LeaveFilingId", leaveFilingId)

            Dim _routingStatusId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("RoutingStatusId")
            rowRoutingStatus = Me.dsLeaveFiling.RoutingStatus.FindByRoutingStatusId(_routingStatusId)
            txtRoutingStatus.Text = rowRoutingStatus.RoutingStatusName.ToString.Trim

            txtFileId.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "LeaveFilingId", False))
            txtDateFiled.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "CreationDate", False))

            Dim _empId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("EmployeeId")
            Dim _paramEmpId(0) As SqlParameter
            _paramEmpId(0) = New SqlParameter("@EmployeeId", SqlDbType.VarChar)
            _paramEmpId(0).Value = _empId

            GetEmployeeInfo(_empId)

            GetTotalLeaveCredits(cmbName.SelectedValue)
            GetLeaveBalance(cmbName.SelectedValue)

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

            'don't allow editing of leave information
            cmbLeaveType.Enabled = False
            dtpFrom.Enabled = False
            dtpTo.Enabled = False
            txtReason.ReadOnly = True

            'leave opened by others
            If Not employeeId.Equals(_empId) Then
                Dim _superiorIds As New List(Of Integer) From {13, 19, 4, 17, 7, 3, 6}
                Dim _managerIds As New List(Of Integer) From {2, 21}
                Dim _clinicIds As New List(Of Integer) From {3}
                Dim _leadersIds As New List(Of Integer) From {7, 17, 27, 28}

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

                    If _leadersIds.Contains(positionId) Then
                        isLeader = True
                    End If

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

                        Me.ActiveControl = txtClinicRemarks
                    End If
                End If

                'opened by the requestor
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
        ElseIf e.KeyCode.Equals(Keys.F10) Then
            btnSave.PerformClick()
        End If
    End Sub

    Private Sub cmbName_Validating(sender As Object, e As CancelEventArgs) Handles cmbName.Validating
        If isLeader = True Then
            If cmbName.SelectedValue = 0 Then
                e.Cancel = False
            End If
        End If
    End Sub

    Private Sub cmbName_Validated(sender As Object, e As EventArgs) Handles cmbName.Validated
        If isLeader = True Then
            GetEmployeeInfo(cmbName.SelectedValue)
        End If
    End Sub

    Private Sub cmbLeaveType_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbLeaveType.Validating

    End Sub

    Private Sub cmbLeaveType_Validated(sender As Object, e As EventArgs) Handles cmbLeaveType.Validated
        If Not cmbLeaveType.SelectedValue = 0 Then
            GetTotalLeaveCredits(employeeId)
            GetLeaveBalance(employeeId)
        Else
            txtTotalLeaveCredits.Text = String.Empty
            txtBalance.Text = String.Empty
        End If
    End Sub

    Private Sub cmbLeaveType_Leave(sender As Object, e As EventArgs) Handles cmbLeaveType.Leave

    End Sub

    Private Sub cmbSuperiorName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbSuperiorName.Validating

    End Sub

    Private Sub cmbSuperiorName_Validated(sender As Object, e As EventArgs) Handles cmbSuperiorName.Validated
        If Not cmbSuperiorName.SelectedValue = 0 Then
            Dim _superiorPositionId As Integer = dbMethodServer.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & cmbSuperiorName.SelectedValue & "", CommandType.Text)
            rowSuperiorPositions = Me.dsJeonsoft.tblPositions.FindById(_superiorPositionId)
            txtSuperiorPosition.Text = rowSuperiorPositions.Name.ToString.Trim
        Else
            txtSuperiorPosition.Text = String.Empty
        End If
    End Sub

    Private Sub cmbManagerName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbManagerName.Validating

    End Sub

    Private Sub cmbManagerName_Validated(sender As Object, e As EventArgs) Handles cmbManagerName.Validated
        If Not cmbManagerName.SelectedValue = 0 Then
            Dim _managerPositionId As Integer = dbMethodServer.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & cmbManagerName.SelectedValue & "", CommandType.Text)
            rowManagerPositions = Me.dsJeonsoft.tblPositions.FindById(_managerPositionId)
            txtManagerPosition.Text = rowManagerPositions.Name.ToString.Trim
        Else
            txtManagerPosition.Text = String.Empty
        End If
    End Sub

    Private Sub dtpTo_Validated(sender As Object, e As EventArgs) Handles dtpTo.Validated
        GetTotalDays()
    End Sub

    Private Sub dtpFrom_Validated(sender As Object, e As EventArgs) Handles dtpFrom.Validated
        GetTotalDays()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            GetTotalDays()

            If cmbLeaveType.SelectedValue = 0 Then
                MessageBox.Show("Please select leave type.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbLeaveType.Focus()
                Return
            End If

            If dtpFrom.Value.ToShortDateString > dtpTo.Value.ToShortDateString Then
                MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFrom.Focus()
                Return
            End If

            If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                MessageBox.Show("Please enter reason.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtReason.Focus()
                Return
            End If

            If Not isClinic = True Then
                If cmbManagerName.SelectedValue = 0 Then
                    MessageBox.Show("Please select your manager.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbManagerName.Focus()
                    Return
                End If
            End If

            Me.Validate()

            'new transaction
            If leaveFilingId = 0 Then
                Dim _newRowLeave As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.NewLeaveFilingRow

                With _newRowLeave
                    .CreationDate = DateTime.Now
                    If isLeader Then
                        .EmployeeId = cmbName.SelectedValue
                    Else
                        .EmployeeId = employeeId
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
                    .EncoderId = employeeId
                    .EncoderDate = DateTime.Now

                    .LeaveTypeId = cmbLeaveType.SelectedValue
                    .LastModifiedId = employeeId
                    .LastModifiedDate = DateTime.Now
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
                        .LastModifiedId = employeeId
                        .LastModifiedDate = DateTime.Now
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
                        .LastModifiedId = employeeId
                        .LastModifiedDate = DateTime.Now
                    End With
                ElseIf isClinic = True Then
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
                        .ClinicApprovalDate = DateTime.Now

                        If String.IsNullOrEmpty(txtClinicRemarks.Text.Trim) Then
                            .SetClinicRemarksNull()
                        Else
                            .ClinicRemarks = txtClinicRemarks.Text.Trim
                        End If

                        .RoutingStatusId = 4
                        .LastModifiedId = employeeId
                        .LastModifiedDate = DateTime.Now
                    End With
                End If
            End If

            Me.bsLeaveFiling.EndEdit()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetTotalLeaveCredits(ByVal _empId As Integer)
        Try
            txtTotalLeaveCredits.Text = dbMethodServer.ExecuteScalar("SELECT Quantity FROM tblEmployeeLeaves WHERE EmployeeId = " & _empId & " AND LeaveTypeId = " & cmbLeaveType.SelectedValue & "", CommandType.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

End Class