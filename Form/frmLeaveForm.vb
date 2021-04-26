Imports System.Windows.Forms
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
    Private rowSuperiorPositions1 As tblPositionsRow
    Private rowSuperiorPositions2 As tblPositionsRow
    Private rowManagerPositions As tblPositionsRow
    Private rowRoutingStatus As RoutingStatusRow
    Private rowScreening As ScreeningRow
    'bindingsource
    Private bsLeaveFiling As New BindingSource
    Private WithEvents bsSuperior1 As New BindingSource
    Private WithEvents bsSuperior2 As New BindingSource
    Private WithEvents bsManager As New BindingSource
    'custom bindings
    Private WithEvents dateCreated As Binding
    Private WithEvents superiorApprovalDate1 As Binding
    Private WithEvents superiorApprovalDate2 As Binding
    Private WithEvents managerApprovalDate As Binding
    Private WithEvents screenDate As Binding

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
    Private requestorPositionId As Integer = 0
    'dictionary
    Private dictSuperior1 As New Dictionary(Of String, Integer)
    Private dictSuperior2 As New Dictionary(Of String, Integer)
    Private dictManager As New Dictionary(Of String, Integer)
    'datatables for approvers
    Private dtSuperiorName1 As New DataTable
    Private dtSuperiorName2 As New DataTable
    Private dtManagerName As New DataTable

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

        Me.adpPositions.Fill(Me.dsJeonsoft.tblPositions)
        Me.adpTeams.Fill(Me.dsJeonsoft.tblTeams)

        FillSuperiorStatus1()
        FillSuperiorStatus2()
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
            rowRoutingStatus = Me.dsLeaveFiling.RoutingStatus.FindByRoutingStatusId(6)
            txtRoutingStatus.Text = rowRoutingStatus.RoutingStatusName.ToString.Trim
            GetEmployeeInfo(employeeId)

            FillApproversNew(employeeId, departmentId, teamId, positionId)

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

                txtClinicName.Text = cmbClinicName.Text
                txtClinicStatus.Text = "Fit To Work"
                screenDate = New Binding("Text", Me.bsLeaveFiling.Current, "ClinicApprovalDate")
                txtClinicDateApproved.DataBindings.Add(screenDate)
                txtClinicRemarks.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ClinicRemarks"))
            End If

            FillApproversOld(requestorEmployeeId, requestorDepartmentId, requestorTeamId, requestorPositionId)
            cmbLeaveType.Enabled = False

            'fill superior 1 approval status
            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId1") Is DBNull.Value Then
                cmbSuperiorName1.SelectedValue = 0
                cmbSuperiorStatus1.SelectedValue = 0
            Else
                cmbSuperiorName1.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "SuperiorId1"))
                superiorApprovalDate1 = New Binding("Text", Me.bsLeaveFiling.Current, "SuperiorApprovalDate1", False)
                txtSuperiorDateApproved1.DataBindings.Add(superiorApprovalDate1)

                If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorIsApproved1") = True Then
                    cmbSuperiorStatus1.SelectedValue = 1
                ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorIsApproved1") = False AndAlso CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Then
                    cmbSuperiorStatus1.SelectedValue = 0
                Else
                    cmbSuperiorStatus1.SelectedValue = 2
                End If
            End If
            txtSuperiorRemarks1.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "SuperiorRemarks1"))

            'fill superior 2 approval status
            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId2") Is DBNull.Value Then
                cmbSuperiorName2.SelectedValue = 0
                cmbSuperiorStatus2.SelectedValue = 0
            Else
                cmbSuperiorName2.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "SuperiorId2"))
                superiorApprovalDate2 = New Binding("Text", Me.bsLeaveFiling.Current, "SuperiorApprovalDate2", False)
                txtSuperiorDateApproved2.DataBindings.Add(superiorApprovalDate2)

                If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorIsApproved2") = True Then
                    cmbSuperiorStatus2.SelectedValue = 1
                ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorIsApproved2") = False AndAlso CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Then
                    cmbSuperiorStatus2.SelectedValue = 0
                Else
                    cmbSuperiorStatus2.SelectedValue = 2
                End If
            End If
            txtSuperiorRemarks2.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "SuperiorRemarks2"))

            'fill manager approval status
            cmbManagerName.DataBindings.Add(New Binding("SelectedValue", Me.bsLeaveFiling.Current, "ManagerId"))
            managerApprovalDate = New Binding("Text", Me.bsLeaveFiling.Current, "ManagerApprovalDate", False)
            txtManagerDateApproved.DataBindings.Add(managerApprovalDate)
            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerIsApproved") = True Then
                cmbManagerStatus.SelectedValue = 1
            ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerIsApproved") = False And CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value Then
                cmbManagerStatus.SelectedValue = 0
            Else
                cmbManagerStatus.SelectedValue = 2
            End If
            txtManagerRemarks.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "ManagerRemarks"))

            'disable editing since the record was encoded from screening
            If CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveTypeId") = 1 Then
                dtpFrom.Enabled = False
                dtpTo.Enabled = False
                txtReason.ReadOnly = True
            Else
                'disable editing if any one of the approvers approved or disapproved the leave
                If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Or _
                    Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Or _
                    Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value Then
                    dtpFrom.Enabled = False
                    dtpTo.Enabled = False
                    txtReason.ReadOnly = True
                End If
            End If
        End If
    End Sub

    Private Sub frmLeaveFiling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not leaveFileId = 0 Then
            'disable editing if leave form is opened by others (eg. approvers, hr, clinic)
            If Not requestorEmployeeId = employeeId Then
                dtpFrom.Enabled = False
                dtpTo.Enabled = False
                txtReason.ReadOnly = True
            Else
                Me.ActiveControl = txtReason
                txtReason.Select(txtReason.Text.Length, 0)
            End If

            'check if has immediate superior 1, then allow changing of approvers if still not approved
            If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId1") Is DBNull.Value AndAlso _
                CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId1") = employeeId Then
                cmbSuperiorStatus1.Enabled = True
                cmbSuperiorName1.Enabled = True
                txtSuperiorRemarks1.ReadOnly = False
                Me.ActiveControl = txtSuperiorRemarks1
                txtSuperiorRemarks1.Select(txtSuperiorRemarks1.Text.Trim.Length, 0)
            Else
                cmbSuperiorStatus1.Enabled = False

                If requestorEmployeeId = employeeId AndAlso CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Then
                    cmbSuperiorName1.Enabled = True
                    txtSuperiorRemarks1.ReadOnly = True
                Else
                    cmbSuperiorName1.Enabled = False
                    txtSuperiorRemarks1.ReadOnly = True
                End If
            End If

            'check if has immediate superior 2, then allow changing of approvers if still not approved
            If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId2") Is DBNull.Value AndAlso _
                CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId2") = employeeId Then
                cmbSuperiorStatus2.Enabled = True
                cmbSuperiorName2.Enabled = True
                txtSuperiorRemarks2.ReadOnly = False
                Me.ActiveControl = txtSuperiorRemarks2
                txtSuperiorRemarks2.Select(txtSuperiorRemarks2.Text.Trim.Length, 0)
            Else
                cmbSuperiorStatus2.Enabled = False

                If requestorEmployeeId = employeeId AndAlso CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Then
                    cmbSuperiorName2.Enabled = True
                    txtSuperiorRemarks2.ReadOnly = True
                Else
                    cmbSuperiorName2.Enabled = False
                    txtSuperiorRemarks2.ReadOnly = True
                End If
            End If

            'allow changing of manager if still not approved
            If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId") Is DBNull.Value AndAlso _
                CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId") = employeeId Then
                cmbManagerStatus.Enabled = True
                cmbManagerName.Enabled = True
                txtManagerRemarks.ReadOnly = False
                Me.ActiveControl = txtManagerRemarks
                txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)
            Else
                cmbManagerStatus.Enabled = False

                If requestorEmployeeId = employeeId AndAlso CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value Then
                    cmbManagerName.Enabled = True
                    txtManagerRemarks.ReadOnly = True
                Else
                    cmbManagerName.Enabled = False
                    txtManagerRemarks.ReadOnly = True
                End If
            End If
        End If
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
        If leaveFileId = 0 Then
            dtpFrom.Value = Date.Now.Date
            dtpTo.Value = Date.Now.Date
        End If

        If cmbSuperiorName1.Items.Count = 0 Then
            cmbSuperiorName1.Enabled = False
        Else
            cmbSuperiorName1.Enabled = True
        End If

        If cmbSuperiorName2.Items.Count = 0 Then
            cmbSuperiorName2.Enabled = False
        Else
            cmbSuperiorName2.Enabled = True
        End If

        If cmbManagerName.Items.Count = 0 Then
            cmbManagerName.Enabled = False
        Else
            cmbManagerName.Enabled = True
        End If

        If cmbLeaveType.SelectedValue = 1 Then 'sick leave
            screenId = GetScreenId(1, employeeId)

            If screenId > 0 Then
                GetScreeningByScreenId(screenId)
                GetTotalLeaveCredits(employeeId)
                GetLeaveBalance(employeeId)
            Else
                ResetForm()
                MessageBox.Show("No health screening record found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbLeaveType.Focus()
                Return
            End If

            Me.ActiveControl = cmbSuperiorName1
            cmbSuperiorName1.Select(cmbSuperiorName1.Text.Trim.Length, 0)

        ElseIf cmbLeaveType.SelectedValue = 2 Then 'vacation leave
            screenId = GetScreenId(2, employeeId)

            If screenId > 0 Then
                GetScreeningByScreenId(screenId)
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

            Me.ActiveControl = txtReason
            txtReason.Select(txtReason.Text.Trim.Length, 0)

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

            Me.ActiveControl = txtReason
            txtReason.Select(txtReason.Text.Trim.Length, 0)
        Else
            ResetForm()
        End If

        txtNumberOfDays.Text = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
    End Sub

    Private Sub dtpFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged
        If Not cmbLeaveType.SelectedValue = 3 Then
            txtNumberOfDays.Text = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
        Else
            dtpTo.Value = dtpFrom.Value
            txtNumberOfDays.Text = 1
        End If
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
        If Not cmbLeaveType.SelectedValue = 3 Then
            txtNumberOfDays.Text = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
        Else
            dtpFrom.Value = dtpTo.Value
            txtNumberOfDays.Text = 1
        End If
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

    Private Sub cmbSuperiorName1_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSuperiorName1.SelectedValueChanged
        If Not cmbSuperiorName1.SelectedValue = 0 Then
            Dim _positionId As Integer = dbJeonsoft.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & cmbSuperiorName1.SelectedValue & "", CommandType.Text)
            rowSuperiorPositions1 = Me.dsJeonsoft.tblPositions.FindById(_positionId)
            txtSuperiorPosition1.Text = rowSuperiorPositions1.Name.ToString.Trim
        Else
            txtSuperiorPosition1.Text = String.Empty
        End If
    End Sub

    Private Sub cmbSuperiorName2_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbSuperiorName2.SelectedValueChanged
        If Not cmbSuperiorName2.SelectedValue = 0 Then
            Dim _positionId As Integer = dbJeonsoft.ExecuteScalar("SELECT PositionId FROM dbo.tblEmployees WHERE Id = " & cmbSuperiorName2.SelectedValue & "", CommandType.Text)
            rowSuperiorPositions2 = Me.dsJeonsoft.tblPositions.FindById(_positionId)
            txtSuperiorPosition2.Text = rowSuperiorPositions2.Name.ToString.Trim
        Else
            txtSuperiorPosition2.Text = String.Empty
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

            If cmbLeaveType.SelectedValue = 0 Then
                MessageBox.Show("Please select leave type.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbLeaveType.Focus()
                Return
            End If

            'If leaveFileId = 0 AndAlso cmbLeaveType.SelectedValue = 2 AndAlso cmbClinicName.SelectedValue = 0 Then
            '    If dtpFrom.Value.Date < Date.Now.Date.AddDays(3) Then
            '        MessageBox.Show("Leave must be files at least three (3) days before going on leave.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        dtpFrom.Focus()
            '        Return
            '    End If
            'End If

            If dtpFrom.Value.Date > dtpTo.Value.Date Then
                MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFrom.Focus()
                Return
            End If

            If String.IsNullOrEmpty(txtReason.Text.Trim) AndAlso Not cmbLeaveType.SelectedValue = 3 Then
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
                If employeeId.Equals(cmbSuperiorName1.SelectedValue) Then
                    MessageBox.Show("Please select different immediate superior 1.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbSuperiorName1.Focus()
                    Return
                End If

                If employeeId.Equals(cmbSuperiorName2.SelectedValue) Then
                    MessageBox.Show("Please select different immediate superior 2.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbSuperiorName2.Focus()
                    Return
                End If

                If employeeId.Equals(cmbManagerName.SelectedValue) Then
                    MessageBox.Show("Please select different manager or last approver.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbSuperiorName1.Focus()
                    Return
                End If

                Dim _newRowLeave As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.NewLeaveFilingRow

                With _newRowLeave
                    .DateCreated = DateTime.Now

                    If cmbLeaveType.SelectedValue = 1 Then 'sick leave
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

                    ElseIf cmbLeaveType.SelectedValue = 2 Then 'vacation leave
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

                    ElseIf cmbLeaveType.SelectedValue = 3 Then 'brithday leave
                        .LeaveTypeId = 3

                        If IsBirthMonth(employeeId) = False Then
                            MessageBox.Show("Birthday leave must be within your birth month.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            dtpFrom.Focus()
                            Return
                        End If

                        .SetScreenIdNull()
                        .ClinicIsApproved = 0
                        .SetClinicIdNull()
                        .SetClinicApprovalDateNull()
                        .SetClinicRemarksNull()
                        .IsLateFiling = 0

                        If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                            .SetReasonNull()
                        Else
                            .Reason = txtReason.Text.Trim
                        End If
                    End If

                    If cmbSuperiorName1.SelectedValue = 0 Then 'no immediate superior 1
                        .SetSuperiorId1Null()

                        If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                            .SetSuperiorId2Null()
                            .RoutingStatusId = 3

                            'send to manager directly if no immediate superior 1 and 2
                            _frmMain.SendEmailApprovers(cmbManagerName.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)

                        Else 'with immediate superior 2
                            .RoutingStatusId = 4
                            .SuperiorId2 = cmbSuperiorName2.SelectedValue

                            _frmMain.SendEmailApprovers(cmbSuperiorName2.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)
                        End If

                    Else 'with immediate superior 1
                        .RoutingStatusId = 5
                        .SuperiorId1 = cmbSuperiorName1.SelectedValue

                        _frmMain.SendEmailApprovers(cmbSuperiorName1.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)

                        If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                            .SetSuperiorId2Null()
                        Else
                            .SuperiorId2 = cmbSuperiorName2.SelectedValue
                        End If

                        If cmbManagerName.SelectedValue = 0 Then
                            .SetManagerIdNull()
                        Else
                            .ManagerId = cmbManagerName.SelectedValue
                        End If
                    End If

                    If teamId = 0 Then
                        .SetTeamIdNull()
                    Else
                        .TeamId = teamId
                    End If

                    .SuperiorIsApproved1 = 0
                    .SetSuperiorApprovalDate1Null()
                    .SetSuperiorRemarks1Null()

                    .SuperiorIsApproved2 = 0
                    .SetSuperiorApprovalDate2Null()
                    .SetSuperiorRemarks2Null()

                    .ManagerId = cmbManagerName.SelectedValue
                    .ManagerIsApproved = 0
                    .SetManagerApprovalDateNull()
                    .SetManagerRemarksNull()

                    .EmployeeId = employeeId
                    .DepartmentId = departmentId
                    .StartDate = dtpFrom.Value.Date
                    .EndDate = dtpTo.Value.Date
                    .Quantity = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
                    .Reason = txtReason.Text.Trim
                    .IsEncoded = False

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

            Else 'exisiting file
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(leaveFileId)

                With _leaveFilingRow
                    If .IsSuperiorId1Null = False AndAlso .SuperiorId1 = employeeId Then
                        'If cmbSuperiorStatus1.SelectedValue = 0 Then
                        '    MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '    txtSuperiorRemarks1.Focus()
                        '    txtSuperiorRemarks1.Select(txtSuperiorRemarks1.Text.Trim.Length, 0)
                        '    Return
                        'End If

                        If cmbSuperiorStatus1.SelectedValue = 0 Then
                            If String.IsNullOrEmpty(txtSuperiorRemarks1.Text.Trim) = False Then
                                If SaveRemarksOnly() = Windows.Forms.DialogResult.Yes Then
                                    .SuperiorRemarks1 = txtSuperiorRemarks1.Text.Trim
                                Else
                                    Return
                                End If
                            Else
                                .SetSuperiorRemarks1Null()
                            End If
                        Else
                            If cmbSuperiorStatus1.SelectedValue = 1 Then
                                If Confirmation(1) = Windows.Forms.DialogResult.Yes Then
                                    If .IsSuperiorId2Null = True Then
                                        .RoutingStatusId = 3
                                        _frmMain.SendEmailApprovers(cmbManagerName.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)
                                    Else
                                        .RoutingStatusId = 4
                                        _frmMain.SendEmailApprovers(cmbSuperiorName2.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)
                                    End If

                                    .SuperiorIsApproved1 = 1
                                Else
                                    Return
                                End If

                            ElseIf cmbSuperiorStatus1.SelectedValue = 2 Then
                                If Confirmation(2) = Windows.Forms.DialogResult.Yes Then
                                    .SuperiorIsApproved1 = 0

                                    _frmMain.SendEmailRequestor(False, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)
                                    _frmMain.SendEmailHr(False, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)
                                Else
                                    Return
                                End If
                            End If

                            .SuperiorApprovalDate1 = DateTime.Now
                            If String.IsNullOrEmpty(txtSuperiorRemarks1.Text.Trim) Then
                                .SetSuperiorRemarks1Null()
                            Else
                                .SuperiorRemarks1 = txtSuperiorRemarks1.Text.Trim
                            End If
                        End If

                    ElseIf .IsSuperiorId2Null = False AndAlso .SuperiorId2 = employeeId Then
                        'If cmbSuperiorStatus2.SelectedValue = 0 Then
                        '    MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '    txtSuperiorRemarks2.Focus()
                        '    txtSuperiorRemarks2.Select(txtSuperiorRemarks2.Text.Trim.Length, 0)
                        '    Return
                        'End If

                        If cmbSuperiorStatus2.SelectedValue = 0 Then
                            If String.IsNullOrEmpty(txtSuperiorRemarks2.Text.Trim) = False Then
                                If SaveRemarksOnly() = Windows.Forms.DialogResult.Yes Then
                                    .SuperiorRemarks2 = txtSuperiorRemarks2.Text.Trim
                                Else
                                    Return
                                End If
                            Else
                                .SetSuperiorRemarks2Null()
                            End If
                        Else
                            If cmbSuperiorStatus2.SelectedValue = 1 Then
                                If Confirmation(1) = Windows.Forms.DialogResult.Yes Then
                                    .RoutingStatusId = 3
                                    .SuperiorIsApproved2 = 1

                                    _frmMain.SendEmailApprovers(cmbManagerName.SelectedValue, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)
                                Else
                                    Return
                                End If

                            ElseIf cmbSuperiorStatus2.SelectedValue = 2 Then
                                If Confirmation(2) = Windows.Forms.DialogResult.Yes Then
                                    .RoutingStatusId = 7
                                    .SuperiorIsApproved2 = 0

                                    _frmMain.SendEmailRequestor(False, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)
                                    _frmMain.SendEmailHr(False, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)
                                Else
                                    Return
                                End If
                            End If

                            .SuperiorApprovalDate2 = DateTime.Now
                            If String.IsNullOrEmpty(txtSuperiorRemarks2.Text.Trim) Then
                                .SetSuperiorRemarks2Null()
                            Else
                                .SuperiorRemarks2 = txtSuperiorRemarks2.Text.Trim
                            End If
                        End If

                    ElseIf .ManagerId = employeeId Then
                        'If cmbManagerStatus.SelectedValue = 0 Then
                        '    MessageBox.Show("Please select status.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '    txtManagerRemarks.Focus()
                        '    txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)
                        '    Return
                        'End If

                        If cmbManagerStatus.SelectedValue = 0 Then
                            If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) = False Then
                                If SaveRemarksOnly() = Windows.Forms.DialogResult.Yes Then
                                    .ManagerRemarks = txtManagerRemarks.Text.Trim
                                Else
                                    Return
                                End If
                            Else
                                .SetManagerRemarksNull()
                            End If

                        Else
                            If cmbManagerStatus.SelectedValue = 1 Then
                                If Confirmation(1) = Windows.Forms.DialogResult.Yes Then
                                    .RoutingStatusId = 2
                                    .ManagerIsApproved = 1

                                    _frmMain.SendEmailRequestor(True, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)
                                    _frmMain.SendEmailHr(True, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)

                                Else
                                    Return
                                End If

                            ElseIf cmbManagerStatus.SelectedValue = 2 Then
                                If Confirmation(2) = Windows.Forms.DialogResult.Yes Then
                                    .RoutingStatusId = 7
                                    .ManagerIsApproved = 0

                                    _frmMain.SendEmailRequestor(False, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)
                                    _frmMain.SendEmailHr(False, cmbLeaveType.Text, StrConv(txtName.Text.Trim, VbStrConv.ProperCase), txtDepartment.Text.Trim, dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), txtReason.Text.Trim)
                                Else
                                    Return
                                End If
                            End If

                            .ManagerApprovalDate = DateTime.Now
                            If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) Then
                                .SetManagerRemarksNull()
                            Else
                                .ManagerRemarks = txtManagerRemarks.Text.Trim
                            End If
                        End If
                    Else
                        If requestorEmployeeId = employeeId AndAlso txtClinicStatus.Text.Trim = "" AndAlso txtReason.Enabled = True AndAlso dtpFrom.Enabled = True Then
                            .StartDate = dtpFrom.Value.Date
                            .EndDate = dtpTo.Value.Date
                            .Reason = txtReason.Text.Trim
                            .ModifiedBy = employeeId
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
        If employeeId = requestorEmployeeId AndAlso leaveFileId <> 0 And (Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Or Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Or Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value) Then
            MessageBox.Show("Cannot delete approved/disapproved leave.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            If MessageBox.Show("Delete this record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                'set screening record IsUsed flag to false
                If cmbLeaveType.SelectedValue = 1 Then
                    Me.adpScreening.FillByScreenId(Me.dsLeaveFiling.Screening, screenId)
                    Dim _screeningRow As ScreeningRow = Me.dsLeaveFiling.Screening.FindByScreenId(screenId)
                    With _screeningRow
                        .IsUsed = 0
                    End With
                    Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                End If
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

    Private Sub superiorApprovalDate1_Format(sender As Object, e As ConvertEventArgs) Handles superiorApprovalDate1.Format
        If Not e.Value Is DBNull.Value Then
            e.Value = Format(e.Value, "MMMM dd, yyyy  HH:mm")
        Else
            e.Value = ""
        End If
    End Sub

    Private Sub superiorApprovalDate2_Format(sender As Object, e As ConvertEventArgs) Handles superiorApprovalDate2.Format
        If Not e.Value Is DBNull.Value Then
            e.Value = Format(e.Value, "MMMM dd, yyyy  HH:mm")
        Else
            e.Value = ""
        End If
    End Sub

    Private Sub managerApprovalDate_Format(sender As Object, e As ConvertEventArgs) Handles managerApprovalDate.Format
        If Not e.Value Is DBNull.Value Then
            e.Value = Format(e.Value, "MMMM dd, yyyy  HH:mm")
        Else
            e.Value = ""
        End If
    End Sub

    Private Sub screenDate_Format(sender As Object, e As ConvertEventArgs) Handles screenDate.Format
        If Not e.Value Is DBNull.Value Then
            e.Value = Format(e.Value, "MMMM dd, yyyy  HH:mm")
        Else
            e.Value = DateTime.Now.ToString("MMMM dd, yyyy  HH:mm")
        End If
    End Sub

#Region "Sub"
    'check if birthday of the requestor is within the selected month
    Private Function IsBirthMonth(ByVal _employeeId As Integer) As Boolean
        Dim _bday As Date

        If leaveFileId = 0 AndAlso cmbLeaveType.SelectedValue = 3 Then
            Dim _prmBday(0) As SqlParameter
            _prmBday(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmBday(0).Value = _employeeId

            Dim _reader As IDataReader = dbJeonsoft.ExecuteReader("SELECT CAST(BirthDate AS DATE) AS BirthDate FROM dbo.tblEmployees WHERE Id = @EmployeeId", CommandType.Text, _prmBday)

            While _reader.Read
                If Not _reader.Item("BirthDate").ToString Is DBNull.Value Then
                    _bday = _reader.Item("BirthDate").ToString
                Else
                    _bday = Date.Now.Date
                End If
            End While
            _reader.Close()
        End If

        If Not _bday.Date.Date.Month.Equals(dtpFrom.Value.Date.Month) Then
            Return False
        Else
            Return True
        End If
    End Function

    'get requestor's employee information
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
                    requestorPositionId = _reader.Item("PositionId").ToString
                End If
            End While
            _reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetScreenId(ByVal _leaveType As Integer, ByVal _empId As Integer) As Integer
        Dim _screenId As Integer = 0

        Try
            Dim _prmScreen(1) As SqlParameter
            _prmScreen(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmScreen(0).Value = _empId
            _prmScreen(1) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
            _prmScreen(1).Value = _leaveType
            _screenId = dbLeaveFiling.ExecuteScalar("SELECT TOP 1 (ScreenId) FROM Screening WHERE EmployeeId = @EmployeeId AND LeaveTypeId = @LeaveTypeId AND IsUsed = 0 AND IsFitToWork = 1 ORDER BY ScreenId DESC", CommandType.Text, _prmScreen)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _screenId
    End Function

    'get screening information from clinic records
    Private Sub GetScreeningByScreenId(ByVal _screenId As Integer)
        Try
            dtpFrom.Enabled = False
            dtpTo.Enabled = False
            txtReason.ReadOnly = True

            Me.adpScreening.FillByScreenId(Me.dsLeaveFiling.Screening, _screenId)
            rowScreening = Me.dsLeaveFiling.Screening.FindByScreenId(_screenId)
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

            txtClinicName.Text = StrConv(cmbClinicName.Text, VbStrConv.ProperCase)
            dtpFrom.Value = rowScreening.AbsentFrom.Date
            dtpTo.Value = rowScreening.AbsentTo.Date
            txtReason.Text = rowScreening.Reason.ToString.Trim
            txtNumberOfDays.Text = rowScreening.Quantity.ToString
            txtClinicDateApproved.Text = rowScreening.ScreenDate.ToString("MMMM dd, yyyy HH:mm")
            txtClinicRemarks.Text = rowScreening.Diagnosis.Trim
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'get total leave credits
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

    'get leave balance
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

    'compute total days between two dates - excluding sundays, legal, special and company holidays
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

    'check if sunday
    Private Function IsWeekend(ByVal _date As Date) As Boolean
        If _date.DayOfWeek.Equals(DayOfWeek.Sunday) Then
            Return True
        Else
            Return False
        End If
    End Function

    'check if included to holiday list
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

        cmbSuperiorStatus1.Enabled = False
        cmbSuperiorStatus1.SelectedValue = 0
        cmbSuperiorName1.Enabled = False
        cmbSuperiorName1.SelectedValue = 0
        txtSuperiorRemarks1.Text = String.Empty
        txtSuperiorRemarks1.ReadOnly = True

        cmbSuperiorStatus2.Enabled = False
        cmbSuperiorStatus2.SelectedValue = 0
        cmbSuperiorName2.Enabled = False
        cmbSuperiorName2.SelectedValue = 0
        txtSuperiorRemarks2.Text = String.Empty
        txtSuperiorRemarks2.ReadOnly = True

        cmbManagerStatus.Enabled = False
        cmbManagerStatus.SelectedValue = 0
        cmbManagerName.Enabled = False
        cmbManagerName.SelectedValue = 0
        txtManagerRemarks.Text = String.Empty
        txtManagerRemarks.ReadOnly = True
    End Sub

    Private Sub FillSuperiorStatus1()
        dictSuperior1.Add(" < Select Status > ", 0)
        dictSuperior1.Add("Approve", 1)
        dictSuperior1.Add("Disapprove", 2)
        cmbSuperiorStatus1.DisplayMember = "Key"
        cmbSuperiorStatus1.ValueMember = "Value"
        cmbSuperiorStatus1.DataSource = New BindingSource(dictSuperior1, Nothing)
    End Sub

    Private Sub FillSuperiorStatus2()
        dictSuperior2.Add(" < Select Status > ", 0)
        dictSuperior2.Add("Approve", 1)
        dictSuperior2.Add("Disapprove", 2)
        cmbSuperiorStatus2.DisplayMember = "Key"
        cmbSuperiorStatus2.ValueMember = "Value"
        cmbSuperiorStatus2.DataSource = New BindingSource(dictSuperior2, Nothing)
    End Sub

    Private Sub FillManagerStatus()
        dictManager.Add(" < Select Status > ", 0)
        dictManager.Add("Approve", 1)
        dictManager.Add("Disapprove", 2)
        cmbManagerStatus.DisplayMember = "Key"
        cmbManagerStatus.ValueMember = "Value"
        cmbManagerStatus.DataSource = New BindingSource(dictManager, Nothing)
    End Sub

    'check if no line leader
    Private Function IsNoLeader(ByVal _departmentId As Integer, ByVal _teamId As Integer) As Boolean
        Dim _noLeader As Boolean = False
        Dim _count As Integer = 0
        Try
            Dim _prm(1) As SqlParameter
            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
            _prm(0).Value = _departmentId
            _prm(1) = New SqlParameter("@TeamId", SqlDbType.Int)
            _prm(1).Value = _teamId
            _count = dbJeonsoft.ExecuteScalar("SELECT COUNT(Id) AS Count FROM dbo.tblEmployees WHERE DepartmentId = @DepartmentId AND TeamId = @TeamId AND PositionId = 7", CommandType.Text, _prm)
            If _count > 0 Then
                _noLeader = False
            Else
                _noLeader = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _noLeader
    End Function

    'check if no sr line leader
    Private Function IsNoSrLeader(ByVal _departmentId As Integer, ByVal _teamId As Integer) As Boolean
        Dim _noSrLeader As Boolean = False
        Dim _count As Integer = 0
        Try
            Dim _prm(1) As SqlParameter
            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
            _prm(0).Value = _departmentId
            _prm(1) = New SqlParameter("@TeamId", SqlDbType.Int)
            _prm(1).Value = _teamId
            _count = dbJeonsoft.ExecuteScalar("SELECT COUNT(Id) AS Count FROM dbo.tblEmployees WHERE DepartmentId = @DepartmentId AND TeamId = @TeamId AND PositionId = 17", CommandType.Text, _prm)
            If _count > 0 Then
                _noSrLeader = False
            Else
                _noSrLeader = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _noSrLeader
    End Function

    'check if no sr mngr
    Private Function IsNoSrMngr(ByVal _departmentId As Integer) As Boolean
        Dim _noSrMngr As Boolean = False
        Dim _count As Integer = 0
        Try
            Dim _prm(0) As SqlParameter
            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
            _prm(0).Value = _departmentId
            _count = dbJeonsoft.ExecuteScalar("SELECT COUNT(Id) AS Count FROM dbo.tblEmployees WHERE DepartmentId = @DepartmentId AND PositionId = 21", CommandType.Text, _prm)
            If _count > 0 Then
                _noSrMngr = False
            Else
                _noSrMngr = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return _noSrMngr
    End Function

    Private Sub FillApproversOld(ByVal _employeeId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _positionId As Integer)
        'immediate superior 1
        Select Case _departmentId
            Case 4 'production
                Select Case _positionId
                    Case 21, 2, 13, 19, 4 'sr mngr, mngr, asst mngr, sv, asv

                    Case 1 'staff
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 6
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)

                    Case 17 'sr line leader
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 7
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)

                    Case 7 'leader
                        If IsNoSrLeader(_departmentId, _teamId) = True Then
                            Dim _prm(1) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 7
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                        Else
                            Dim _prm(2) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 4
                            _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                            _prm(2).Value = _teamId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                        End If

                    Case Else
                        If IsNoLeader(_departmentId, _teamId) = True Then
                            If IsNoSrLeader(_departmentId, _teamId) = True Then
                                Dim _prm(2) As SqlParameter
                                _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                                _prm(0).Value = _departmentId
                                _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                                _prm(1).Value = 7
                                _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                                _prm(2).Value = _teamId
                                dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                            Else
                                Dim _prm(2) As SqlParameter
                                _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                                _prm(0).Value = _departmentId
                                _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                                _prm(1).Value = 4
                                _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                                _prm(2).Value = _teamId
                                dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                            End If
                        Else
                            Dim _prm(2) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 3
                            _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                            _prm(2).Value = _teamId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                        End If
                End Select

            Case Else
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr

                    Case Else
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 13
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                End Select
        End Select

        'immediate superior 2
        Select Case _departmentId
            Case 4 'production
                Select Case _positionId
                    Case 21, 2, 13, 19 'sr mngr, mngr, asst mngr, sv

                    Case 4 'asv
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 5
                        _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                        _prm(2).Value = _teamId
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)

                    Case 1 'staff
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 4
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)

                    Case 17 'sr line leader
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 4
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)

                    Case 7 'leader
                        If IsNoSrLeader(_departmentId, _teamId) = True Then
                            Dim _prm(1) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 4
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                        Else
                            Dim _prm(2) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 3
                            _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                            _prm(2).Value = _teamId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                        End If

                    Case Else
                        If IsNoSrLeader(_departmentId, _teamId) Then
                            Dim _prm(2) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 2
                            _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                            _prm(2).Value = _employeeId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                        Else
                            Dim _prm(2) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 3
                            _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                            _prm(2).Value = _employeeId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                        End If
                End Select

            Case 14 'purchasing
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr
                        Dim _prm(0) As SqlParameter 'sir funaki
                        _prm(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(0).Value = 276
                        dbLeaveFiling.FillCmbWithCaption("SELECT E.Id, (E.FirstName + ' ' + ISNULL(SUBSTRING(CASE WHEN LEN(TRIM(E.MiddleName)) = 0 THEN NULL WHEN TRIM(E.MiddleName) = '-' THEN NULL ELSE TRIM(E.MiddleName) END, 1, 1) + '. ' , '') + E.LastName) AS Name FROM NBCTECHDB.dbo.tblEmployees E WHERE E.Active = 1 AND E.Id <> 1 AND E.Id = @EmployeeId ORDER BY E.Name ASC", CommandType.Text, "Id", "Name", cmbSuperiorName2, "", _prm)

                    Case Else
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 3
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                End Select

            Case Else
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr

                    Case Else
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 3
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                End Select

        End Select

        'manager
        Select Case _departmentId
            Case 4 'production
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr
                        Dim _prm(0) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 9
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)

                    Case 13, 19, 4, 17, 1, 11 'asst mngr, sv, asv, sr line leader, staff, clerk
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 7
                        _prm(1) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(1).Value = _departmentId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)

                    Case Else
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 4
                        _prm(1) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(1).Value = _departmentId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)
                End Select

            Case 19 'qc
                Dim _prm(0) As SqlParameter 'sir alvin aranes
                _prm(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                _prm(0).Value = 117
                dbLeaveFiling.FillCmbWithCaption("SELECT E.Id, (E.FirstName + ' ' + ISNULL(SUBSTRING(CASE WHEN LEN(TRIM(E.MiddleName)) = 0 THEN NULL WHEN TRIM(E.MiddleName) = '-' THEN NULL ELSE TRIM(E.MiddleName) END, 1, 1) + '. ' , '') + E.LastName) AS Name FROM NBCTECHDB.dbo.tblEmployees E WHERE E.Active = 1 AND E.Id <> 1 AND E.Id = @EmployeeId ORDER BY E.Name ASC", CommandType.Text, "Id", "Name", cmbManagerName, "", _prm)

            Case Else
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr
                        Dim _prm(0) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 9
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)

                    Case Else
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 7
                        _prm(1) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(1).Value = _departmentId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)
                End Select
        End Select
    End Sub

    Private Sub FillApproversNew(ByVal _employeeId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _positionId As Integer)
        'immediate superior 1
        Select Case _departmentId
            Case 4 'production
                Select Case _positionId
                    Case 21, 2, 13, 19, 4 'sr mngr, mngr, asst mngr, sv, asv

                    Case 1 'staff
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 6
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)

                    Case 17 'sr line leader
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 7
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)

                    Case 7 'leader
                        If IsNoSrLeader(_departmentId, _teamId) = True Then
                            Dim _prm(2) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 7
                            _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                            _prm(2).Value = _employeeId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                        Else
                            Dim _prm(3) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 4
                            _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                            _prm(2).Value = _teamId
                            _prm(3) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                            _prm(3).Value = _employeeId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                        End If

                    Case Else
                        If IsNoLeader(_departmentId, _teamId) = True Then
                            If IsNoSrLeader(_departmentId, _teamId) = True Then
                                Dim _prm(3) As SqlParameter
                                _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                                _prm(0).Value = _departmentId
                                _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                                _prm(1).Value = 7
                                _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                                _prm(2).Value = _teamId
                                _prm(3) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                                _prm(3).Value = _employeeId
                                dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                            Else
                                Dim _prm(2) As SqlParameter
                                _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                                _prm(0).Value = _departmentId
                                _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                                _prm(1).Value = 4
                                _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                                _prm(2).Value = _employeeId
                                dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                            End If
                        Else
                            Dim _prm(3) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 3
                            _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                            _prm(2).Value = _teamId
                            _prm(3) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                            _prm(3).Value = _employeeId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                        End If
                End Select

            Case Else
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr

                    Case Else
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 13
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName1, "", _prm)
                End Select
        End Select

        'immediate superior 2
        Select Case _departmentId
            Case 4 'production
                Select Case _positionId
                    Case 21, 2, 13, 19 'sr mngr, mngr, asst mngr, sv

                    Case 4 'asv
                        Dim _prm(3) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 5
                        _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                        _prm(2).Value = _teamId
                        _prm(3) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(3).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)

                    Case 1 'staff
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 4
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)

                    Case 17 'sr line leader
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 4
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)

                    Case 7 'leader
                        If IsNoSrLeader(_departmentId, _teamId) = True Then
                            Dim _prm(2) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 4
                            _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                            _prm(2).Value = _employeeId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                        Else
                            Dim _prm(3) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 3
                            _prm(2) = New SqlParameter("@TeamId", SqlDbType.Int)
                            _prm(2).Value = _teamId
                            _prm(3) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                            _prm(3).Value = _employeeId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                        End If

                    Case Else
                        If IsNoSrLeader(_departmentId, _teamId) Then
                            Dim _prm(2) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 2
                            _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                            _prm(2).Value = _employeeId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                        Else
                            Dim _prm(2) As SqlParameter
                            _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                            _prm(0).Value = _departmentId
                            _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                            _prm(1).Value = 3
                            _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                            _prm(2).Value = _employeeId
                            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                        End If
                End Select

            Case 14 'purchasing
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr
                        Dim _prm(0) As SqlParameter 'sir funaki
                        _prm(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(0).Value = 276
                        dbLeaveFiling.FillCmbWithCaption("SELECT E.Id, (E.FirstName + ' ' + ISNULL(SUBSTRING(CASE WHEN LEN(TRIM(E.MiddleName)) = 0 THEN NULL WHEN TRIM(E.MiddleName) = '-' THEN NULL ELSE TRIM(E.MiddleName) END, 1, 1) + '. ' , '') + E.LastName) AS Name FROM NBCTECHDB.dbo.tblEmployees E WHERE E.Active = 1 AND E.Id <> 1 AND E.Id = @EmployeeId ORDER BY E.Name ASC", CommandType.Text, "Id", "Name", cmbSuperiorName2, "", _prm)

                    Case Else
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 3
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                End Select

            Case Else
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr

                    Case Else
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(0).Value = _departmentId
                        _prm(1) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(1).Value = 3
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "Id", "Name", cmbSuperiorName2, "", _prm)
                End Select

        End Select

        'manager
        Select Case _departmentId
            Case 4 'production
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 9
                        _prm(1) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(1).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)

                    Case 13, 19, 4, 17, 1, 11 'asst mngr, sv, asv, sr line leader, staff, clerk
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 7
                        _prm(1) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(1).Value = _departmentId
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)

                    Case Else
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 4
                        _prm(1) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(1).Value = _departmentId
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)
                End Select

            Case 19 'qc
                Dim _prm(0) As SqlParameter 'sir alvin aranes
                _prm(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                _prm(0).Value = 117
                dbLeaveFiling.FillCmbWithCaption("SELECT E.Id, (E.FirstName + ' ' + ISNULL(SUBSTRING(CASE WHEN LEN(TRIM(E.MiddleName)) = 0 THEN NULL WHEN TRIM(E.MiddleName) = '-' THEN NULL ELSE TRIM(E.MiddleName) END, 1, 1) + '. ' , '') + E.LastName) AS Name FROM NBCTECHDB.dbo.tblEmployees E WHERE E.Active = 1 AND E.Id <> 1 AND E.Id = @EmployeeId ORDER BY E.Name ASC", CommandType.Text, "Id", "Name", cmbManagerName, "", _prm)

            Case Else
                Select Case _positionId
                    Case 21, 2 'sr mngr, mngr
                        Dim _prm(1) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 9
                        _prm(1) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(1).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)

                    Case Else
                        Dim _prm(2) As SqlParameter
                        _prm(0) = New SqlParameter("@AccessLevel", SqlDbType.Int)
                        _prm(0).Value = 7
                        _prm(1) = New SqlParameter("@DepartmentId", SqlDbType.Int)
                        _prm(1).Value = _departmentId
                        _prm(2) = New SqlParameter("@EmployeeId", SqlDbType.Int)
                        _prm(2).Value = _employeeId
                        dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "Id", "Name", cmbManagerName, "", _prm)
                End Select
        End Select
    End Sub

    Private Function SaveRemarksOnly() As DialogResult
        If MessageBox.Show("No selected status. Would you like to save only the remarks?", "", _
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                           MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

            Return Windows.Forms.DialogResult.Yes
        Else
            Return Windows.Forms.DialogResult.No
        End If
    End Function

    Private Function Confirmation(ByVal _status As Integer) As DialogResult
        If _status = 1 Then
            If MessageBox.Show("Are you sure you want to approve this record?" & Environment.NewLine & "This cannot be modify.", "", _
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                   MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                Return Windows.Forms.DialogResult.Yes
            Else
                Return Windows.Forms.DialogResult.No
            End If
        Else
            If MessageBox.Show("Are you sure you want to disapprove this record?" & Environment.NewLine & "This cannot be modify.", "", _
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                   MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

                Return Windows.Forms.DialogResult.Yes
            Else
                Return Windows.Forms.DialogResult.No
            End If
        End If
    End Function

#End Region

End Class