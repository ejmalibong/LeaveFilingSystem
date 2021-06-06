Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports BlackCoffeeLibrary.BlackCoffee
Imports LeaveFilingSystem
Imports LeaveFilingSystem.dsLeaveFiling
Imports LeaveFilingSystem.dsLeaveFilingTableAdapters
Imports LeaveFilingSystem.dsJeonsoft
Imports LeaveFilingSystem.dsJeonsoftTableAdapters

Public Class frmChangePassword
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private main As New Main

    Private employeeId As Integer = 0

    Public Sub New(ByVal _employeeId As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        employeeId = _employeeId
    End Sub

    Private Sub frmChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCurrentPassword.UseSystemPasswordChar = True
        txtNewPassword.UseSystemPasswordChar = True
        txtConfirmPassword.UseSystemPasswordChar = True
        txtCurrentPassword.PasswordChar = "●"
        txtNewPassword.PasswordChar = "●"
        txtConfirmPassword.PasswordChar = "●"

        chkBoxShow.Checked = False

        GetCurrentPassword(employeeId)

        Me.ActiveControl = txtNewPassword
    End Sub

    Private Sub frmChangePassword_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        main.FormTrap(Me)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not txtNewPassword.Text.Equals(txtConfirmPassword.Text) Then
                MessageBox.Show("New password and confirmation password did not match.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtNewPassword.Focus()
                Return
            End If

            Dim _rowAffected As Integer = 0
            Dim _prm(1) As SqlParameter
            _prm(0) = New SqlParameter("@Password", SqlDbType.NVarChar)
            _prm(0).Value = txtNewPassword.Text.Trim
            _prm(1) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prm(1).Value = employeeId

            _rowAffected = dbLeaveFiling.ExecuteNonQuery("UPDATE Employee SET Password = @Password WHERE EmployeeId = @EmployeeId", CommandType.Text, _prm)

            If _rowAffected > 0 Then
                MessageBox.Show("Password successfully changed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Password did not changed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            btnClose.PerformClick()
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkBoxShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkBoxShow.CheckedChanged
        If chkBoxShow.Checked = True Then
            txtCurrentPassword.UseSystemPasswordChar = False
            txtNewPassword.UseSystemPasswordChar = False
            txtConfirmPassword.UseSystemPasswordChar = False
            txtCurrentPassword.PasswordChar = ""
            txtNewPassword.PasswordChar = ""
            txtConfirmPassword.PasswordChar = ""

        ElseIf chkBoxShow.Checked = False Then
            txtCurrentPassword.UseSystemPasswordChar = True
            txtNewPassword.UseSystemPasswordChar = True
            txtConfirmPassword.UseSystemPasswordChar = True
            txtCurrentPassword.PasswordChar = "●"
            txtNewPassword.PasswordChar = "●"
            txtConfirmPassword.PasswordChar = "●"

        End If
    End Sub

#Region "Sub"
    Private Sub GetCurrentPassword(ByVal _employeeId As Integer)
        Dim _currPassword As String = String.Empty

        Try
            Dim _prm(0) As SqlParameter
            _prm(0) = New SqlParameter("@EmployeeId", SqlDbType.Int)
            _prm(0).Value = _employeeId

            _currPassword = dbLeaveFiling.ExecuteScalar("SELECT TRIM(Password) AS Password FROM dbo.Employee WHERE EmployeeId = @EmployeeId", _
                                                        CommandType.Text, _prm)

            txtCurrentPassword.Text = _currPassword
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

End Class