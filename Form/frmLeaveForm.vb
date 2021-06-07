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
    'data objects
    Private dsLeaveFiling As New dsLeaveFiling
    Private dsJeonsoft As New dsJeonsoft
    Private adpPositions As New tblPositionsTableAdapter
    Private adpTeams As New tblTeamsTableAdapter
    Private adpRoutingStatus As New RoutingStatusTableAdapter
    Private adpLeaveFiling As New LeaveFilingTableAdapter
    Private adpScreening As New ScreeningTableAdapter
    Private dtRoutingStatus As New RoutingStatusDataTable
    Private dtLeaveFiling As New LeaveFilingDataTable
    Private rowClinicPositions As tblPositionsRow
    Private rowSuperiorPositions1 As tblPositionsRow
    Private rowSuperiorPositions2 As tblPositionsRow
    Private rowManagerPositions As tblPositionsRow
    Private rowRoutingStatus As RoutingStatusRow
    Private rowScreening As ScreeningRow
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
    'requestor's information for new filing
    Private leaveFileId As Integer = 0
    Private screenId As Integer = 0
    Private employeeId As Integer = 0
    Private positionId As Integer = 0
    Private departmentId As Integer = 0
    Private teamId As Integer = 0
    Private employmentTypeId As Integer = 0
    Private emailAddress As String = String.Empty
    Private mobileNumber As String = String.Empty
    Private nbcEmailAddress As String = String.Empty
    Private birthDate As Date
    Private maritalStatusId As Integer = 0
    Private genderId As Integer = 0
    'requestor's information
    Private requestorEmployeeId As Integer = 0
    Private requestorDepartmentId As Integer = 0
    Private requestorTeamId As Integer = 0
    Private requestorPositionId As Integer = 0
    Private requestorGenderId As Integer = 0
    Private requestorEmploymentTypeId As Integer = 0
    'dictionaries
    Private dictSuperior1 As New Dictionary(Of String, Integer)
    Private dictSuperior2 As New Dictionary(Of String, Integer)
    Private dictManager As New Dictionary(Of String, Integer)

    Public Sub New(ByVal _dataset As DataSet, ByVal _employeeId As Integer, ByVal _positionId As Integer, ByVal _departmentId As Integer, _
                   ByVal _teamId As Integer, ByVal _employmentTypeId As Integer, Optional ByVal _leaveFileId As Integer = 0)

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

        FillApproverStatus()

        dbLeaveFiling.FillCmbWithCaption("RdClinicCombined", CommandType.StoredProcedure, "EmployeeId", "EmployeeName", cmbClinicName, "< None >")

        'new filing
        If leaveFileId = 0 Then
            Me.dsLeaveFiling = _dataset
            Me.dsLeaveFiling.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
            Me.adpRoutingStatus.Fill(Me.dsLeaveFiling.RoutingStatus)

            ResetForm()

            txtFileId.Visible = False
            lblFileId.Visible = False
            txtDateCreated.Text = String.Format("{0:MMMM dd, yyyy HH:mm}", serverDate)
            rowRoutingStatus = Me.dsLeaveFiling.RoutingStatus.FindByRoutingStatusId(6)
            txtRoutingStatus.Text = rowRoutingStatus.RoutingStatusName.ToString.Trim

            FillEmployeeInformation(employeeId)
            FillLeaveTypes(employmentTypeId, genderId, positionId)
            FillApproversNew(employeeId)

            Me.ActiveControl = cmbLeaveType

            'existing file
        Else
            Me.adpLeaveFiling.FillByLeaveFileId(Me.dsLeaveFiling.LeaveFiling, leaveFileId)
            Me.bsLeaveFiling.DataSource = Me.dsLeaveFiling
            Me.bsLeaveFiling.DataMember = dtLeaveFiling.TableName
            Me.bsLeaveFiling.Position = Me.bsLeaveFiling.Find("LeaveFileId", leaveFileId)
            requestorEmployeeId = CType(Me.bsLeaveFiling.Current, DataRowView).Item("EmployeeId")
            FillEmployeeInformation(requestorEmployeeId)
            FillLeaveTypes(requestorEmploymentTypeId, requestorGenderId, requestorPositionId)

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
            txtLeaveCredits.Text = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveCredits")
            txtBalance.Text = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveBalance")
            txtNumberOfDays.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "Quantity"))
            txtReason.DataBindings.Add(New Binding("Text", Me.bsLeaveFiling.Current, "Reason"))

            If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ScreenId") Is DBNull.Value Then
                screenId = CType(Me.bsLeaveFiling.Current, DataRowView).Item("ScreenId")
                Me.adpScreening.FillByScreenId(Me.dsLeaveFiling.Screening, screenId)
                rowScreening = Me.dsLeaveFiling.Screening.FindByScreenId(screenId)

                Dim _prmEmployeeCode(0) As SqlParameter
                _prmEmployeeCode(0) = New SqlParameter("@EmployeeId", SqlDbType.VarChar)
                _prmEmployeeCode(0).Value = rowScreening.ScreenBy.ToString

                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinicCombined", CommandType.StoredProcedure, _prmEmployeeCode)

                While _reader.Read
                    cmbClinicName.SelectedValue = _reader.Item("EmployeeId")
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
                ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorIsApproved1") = False AndAlso _
                    CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Then
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
                ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorIsApproved2") = False AndAlso _
                    CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Then
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
            ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerIsApproved") = False And _
                CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value Then
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
            If Not requestorEmployeeId = employeeId Then  'disable editing if leave form is opened by others (eg. approvers, hr, clinic)
                dtpFrom.Enabled = False
                dtpTo.Enabled = False
                txtReason.ReadOnly = True

                'with immediate superior 1
                If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId1") Is DBNull.Value Then
                    If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId1") = employeeId AndAlso _
                        Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Then 'approved - lock editing
                        cmbSuperiorStatus1.Enabled = False
                        cmbSuperiorName1.Enabled = False
                        txtSuperiorRemarks1.ReadOnly = True

                    ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId1") = employeeId AndAlso _
                        CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Then 'not approved - open editing
                        cmbSuperiorStatus1.Enabled = True
                        cmbSuperiorName1.Enabled = True
                        txtSuperiorRemarks1.ReadOnly = False

                    Else
                        cmbSuperiorStatus1.Enabled = False
                        cmbSuperiorName1.Enabled = False
                        txtSuperiorRemarks1.ReadOnly = True
                    End If

                    Me.ActiveControl = txtSuperiorRemarks1
                    txtSuperiorRemarks1.Select(txtSuperiorRemarks1.Text.Trim.Length, 0)

                    'without immediate superior 1
                Else
                    cmbSuperiorStatus1.Enabled = False
                    cmbSuperiorName1.Enabled = False
                    txtSuperiorRemarks1.ReadOnly = True
                End If

                'with immediate superior 2
                If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId2") Is DBNull.Value Then
                    If CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId2") = employeeId AndAlso _
                        Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Then 'approved - lock editing
                        cmbSuperiorStatus2.Enabled = False
                        cmbSuperiorName2.Enabled = False
                        txtSuperiorRemarks2.ReadOnly = True

                    ElseIf CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId2") = employeeId AndAlso _
                        CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Then 'not approved - open editing
                        cmbSuperiorStatus2.Enabled = True
                        cmbSuperiorName2.Enabled = True
                        txtSuperiorRemarks2.ReadOnly = False

                    Else
                        cmbSuperiorStatus2.Enabled = False
                        cmbSuperiorName2.Enabled = False
                        txtSuperiorRemarks2.ReadOnly = True
                    End If

                    Me.ActiveControl = txtSuperiorRemarks2
                    txtSuperiorRemarks2.Select(txtSuperiorRemarks2.Text.Trim.Length, 0)

                    'without immediate superior 2
                Else
                    cmbSuperiorStatus2.Enabled = False
                    cmbSuperiorName2.Enabled = False
                    txtSuperiorRemarks2.ReadOnly = True
                End If

                'last approver or manager
                If CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerId") = employeeId Then
                    If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value Then 'approved - lock editing
                        cmbManagerStatus.Enabled = False
                        cmbManagerName.Enabled = False
                        txtManagerRemarks.ReadOnly = True

                    Else 'not approved - open editing
                        cmbManagerStatus.Enabled = True
                        cmbManagerName.Enabled = True
                        txtManagerRemarks.ReadOnly = False
                    End If

                    Me.ActiveControl = txtManagerRemarks
                    txtManagerRemarks.Select(txtManagerRemarks.Text.Trim.Length, 0)
                Else
                    cmbManagerStatus.Enabled = False
                    cmbManagerName.Enabled = False
                    txtManagerRemarks.ReadOnly = True
                End If

            Else 'opened by requestor
                cmbSuperiorStatus1.Enabled = False
                cmbSuperiorStatus2.Enabled = False
                cmbManagerStatus.Enabled = False

                If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId1") Is DBNull.Value Then 'with immediate superior 1
                    If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Then 'approved - lock editing
                        cmbSuperiorName1.Enabled = False
                        txtSuperiorRemarks1.ReadOnly = True

                    Else 'if contains approval - lock editing
                        If (Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Or _
                            Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value) = True Then
                            cmbSuperiorName1.Enabled = False
                            txtSuperiorRemarks1.ReadOnly = True
                        Else
                            cmbSuperiorName1.Enabled = True
                            txtSuperiorRemarks1.ReadOnly = False
                        End If
                    End If

                    'without immediate superior 1
                Else
                    If (Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Or _
                        Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value) = True Then
                        cmbSuperiorName1.Enabled = False
                        txtSuperiorRemarks1.ReadOnly = True
                    Else
                        cmbSuperiorName1.Enabled = True
                        txtSuperiorRemarks1.ReadOnly = False
                    End If
                End If

                If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorId2") Is DBNull.Value Then 'with immediate superior 2
                    If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Then 'approved - lock editing
                        cmbSuperiorName2.Enabled = False
                        txtSuperiorRemarks2.ReadOnly = True

                    Else 'if contains approval - lock editing
                        If (Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Or _
                            Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value) = True Then
                            cmbSuperiorName2.Enabled = False
                            txtSuperiorRemarks2.ReadOnly = True
                        Else
                            cmbSuperiorName2.Enabled = True
                            txtSuperiorRemarks2.ReadOnly = False
                        End If
                    End If

                    'without immediate superior 2
                Else
                    If (Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Or _
                        Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value) = True Then
                        cmbSuperiorName2.Enabled = False
                        txtSuperiorRemarks2.ReadOnly = True
                    Else
                        cmbSuperiorName2.Enabled = True
                        txtSuperiorRemarks2.ReadOnly = False
                    End If
                End If

                'last approver or manager
                If Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value Then 'approved - lock editing
                    cmbManagerName.Enabled = False
                    txtManagerRemarks.ReadOnly = True

                Else 'if contains approval - lock editing
                    If (Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Or _
                        Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value) = True Then
                        cmbManagerName.Enabled = False
                        txtManagerRemarks.ReadOnly = True
                    Else
                        cmbManagerName.Enabled = True
                        txtManagerRemarks.ReadOnly = False
                    End If
                End If

                Me.ActiveControl = txtReason
                txtReason.Select(txtReason.Text.Length, 0)
            End If
        End If
    End Sub

    Private Sub frmLeaveFiling_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode.Equals(Keys.Enter) Then
            e.Handled = True
            If Not Me.ActiveControl.Equals(txtReason) Then
                Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
            End If
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

        Select Case cmbLeaveType.SelectedValue
            Case 1, 9, 14 'sick leave, ecq leave, ecq - for quarantine
                screenId = GetScreenId(cmbLeaveType.SelectedValue, employeeId)

                If screenId > 0 Then
                    GetScreeningByScreenId(screenId)

                    Me.ActiveControl = cmbSuperiorName1
                    cmbSuperiorName1.Select(cmbSuperiorName1.Text.Trim.Length, 0)
                Else
                    ResetForm()
                    MessageBox.Show("No health screening record found.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbLeaveType.Focus()
                    Return
                End If

            Case 2, 12, 13 'vacation leave, half day, undertime
                screenId = GetScreenId(cmbLeaveType.SelectedValue, employeeId)

                If screenId > 0 Then
                    GetScreeningByScreenId(screenId)

                    Me.ActiveControl = cmbSuperiorName1
                    cmbSuperiorName1.Select(cmbSuperiorName1.Text.Trim.Length, 0)
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

                    Me.ActiveControl = txtReason
                    txtReason.Select(txtReason.Text.Trim.Length, 0)
                End If

            Case 0
                ResetForm()

            Case Else
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

        End Select

        GetLeaveCredits(employeeId)
        GetLeaveBalance(employeeId)

        If cmbLeaveType.SelectedValue = 12 Then
            txtNumberOfDays.Text = 0.5
        Else
            txtNumberOfDays.Text = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
        End If

    End Sub

    Private Sub dtpFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged
        If cmbLeaveType.SelectedValue = 3 Then 'birthday leave
            dtpTo.Value = dtpFrom.Value
            txtNumberOfDays.Text = 1
        ElseIf cmbLeaveType.SelectedValue = 12 Then 'half day
            dtpTo.Value = dtpFrom.Value
            txtNumberOfDays.Text = 0.5
        Else
            txtNumberOfDays.Text = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
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
        If cmbLeaveType.SelectedValue = 3 Then 'birthday leave
            dtpFrom.Value = dtpTo.Value
            txtNumberOfDays.Text = 1
        ElseIf cmbLeaveType.SelectedValue = 12 Then 'half day
            dtpTo.Value = dtpFrom.Value
            txtNumberOfDays.Text = 0.5
        Else
            txtNumberOfDays.Text = GetTotalDays(dtpFrom.Value.Date, dtpTo.Value.Date)
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

    Private Sub cmbSuperiorName1_Validated(sender As Object, e As EventArgs) Handles cmbSuperiorName1.Validated
        If cmbSuperiorName1.Text.Trim.Length = 0 Then
            cmbSuperiorName1.SelectedValue = 0
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

    Private Sub cmbSuperiorName2_Validated(sender As Object, e As EventArgs) Handles cmbSuperiorName2.Validated
        If cmbSuperiorName2.Text.Trim.Length = 0 Then
            cmbSuperiorName2.SelectedValue = 0
        End If
    End Sub

    Private Sub cmbManagerName_Validated(sender As Object, e As EventArgs) Handles cmbManagerName.Validated
        If cmbManagerName.Text.Trim.Length = 0 Then
            cmbManagerName.SelectedValue = 0
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

            If cmbSuperiorName1.SelectedValue.Equals(cmbSuperiorName2.SelectedValue) AndAlso (Not cmbSuperiorName1.SelectedValue = 0 AndAlso Not cmbSuperiorName2.SelectedValue = 0) Then
                MessageBox.Show("Immediate superior 1 and 2 cannot be the same.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbSuperiorName1.Focus()
                Return
            ElseIf cmbSuperiorName1.SelectedValue.Equals(cmbManagerName.SelectedValue) AndAlso (Not cmbSuperiorName1.SelectedValue = 0 AndAlso Not cmbManagerName.SelectedValue = 0) Then
                MessageBox.Show("Immediate superior 1 and the last approver cannot be the same.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmbSuperiorName1.Focus()
                Return
            Else
                If cmbSuperiorName2.SelectedValue.Equals(cmbManagerName.SelectedValue) AndAlso (Not cmbSuperiorName2.SelectedValue = 0 AndAlso Not cmbManagerName.SelectedValue = 0) Then
                    MessageBox.Show("Immediate superior 2 and the last approver cannot be the same.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    cmbSuperiorName2.Focus()
                    Return
                End If
            End If

            If dtpFrom.Value.Date > dtpTo.Value.Date Then
                MessageBox.Show("Start date is later than end date.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                dtpFrom.Focus()
                Return
            End If

            If String.IsNullOrEmpty(txtReason.Text.Trim) AndAlso Not cmbLeaveType.SelectedValue = 3 Then
                MessageBox.Show("Reason is required.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                If cmbLeaveType.SelectedValue = 3 AndAlso IsBirthMonth(employeeId) = False Then
                    MessageBox.Show("Birthday leave must be within your birth month.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    dtpFrom.Focus()
                    Return
                End If

                Dim _newRowLeave As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.NewLeaveFilingRow

                With _newRowLeave
                    .DateCreated = DateTime.Now

                    Select Case cmbLeaveType.SelectedValue
                        Case 1, 2, 9, 12, 13, 14 'sick leave, vacation leave, ecq leave, half day leave, undertime, ecq - quarantine

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
                                .Reason = rowScreening.Reason.ToString.Trim
                            Else
                                .SetScreenIdNull()
                                .SetClinicIdNull()
                                .ClinicIsApproved = 0
                                .SetClinicApprovalDateNull()
                                .SetClinicRemarksNull()
                                .IsLateFiling = 0
                                .Reason = txtReason.Text.Trim
                            End If

                        Case Else
                            .SetScreenIdNull()
                            .ClinicIsApproved = 0
                            .SetClinicIdNull()
                            .SetClinicApprovalDateNull()
                            .SetClinicRemarksNull()
                            .IsLateFiling = 0

                            If cmbLeaveType.SelectedValue = 3 Then 'birthday leave
                                If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                                    .SetReasonNull()
                                Else
                                    .Reason = txtReason.Text.Trim
                                End If
                            Else
                                If String.IsNullOrEmpty(txtReason.Text.Trim) Then
                                    .SetReasonNull()
                                Else
                                    .Reason = txtReason.Text.Trim
                                End If
                            End If

                    End Select

                    .LeaveTypeId = cmbLeaveType.SelectedValue

                    If cmbSuperiorName1.SelectedValue = 0 Then 'no immediate superior 1
                        .SetSuperiorId1Null()

                        If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                            .SetSuperiorId2Null()
                            .RoutingStatusId = 3

                        Else 'with immediate superior 2
                            .RoutingStatusId = 4
                            .SuperiorId2 = cmbSuperiorName2.SelectedValue
                        End If

                    Else 'with immediate superior 1
                        .RoutingStatusId = 5
                        .SuperiorId1 = cmbSuperiorName1.SelectedValue

                        If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                            .SetSuperiorId2Null()
                        Else
                            .SuperiorId2 = cmbSuperiorName2.SelectedValue
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
                    .Quantity = txtNumberOfDays.Text.Trim
                    .IsEncoded = False

                    If String.IsNullOrEmpty(txtLeaveCredits.Text.Trim) Then
                        .LeaveCredits = 0
                    Else
                        .LeaveCredits = txtLeaveCredits.Text.Trim()
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

                'process management
                With _newRowLeave
                    If cmbSuperiorName1.SelectedValue = 0 Then 'no immediate superior 1
                        .SetSuperiorId1Null()

                        If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                            'send to manager directly if no immediate superior 1 and 2
                            If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                            cmbManagerName.SelectedValue, _
                                                            cmbLeaveType.Text, _
                                                            StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                            txtDepartment.Text.Trim, _
                                                            dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                            txtReason.Text.Trim)
                            Else
                                _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                            cmbManagerName.SelectedValue, _
                                                            cmbLeaveType.Text, _
                                                            StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                            txtDepartment.Text.Trim, _
                                                            dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                            txtReason.Text.Trim)
                            End If

                        Else 'with immediate superior 2
                            If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                            cmbSuperiorName2.SelectedValue, _
                                                            cmbLeaveType.Text, _
                                                            StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                            txtDepartment.Text.Trim, _
                                                            dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                            txtReason.Text.Trim)
                            Else
                                _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                            cmbSuperiorName2.SelectedValue, _
                                                            cmbLeaveType.Text, _
                                                            StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                            txtDepartment.Text.Trim, _
                                                            dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                            txtReason.Text.Trim)
                            End If
                        End If

                    Else 'with immediate superior 1
                        If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                            _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                        cmbSuperiorName1.SelectedValue, _
                                                        cmbLeaveType.Text, _
                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                        txtDepartment.Text.Trim, _
                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                        txtReason.Text.Trim)
                        Else
                            _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                        cmbSuperiorName1.SelectedValue, _
                                                        cmbLeaveType.Text, _
                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                        txtDepartment.Text.Trim, _
                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                        txtReason.Text.Trim)
                        End If
                    End If
                End With

                Me.dsLeaveFiling.AcceptChanges()

            Else 'existing file
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(leaveFileId)

                With _leaveFilingRow
                    'immediate superior 1
                    If .IsSuperiorId1Null = False AndAlso .SuperiorId1 = employeeId Then
                        If cmbSuperiorStatus1.SelectedValue = 0 AndAlso .IsSuperiorApprovalDate1Null = True Then 'remarks only
                            If String.IsNullOrEmpty(txtSuperiorRemarks1.Text.Trim) = False Then
                                If SaveRemarksOnly() = Windows.Forms.DialogResult.Yes Then
                                    .SuperiorRemarks1 = txtSuperiorRemarks1.Text.Trim

                                    _frmMain.SendRemarksNofitication(requestorEmployeeId, _
                                                                     "Immediate Superior 1", _
                                                                     cmbSuperiorName1.Text.Trim, _
                                                                     txtSuperiorRemarks1.Text.Trim, _
                                                                     cmbLeaveType.Text, _
                                                                     dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                     dtpTo.Value.Date.ToString("MMMM dd, yyyy"))
                                Else
                                    Return
                                End If
                            Else
                                .SetSuperiorRemarks1Null()
                            End If

                        Else
                            If cmbSuperiorStatus1.SelectedValue = 1 AndAlso .IsSuperiorApprovalDate1Null = True Then 'approved
                                If Confirmation(1) = Windows.Forms.DialogResult.Yes Then
                                    If .IsSuperiorId2Null = True Then
                                        .RoutingStatusId = 3

                                        If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                            _frmMain.SendEmailApprovers(leaveFileId, _
                                                                        cmbManagerName.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        Else
                                            _frmMain.SendEmailApprovers(leaveFileId, _
                                                                        cmbManagerName.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        End If

                                    Else
                                        .RoutingStatusId = 4

                                        If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                            _frmMain.SendEmailApprovers(leaveFileId, _
                                                                        cmbSuperiorName2.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        Else
                                            _frmMain.SendEmailApprovers(leaveFileId, _
                                                                        cmbSuperiorName2.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        End If
                                    End If

                                    .SuperiorIsApproved1 = 1
                                Else
                                    Return
                                End If

                                .SuperiorApprovalDate1 = DateTime.Now
                                If String.IsNullOrEmpty(txtSuperiorRemarks1.Text.Trim) Then
                                    .SetSuperiorRemarks1Null()
                                Else
                                    .SuperiorRemarks1 = txtSuperiorRemarks1.Text.Trim
                                End If

                            ElseIf cmbSuperiorStatus1.SelectedValue = 2 AndAlso .IsSuperiorApprovalDate1Null = True Then 'disapproved
                                If Confirmation(2) = Windows.Forms.DialogResult.Yes Then
                                    If .IsSuperiorId2Null = True Then
                                        .RoutingStatusId = 3

                                        If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                            _frmMain.SendEmailApprovers(leaveFileId, _
                                                                        cmbManagerName.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        Else
                                            _frmMain.SendEmailApprovers(leaveFileId, _
                                                                        cmbManagerName.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        End If
                                    Else
                                        .RoutingStatusId = 4

                                        If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                            _frmMain.SendEmailApprovers(leaveFileId, _
                                                                        cmbSuperiorName2.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        Else
                                            _frmMain.SendEmailApprovers(leaveFileId, _
                                                                        cmbSuperiorName2.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        End If
                                    End If

                                    .SuperiorIsApproved1 = 0
                                Else
                                    Return
                                End If

                                .SuperiorApprovalDate1 = DateTime.Now
                                If String.IsNullOrEmpty(txtSuperiorRemarks1.Text.Trim) Then
                                    .SetSuperiorRemarks1Null()
                                Else
                                    .SuperiorRemarks1 = txtSuperiorRemarks1.Text.Trim
                                End If

                            ElseIf cmbSuperiorStatus1.SelectedValue = 3 AndAlso .IsManagerApprovalDateNull = True Then 'returned for revision
                                .RoutingStatusId = 6
                                .SuperiorIsApproved1 = 0
                                .SetSuperiorApprovalDate1Null()
                                .SuperiorIsApproved2 = 0
                                .SetSuperiorApprovalDate2Null()
                                .ManagerIsApproved = 0
                                .SetManagerApprovalDateNull()
                                .ModifiedBy = employeeId
                                .ModifiedDate = DateTime.Now

                                _frmMain.SendEmailReturned(requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                            End If
                        End If

                        'immediate superior 2
                    ElseIf .IsSuperiorId2Null = False AndAlso .SuperiorId2 = employeeId Then
                        If cmbSuperiorStatus2.SelectedValue = 0 AndAlso .IsSuperiorApprovalDate2Null = True Then 'remarks only
                            If String.IsNullOrEmpty(txtSuperiorRemarks2.Text.Trim) = False Then
                                If SaveRemarksOnly() = Windows.Forms.DialogResult.Yes Then
                                    .SuperiorRemarks2 = txtSuperiorRemarks2.Text.Trim

                                    _frmMain.SendRemarksNofitication(requestorEmployeeId, _
                                                                     "Immediate Superior 2", _
                                                                     cmbSuperiorName2.Text.Trim, _
                                                                     txtSuperiorRemarks2.Text.Trim, _
                                                                     cmbLeaveType.Text, _
                                                                     dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                     dtpTo.Value.Date.ToString("MMMM dd, yyyy"))
                                Else
                                    Return
                                End If
                            Else
                                .SetSuperiorRemarks2Null()
                            End If

                        Else
                            If cmbSuperiorStatus2.SelectedValue = 1 AndAlso .IsSuperiorApprovalDate2Null = True Then 'approved
                                If Confirmation(1) = Windows.Forms.DialogResult.Yes Then
                                    .RoutingStatusId = 3
                                    .SuperiorIsApproved2 = 1

                                    If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                        _frmMain.SendEmailApprovers(leaveFileId, _
                                                                    cmbManagerName.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    Else
                                        _frmMain.SendEmailApprovers(leaveFileId, _
                                                                    cmbManagerName.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    End If
                                Else
                                    Return
                                End If

                                .SuperiorApprovalDate2 = DateTime.Now
                                If String.IsNullOrEmpty(txtSuperiorRemarks2.Text.Trim) Then
                                    .SetSuperiorRemarks2Null()
                                Else
                                    .SuperiorRemarks2 = txtSuperiorRemarks2.Text.Trim
                                End If

                            ElseIf cmbSuperiorStatus2.SelectedValue = 2 AndAlso .IsSuperiorApprovalDate2Null = True Then 'disapproved
                                If Confirmation(2) = Windows.Forms.DialogResult.Yes Then
                                    .RoutingStatusId = 3
                                    .SuperiorIsApproved2 = 0

                                    If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                        _frmMain.SendEmailApprovers(leaveFileId, _
                                                                    cmbManagerName.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    Else
                                        _frmMain.SendEmailApprovers(leaveFileId, _
                                                                    cmbManagerName.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    End If
                                Else
                                    Return
                                End If

                                .SuperiorApprovalDate2 = DateTime.Now
                                If String.IsNullOrEmpty(txtSuperiorRemarks2.Text.Trim) Then
                                    .SetSuperiorRemarks2Null()
                                Else
                                    .SuperiorRemarks2 = txtSuperiorRemarks2.Text.Trim
                                End If

                            ElseIf cmbSuperiorStatus2.SelectedValue = 3 AndAlso .IsManagerApprovalDateNull = True Then
                                .RoutingStatusId = 6
                                .SuperiorIsApproved1 = 0
                                .SetSuperiorApprovalDate1Null()
                                .SuperiorIsApproved2 = 0
                                .SetSuperiorApprovalDate2Null()
                                .ManagerIsApproved = 0
                                .SetManagerApprovalDateNull()
                                .ModifiedBy = employeeId
                                .ModifiedDate = DateTime.Now

                                _frmMain.SendEmailReturned(requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                            End If
                        End If

                        'manager/last approver
                    ElseIf .IsManagerIdNull = False AndAlso .ManagerId = employeeId Then
                        If cmbManagerStatus.SelectedValue = 0 AndAlso .IsManagerApprovalDateNull = True Then
                            If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) = False Then
                                If SaveRemarksOnly() = Windows.Forms.DialogResult.Yes Then
                                    .ManagerRemarks = txtManagerRemarks.Text.Trim

                                    _frmMain.SendRemarksNofitication(requestorEmployeeId, _
                                                                     "Manager / Last Approver", _
                                                                     cmbManagerName.Text.Trim, _
                                                                     txtManagerRemarks.Text.Trim, _
                                                                     cmbLeaveType.Text, _
                                                                     dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                     dtpTo.Value.Date.ToString("MMMM dd, yyyy"))
                                Else
                                    Return
                                End If
                            Else
                                .SetManagerRemarksNull()
                            End If

                        Else 'approved or disapproved
                            If cmbManagerStatus.SelectedValue = 1 AndAlso .IsManagerApprovalDateNull = True Then
                                If Confirmation(1) = Windows.Forms.DialogResult.Yes Then
                                    .RoutingStatusId = 2
                                    .ManagerIsApproved = 1

                                    _frmMain.SendEmailRequestor(True, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                    If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                        _frmMain.SendEmailHr(leaveFileId, _
                                                             True, _
                                                             cmbLeaveType.Text, _
                                                             StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                             txtDepartment.Text.Trim, _
                                                             dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                             txtReason.Text.Trim)
                                    Else
                                        _frmMain.SendEmailHr(leaveFileId, _
                                                             True, _
                                                             cmbLeaveType.Text, _
                                                             StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                             txtDepartment.Text.Trim, _
                                                             dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                             txtReason.Text.Trim)
                                    End If
                                Else
                                    Return
                                End If

                                .ManagerApprovalDate = DateTime.Now
                                If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) Then
                                    .SetManagerRemarksNull()
                                Else
                                    .ManagerRemarks = txtManagerRemarks.Text.Trim
                                End If

                            ElseIf cmbManagerStatus.SelectedValue = 2 AndAlso .IsManagerApprovalDateNull = True Then
                                If Confirmation(2) = Windows.Forms.DialogResult.Yes Then
                                    .RoutingStatusId = 7
                                    .ManagerIsApproved = 0

                                    _frmMain.SendEmailRequestor(False, requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)

                                    If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                        _frmMain.SendEmailHr(leaveFileId, _
                                                             False, _
                                                             cmbLeaveType.Text, _
                                                             StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                             txtDepartment.Text.Trim, _
                                                             dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                             txtReason.Text.Trim)
                                    Else
                                        _frmMain.SendEmailHr(leaveFileId, _
                                                             False, _
                                                             cmbLeaveType.Text, _
                                                             StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                             txtDepartment.Text.Trim, _
                                                             dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                             txtReason.Text.Trim)
                                    End If
                                Else
                                    Return
                                End If

                                .ManagerApprovalDate = DateTime.Now
                                If String.IsNullOrEmpty(txtManagerRemarks.Text.Trim) Then
                                    .SetManagerRemarksNull()
                                Else
                                    .ManagerRemarks = txtManagerRemarks.Text.Trim
                                End If

                            ElseIf cmbManagerStatus.SelectedValue = 3 AndAlso .IsManagerApprovalDateNull = True Then
                                .RoutingStatusId = 6
                                .SuperiorIsApproved1 = 0
                                .SetSuperiorApprovalDate1Null()
                                .SuperiorIsApproved2 = 0
                                .SetSuperiorApprovalDate2Null()
                                .ManagerIsApproved = 0
                                .SetManagerApprovalDateNull()
                                .ModifiedBy = employeeId
                                .ModifiedDate = DateTime.Now

                                _frmMain.SendEmailReturned(requestorEmployeeId, cmbLeaveType.Text, dtpFrom.Value.Date, dtpTo.Value.Date)
                            End If
                        End If

                    Else
                        'opened by requestor or others (hr)
                        If requestorEmployeeId = employeeId AndAlso .ModifiedBy = employeeId Then
                            If Not cmbClinicName.SelectedValue = 0 Then
                                .StartDate = dtpFrom.Value.Date
                                .EndDate = dtpTo.Value.Date
                                .Reason = txtReason.Text.Trim
                                .ModifiedBy = employeeId
                                .ModifiedDate = DateTime.Now
                            End If

                            If cmbSuperiorName1.SelectedValue = 0 Then 'no immediate superior 1
                                .SetSuperiorId1Null()

                                If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                                    .SetSuperiorId2Null()
                                    .RoutingStatusId = 3

                                    'send to manager directly if no immediate superior 1 and 2
                                    If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                        _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                    cmbManagerName.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    Else
                                        _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                    cmbManagerName.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    End If

                                ElseIf Not cmbSuperiorName2.SelectedValue = 0 And .IsSuperiorApprovalDate2Null = True Then 'with immediate superior 2
                                    .RoutingStatusId = 4
                                    .SuperiorId2 = cmbSuperiorName2.SelectedValue

                                    If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                        _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                    cmbSuperiorName2.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    Else
                                        _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                    cmbSuperiorName2.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    End If
                                End If

                            ElseIf Not cmbSuperiorName1.SelectedValue = 0 And .IsSuperiorApprovalDate1Null = True Then 'with immediate superior 1
                                .RoutingStatusId = 5
                                .SuperiorId1 = cmbSuperiorName1.SelectedValue

                                If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                                    .SetSuperiorId2Null()
                                Else
                                    .SuperiorId2 = cmbSuperiorName2.SelectedValue
                                End If

                                If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                    _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                cmbSuperiorName1.SelectedValue, _
                                                                cmbLeaveType.Text, _
                                                                StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                txtDepartment.Text.Trim, _
                                                                dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                txtReason.Text.Trim)
                                Else
                                    _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                cmbSuperiorName1.SelectedValue, _
                                                                cmbLeaveType.Text, _
                                                                StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                txtDepartment.Text.Trim, _
                                                                dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                txtReason.Text.Trim)
                                End If
                            End If

                        ElseIf requestorEmployeeId = employeeId AndAlso .ModifiedBy <> employeeId Then
                            With _leaveFilingRow
                                Select Case cmbLeaveType.SelectedValue
                                    Case 1, 2, 9, 11, 12 'sick leave, vacation leave, ecq leave, half day leave, undertime

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

                                            .Reason = rowScreening.Reason.ToString.Trim
                                        Else
                                            .SetScreenIdNull()
                                            .ClinicIsApproved = 0
                                            .SetClinicIdNull()
                                            .SetClinicApprovalDateNull()
                                            .SetClinicRemarksNull()
                                            .IsLateFiling = 0

                                            .Reason = txtReason.Text.Trim
                                        End If

                                    Case 3  'bday leave
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

                                    Case Else
                                        .SetScreenIdNull()
                                        .ClinicIsApproved = 0
                                        .SetClinicIdNull()
                                        .SetClinicApprovalDateNull()
                                        .SetClinicRemarksNull()
                                        .IsLateFiling = 0

                                        .Reason = txtReason.Text.Trim
                                End Select

                                .LeaveTypeId = cmbLeaveType.SelectedValue

                                If cmbSuperiorName1.SelectedValue = 0 Then 'no immediate superior 1
                                    .SetSuperiorId1Null()

                                    If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                                        .SetSuperiorId2Null()
                                        .RoutingStatusId = 3

                                    Else 'with immediate superior 2
                                        .RoutingStatusId = 4
                                        .SuperiorId2 = cmbSuperiorName2.SelectedValue
                                    End If

                                Else 'with immediate superior 1
                                    .RoutingStatusId = 5
                                    .SuperiorId1 = cmbSuperiorName1.SelectedValue

                                    If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                                        .SetSuperiorId2Null()
                                    Else
                                        .SuperiorId2 = cmbSuperiorName2.SelectedValue
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
                                .IsEncoded = False

                                If String.IsNullOrEmpty(txtLeaveCredits.Text.Trim) Then
                                    .LeaveCredits = 0
                                Else
                                    .LeaveCredits = txtLeaveCredits.Text.Trim()
                                End If

                                If String.IsNullOrEmpty(txtBalance.Text.Trim) Then
                                    .LeaveBalance = 0
                                Else
                                    .LeaveBalance = txtBalance.Text.Trim
                                End If

                                .ModifiedBy = employeeId
                                .ModifiedDate = DateTime.Now

                                If cmbSuperiorName1.SelectedValue = 0 Then 'no immediate superior 1
                                    .SetSuperiorId1Null()

                                    If cmbSuperiorName2.SelectedValue = 0 Then 'no immediate superior 2
                                        'send to manager directly if no immediate superior 1 and 2
                                        If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                            _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                        cmbManagerName.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        Else
                                            _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                        cmbManagerName.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        End If

                                    Else 'with immediate superior 2
                                        If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                            _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                        cmbSuperiorName2.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        Else
                                            _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                        cmbSuperiorName2.SelectedValue, _
                                                                        cmbLeaveType.Text, _
                                                                        StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                        txtDepartment.Text.Trim, _
                                                                        dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                        txtReason.Text.Trim)
                                        End If
                                    End If

                                Else 'with immediate superior 1
                                    If dtpFrom.Value.Date.Equals(dtpTo.Value.Date) Then
                                        _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                    cmbSuperiorName1.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    Else
                                        _frmMain.SendEmailApprovers(.LeaveFileId, _
                                                                    cmbSuperiorName1.SelectedValue, _
                                                                    cmbLeaveType.Text, _
                                                                    StrConv(txtName.Text.Trim, VbStrConv.ProperCase), _
                                                                    txtDepartment.Text.Trim, _
                                                                    dtpFrom.Value.Date.ToString("MMMM dd, yyyy") & " - " & dtpTo.Value.Date.ToString("MMMM dd, yyyy"), _
                                                                    txtReason.Text.Trim)
                                    End If
                                End If
                            End With

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
        If leaveFileId <> 0 Then
            If (Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate1") Is DBNull.Value Or _
                Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("SuperiorApprovalDate2") Is DBNull.Value Or _
                Not CType(Me.bsLeaveFiling.Current, DataRowView).Item("ManagerApprovalDate") Is DBNull.Value) Then
                MessageBox.Show("Not allowed to delete approved/disapproved leave.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Else
                If MessageBox.Show("Delete this record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                    'set screening record isused flag to false
                    Select Case cmbLeaveType.SelectedValue
                        Case 1, 2, 9, 12, 13, 14
                            If Not cmbClinicName.SelectedValue = 0 Then
                                Me.adpScreening.FillByScreenId(Me.dsLeaveFiling.Screening, screenId)
                                Dim _screeningRow As ScreeningRow = Me.dsLeaveFiling.Screening.FindByScreenId(screenId)
                                With _screeningRow
                                    .IsUsed = 0
                                End With
                                Me.adpScreening.Update(Me.dsLeaveFiling.Screening)
                                Me.dsLeaveFiling.AcceptChanges()
                            End If
                    End Select

                    Me.bsLeaveFiling.RemoveCurrent()
                    Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                    Me.dsLeaveFiling.AcceptChanges()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If
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
    'get requestor's information
    Private Sub FillEmployeeInformation(ByVal _employeeId As Integer)
        Try
            Dim _prm(0) As SqlParameter
            _prm(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prm(0).Value = _employeeId

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdEmployee", CommandType.StoredProcedure, _prm)

            While _reader.Read
                txtIdNumber.Text = _reader.Item("EmployeeCode").ToString.Trim
                txtName.Text = _reader.Item("EmployeeName").ToString.Trim

                If Not _reader.Item("TeamName") Is DBNull.Value Then
                    If _reader.Item("DepartmentName").ToString.Trim.Equals(_reader.Item("TeamName").ToString.Trim) Then
                        txtDepartment.Text = _reader.Item("DepartmentName").ToString.Trim
                    Else
                        txtDepartment.Text = _reader.Item("DepartmentName").ToString.Trim & " - " & _reader.Item("TeamName").ToString.Trim
                    End If
                    teamId = _reader.Item("TeamId")
                Else
                    txtDepartment.Text = _reader.Item("DepartmentName").ToString.Trim
                End If

                txtPosition.Text = _reader.Item("PositionName").ToString.Trim
                txtEmpStatus.Text = _reader.Item("EmploymentTypeName").ToString.Trim
                txtDateHired.Text = CDate(_reader.Item("DateHired")).ToString("MMMM dd, yyyy")

                If Not _reader.Item("EmailAddress") Is DBNull.Value Then
                    emailAddress = _reader.Item("EmailAddress").ToString
                Else
                    emailAddress = String.Empty
                End If

                If Not _reader.Item("MobileNo") Is DBNull.Value Then
                    mobileNumber = _reader.Item("MobileNo")
                Else
                    mobileNumber = String.Empty
                End If

                If Not _reader.Item("NbcEmailAddress") Is DBNull.Value Then
                    nbcEmailAddress = _reader.Item("NbcEmailAddress")
                Else
                    nbcEmailAddress = String.Empty
                End If

                If Not _reader.Item("BirthDate") Is DBNull.Value Then
                    birthDate = _reader.Item("BirthDate")
                Else
                    birthDate = Nothing
                End If

                If Not _reader.Item("MaritalStatusId") Is DBNull.Value Then
                    maritalStatusId = _reader.Item("MaritalStatusId")
                Else
                    maritalStatusId = 0
                End If

                If Not _reader.Item("GenderId") Is DBNull.Value Then
                    genderId = _reader.Item("GenderId")
                Else
                    genderId = 0
                End If

                'exising record
                If leaveFileId <> 0 Then
                    If Not _reader.Item("TeamName") Is DBNull.Value Then
                        requestorTeamId = _reader.Item("TeamId").ToString
                    End If

                    requestorEmploymentTypeId = _reader.Item("EmploymentTypeId").ToString
                    requestorDepartmentId = _reader.Item("DepartmentId").ToString
                    requestorPositionId = _reader.Item("PositionId").ToString
                    requestorGenderId = _reader.Item("GenderId").ToString
                End If
            End While
            _reader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillLeaveTypes(ByVal _employmentTypeId As Integer, ByVal _genderId As Integer, ByVal _positionId As Integer)
        Try
            Dim _prm(2) As SqlParameter
            _prm(0) = New SqlParameter("@EmploymentTypeId", SqlDbType.Int)
            _prm(0).Value = _employmentTypeId
            _prm(1) = New SqlParameter("@GenderId", SqlDbType.Int)
            _prm(1).Value = _genderId
            _prm(2) = New SqlParameter("@PositionId", SqlDbType.Int)
            _prm(2).Value = _positionId

            dbLeaveFiling.FillCmbWithCaption("RdLeaveType", CommandType.StoredProcedure, "LeaveTypeId", "LeaveTypeName", cmbLeaveType, "< Select Leave Type >", _prm)
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

            'allows to pick unfit to work record for payroll purposes
            _screenId = dbLeaveFiling.ExecuteScalar("SELECT TOP 1 (ScreenId) FROM Screening WHERE EmployeeId = @EmployeeId AND " & _
                                                    "LeaveTypeId = @LeaveTypeId AND IsUsed = 0 ORDER BY ScreenId DESC", _
                                                    CommandType.Text, _prmScreen)

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

            If rowScreening.IsFitToWork = True Then
                txtClinicStatus.Text = "Fit To Work"
            Else
                txtClinicStatus.Text = "Unfit To Work"
            End If

            Dim _prmEmployeeCode(0) As SqlParameter
            _prmEmployeeCode(0) = New SqlParameter("@EmployeeId", SqlDbType.VarChar)
            _prmEmployeeCode(0).Value = rowScreening.ScreenBy.ToString

            Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdClinicCombined", CommandType.StoredProcedure, _prmEmployeeCode)

            While _reader.Read
                cmbClinicName.SelectedValue = _reader.Item("EmployeeId")
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

    'get leave credits
    Private Sub GetLeaveCredits(ByVal _empId As Integer)
        Try
            Dim _leaveCredits As Double = 0
            Dim _prmCredits(1) As SqlParameter
            _prmCredits(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmCredits(0).Value = _empId
            _prmCredits(1) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
            _prmCredits(1).Value = cmbLeaveType.SelectedValue

            '_leaveCredits = dbJeonsoft.ExecuteScalar("SELECT Quantity FROM dbo.tblEmployeeLeaves WHERE EmployeeId = @EmployeeId AND LeaveTypeId = @LeaveTypeId", _
            '                                         CommandType.Text, _prmCredits)

            _leaveCredits = dbJeonsoft.ExecuteScalar("SELECT TOP 1 EndBalance FROM dbo.tblLeaveLedger WHERE YEAR(Date) = YEAR(GETDATE()) AND " & _
                                                     "EmployeeId = @EmployeeId AND LeaveTypeId = @LeaveTypeId ORDER BY Date ASC", _
                                                     CommandType.Text, _prmCredits)
            txtLeaveCredits.Text = _leaveCredits
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'get leave balance
    Private Sub GetLeaveBalance(ByVal _empId As Integer)
        Try
            Dim _leaveBalance As Double = 0
            Dim _prmBalance(3) As SqlParameter
            _prmBalance(0) = New SqlParameter("@CompanyId", SqlDbType.Int)
            _prmBalance(0).Value = 1
            _prmBalance(1) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmBalance(1).Value = _empId
            _prmBalance(2) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
            _prmBalance(2).Value = cmbLeaveType.SelectedValue
            _prmBalance(3) = New SqlParameter("@Date", SqlDbType.Date)
            _prmBalance(3).Value = DBNull.Value

            _leaveBalance = dbJeonsoft.ExecuteFunction(Of Double)("dbo.fnGetLeaveBalance", _prmBalance)
            txtBalance.Text = _leaveBalance
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetForm()
        cmbLeaveType.SelectedValue = 0
        dtpFrom.Enabled = False
        dtpFrom.Value = Date.Now.Date
        dtpTo.Enabled = False
        dtpTo.Value = Date.Now.Date
        txtReason.Text = String.Empty
        txtReason.ReadOnly = True

        txtLeaveCredits.Text = String.Empty
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

    Private Sub FillApproverStatus()
        dictSuperior1.Add(" < Select Status > ", 0)
        dictSuperior1.Add("Approved", 1)
        dictSuperior1.Add("Disapproved", 2)
        dictSuperior1.Add("Return to Requestor for Revision", 3)
        cmbSuperiorStatus1.DisplayMember = "Key"
        cmbSuperiorStatus1.ValueMember = "Value"
        cmbSuperiorStatus1.DataSource = New BindingSource(dictSuperior1, Nothing)

        dictSuperior2.Add(" < Select Status > ", 0)
        dictSuperior2.Add("Approved", 1)
        dictSuperior2.Add("Disapproved", 2)
        dictSuperior2.Add("Return to Requestor for Revision", 3)
        cmbSuperiorStatus2.DisplayMember = "Key"
        cmbSuperiorStatus2.ValueMember = "Value"
        cmbSuperiorStatus2.DataSource = New BindingSource(dictSuperior2, Nothing)

        dictManager.Add(" < Select Status > ", 0)
        dictManager.Add("Approved", 1)
        dictManager.Add("Disapproved", 2)
        dictManager.Add("Return to Requestor for Revision", 3)
        cmbManagerStatus.DisplayMember = "Key"
        cmbManagerStatus.ValueMember = "Value"
        cmbManagerStatus.DataSource = New BindingSource(dictManager, Nothing)
    End Sub

    Private Sub FillApproversNew(ByVal _employeeId As Integer)
        Try
            'immediate superior 1 - clerk, staff, line leader, sr line leader, asv, sr engr, sv, sr staff
            Dim _prmSup1(0) As SqlParameter
            _prmSup1(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmSup1(0).Value = _employeeId
            dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "EmployeeId", "EmployeeName", cmbSuperiorName1, "< None >", _prmSup1)

            'immediate superior 2 - sr mngr, mngr, asst mngr, sv, asv, sr engr, sr staff, sr line leader, sr technician, sr nurse
            Dim _prmSup2(0) As SqlParameter
            _prmSup2(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prmSup2(0).Value = _employeeId
            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "EmployeeId", "EmployeeName", cmbSuperiorName2, "< None >", _prmSup2)

            'last approver - dgm, sr mngr, mngr, asst mngr, sv, asv
            dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "EmployeeId", "EmployeeName", cmbManagerName, "< None >")
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillApproversOld(ByVal _employeeId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _positionId As Integer)
        Try
            'immediate superior 1 - clerk, staff, line leader, sr line leader, asv, sr engr, sv, sr staff
            dbLeaveFiling.FillCmbWithCaption("RdSuperior1", CommandType.StoredProcedure, "EmployeeId", "EmployeeName", cmbSuperiorName1, "< None >")

            'immediate superior 2 - sr mngr, mngr, asst mngr, sv, asv, sr engr, sr staff, sr line leader, sr technician, sr nurse
            dbLeaveFiling.FillCmbWithCaption("RdSuperior2", CommandType.StoredProcedure, "EmployeeId", "EmployeeName", cmbSuperiorName2, "< None >")

            'last approver - dgm, sr mngr, mngr, asst mngr, sv, asv
            dbLeaveFiling.FillCmbWithCaption("RdManager", CommandType.StoredProcedure, "EmployeeId", "EmployeeName", cmbManagerName, "< None >")
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Functions"
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

    Private Function SaveRemarksOnly() As DialogResult
        If MessageBox.Show("No selected status. Would you like to save only the remarks/comments?", "", _
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                           MessageBoxDefaultButton.Button2) = DialogResult.Yes Then

            Return Windows.Forms.DialogResult.Yes
        Else
            Return Windows.Forms.DialogResult.No
        End If
    End Function

    Private Function Confirmation(ByVal _statusId As Integer) As DialogResult
        If _statusId = 1 Then
            If MessageBox.Show("Are you sure you want to approve this record?" & Environment.NewLine & "This cannot be modified.", "", _
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Return Windows.Forms.DialogResult.Yes
            Else
                Return Windows.Forms.DialogResult.No
            End If
        Else
            If MessageBox.Show("Are you sure you want to disapprove this record?" & Environment.NewLine & "This cannot be modified.", "", _
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Return Windows.Forms.DialogResult.Yes
            Else
                Return Windows.Forms.DialogResult.No
            End If
        End If
    End Function
#End Region

End Class
