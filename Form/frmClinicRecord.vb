Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsJeonsoftTableAdapters

Public Class frmClinicRecord
    Private connection As New clsConnection
    Private dbMethodJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private dbMethodLeave As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main
    'user info
    Private employeeId As Integer = 0
    Private employmentTypeId As Integer = 0
    Private positionId As Integer = 0
    Private teamId As Integer = 0
    Private departmentId As Integer = 0
    'pagination
    Private pageSize As Integer
    Private pageIndex As Integer
    Private totalCount As Integer
    Private pageCount As Integer

    Private table As New DataTable
    Private isFiltered As Boolean = False
    Private indexScroll As Integer = 0
    Private indexPosition As Integer = 0
    'search criteria
    Private dictionary As New Dictionary(Of String, Integer)
    'flags
    Private isManager As Boolean = False
    Private isSuperior As Boolean = False
    Private isClinic As Boolean = False
    Private isHr As Boolean = False
    'dataset
    Private dsLeaveFiling As New dsLeaveFiling
    Private adpLeaveFiling As New LeaveFilingTableAdapter

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

    Private Sub frmClinicList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ActiveControl = dgvList
        pageSize = 100
        pageIndex = 0
        SetPage()

        Dim _managerIds As New List(Of Integer) From {15, 21, 2, 13, 19, 4} 'dgm, sr mngr, mngr, asst mngr, sv, asv
        Dim _superiorIds As New List(Of Integer) From {13, 19, 4, 3, 6, 17, 7, 25} 'asst mngr, sv, asv, sr engr, sr staff, sr line leader, line leader, sr nurse

        If _managerIds.Contains(positionId) Then
            isManager = True
        ElseIf _superiorIds.Contains(positionId) Then
            isSuperior = True
        Else
            grpStatus.Enabled = False
            btnApprove.Visible = False
            btnDisapprove.Visible = False
        End If

        SearchCriteria()

        Application.EnableVisualStyles()
        main.EnableDoubleBuffered(dgvList)

        rdPending.Checked = True
    End Sub

    Private Sub frmClinicList_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        main.FormTrap(Me)
    End Sub

    Private Sub frmClinicList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode.Equals(Keys.F2) Then

        ElseIf e.KeyCode.Equals(Keys.F3) Then
            e.Handled = True
            btnView.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F5) Then
            e.Handled = True
            btnRefresh.PerformClick()
        End If
    End Sub

    Private Sub frmClinicList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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

    Private Sub dgvList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvList.DataBindingComplete
        Try
            For _i As Integer = 0 To dgvList.Rows.Count - 1
                If dgvList.Rows(_i).Cells("ColLeaveTypeId").Value = 2 Then
                    dgvList.Rows(_i).Cells("ColClinicClearance").Value = "N/A"
                Else
                    If dgvList.Rows(_i).Cells("ColClinicIsApproved").Value = 1 Then
                        dgvList.Rows(_i).Cells("ColClinicClearance").Value = "Done"
                    Else
                        dgvList.Rows(_i).Cells("ColClinicClearance").Value = "Pending"
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

    Private Sub btnApplyLeave_Click(sender As Object, e As EventArgs) Handles btnApplyLeave.Click
        Try
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
                Dim _leaveFilingId As Integer = dgvList.CurrentRow.Cells("ColLeaveFilingId").Value

                Using frmLeaveFiling As New frmLeaveForm(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId, _leaveFilingId)
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

    Private Sub trxStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdApproved.CheckedChanged, rdPending.CheckedChanged
        If rdApproved.Checked = True Then
            pageSize = 100
            pageIndex = 0
            SetPage(1, 1)
        ElseIf rdPending.Checked = True Then
            pageSize = 100
            pageIndex = 0
            SetPage(0, 1)
        End If
    End Sub

    Private Sub dgvList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvList.DataError
        e.Cancel = False
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            If dgvList.Rows.Count > 0 Then
                Dim _leaveFilingId As Integer = dgvList.CurrentRow.Cells("ColLeaveFilingId").Value
                Me.adpLeaveFiling.FillByLeaveFilingId(Me.dsLeaveFiling.LeaveFiling, _leaveFilingId)
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFilingId(_leaveFilingId)

                With _leaveFilingRow
                    .ClinicIsApproved = 1
                    .ClinicId = employeeId
                    .ClinicApprovalDate = DateTime.Now
                    .SetClinicRemarksNull()

                    If _leaveFilingRow.IsSuperiorIdNull = True Then
                        .RoutingStatusId = 2
                    Else
                        .RoutingStatusId = 4
                    End If
                End With
            End If
            Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)

            RefreshValues()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDisapprove_Click(sender As Object, e As EventArgs) Handles btnDisapprove.Click
        Try
            If dgvList.Rows.Count > 0 Then
                Dim _leaveFilingId As Integer = dgvList.CurrentRow.Cells("ColLeaveFilingId").Value
                Me.adpLeaveFiling.FillByLeaveFilingId(Me.dsLeaveFiling.LeaveFiling, _leaveFilingId)
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFilingId(_leaveFilingId)

                With _leaveFilingRow
                    .ClinicIsApproved = 0
                    .ClinicId = employeeId
                    .ClinicApprovalDate = DateTime.Now
                    .ClinicRemarks = "Disapproved"
                    .RoutingStatusId = 7
                End With
            End If
            Me.adpLeaveFiling.Update(Me.dsLeaveFiling.LeaveFiling)
            Me.dsLeaveFiling.AcceptChanges()

            RefreshValues()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Go()
    End Sub

    Private Sub BindingNavigatorMoveFirstItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveFirstItem.Click
        pageIndex = 0
        If rdApproved.Checked = True Then
            SetPage(1, 1)
        ElseIf rdPending.Checked = True Then
            SetPage(0, 1)
        End If
    End Sub

    Private Sub BindingNavigatorMovePreviousItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMovePreviousItem.Click
        pageIndex -= 1
        If pageIndex < 0 Then
            pageIndex = 0
        End If
        If rdApproved.Checked = True Then
            SetPage(1, 1)
        ElseIf rdPending.Checked = True Then
            SetPage(0, 1)
        End If
    End Sub

    Private Sub BindingNavigatorMoveNextItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveNextItem.Click
        pageIndex += 1
        If pageIndex > pageCount - 1 Then
            pageIndex = pageCount - 1
        End If
        If rdApproved.Checked = True Then
            SetPage(1, 1)
        ElseIf rdPending.Checked = True Then
            SetPage(0, 1)
        End If
    End Sub

    Private Sub BindingNavigatorMoveLastItem_Click(sender As Object, e As EventArgs) Handles BindingNavigatorMoveLastItem.Click
        pageIndex = pageCount - 1
        If rdApproved.Checked = True Then
            SetPage(1, 1)
        ElseIf rdPending.Checked = True Then
            SetPage(0, 1)
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

#Region "Sub"
    Public Sub RefreshValues()
        If dgvList IsNot Nothing AndAlso dgvList.CurrentRow IsNot Nothing Then Me.Invoke(New Action(AddressOf GetScrollingIndex))
        pageSize = 100
        SetPage()
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

    Private Sub SetPage(Optional ByVal _clinicIsApproved As Integer = 0, Optional ByVal _leaveTypeId As Integer = 0)
        Try
            totalCount = 0

            Dim _param(4) As SqlParameter
            _param(0) = New SqlParameter("@PageSize", SqlDbType.Int)
            _param(0).Value = pageSize
            _param(1) = New SqlParameter("@PageIndex", SqlDbType.Int)
            _param(1).Value = pageIndex
            _param(2) = New SqlParameter("@TotalCount", SqlDbType.Int)
            _param(2).Direction = ParameterDirection.Output
            _param(3) = New SqlParameter("@ClinicIsApproved", SqlDbType.Int)
            _param(3).Value = _clinicIsApproved
            _param(4) = New SqlParameter("@LeaveTypeId", SqlDbType.Int)
            _param(4).Value = _leaveTypeId

            table = dbMethodLeave.FillDataTableSp("RdLeaveFilingPage", _param)
            totalCount = Convert.ToInt32(_param(2).Value)

            dgvList.AutoGenerateColumns = False
            dgvList.DataSource = table

            If totalCount Mod pageSize = 0 Then
                If table.Rows.Count = 0 Then
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
            If rdApproved.Checked = True Then
                SetPage(1, 1)
            ElseIf rdPending.Checked = True Then
                SetPage(0, 1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

End Class