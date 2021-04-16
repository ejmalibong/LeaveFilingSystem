Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsJeonsoftTableAdapters

Public Class frmHrRecord
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
    'constructor
    Private employeeId As Integer = 0
    Private employmentTypeId As Integer = 0
    Private positionId As Integer = 0
    Private teamId As Integer = 0
    Private departmentId As Integer = 0
    'paging
    Private pageSize As Integer
    Private pageIndex As Integer
    Private totalCount As Integer
    Private pageCount As Integer
    Private indexScroll As Integer = 0
    Private indexPosition As Integer = 0

    Public Sub New(ByVal _employeeId As Integer, ByVal _positionId As Integer, ByVal _departmentId As Integer, ByVal _teamId As Integer, ByVal _employmentTypeId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
        positionId = _positionId
        departmentId = _departmentId
        teamId = _teamId
        employmentTypeId = _employmentTypeId
    End Sub

    Private Sub frmHrList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Application.EnableVisualStyles()

        pageIndex = 0
        pageSize = 100
        BindPage()

        rdPending.Checked = True

        main.EnableDoubleBuffered(dgvList)
        Me.ActiveControl = dgvList
    End Sub

    Private Sub frmHrList_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        main.FormTrap(Me)
    End Sub

    Private Sub frmHrList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode.Equals(Keys.F3) Then
            e.Handled = True
            btnView.PerformClick()
        ElseIf e.KeyCode.Equals(Keys.F5) Then
            e.Handled = True
            btnRefresh.PerformClick()
        End If
    End Sub

    Private Sub frmHrList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            If Me.dgvList.SelectedCells.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Using frmLeaveFiling As New frmLeaveForm(Me.dsLeaveFiling, employeeId, positionId, departmentId, teamId, employmentTypeId, _leaveFileId)
                    frmLeaveFiling.ShowDialog(Me)
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvList_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvList.DataError
        e.Cancel = False
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            If Me.dgvList.SelectedCells.Count > 0 Then
                Dim _leaveFileId As Integer = CType(Me.bsLeaveFiling.Current, DataRowView).Item("LeaveFileId")
                Me.adpLeaveFiling.FillByLeaveFileId(Me.dsLeaveFiling.LeaveFiling, _leaveFileId)
                Dim _leaveFilingRow As LeaveFilingRow = Me.dsLeaveFiling.LeaveFiling.FindByLeaveFileId(_leaveFileId)

                With _leaveFilingRow
                    .RoutingStatusId = 1
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
           
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trxStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdApproved.CheckedChanged, rdPending.CheckedChanged, rdDisapproved.CheckedChanged
        pageSize = 100
        pageIndex = 0
        BindPage()
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
    Private Sub BindPage(Optional ByVal _routingStatusId As Integer = 0)
        Try
            totalCount = 0

            If rdPending.Checked = True Then
                Me.adpLeaveFiling.FillByRoutingStatusId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, 2)
            ElseIf rdApproved.Checked = True Then
                Me.adpLeaveFiling.FillByRoutingStatusId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, 1)
            ElseIf rdDisapproved.Checked = True Then
                Me.adpLeaveFiling.FillByRoutingStatusId(Me.dsLeaveFiling.LeaveFiling, pageIndex, pageSize, totalCount, Nothing, 6)
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
#End Region

End Class