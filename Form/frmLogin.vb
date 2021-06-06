Imports System.Data.SqlClient
Imports BlackCoffeeLibrary.BlackCoffee
Imports System.Deployment.Application

Public Class frmLogin
    Private connection As New clsConnection
    Private dbLeaveFiling As New SqlDbMethod(connection.LocalConnection)
    Private dbJeonsoft As New SqlDbMethod(connection.JeonsoftConnection)
    Private main As New Main

    Private employeeId As Integer = 0
    Private employeeCode As String = String.Empty
    Private employeeName As String = String.Empty
    Private positionId As Integer = 0
    Private positionName As String = String.Empty
    Private departmentId As Integer = 0
    Private departmentName As String = String.Empty
    Private teamId As String = 0
    Private teamName As String = String.Empty
    Private employmentTypeId As Integer = 0
    Private employmentTypeName As String = String.Empty
    Private emailAddress As String = String.Empty
    Private mobileNumber As String = String.Empty
    Private nbcEmailAddress As String = String.Empty

    Private arrSplitted() As String

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'sir alvin
        'txtEmployeeId.Text = "1705-025"

        'sir shimamura
        'txtEmployeeId.Text = "1803-000"

        'sir clark
        'txtEmployeeId.Text = "1709-006"

        'sir emman
        'txtEmployeeId.Text = "1701-066"

        'sir harry
        'txtEmployeeId.Text = "1807-002"

        'sir noriel
        'txtEmployeeId.Text = "1901-033"

        'sir jhon
        'txtEmployeeId.Text = "1811-031"

        'sir tony
        'txtEmployeeId.Text = "1605-002"

        'mam liza
        'txtEmployeeId.Text = "2009-015"

        'jen
        'txtEmployeeId.Text = "1910-020"

        'operator
        'txtEmployeeId.Text = "1811-021"

        'line leader
        'txtEmployeeId.Text = "1705-022"

        'sir oliver
        'txtEmployeeId.Text = "1511-002"

        'mam donna
        'txtEmployeeId.Text = "1907-002"

        'nurse
        'txtEmployeeId.Text = "1805-003"

        'sir eldrin
        'txtEmployeeId.Text = "2005-002"

        'mam cheenee
        'txtEmployeeId.Text = "1708-001"
        'txtPassword.Text = "diaz"

        'mam wella
        'txtEmployeeId.Text = "1709-001"

        'mam cath
        'txtEmployeeId.Text = "1802-001"
        'txtPassword.Text = "delapena"

        'mam mj
        'txtEmployeeId.Text = "1701-075"

        'mam meds
        'txtEmployeeId.Text = "2006-021"

        'mam gavileno
        'txtEmployeeId.Text = "2103-000"

        'mam dette
        'txtEmployeeId.Text = "1503-001"

        ''me
        'txtEmployeeId.Text = "2009-002"
        'txtPassword.Text = "malibong"

        'sir vincent
        'txtEmployeeId.Text = "2011-002"

        'cnc operator
        'txtEmployeeId.Text = "1902-020"

        'txtEmployeeId.Text = "2005-003"

        'probi
        'txtEmployeeId.Text = "2010-011"

        'mam sher
        'txtEmployeeId.Text = "2104-020"

        'mam april
        'txtEmployeeId.Text = "1710-002"

        'sir yamamoto
        'txtEmployeeId.Text = "1907-000"

        If ApplicationDeployment.IsNetworkDeployed Then
            lblVersion.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Else
            lblVersion.Text = Application.ProductVersion.ToString
        End If

        Me.ActiveControl = txtEmployeeId
    End Sub

    Private Sub frmLogin_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.ActiveControl = txtEmployeeId
    End Sub

    Private Sub chkBoxShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkBoxShow.CheckedChanged
        If chkBoxShow.Checked = True Then
            txtPassword.UseSystemPasswordChar = False
            txtPassword.PasswordChar = ""

        ElseIf chkBoxShow.Checked = False Then
            txtPassword.UseSystemPasswordChar = True
            txtPassword.PasswordChar = "●"

        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            If String.IsNullOrEmpty(txtEmployeeId.Text.Trim) Then
                MessageBox.Show("Please enter your employee ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtEmployeeId.Focus()
                Return
            End If

            If String.IsNullOrEmpty(txtPassword.Text.Trim) Then
                MessageBox.Show("Please enter your password.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Focus()
                Return
            End If

            Dim _count1 As Integer = 0
            Dim _prm1(1) As SqlParameter
            _prm1(0) = New SqlParameter("@EmployeeCode", SqlDbType.NVarChar)
            _prm1(0).Value = txtEmployeeId.Text.Trim
            _prm1(1) = New SqlParameter("@Password", SqlDbType.NVarChar)
            _prm1(1).Value = txtPassword.Text.Trim

            'use latin1 general collation for case-sensitive password
            _count1 = dbLeaveFiling.ExecuteScalar("SELECT COUNT(Id) FROM VwEmployee WHERE EmployeeCode = @EmployeeCode AND " & _
                                                  "(TRIM(Password) COLLATE Latin1_General_CS_AS = @Password)", CommandType.Text, _prm1)

            If _count1 > 0 Then
                Dim _prm2(1) As SqlParameter
                _prm2(0) = New SqlParameter("@EmployeeCode", SqlDbType.NVarChar)
                _prm2(0).Value = txtEmployeeId.Text.Trim
                _prm2(1) = New SqlParameter("@Password", SqlDbType.NVarChar)
                _prm2(1).Value = txtPassword.Text.Trim

                Dim _reader As IDataReader = dbLeaveFiling.ExecuteReader("RdEmployee", CommandType.StoredProcedure, _prm2)

                While _reader.Read
                    employeeId = _reader.Item("Id")
                    employeeCode = _reader.Item("EmployeeCode").ToString.Trim
                    employeeName = _reader.Item("EmployeeName").ToString.Trim
                    positionId = _reader.Item("PositionId")
                    positionName = _reader.Item("PositionName").ToString.Trim
                    departmentId = _reader.Item("DepartmentId")
                    departmentName = _reader.Item("DepartmentName").ToString.Trim
                    employmentTypeId = _reader.Item("EmploymentTypeId")
                    employmentTypeName = _reader.Item("EmploymentTypeName").ToString.Trim

                    If Not _reader.Item("EmailAddress") Is DBNull.Value Then
                        emailAddress = _reader.Item("EmailAddress")
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

                    If _reader.Item("TeamId") Is DBNull.Value Then
                        teamId = 0
                        teamName = String.Empty
                    Else
                        teamId = _reader.Item("TeamId")
                        teamName = _reader.Item("TeamName").ToString.Trim
                    End If
                End While
                _reader.Close()

                Me.Hide()
                Dim _frmMain As New frmMain(employeeId, employeeCode, employeeName, positionId, positionName, _
                                            departmentId, departmentName, teamId, teamName, employmentTypeId, employmentTypeName, _
                                            emailAddress, mobileNumber, nbcEmailAddress)
                _frmMain.Show()
                txtEmployeeId.Clear()
            Else
                MessageBox.Show("Incorrect employee ID or password.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Clear()
                txtPassword.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

End Class
