Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters

Public Class frmHolidaySync
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private main As New Main

    Private year As Integer

    Public Sub New(ByVal _year As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        year = _year
    End Sub

    Private Sub frmHolidaySync_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtYear.Text = year
        Me.ActiveControl = txtYear

        txtYear.Select(txtYear.Text.Trim.Length, 0)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim _count As Integer = 0
            Dim _totalCount As Integer = 0
            Dim _rowsAffected As Integer = 0

            Dim _prm1(1) As SqlParameter
            _prm1(0) = New SqlParameter("@Year", SqlDbType.Int)
            _prm1(0).Value = txtYear.Text.Trim
            _prm1(1) = New SqlParameter("@TotalCount", SqlDbType.Int)
            _prm1(1).Direction = ParameterDirection.Output

            dbLeaveFiling.ExecuteScalar("CntHoliday", CommandType.StoredProcedure, _prm1)
            _count = _prm1(1).Value

            If _count > 0 Then
                Dim _prm2(0) As SqlParameter
                _prm2(0) = New SqlParameter("@Year", SqlDbType.Int)
                _prm2(0).Value = txtYear.Text.Trim

                _rowsAffected = dbLeaveFiling.ExecuteNonQuery("InsHoliday", CommandType.StoredProcedure, _prm2)

                If _rowsAffected > 0 Then
                    MessageBox.Show(_rowsAffected & " rows affected.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("No rows affected.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                MessageBox.Show("No items need to be imported for the year " & txtYear.Text.Trim & ".", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtYear_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtYear.KeyPress
        If InStr("1234567890", e.KeyChar) = 0 And Asc(e.KeyChar) <> 8 Or (e.KeyChar = "." And InStr(txtYear.Text, ".") > 0) Then
            e.KeyChar = Chr(0)
            e.Handled = True
        End If
    End Sub

End Class
