Imports System.Reflection
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsJeonsoftTableAdapters
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters

Public Class frmLeaveList
    Private clsConnection As New clsConnection
    Private clsMain As New Main
    Private clsJeonsoft As New SqlDbMethod(clsConnection.JeonsoftConnection)
    Private clsLocal As New SqlDbMethod(clsConnection.LocalConnection)

    Private dictionary As New Dictionary(Of String, Integer)
    'access control
    Private employeeId As Integer = 0
    Private positionId As Integer = 0
    Private departmentId As Integer = 0
    Private teamId As Integer = 0
    Private employmentTypeId As Integer = 0

    Private indexScroll As Integer = 0
    Private indexPosition As Integer = 0

    'access dataset
    Private dsLeaveFiling As New dsLeaveFiling
    Private dsJeonsoft As New dsJeonsoft

    Private bsLeaveFiling As New BindingSource
    Private bsLeaveType As New BindingSource
    Private bsEmployee As New BindingSource
    Private bsEncoder As New BindingSource
    Private bsRoutingStatus As New BindingSource

    Private adpLeaveFiling As New LeaveFilingTableAdapter
    Private adpLeaveType As New LeaveTypesTableAdapter
    Private adpRoutingStatus As New RoutingStatusTableAdapter
    Private adpEmployees As New tblEmployeesTableAdapter

    Private dtLeaveFiling As New LeaveFilingDataTable
    Private dtLeaveType As New LeaveTypesDataTable
    Private dtRoutingStatus As New RoutingStatusDataTable
    Private dtEmployeeName As New tblEmployeesDataTable
    Private dtEncoderName As New tblEmployeesDataTable

    Private isLeader As Boolean = False
    Private isClinic As Boolean = False
    Private isSuperior As Boolean = False
    Private isManager As Boolean = False

    Public Sub New(ByVal _employeeId As Integer, ByVal _positionId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _employmentTypeId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        positionId = _positionId
        departmentId = _departmentId
        teamId = _teamId
        employmentTypeId = _employmentTypeId

        'Me.adpLeaveFiling.FillByLeaveFilingId(Me.dsLeaveFiling.LeaveFiling, Nothing)
        Me.adpRoutingStatus.Fill(Me.dsLeaveFiling.RoutingStatus)
        Me.adpLeaveType.Fill(Me.dsLeaveFiling.LeaveTypes)
        Me.adpEmployees.Fill(Me.dsJeonsoft.tblEmployees)
    End Sub

    Private Sub frmMntTrxApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.bsLeaveFiling.DataSource = Me.dsLeaveFiling
        Me.bsLeaveFiling.DataMember = dtLeaveFiling.TableName
        Me.bsLeaveFiling.Filter = String.Format("EmployeeId = {0}", employeeId)
        Me.bsLeaveFiling.Sort = "LastModifiedDate DESC, LeaveFilingId DESC"
        dgvList.AutoGenerateColumns = False
        dgvList.DataSource = Me.bsLeaveFiling

        Me.bsLeaveType.DataSource = Me.dsLeaveFiling
        Me.bsLeaveType.DataMember = dtLeaveType.TableName

        Dim _colLeaveType As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
        _colLeaveType.DataPropertyName = "LeaveTypeId"
        _colLeaveType.HeaderText = "Leave Type"
        _colLeaveType.DataSource = Me.bsLeaveType
        _colLeaveType.ValueMember = "LeaveTypeId"
        _colLeaveType.DisplayMember = "LeaveTypeName"
        _colLeaveType.Width = 125
        _colLeaveType.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        _colLeaveType.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        _colLeaveType.SortMode = DataGridViewColumnSortMode.Automatic
        dgvList.Columns.Insert(2, _colLeaveType)

        Me.bsEncoder.DataSource = Me.dsJeonsoft
        Me.bsEncoder.DataMember = dtEncoderName.TableName

        Dim _colEncoder As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
        _colEncoder.DataPropertyName = "EncoderId"
        _colEncoder.HeaderText = "Encoder"
        _colEncoder.DataSource = Me.bsEncoder
        _colEncoder.ValueMember = "Id"
        _colEncoder.DisplayMember = "FirstName"
        _colEncoder.Width = 120
        _colEncoder.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        _colEncoder.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        _colEncoder.SortMode = DataGridViewColumnSortMode.Automatic
        dgvList.Columns.Insert(7, _colEncoder)

        Me.bsRoutingStatus.DataSource = Me.dsLeaveFiling
        Me.bsRoutingStatus.DataMember = dtRoutingStatus.TableName

        Dim _colRoutingStatus As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
        _colRoutingStatus.DataPropertyName = "RoutingStatusId"
        _colRoutingStatus.HeaderText = "Status"
        _colRoutingStatus.DataSource = Me.bsRoutingStatus
        _colRoutingStatus.ValueMember = "RoutingStatusId"
        _colRoutingStatus.DisplayMember = "RoutingStatusName"
        _colRoutingStatus.Width = 190
        _colRoutingStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        _colRoutingStatus.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        _colRoutingStatus.SortMode = DataGridViewColumnSortMode.Automatic
        dgvList.Columns.Insert(10, _colRoutingStatus)

        Me.bsEmployee.DataSource = Me.dsJeonsoft
        Me.bsEmployee.DataMember = dtEmployeeName.TableName

        Dim _colEmployee As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
        _colEmployee.DataPropertyName = "EmployeeId"
        _colEmployee.HeaderText = "Name"
        _colEmployee.DataSource = Me.bsEmployee
        _colEmployee.ValueMember = "Id"
        _colEmployee.DisplayMember = "Name"
        _colEmployee.Width = 175
        _colEmployee.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        _colEmployee.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
        _colEmployee.SortMode = DataGridViewColumnSortMode.Automatic
        dgvList.Columns.Insert(1, _colEmployee)

        SearchCriteria()

        clsMain.EnableDoubleBuffered(dgvList)

        Dim _superiorIds As New List(Of Integer) From {13, 19, 4, 17, 7, 3, 6}
        Dim _managerIds As New List(Of Integer) From {2, 21}
        Dim _clinicIds As New List(Of Integer) From {3}
        Dim _leadersIds As New List(Of Integer) From {7, 17, 27, 28}

        If _superiorIds.Contains(positionId) Then
            isSuperior = True

            If _leadersIds.Contains(positionId) Then
                isLeader = True
            End If
        ElseIf _managerIds.Contains(positionId) Then
            isManager = True
        ElseIf _clinicIds.Contains(teamId) Then
            isClinic = True
        Else
            grpStatus.Enabled = False
            btnApprove.Visible = False
            btnDisapprove.Visible = False
        End If

        rdMyFile.Checked = True
        If isLeader = True Then
            Me.bsLeaveFiling.Filter = String.Format("EmployeeId = {0} OR EncoderId = {1}", employeeId, employeeId)
        Else
            Me.bsLeaveFiling.Filter = String.Format("EmployeeId = {0}", employeeId)
        End If

    End Sub

    Private Sub frmLeaveList_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        clsMain.FormTrap(Me)
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
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvTransactionHeader_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvList.DataBindingComplete
        Try
            For _i As Integer = 0 To dgvList.Rows.Count - 1
                If dgvList.Rows(_i).Cells("ColLeaveTypeId").Value = 2 Then
                    dgvList.Rows(_i).Cells("ColClinicClearance").Value = "N/A"
                Else
                    If dgvList.Rows(_i).Cells("ColClinicIsApproved").Value = True Then
                        dgvList.Rows(_i).Cells("ColClinicClearance").Value = "Done"
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            RefreshValues()
        Catch ex As Exception
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnNewFiling_Click(sender As Object, e As EventArgs) Handles btnFileLeave.Click
        Try
            Using frmLeaveFiling As New frmLeaveFiling(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId)
                frmLeaveFiling.ShowDialog(Me)

                If frmLeaveFiling.DialogResult = Windows.Forms.DialogResult.OK Then
                    If Me.dsLeaveFiling.HasChanges() Then
                        Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                        Me.dsLeaveFiling.AcceptChanges()
                    End If
                Else
                    Me.bsLeaveFiling.CancelEdit()
                    Me.dsLeaveFiling.RejectChanges()
                End If
            End Using

            RefreshValues()
        Catch ex As Exception
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            If Me.dgvList.SelectedRows.Count > 0 Then
                Dim _leaveFilingId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFilingId")

                Using frmLeaveFiling As New frmLeaveFiling(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId, _leaveFilingId)
                    frmLeaveFiling.ShowDialog(Me)

                    If frmLeaveFiling.DialogResult = Windows.Forms.DialogResult.OK Then
                        If Me.dsLeaveFiling.HasChanges() Then
                            Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
                            Me.dsLeaveFiling.AcceptChanges()
                        End If

                        Me.bsLeaveFiling.ResetBindings(False)
                    Else
                        Me.bsLeaveFiling.CancelEdit()
                        Me.dsLeaveFiling.RejectChanges()
                    End If
                End Using

                RefreshValues()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub trxStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdApproved.CheckedChanged, rdPending.CheckedChanged, rdMyFile.CheckedChanged
        If rdApproved.Checked = True Then
            If isClinic = True Then
                Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 4 AND ClinicIsApproved = 1", employeeId)
            ElseIf isSuperior = True Then
                Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 3 AND SuperiorId = {0} AND SuperiorIsApproved = 1", employeeId)
            ElseIf isManager = True Then
                Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 2 AND ManagerId = {0} AND ManagerIsApproved = 1", employeeId)
            End If
        ElseIf rdPending.Checked = True Then
            If isClinic = True Then
                Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 5 AND ClinicIsApproved = 0", employeeId)
            ElseIf isSuperior = True Then
                Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 4 AND SuperiorId = {0} AND SuperiorIsApproved = 0", employeeId)
            ElseIf isManager = True Then
                Me.bsLeaveFiling.Filter = String.Format("RoutingStatusId = 3 AND ManagerId = {0} AND ManagerIsApproved = 0", employeeId)
            End If
        Else
            If isLeader = True Then
                Me.bsLeaveFiling.Filter = String.Format("EmployeeId = {0} OR EncoderId = {1}", employeeId, employeeId)
            Else
                Me.bsLeaveFiling.Filter = String.Format("EmployeeId = {0}", employeeId)
            End If
        End If
    End Sub

    Private Sub dgvList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvList.DataError
        e.Cancel = False
    End Sub

    Public Sub RefreshValues()
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf GetScrollingIndex))

        Me.dsLeaveFiling.EnforceConstraints = False
        'Me.adpLeaveFiling.FillByLeaveFilingId(Me.dsLeaveFiling.LeaveFiling, Nothing)
        Me.adpRoutingStatus.Fill(Me.dsLeaveFiling.RoutingStatus)
        Me.dsLeaveFiling.EnforceConstraints = True

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

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            If dgvList.Rows.Count > 0 Then
                Dim _leaveFilingId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFilingId")
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFilingId(_leaveFilingId)

                If isSuperior = True Then
                    With _leaveFilingRow
                        .SuperiorIsApproved = 1
                        .SuperiorApprovalDate = DateTime.Now
                        .SetSuperiorRemarksNull()
                        .RoutingStatusId = 3
                        .LastModifiedId = employeeId
                        .LastModifiedDate = DateTime.Now
                    End With

                ElseIf isManager = True Then
                    With _leaveFilingRow
                        .ManagerIsApproved = 1
                        .ManagerApprovalDate = DateTime.Now
                        .SetManagerRemarksNull()
                        .RoutingStatusId = 2
                        .LastModifiedId = employeeId
                        .LastModifiedDate = DateTime.Now
                    End With

                ElseIf isClinic = True Then
                    With _leaveFilingRow
                        .ClinicIsApproved = 1
                        .ClinicApprovalDate = DateTime.Now
                        .SetClinicRemarksNull()
                        .RoutingStatusId = 4
                        .LastModifiedId = employeeId
                        .LastModifiedDate = DateTime.Now
                    End With
                End If
            End If

            Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
            Me.dsLeaveFiling.AcceptChanges()
        Catch ex As Exception
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDisapprove_Click(sender As Object, e As EventArgs) Handles btnDisapprove.Click
        Try
            If dgvList.Rows.Count > 0 Then
                Dim _leaveFilingId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFilingId")
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFilingId(_leaveFilingId)

                If isSuperior = True Then
                    With _leaveFilingRow
                        .SuperiorIsApproved = 0
                        .SuperiorApprovalDate = DateTime.Now
                        .SetSuperiorRemarksNull()
                        .RoutingStatusId = 7
                        .LastModifiedId = employeeId
                        .LastModifiedDate = DateTime.Now
                    End With

                ElseIf isManager = True Then
                    With _leaveFilingRow
                        .ManagerIsApproved = 0
                        .ManagerApprovalDate = DateTime.Now
                        .SetManagerRemarksNull()
                        .RoutingStatusId = 7
                        .LastModifiedId = employeeId
                        .LastModifiedDate = DateTime.Now
                    End With

                ElseIf isClinic = True Then
                    With _leaveFilingRow
                        .ClinicIsApproved = 0
                        .ClinicApprovalDate = DateTime.Now
                        .SetClinicRemarksNull()
                        .RoutingStatusId = 7
                        .LastModifiedId = employeeId
                        .LastModifiedDate = DateTime.Now
                    End With
                End If
            End If

            Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
            Me.dsLeaveFiling.AcceptChanges()
        Catch ex As Exception
            MessageBox.Show(ex.Message, clsMain.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class