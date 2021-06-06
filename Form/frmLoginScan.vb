Imports System.Data.SqlClient
Imports BlackCoffeeLibrary.BlackCoffee
Imports System.Deployment.Application

Public Class frmLoginScan
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

    Private Sub frmLoginScan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ApplicationDeployment.IsNetworkDeployed Then
            lblVersion.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Else
            lblVersion.Text = Application.ProductVersion.ToString
        End If

        Me.ActiveControl = txtEmployeeId
    End Sub

    Private Sub frmLoginScan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.ActiveControl = txtEmployeeId
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            If String.IsNullOrEmpty(txtEmployeeId.Text.Trim) Then
                MessageBox.Show("Please enter your employee ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtEmployeeId.Focus()
                Return
            End If

            arrSplitted = Split(txtEmployeeId.Text.Trim, " ", 2)
            ValidateId(arrSplitted(0).ToString)
        Catch ex As Exception
            MessageBox.Show(ex.Message, main.SetExcpTitle(ex), MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub txtEmployeeId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEmployeeId.KeyDown
        'If e.KeyCode.Equals(Keys.Enter) Then
        '    e.Handled = True

        '    arrSplitted = Split(txtEmployeeId.Text.Trim, " ", 2)
        '    ValidateId(arrSplitted(0).ToString)
        'End If
    End Sub

#Region "Sub"
    Private Sub ValidateId(ByVal _employeeCode As String)
        Dim _count1 As Integer = 0
        Dim _prm1(0) As SqlParameter
        _prm1(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
        _prm1(0).Value = _employeeCode

        _count1 = dbLeaveFiling.ExecuteScalar("SELECT COUNT(EmployeeId) FROM dbo.Employee WHERE TRIM(EmployeeCode) = @EmployeeCode", CommandType.Text, _prm1)

        If _count1 > 0 Then
            Dim _prm2(0) As SqlParameter
            _prm2(0) = New SqlParameter("@EmployeeCode", SqlDbType.VarChar)
            _prm2(0).Value = _employeeCode

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
            MessageBox.Show("Incorrect employee ID.", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtEmployeeId.Clear()
            txtEmployeeId.Focus()
        End If
    End Sub
#End Region


End Class